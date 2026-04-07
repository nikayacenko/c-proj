using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace c_proj
{
    public partial class FormAddContract : Form
    {
        private NpgsqlConnection con;

        public FormAddContract(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
            LoadClients();
        }
        private void LoadClients()
        {
            string sql = "SELECT id, name FROM clients ORDER BY name";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            comboClient.DisplayMember = "name";
            comboClient.ValueMember = "id";
            comboClient.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string paymentType = comboPaymentType.SelectedIndex == 0 ? "cash" : "cashless";

                NpgsqlCommand cmd = new NpgsqlCommand("insert into contracts (client_id, contract_date, payment_type) VALUES (:clientId, :date, :paymentType)", con);
                cmd.Parameters.AddWithValue("clientId", (int)comboClient.SelectedValue);
                cmd.Parameters.AddWithValue("date", datePicker.Value.Date);
                cmd.Parameters.AddWithValue("paymentType", paymentType);
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
