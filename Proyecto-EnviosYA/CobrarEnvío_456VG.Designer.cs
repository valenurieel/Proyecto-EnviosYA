namespace Proyecto_EnviosYA
{
    partial class CobrarEnvío_456VG
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
            this.txtDNI456VG = new System.Windows.Forms.TextBox();
            this.txtNTarj456VG = new System.Windows.Forms.TextBox();
            this.txtTitular456VG = new System.Windows.Forms.TextBox();
            this.txtCVC456VG = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1456VG = new System.Windows.Forms.DateTimePicker();
            this.cmbMedPago456VG = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAggTarj456VG = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblImporte456VG = new System.Windows.Forms.Label();
            this.dataGridView1456VG = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvPaquetesDetalle = new System.Windows.Forms.DataGridView();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1456VG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaquetesDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDNI456VG
            // 
            this.txtDNI456VG.Location = new System.Drawing.Point(35, 412);
            this.txtDNI456VG.Name = "txtDNI456VG";
            this.txtDNI456VG.Size = new System.Drawing.Size(121, 22);
            this.txtDNI456VG.TabIndex = 0;
            this.txtDNI456VG.TextChanged += new System.EventHandler(this.txtDNI456VG_TextChanged_1);
            this.txtDNI456VG.Leave += new System.EventHandler(this.txtDNI456VG_Leave_1);
            // 
            // txtNTarj456VG
            // 
            this.txtNTarj456VG.Location = new System.Drawing.Point(193, 360);
            this.txtNTarj456VG.Name = "txtNTarj456VG";
            this.txtNTarj456VG.Size = new System.Drawing.Size(121, 22);
            this.txtNTarj456VG.TabIndex = 1;
            // 
            // txtTitular456VG
            // 
            this.txtTitular456VG.Location = new System.Drawing.Point(193, 412);
            this.txtTitular456VG.Name = "txtTitular456VG";
            this.txtTitular456VG.Size = new System.Drawing.Size(121, 22);
            this.txtTitular456VG.TabIndex = 2;
            // 
            // txtCVC456VG
            // 
            this.txtCVC456VG.Location = new System.Drawing.Point(343, 412);
            this.txtCVC456VG.Name = "txtCVC456VG";
            this.txtCVC456VG.Size = new System.Drawing.Size(121, 22);
            this.txtCVC456VG.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 394);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "DNI:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 342);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Número de Tarjeta:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 394);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Titular:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(340, 342);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Fecha de Vencimiento:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 394);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "CVC:";
            // 
            // dateTimePicker1456VG
            // 
            this.dateTimePicker1456VG.Location = new System.Drawing.Point(343, 360);
            this.dateTimePicker1456VG.Name = "dateTimePicker1456VG";
            this.dateTimePicker1456VG.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1456VG.TabIndex = 10;
            // 
            // cmbMedPago456VG
            // 
            this.cmbMedPago456VG.FormattingEnabled = true;
            this.cmbMedPago456VG.Items.AddRange(new object[] {
            "Mercado Pago",
            "Débito",
            "Crédito"});
            this.cmbMedPago456VG.Location = new System.Drawing.Point(35, 360);
            this.cmbMedPago456VG.Name = "cmbMedPago456VG";
            this.cmbMedPago456VG.Size = new System.Drawing.Size(121, 23);
            this.cmbMedPago456VG.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 342);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "Medio de Pago:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(415, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(146, 28);
            this.label8.TabIndex = 57;
            this.label8.Text = "Cobrar Envío";
            // 
            // btnAggTarj456VG
            // 
            this.btnAggTarj456VG.Location = new System.Drawing.Point(811, 430);
            this.btnAggTarj456VG.Name = "btnAggTarj456VG";
            this.btnAggTarj456VG.Size = new System.Drawing.Size(94, 39);
            this.btnAggTarj456VG.TabIndex = 58;
            this.btnAggTarj456VG.Text = "Cobrar Envío";
            this.btnAggTarj456VG.UseVisualStyleBackColor = true;
            this.btnAggTarj456VG.Click += new System.EventHandler(this.btnAggTarj456VG_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(33, 451);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 18);
            this.label7.TabIndex = 59;
            this.label7.Text = "Importe Total del Envío:";
            // 
            // lblImporte456VG
            // 
            this.lblImporte456VG.AutoSize = true;
            this.lblImporte456VG.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImporte456VG.Location = new System.Drawing.Point(215, 448);
            this.lblImporte456VG.Name = "lblImporte456VG";
            this.lblImporte456VG.Size = new System.Drawing.Size(82, 22);
            this.lblImporte456VG.TabIndex = 60;
            this.lblImporte456VG.Text = "IMPORTE";
            // 
            // dataGridView1456VG
            // 
            this.dataGridView1456VG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1456VG.Location = new System.Drawing.Point(12, 61);
            this.dataGridView1456VG.Name = "dataGridView1456VG";
            this.dataGridView1456VG.Size = new System.Drawing.Size(893, 115);
            this.dataGridView1456VG.TabIndex = 61;
            this.dataGridView1456VG.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1456VG_CellClick);
            this.dataGridView1456VG.SelectionChanged += new System.EventHandler(this.dataGridView1456VG_SelectionChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(811, 388);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 27);
            this.button1.TabIndex = 62;
            this.button1.Text = "Volver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvPaquetesDetalle
            // 
            this.dgvPaquetesDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaquetesDetalle.Location = new System.Drawing.Point(12, 216);
            this.dgvPaquetesDetalle.Name = "dgvPaquetesDetalle";
            this.dgvPaquetesDetalle.Size = new System.Drawing.Size(893, 115);
            this.dgvPaquetesDetalle.TabIndex = 63;
            this.dgvPaquetesDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaquetesDetalle_CellContentClick);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Open Sans SemiBold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(12, 187);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(124, 26);
            this.label15.TabIndex = 64;
            this.label15.Text = "Detalle Envío";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Open Sans SemiBold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(173, 26);
            this.label9.TabIndex = 65;
            this.label9.Text = "Envíos no Pagados";
            // 
            // CobrarEnvío_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(919, 480);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.dgvPaquetesDetalle);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1456VG);
            this.Controls.Add(this.lblImporte456VG);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnAggTarj456VG);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbMedPago456VG);
            this.Controls.Add(this.dateTimePicker1456VG);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCVC456VG);
            this.Controls.Add(this.txtTitular456VG);
            this.Controls.Add(this.txtNTarj456VG);
            this.Controls.Add(this.txtDNI456VG);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CobrarEnvío_456VG";
            this.Text = "Cobrar Envío";
            this.Load += new System.EventHandler(this.CobrarEnvío_456VG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1456VG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaquetesDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDNI456VG;
        private System.Windows.Forms.TextBox txtNTarj456VG;
        private System.Windows.Forms.TextBox txtTitular456VG;
        private System.Windows.Forms.TextBox txtCVC456VG;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1456VG;
        private System.Windows.Forms.ComboBox cmbMedPago456VG;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAggTarj456VG;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblImporte456VG;
        private System.Windows.Forms.DataGridView dataGridView1456VG;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvPaquetesDetalle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label9;
    }
}