using System;
using System.Windows.Forms;

namespace c_proj
{
    public partial class FormShipQuantity : Form
    {
        public int ShipQuantity { get; private set; }
        public FormShipQuantity(int productId, int maxQuantity)
        {
            InitializeComponent();
            this.Text = "Отгрузка товара";
            nudQuantity.Maximum = maxQuantity;
            nudQuantity.Minimum = 1;
            nudQuantity.Value = 1;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ShipQuantity = (int)nudQuantity.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
