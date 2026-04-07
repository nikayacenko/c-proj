using Npgsql;
using System;
using System.Windows.Forms;

namespace c_proj
{
    public partial class Form1 : Form
    {
        public NpgsqlConnection con;
        public Form1()
        {
            InitializeComponent();
            MyLoad();
        }
        public void MyLoad()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            con = new NpgsqlConnection("Server=localhost;Port=5432;UserID=postgres;Password=8589415;Database=nikazavr");
            con.Open();
        }

        private void productBtn_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(con, TablesEnum.Product);
            f.ShowDialog();
        }

        private void clientsBtn_Click(object sender, EventArgs e)
        {
            Form2 f1 = new Form2(con, TablesEnum.Clients);
            f1.ShowDialog();
        }

        private void reportNotShippedBtn_Click(object sender, EventArgs e)
        {
            FormReportNotShipped f = new FormReportNotShipped(con);
            f.ShowDialog();
        }

        private void reportContractVsShipmentBtn_Click(object sender, EventArgs e)
        {
            FormReportContractVsShipment f = new FormReportContractVsShipment(con);
            f.ShowDialog();
        }

        private void contractsBtn_Click(object sender, EventArgs e)
        {
            FormContracts f = new FormContracts(con);
            f.ShowDialog();
        }
    }
}
