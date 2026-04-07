namespace c_proj
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.productBtn = new System.Windows.Forms.Button();
            this.clientsBtn = new System.Windows.Forms.Button();
            this.reportNotShippedBtn = new System.Windows.Forms.Button();
            this.reportContractVsShipmentBtn = new System.Windows.Forms.Button();
            this.contractsBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // productBtn
            // 
            this.productBtn.Location = new System.Drawing.Point(32, 67);
            this.productBtn.Name = "productBtn";
            this.productBtn.Size = new System.Drawing.Size(212, 23);
            this.productBtn.TabIndex = 0;
            this.productBtn.Text = "продукты";
            this.productBtn.UseVisualStyleBackColor = true;
            this.productBtn.Click += new System.EventHandler(this.productBtn_Click);
            // 
            // clientsBtn
            // 
            this.clientsBtn.Location = new System.Drawing.Point(32, 117);
            this.clientsBtn.Name = "clientsBtn";
            this.clientsBtn.Size = new System.Drawing.Size(212, 23);
            this.clientsBtn.TabIndex = 1;
            this.clientsBtn.Text = "клиенты";
            this.clientsBtn.UseVisualStyleBackColor = true;
            this.clientsBtn.Click += new System.EventHandler(this.clientsBtn_Click);
            // 
            // reportNotShippedBtn
            // 
            this.reportNotShippedBtn.Location = new System.Drawing.Point(32, 167);
            this.reportNotShippedBtn.Name = "reportNotShippedBtn";
            this.reportNotShippedBtn.Size = new System.Drawing.Size(212, 23);
            this.reportNotShippedBtn.TabIndex = 2;
            this.reportNotShippedBtn.Text = "неотгруженные товары";
            this.reportNotShippedBtn.UseVisualStyleBackColor = true;
            this.reportNotShippedBtn.Click += new System.EventHandler(this.reportNotShippedBtn_Click);
            // 
            // reportContractVsShipmentBtn
            // 
            this.reportContractVsShipmentBtn.Location = new System.Drawing.Point(32, 219);
            this.reportContractVsShipmentBtn.Name = "reportContractVsShipmentBtn";
            this.reportContractVsShipmentBtn.Size = new System.Drawing.Size(212, 23);
            this.reportContractVsShipmentBtn.TabIndex = 3;
            this.reportContractVsShipmentBtn.Text = "соотношение товаров";
            this.reportContractVsShipmentBtn.UseVisualStyleBackColor = true;
            this.reportContractVsShipmentBtn.Click += new System.EventHandler(this.reportContractVsShipmentBtn_Click);
            // 
            // contractsBtn
            // 
            this.contractsBtn.Location = new System.Drawing.Point(32, 271);
            this.contractsBtn.Name = "contractsBtn";
            this.contractsBtn.Size = new System.Drawing.Size(212, 23);
            this.contractsBtn.TabIndex = 4;
            this.contractsBtn.Text = "договоры";
            this.contractsBtn.UseVisualStyleBackColor = true;
            this.contractsBtn.Click += new System.EventHandler(this.contractsBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 348);
            this.Controls.Add(this.contractsBtn);
            this.Controls.Add(this.reportContractVsShipmentBtn);
            this.Controls.Add(this.reportNotShippedBtn);
            this.Controls.Add(this.clientsBtn);
            this.Controls.Add(this.productBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button productBtn;
        private System.Windows.Forms.Button clientsBtn;
        private System.Windows.Forms.Button reportNotShippedBtn;
        private System.Windows.Forms.Button reportContractVsShipmentBtn;
        private System.Windows.Forms.Button contractsBtn;
    }
}

