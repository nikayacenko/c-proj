using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace c_proj
{
    public partial class FormAddContractItem : Form
    {
        private NpgsqlConnection con;
        private int contractId;
        public FormAddContractItem(NpgsqlConnection con, int contractId)
        {
            InitializeComponent();
            this.con = con;
            this.contractId = contractId;
            LoadProducts();
        }

        private void LoadProducts()
        {
            string sql = "select id, name, price from product ORDER BY name";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            comboProduct.DisplayMember = "name";
            comboProduct.ValueMember = "id";
            comboProduct.DataSource = dt;
        }
        //private void ProductSelectedChanged(object sender, EventArgs e)
        //{
        //    CalculateAmount(null, null);
        //}

        //private void CalculateAmount(object sender, EventArgs e)
        //{
        //    if (comboProduct.SelectedItem != null)
        //    {
        //        DataRowView drv = (DataRowView)comboProduct.SelectedItem;
        //        decimal price = Convert.ToDecimal(drv["price"]);
        //        int quantity = (int)nudQuantity.Value;
        //        decimal amount = price * quantity;
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int productId = (int)comboProduct.SelectedValue;
                int quantity = (int)nudQuantity.Value;
                NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO contract_items (contract_id, product_id, quantity) VALUES (:contractId, :productId, :quantity)", con);
                cmd.Parameters.AddWithValue("contractId", contractId);
                cmd.Parameters.AddWithValue("productId", productId);
                cmd.Parameters.AddWithValue("quantity", quantity);
                cmd.ExecuteNonQuery();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
