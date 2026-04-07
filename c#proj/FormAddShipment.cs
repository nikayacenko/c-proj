using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace c_proj
{
    public partial class FormAddShipment : Form
    {
        private NpgsqlConnection con;
        private int contractId;
        private DataTable dtRemaining;

        public FormAddShipment(NpgsqlConnection con, int contractId)
        {
            InitializeComponent();
            this.con = con;
            this.contractId = contractId;
            LoadRemainingItems();
        }
        private void LoadRemainingItems()
        {
            try
            {
                string sql = @"
                    SELECT 
                        ci.product_id AS ID,
                        p.name AS Товар,
                        (ci.quantity - COALESCE(si_sum.shipped, 0)) AS Остаток,
                        p.price AS Цена
                    FROM contract_items ci
                    JOIN product p ON ci.product_id = p.id
                    LEFT JOIN (
                        SELECT product_id, SUM(quantity) AS shipped
                        FROM shipment_items si
                        JOIN shipments s ON si.shipment_id = s.id
                        WHERE s.contract_id = " + contractId + @"
                        GROUP BY product_id
                    ) si_sum ON ci.product_id = si_sum.product_id
                    WHERE ci.contract_id = " + contractId + @"
                      AND (ci.quantity - COALESCE(si_sum.shipped, 0)) > 0";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                dtRemaining = new DataTable();
                da.Fill(dtRemaining);
                dataGridView.DataSource = dtRemaining;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShip_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Выберите товар для отгрузки!");
                return;
            }
            //получаем данные товара
            int productId = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value);
            int remaining = Convert.ToInt32(dataGridView.CurrentRow.Cells["Остаток"].Value);
            decimal price = Convert.ToDecimal(dataGridView.CurrentRow.Cells["Цена"].Value);
            //Запрашиваем количество отгрузки
            FormShipQuantity f = new FormShipQuantity(productId, remaining);
            if (f.ShowDialog() == DialogResult.OK)
            {
                int shipQuantity = f.ShipQuantity;
                decimal amount = price * shipQuantity;

                try
                {
                    // Создаём отгрузку
                    NpgsqlCommand cmdShip = new NpgsqlCommand(
                        "INSERT INTO shipments (contract_id, shipment_date) VALUES (:contractId, :date) RETURNING id", con);
                    cmdShip.Parameters.AddWithValue("contractId", contractId);
                    cmdShip.Parameters.AddWithValue("date", DateTime.Now.Date);
                    int shipmentId = (int)cmdShip.ExecuteScalar();

                    // Привязываем товар к отгрузке
                    NpgsqlCommand cmdItem = new NpgsqlCommand(
                        "INSERT INTO shipment_items (shipment_id, product_id, quantity) VALUES (:shipmentId, :productId, :quantity)", con);
                    cmdItem.Parameters.AddWithValue("shipmentId", shipmentId);
                    cmdItem.Parameters.AddWithValue("productId", productId);
                    cmdItem.Parameters.AddWithValue("quantity", shipQuantity);
                    cmdItem.ExecuteNonQuery();

                    MessageBox.Show($"Отгружено {shipQuantity} шт. на сумму {amount:N2} руб.");
                    LoadRemainingItems();

                    if (dtRemaining.Rows.Count == 0)
                    {
                        MessageBox.Show("Все товары по договору отгружены!");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
