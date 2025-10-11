namespace Proyecto_EnviosYA
{
    partial class ReasignarEntrega_456VG
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
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridEnvio = new System.Windows.Forms.DataGridView();
            this.btnImprimir456VG = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.LCP456VG = new System.Windows.Forms.Label();
            this.txtCodEnvio = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.dataGridPaquetes = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEnvio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPaquetes)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Open Sans SemiBold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 26);
            this.label9.TabIndex = 95;
            this.label9.Text = "Envío:";
            // 
            // dataGridEnvio
            // 
            this.dataGridEnvio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEnvio.Location = new System.Drawing.Point(12, 185);
            this.dataGridEnvio.Name = "dataGridEnvio";
            this.dataGridEnvio.Size = new System.Drawing.Size(934, 90);
            this.dataGridEnvio.TabIndex = 94;
            // 
            // btnImprimir456VG
            // 
            this.btnImprimir456VG.Location = new System.Drawing.Point(407, 444);
            this.btnImprimir456VG.Name = "btnImprimir456VG";
            this.btnImprimir456VG.Size = new System.Drawing.Size(174, 43);
            this.btnImprimir456VG.TabIndex = 93;
            this.btnImprimir456VG.Text = "Reasignar Envío";
            this.btnImprimir456VG.UseVisualStyleBackColor = true;
            this.btnImprimir456VG.Click += new System.EventHandler(this.btnImprimir456VG_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(389, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(216, 33);
            this.label5.TabIndex = 92;
            this.label5.Text = "Reasignar Envíos";
            // 
            // LCP456VG
            // 
            this.LCP456VG.AutoSize = true;
            this.LCP456VG.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LCP456VG.Location = new System.Drawing.Point(37, 99);
            this.LCP456VG.Name = "LCP456VG";
            this.LCP456VG.Size = new System.Drawing.Size(95, 15);
            this.LCP456VG.TabIndex = 91;
            this.LCP456VG.Text = "Código de Envío:";
            // 
            // txtCodEnvio
            // 
            this.txtCodEnvio.Location = new System.Drawing.Point(40, 117);
            this.txtCodEnvio.Multiline = true;
            this.txtCodEnvio.Name = "txtCodEnvio";
            this.txtCodEnvio.Size = new System.Drawing.Size(150, 27);
            this.txtCodEnvio.TabIndex = 90;
            this.txtCodEnvio.Leave += new System.EventHandler(this.txtCodEnvio_Leave);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(883, 458);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 29);
            this.button3.TabIndex = 96;
            this.button3.Text = "Volver";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Open Sans SemiBold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(7, 288);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(169, 26);
            this.label15.TabIndex = 98;
            this.label15.Text = "Paquetes de Envío";
            // 
            // dataGridPaquetes
            // 
            this.dataGridPaquetes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPaquetes.Location = new System.Drawing.Point(12, 317);
            this.dataGridPaquetes.Name = "dataGridPaquetes";
            this.dataGridPaquetes.Size = new System.Drawing.Size(934, 115);
            this.dataGridPaquetes.TabIndex = 97;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(238, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 100;
            this.label1.Text = "Motivo:";
            // 
            // txtMotivo
            // 
            this.txtMotivo.Location = new System.Drawing.Point(241, 117);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(239, 27);
            this.txtMotivo.TabIndex = 101;
            // 
            // ReasignarEntrega_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(958, 499);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.dataGridPaquetes);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dataGridEnvio);
            this.Controls.Add(this.btnImprimir456VG);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LCP456VG);
            this.Controls.Add(this.txtCodEnvio);
            this.Name = "ReasignarEntrega_456VG";
            this.Text = "Reasignar Entrega";
            this.Load += new System.EventHandler(this.ReasignarEntrega_456VG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEnvio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPaquetes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridEnvio;
        private System.Windows.Forms.Button btnImprimir456VG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LCP456VG;
        private System.Windows.Forms.TextBox txtCodEnvio;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dataGridPaquetes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMotivo;
    }
}