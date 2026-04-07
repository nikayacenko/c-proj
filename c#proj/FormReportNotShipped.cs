using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace c_proj
{
    public partial class FormReportNotShipped : Form
    {
        private NpgsqlConnection con;
        private DataTable dt;

        public FormReportNotShipped(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
            LoadClients();
            dt = new DataTable();
            dateTimePickerStart.Value = DateTime.Now.AddMonths(-1);
            dateTimePickerEnd.Value = DateTime.Now;
        }
        private void LoadClients()
        {
            try
            {
                string sql = "SELECT id, name FROM clients ORDER BY id";
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                DataTable dtClients = new DataTable();
                da.Fill(dtClients);

                clientsListBox.DisplayMember = "name";
                clientsListBox.ValueMember = "id";

                foreach (DataRow row in dtClients.Rows)
                {
                    clientsListBox.Items.Add(new ClientItem
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Name = row["name"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки клиентов: " + ex.Message);
            }
        }

        private void generateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientsListBox.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Выберите хотя бы одного клиента!");
                    return;
                }

                string clientIds = "";
                foreach (ClientItem item in clientsListBox.CheckedItems)
                {
                    if (clientIds != "") clientIds += ",";
                    clientIds += item.Id;
                }

                DateTime startDate = dateTimePickerStart.Value.Date;
                DateTime endDate = dateTimePickerEnd.Value.Date.AddDays(1).AddSeconds(-1);

                string sql = @"
                    SELECT 
                        c.name AS Клиент,
                        p.name AS Товар,
                        (ci.quantity - COALESCE(si_sum.total_shipped, 0)) AS Остаток_к_отгрузке,
                        ((ci.quantity - COALESCE(si_sum.total_shipped, 0)) * p.price) AS Сумма_к_отгрузке
                    FROM contracts ct
                    JOIN clients c ON ct.client_id = c.id
                    JOIN contract_items ci ON ct.id = ci.contract_id
                    JOIN product p ON ci.product_id = p.id
                    LEFT JOIN (
                        SELECT 
                            s.contract_id,
                            si.product_id,
                            SUM(si.quantity) AS total_shipped
                        FROM shipments s
                        JOIN shipment_items si ON s.id = si.shipment_id
                        GROUP BY s.contract_id, si.product_id
                    ) si_sum ON ct.id = si_sum.contract_id AND ci.product_id = si_sum.product_id
                    WHERE ct.client_id IN (" + clientIds + @")
                      AND ct.contract_date BETWEEN :startDate AND :endDate
                      AND (ci.quantity - COALESCE(si_sum.total_shipped, 0)) > 0
                    ORDER BY c.name, p.name";
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                da.SelectCommand.Parameters.AddWithValue("startDate", startDate);
                da.SelectCommand.Parameters.AddWithValue("endDate", endDate);

                dt.Clear();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Нет товаров, ожидающих отгрузки за выбранный период");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта!");
                return;
            }

            try
            {
                // Создаём приложение Excel
                Microsoft.Office.Interop.Excel.Application excelObj = new Microsoft.Office.Interop.Excel.Application();
                excelObj.Visible = true;

                // Создаём новую книгу
                Workbook workbook = excelObj.Workbooks.Add(Type.Missing);
                Worksheet worksheet = workbook.Sheets[1];
                worksheet.Name = "Неотгруженные товары";

                // Заголовки столбцов
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                 
                }

                // Данные
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value?.ToString() ?? "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при экспорте: " + ex.Message);
            }
        }
        private class ClientItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() { return Name; }
        }
    }
}
