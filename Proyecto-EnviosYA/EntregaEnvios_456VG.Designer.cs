namespace Proyecto_EnviosYA
{
    partial class EntregaEnvios_456VG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntregaEnvios_456VG));
            this.button3 = new System.Windows.Forms.Button();
            this.txtCodEnvio = new System.Windows.Forms.TextBox();
            this.txtDNIDest = new System.Windows.Forms.TextBox();
            this.txtNombreDest = new System.Windows.Forms.TextBox();
            this.txtApellidoDest = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.LCP456VG = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.iconPictureBox1456VG = new FontAwesome.Sharp.IconPictureBox();
            this.btnImprimir456VG = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dataGridPaquetes = new System.Windows.Forms.DataGridView();
            this.dataGridEnvio = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1456VG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPaquetes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEnvio)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(922, 487);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 29);
            this.button3.TabIndex = 66;
            this.button3.Text = "Volver";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtCodEnvio
            // 
            this.txtCodEnvio.Location = new System.Drawing.Point(41, 151);
            this.txtCodEnvio.Multiline = true;
            this.txtCodEnvio.Name = "txtCodEnvio";
            this.txtCodEnvio.Size = new System.Drawing.Size(150, 27);
            this.txtCodEnvio.TabIndex = 67;
            // 
            // txtDNIDest
            // 
            this.txtDNIDest.Location = new System.Drawing.Point(229, 151);
            this.txtDNIDest.Multiline = true;
            this.txtDNIDest.Name = "txtDNIDest";
            this.txtDNIDest.Size = new System.Drawing.Size(150, 27);
            this.txtDNIDest.TabIndex = 68;
            // 
            // txtNombreDest
            // 
            this.txtNombreDest.Location = new System.Drawing.Point(410, 151);
            this.txtNombreDest.Multiline = true;
            this.txtNombreDest.Name = "txtNombreDest";
            this.txtNombreDest.Size = new System.Drawing.Size(150, 27);
            this.txtNombreDest.TabIndex = 69;
            // 
            // txtApellidoDest
            // 
            this.txtApellidoDest.Location = new System.Drawing.Point(592, 151);
            this.txtApellidoDest.Multiline = true;
            this.txtApellidoDest.Name = "txtApellidoDest";
            this.txtApellidoDest.Size = new System.Drawing.Size(150, 27);
            this.txtApellidoDest.TabIndex = 70;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(811, 151);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 29);
            this.button4.TabIndex = 78;
            this.button4.Text = "Verificar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // LCP456VG
            // 
            this.LCP456VG.AutoSize = true;
            this.LCP456VG.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LCP456VG.Location = new System.Drawing.Point(38, 133);
            this.LCP456VG.Name = "LCP456VG";
            this.LCP456VG.Size = new System.Drawing.Size(95, 15);
            this.LCP456VG.TabIndex = 79;
            this.LCP456VG.Text = "Código de Envío:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(226, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 80;
            this.label1.Text = "DNI Destinatario:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(407, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 15);
            this.label2.TabIndex = 81;
            this.label2.Text = "Nombre Destinatario:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(589, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 82;
            this.label3.Text = "Apellido Destinatario:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(408, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(225, 33);
            this.label5.TabIndex = 83;
            this.label5.Text = "Entrega de Envíos";
            // 
            // iconPictureBox1456VG
            // 
            this.iconPictureBox1456VG.BackColor = System.Drawing.Color.LightCoral;
            this.iconPictureBox1456VG.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("iconPictureBox1456VG.BackgroundImage")));
            this.iconPictureBox1456VG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.iconPictureBox1456VG.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1456VG.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconPictureBox1456VG.IconColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1456VG.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1456VG.IconSize = 96;
            this.iconPictureBox1456VG.Location = new System.Drawing.Point(668, 21);
            this.iconPictureBox1456VG.Name = "iconPictureBox1456VG";
            this.iconPictureBox1456VG.Size = new System.Drawing.Size(313, 96);
            this.iconPictureBox1456VG.TabIndex = 84;
            this.iconPictureBox1456VG.TabStop = false;
            // 
            // btnImprimir456VG
            // 
            this.btnImprimir456VG.Location = new System.Drawing.Point(437, 473);
            this.btnImprimir456VG.Name = "btnImprimir456VG";
            this.btnImprimir456VG.Size = new System.Drawing.Size(174, 43);
            this.btnImprimir456VG.TabIndex = 85;
            this.btnImprimir456VG.Text = "Entregar Envío";
            this.btnImprimir456VG.UseVisualStyleBackColor = true;
            this.btnImprimir456VG.Click += new System.EventHandler(this.btnImprimir456VG_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Open Sans SemiBold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(26, 192);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 26);
            this.label9.TabIndex = 89;
            this.label9.Text = "Envío:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Open Sans SemiBold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(26, 313);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(169, 26);
            this.label15.TabIndex = 88;
            this.label15.Text = "Paquetes de Envío";
            // 
            // dataGridPaquetes
            // 
            this.dataGridPaquetes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPaquetes.Location = new System.Drawing.Point(31, 342);
            this.dataGridPaquetes.Name = "dataGridPaquetes";
            this.dataGridPaquetes.Size = new System.Drawing.Size(955, 115);
            this.dataGridPaquetes.TabIndex = 87;
            // 
            // dataGridEnvio
            // 
            this.dataGridEnvio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEnvio.Location = new System.Drawing.Point(31, 221);
            this.dataGridEnvio.Name = "dataGridEnvio";
            this.dataGridEnvio.Size = new System.Drawing.Size(955, 90);
            this.dataGridEnvio.TabIndex = 86;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(721, 473);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 43);
            this.button1.TabIndex = 90;
            this.button1.Text = "Reasignar Entrega";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(909, 151);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 29);
            this.button2.TabIndex = 91;
            this.button2.Text = "limpiar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // EntregaEnvios_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(999, 528);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.dataGridPaquetes);
            this.Controls.Add(this.dataGridEnvio);
            this.Controls.Add(this.btnImprimir456VG);
            this.Controls.Add(this.iconPictureBox1456VG);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LCP456VG);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txtApellidoDest);
            this.Controls.Add(this.txtNombreDest);
            this.Controls.Add(this.txtDNIDest);
            this.Controls.Add(this.txtCodEnvio);
            this.Controls.Add(this.button3);
            this.Name = "EntregaEnvios_456VG";
            this.Text = "Entrega de Envíos";
            this.Load += new System.EventHandler(this.EntregaEnvios_456VG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1456VG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPaquetes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEnvio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtCodEnvio;
        private System.Windows.Forms.TextBox txtDNIDest;
        private System.Windows.Forms.TextBox txtNombreDest;
        private System.Windows.Forms.TextBox txtApellidoDest;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label LCP456VG;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1456VG;
        private System.Windows.Forms.Button btnImprimir456VG;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dataGridPaquetes;
        private System.Windows.Forms.DataGridView dataGridEnvio;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}