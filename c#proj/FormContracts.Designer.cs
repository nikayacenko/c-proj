namespace c_proj
{
    partial class FormContracts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewContracts = new System.Windows.Forms.DataGridView();
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.btnAddContract = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnShipment = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeleteContract = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContracts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewContracts
            // 
            this.dataGridViewContracts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewContracts.Location = new System.Drawing.Point(0, 21);
            this.dataGridViewContracts.Name = "dataGridViewContracts";
            this.dataGridViewContracts.RowHeadersWidth = 51;
            this.dataGridViewContracts.RowTemplate.Height = 24;
            this.dataGridViewContracts.Size = new System.Drawing.Size(543, 179);
            this.dataGridViewContracts.TabIndex = 0;
            this.dataGridViewContracts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewContracts_CellClick);
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Location = new System.Drawing.Point(0, 21);
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.RowHeadersWidth = 51;
            this.dataGridViewItems.Size = new System.Drawing.Size(543, 183);
            this.dataGridViewItems.TabIndex = 1;
            // 
            // btnAddContract
            // 
            this.btnAddContract.Location = new System.Drawing.Point(190, 431);
            this.btnAddContract.Name = "btnAddContract";
            this.btnAddContract.Size = new System.Drawing.Size(240, 23);
            this.btnAddContract.TabIndex = 2;
            this.btnAddContract.Text = "новый договор";
            this.btnAddContract.UseVisualStyleBackColor = true;
            this.btnAddContract.Click += new System.EventHandler(this.btnAddContract_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(190, 489);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(240, 23);
            this.btnAddItem.TabIndex = 3;
            this.btnAddItem.Text = "добавить товар";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(190, 518);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(240, 23);
            this.btnDeleteItem.TabIndex = 4;
            this.btnDeleteItem.Text = "удалить товар";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // btnShipment
            // 
            this.btnShipment.Location = new System.Drawing.Point(190, 547);
            this.btnShipment.Name = "btnShipment";
            this.btnShipment.Size = new System.Drawing.Size(240, 23);
            this.btnShipment.TabIndex = 5;
            this.btnShipment.Text = "отгрузка по договору";
            this.btnShipment.UseVisualStyleBackColor = true;
            this.btnShipment.Click += new System.EventHandler(this.btnShipment_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewContracts);
            this.groupBox1.Location = new System.Drawing.Point(44, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 190);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "список договоров";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewItems);
            this.groupBox2.Location = new System.Drawing.Point(44, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(543, 210);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "товары в договоре";
            // 
            // btnDeleteContract
            // 
            this.btnDeleteContract.Location = new System.Drawing.Point(190, 460);
            this.btnDeleteContract.Name = "btnDeleteContract";
            this.btnDeleteContract.Size = new System.Drawing.Size(240, 23);
            this.btnDeleteContract.TabIndex = 8;
            this.btnDeleteContract.Text = "удалить договор";
            this.btnDeleteContract.UseVisualStyleBackColor = true;
            this.btnDeleteContract.Click += new System.EventHandler(this.btnDeleteContract_Click);
            // 
            // FormContracts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 633);
            this.Controls.Add(this.btnDeleteContract);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnShipment);
            this.Controls.Add(this.btnDeleteItem);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnAddContract);
            this.Name = "FormContracts";
            this.Text = "FormContracts";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContracts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewContracts;
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private System.Windows.Forms.Button btnAddContract;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Button btnShipment;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeleteContract;
    }
}