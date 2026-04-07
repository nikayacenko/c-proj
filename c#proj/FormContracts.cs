using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace c_proj
{
    public partial class FormContracts : Form
    {
        private NpgsqlConnection con;
        private DataTable dtContracts;
        private DataTable dtContractItems;
        private int selectedContractId = -1;
        public FormContracts(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
            LoadContracts();
        }

        private void LoadContracts()
        {
            try
            {
                string sql = @"
                    SELECT 
                        c.id AS Номер,
                        cl.name AS Клиент,
                        c.contract_date AS Дата,
                        CASE WHEN c.payment_type = 'cash' THEN 'Наличные' ELSE 'Безналичный' END AS Оплата
                    FROM contracts c
                    JOIN clients cl ON c.client_id = cl.id
                    ORDER BY c.id DESC";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                dtContracts = new DataTable();
                da.Fill(dtContracts);
                dataGridViewContracts.DataSource = dtContracts;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки договоров: " + ex.Message);
            }
        }
        private void LoadContractItems(int contractId)
        {
            try
            {
                string sql = @"
                    SELECT 
                        ci.id AS ID,
                        p.name AS Товар,
                        ci.quantity AS Количество,
                        (ci.quantity * p.price) AS Сумма   -- вычисляем на лету!
                    FROM contract_items ci
                    JOIN product p ON ci.product_id = p.id
                    WHERE ci.contract_id = " + contractId;

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                dtContractItems = new DataTable();
                da.Fill(dtContractItems);
                dataGridViewItems.DataSource = dtContractItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки товаров: " + ex.Message);
            }
        }
        private void ContractsSelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewContracts.CurrentRow != null)
            {
                selectedContractId = Convert.ToInt32(dataGridViewContracts.CurrentRow.Cells["Номер"].Value);
                LoadContractItems(selectedContractId);
            }
        }

        private void dataGridViewContracts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedContractId = Convert.ToInt32(dataGridViewContracts.Rows[e.RowIndex].Cells["Номер"].Value);
                string clientName = dataGridViewContracts.Rows[e.RowIndex].Cells["Клиент"].Value.ToString();

                LoadContractItems(selectedContractId);
            }
        }

        private void btnAddContract_Click(object sender, EventArgs e)
        {
            FormAddContract f = new FormAddContract(con);
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadContracts();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (selectedContractId == -1)
            {
                MessageBox.Show("Сначала выберите договор!");
                return;
            }

            FormAddContractItem f = new FormAddContractItem(con, selectedContractId);
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadContractItems(selectedContractId);
                LoadContracts(); // обновляем суммы в списке договоров
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.CurrentRow == null)
            {
                MessageBox.Show("Выберите товар для удаления!");
                return;
            }

            if (MessageBox.Show("Удалить товар из договора?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int itemId = Convert.ToInt32(dataGridViewItems.CurrentRow.Cells["ID"].Value);
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM contract_items WHERE id = :id", con);
                cmd.Parameters.AddWithValue("id", itemId);
                cmd.ExecuteNonQuery();
                LoadContractItems(selectedContractId);
                LoadContracts();
            }
        }

        private void btnShipment_Click(object sender, EventArgs e)
        {
            if (selectedContractId == -1)
            {
                MessageBox.Show("Выберите договор для отгрузки!");
                return;
            }

            FormAddShipment f = new FormAddShipment(con, selectedContractId);
            f.ShowDialog();
            LoadContractItems(selectedContractId);
        }

        private void btnDeleteContract_Click(object sender, EventArgs e)
        {
            if (selectedContractId == -1)
            {
                MessageBox.Show("Выберите договор для удаления!");
                return;
            }

            // Получаем информацию о договоре
            string contractInfo = "";
            if (dataGridViewContracts.CurrentRow != null)
            {
                string clientName = dataGridViewContracts.CurrentRow.Cells["Клиент"].Value.ToString();
                string date = dataGridViewContracts.CurrentRow.Cells["Дата"].Value.ToString();
            }            
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM contracts WHERE id = :contractId", con);
                cmd.Parameters.AddWithValue("contractId", selectedContractId);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Обновляем список договоров
                    LoadContracts();

                    // Очищаем таблицу товаров
                    dataGridViewItems.DataSource = null;
                }
                else
                {
                    MessageBox.Show("Договор не найден!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении: " + ex.Message);
            }
            
        }
    }
}
