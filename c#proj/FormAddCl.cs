using Npgsql;
using System;
using System.Windows.Forms;

namespace c_proj
{
    public partial class FormAddCl : Form
    {
        public NpgsqlConnection con;
        int id;
        string buttonText;
        public FormAddCl(NpgsqlConnection con, int id, string buttonText)
        {
            this.con = con;
            this.id = id;
            this.buttonText = buttonText;
            InitializeComponent();
        }

        public FormAddCl(NpgsqlConnection con, int id, string nameC, string adress, string phone, string buttonText)
        {
            InitializeComponent();
            this.con = con;
            this.id = id;
            textBoxName.Text = nameC;
            textBoxAdress.Text = adress;
            textBoxPhone.Text = phone;
            this.buttonText = buttonText;
            buttonInsert.Text = buttonText;
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (id == -1)
            {
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand("insert into clients (name, adress, phone) values (:name, :adress, :phone)", con);
                    command.Parameters.AddWithValue("name", textBoxName.Text);
                    command.Parameters.AddWithValue("adress", textBoxAdress.Text);
                    command.Parameters.AddWithValue("phone", textBoxPhone.Text);
                    command.ExecuteNonQuery();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand("update clients set name = :name, adress=:adress, phone=:phone where id = :id", con);
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("name", textBoxName.Text);
                    command.Parameters.AddWithValue("adress", textBoxAdress.Text);
                    command.Parameters.AddWithValue("phone", textBoxPhone.Text);
                    command.ExecuteNonQuery();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
