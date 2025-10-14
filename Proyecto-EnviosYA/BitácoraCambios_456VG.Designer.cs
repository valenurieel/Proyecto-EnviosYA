namespace Proyecto_EnviosYA
{
    partial class BitácoraCambios_456VG
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvBitacora = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labeldni = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.cmbModulo = new System.Windows.Forms.ComboBox();
            this.cmbLogin = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(120, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 33);
            this.label1.TabIndex = 70;
            this.label1.Text = "Bitácora de Cambios";
            // 
            // dgvBitacora
            // 
            this.dgvBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBitacora.Location = new System.Drawing.Point(32, 110);
            this.dgvBitacora.Name = "dgvBitacora";
            this.dgvBitacora.Size = new System.Drawing.Size(700, 173);
            this.dgvBitacora.TabIndex = 71;
            this.dgvBitacora.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBitacora_CellContentClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(454, 301);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 15);
            this.label5.TabIndex = 80;
            this.label5.Text = "Fecha Fin:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(199, 301);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 79;
            this.label3.Text = "Fecha Inicio:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(454, 361);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 78;
            this.label2.Text = "Nombre:";
            // 
            // labeldni
            // 
            this.labeldni.AutoSize = true;
            this.labeldni.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeldni.Location = new System.Drawing.Point(199, 361);
            this.labeldni.Name = "labeldni";
            this.labeldni.Size = new System.Drawing.Size(29, 15);
            this.labeldni.TabIndex = 77;
            this.labeldni.Text = "DNI:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(414, 319);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(200, 20);
            this.dtpHasta.TabIndex = 76;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(160, 319);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(200, 20);
            this.dtpDesde.TabIndex = 75;
            // 
            // cmbModulo
            // 
            this.cmbModulo.FormattingEnabled = true;
            this.cmbModulo.Location = new System.Drawing.Point(431, 379);
            this.cmbModulo.Name = "cmbModulo";
            this.cmbModulo.Size = new System.Drawing.Size(161, 21);
            this.cmbModulo.TabIndex = 74;
            // 
            // cmbLogin
            // 
            this.cmbLogin.FormattingEnabled = true;
            this.cmbLogin.Location = new System.Drawing.Point(176, 379);
            this.cmbLogin.Name = "cmbLogin";
            this.cmbLogin.Size = new System.Drawing.Size(161, 21);
            this.cmbLogin.TabIndex = 73;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(240, 436);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 39);
            this.button2.TabIndex = 81;
            this.button2.Text = "Aplicar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(414, 436);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 39);
            this.button1.TabIndex = 82;
            this.button1.Text = "Activar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(326, 499);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 24);
            this.button3.TabIndex = 83;
            this.button3.Text = "Limpiar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(704, 514);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 29);
            this.button4.TabIndex = 84;
            this.button4.Text = "Volver";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(425, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(251, 33);
            this.label4.TabIndex = 85;
            this.label4.Text = "Tabla de Choferes_C";
            // 
            // BitácoraCambios_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(800, 555);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labeldni);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.cmbModulo);
            this.Controls.Add(this.cmbLogin);
            this.Controls.Add(this.dgvBitacora);
            this.Controls.Add(this.label1);
            this.Name = "BitácoraCambios_456VG";
            this.Text = "Bitácora de Cambios";
            this.Load += new System.EventHandler(this.BitácoraCambios_456VG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvBitacora;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labeldni;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.ComboBox cmbModulo;
        private System.Windows.Forms.ComboBox cmbLogin;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
    }
}