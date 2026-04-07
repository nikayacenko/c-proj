using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace c_proj
{
    public partial class FormReportContractVsShipment : Form
    {
        private NpgsqlConnection con;
        private DataTable dt;
        public FormReportContractVsShipment(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
            LoadProducts();
            dt = new DataTable();
            dateTimePickerStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateTimePickerEnd.Value = DateTime.Now;
        }

        private void LoadProducts()
        {
            try
            {
                string sql = "SELECT id, name FROM product ORDER BY id";
                
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                DataTable dtProducts = new DataTable();
                da.Fill(dtProducts);

                productsListBox.DisplayMember = "name";
                productsListBox.ValueMember = "id";

                foreach (DataRow row in dtProducts.Rows)
                {
                    productsListBox.Items.Add(new ProductItem
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Name = row["name"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки товаров: " + ex.Message);
            }
        }

        private void generateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (productsListBox.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Выберите хотя бы один товар!");
                    return;
                }

                string productIds = "";
                foreach (ProductItem item in productsListBox.CheckedItems)
                {
                    if (productIds != "") productIds += ",";
                    productIds += item.Id;
                }

                DateTime startDate = dateTimePickerStart.Value.Date;
                DateTime endDate = dateTimePickerEnd.Value.Date.AddDays(1).AddSeconds(-1);

                string sql = @"
                    SELECT 
                        p.id AS ID_товара,
                        p.name AS Товар,
                        COALESCE(contract_data.quantity, 0) AS Законтрактовано_колво,
                        COALESCE(contract_data.amount, 0) AS Законтрактовано_сумма,
                        COALESCE(shipment_data.quantity, 0) AS Отгружено_колво,
                        COALESCE(shipment_data.amount, 0) AS Отгружено_сумма,
                        CASE 
                            WHEN COALESCE(contract_data.quantity, 0) = 0 THEN 0
                            ELSE ROUND(CAST(COALESCE(shipment_data.quantity, 0) AS DECIMAL) / COALESCE(contract_data.quantity, 1) * 100, 2)
                        END AS Процент_отгрузки
                    FROM product p
                    LEFT JOIN (
                        SELECT 
                            ci.product_id,
                            SUM(ci.quantity) AS quantity,
                            SUM(ci.quantity * p2.price) AS amount
                        FROM contract_items ci
                        JOIN contracts c ON ci.contract_id = c.id
                        JOIN product p2 ON ci.product_id = p2.id
                        WHERE c.contract_date BETWEEN :startDate AND :endDate
                        GROUP BY ci.product_id
                    ) contract_data ON p.id = contract_data.product_id
                    LEFT JOIN (
                        SELECT 
                            si.product_id,
                            SUM(si.quantity) AS quantity,
                            SUM(si.quantity * p2.price) AS amount
                        FROM shipment_items si
                        JOIN shipments s ON si.shipment_id = s.id
                        JOIN product p2 ON si.product_id = p2.id
                        WHERE s.shipment_date BETWEEN :startDate AND :endDate
                        GROUP BY si.product_id
                    ) shipment_data ON p.id = shipment_data.product_id
                    WHERE p.id IN (" + productIds + @")
                    ORDER BY p.name";
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                da.SelectCommand.Parameters.AddWithValue("startDate", startDate);
                da.SelectCommand.Parameters.AddWithValue("endDate", endDate);

                dt.Clear();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                // Настройка форматирования
                if (dataGridView1.Columns.Contains("Процент_отгрузки"))
                {
                    dataGridView1.Columns["Процент_отгрузки"].DefaultCellStyle.Format = "0.00";
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных за выбранный период");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        
        private class ProductItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() { return Name; }
        }
    }
}
