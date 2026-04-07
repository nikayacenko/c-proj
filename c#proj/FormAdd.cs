using Npgsql;
using System;
using System.Windows.Forms;

namespace c_proj
{
    public partial class FormAdd : Form
    {
        public NpgsqlConnection con;
        int id;
        string buttonText;

        public FormAdd(NpgsqlConnection con, int id, string buttonText)
        {
            this.con = con;
            this.id = id;
            this.buttonText = buttonText;
            InitializeComponent();
        }

        public FormAdd(NpgsqlConnection con, int id, string nameP, string ed, string buttonText, decimal price)
        {
            InitializeComponent();
            this.con = con;
            this.id = id;
            textBoxName.Text = nameP;
            textBoxED.Text = ed;
            this.buttonText = buttonText;
            buttonInsert.Text = buttonText;
            textBoxPrice.Text = price.ToString();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
        
            if (id == -1)
            {
                try
                {
                    //decimal price = 0;
                    //if (!string.IsNullOrEmpty(textBoxPrice.Text))
                    //{
                    //    price = Convert.ToDecimal(textBoxPrice.Text);
                    //}
                    NpgsqlCommand command = new NpgsqlCommand("insert into product (name, ed, price) values (:name, :ed, :price)", con);
                    command.Parameters.AddWithValue("name", textBoxName.Text);
                    command.Parameters.AddWithValue("Ed", textBoxED.Text);
                    command.Parameters.AddWithValue("price", Convert.ToDecimal(textBoxPrice.Text));
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
                    //decimal price = 0;
                    //if (!string.IsNullOrEmpty(textBoxPrice.Text))
                    //{
                    //    price = Convert.ToDecimal(textBoxPrice.Text);
                    //}
                    NpgsqlCommand command = new NpgsqlCommand("update product set name = :name, ed=:ed, price=:price where id = :id", con);
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("name", textBoxName.Text);
                    command.Parameters.AddWithValue("Ed", textBoxED.Text);
                    command.Parameters.AddWithValue("price", Convert.ToDecimal(textBoxPrice.Text));
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
