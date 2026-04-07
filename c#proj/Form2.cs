using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace c_proj
{
    public partial class Form2 : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        TablesEnum table;
        public Form2(NpgsqlConnection con, TablesEnum table)
        {
            this.table = table;
            InitializeComponent();
            this.con = con;
            Update();
        }
        public void Update()
        {
            String sql;

            NpgsqlDataAdapter da;
            switch (table)
            {
                case TablesEnum.Clients:
                    sql = "Select * from clients";
                    da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "Имя";
                    dataGridView1.Columns[1].HeaderText = "Адрес";
                    dataGridView1.Columns[2].HeaderText = "Телефон";
                    this.StartPosition = FormStartPosition.CenterScreen;
                    break;

                case TablesEnum.Product:
                    sql = "Select * from product";
                    da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "Номер";
                    dataGridView1.Columns[1].HeaderText = "Наименование";
                    dataGridView1.Columns[2].HeaderText = "Ед. измерения";
                    this.StartPosition = FormStartPosition.CenterScreen;
                    break;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = -1;
            switch (table)
            {
                case TablesEnum.Product:
                    FormAdd f1 = new FormAdd(con, id, "добавить");
                    f1.ShowDialog();
                    Update();
                    break;
                case TablesEnum.Clients:
                    FormAddCl f2 = new FormAddCl(con, id, "добавить");
                    f2.ShowDialog();
                    Update();
                    break;
            }
        }

        private void ChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
            string name;
            switch (table)
            {
                case TablesEnum.Product:
                    name = (string)dataGridView1.CurrentRow.Cells["name"].Value;
                    string ed = (string)dataGridView1.CurrentRow.Cells["ed"].Value;
                    decimal price = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["price"].Value);
                    FormAdd f1 = new FormAdd(con, id, name, ed, "изменить", price);
                    f1.ShowDialog();
                    Update();
                    break;
                case TablesEnum.Clients:
                    name = (string)dataGridView1.CurrentRow.Cells["name"].Value;
                    string adress = (string)dataGridView1.CurrentRow.Cells["adress"].Value;
                    string phone = (string)dataGridView1.CurrentRow.Cells["phone"].Value;
                    FormAddCl f2 = new FormAddCl(con, id, name, adress, phone, "изменить");
                    f2.ShowDialog();
                    Update();
                    break;
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
            string name;
            NpgsqlCommand command;
            switch (table)
            {
                case TablesEnum.Product:
                    command = new NpgsqlCommand("delete from product where id = :id", con);
                    command.Parameters.AddWithValue("ID", id);
                    command.ExecuteNonQuery();
                    Update();
                    break;
                case TablesEnum.Clients:
                    command = new NpgsqlCommand("delete from clients where id = :id", con);
                    command.Parameters.AddWithValue("ID", id);
                    command.ExecuteNonQuery();
                    Update();
                    break;
            }

        }
    }
}
