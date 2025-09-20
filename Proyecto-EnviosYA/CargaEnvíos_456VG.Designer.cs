namespace Proyecto_EnviosYA
{
    partial class CargaEnvíos_456VG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CargaEnvíos_456VG));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.iconPictureBox1456VG = new FontAwesome.Sharp.IconPictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.LCP456VG = new System.Windows.Forms.Label();
            this.cmbEstadoEnv = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1456VG)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 169);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1000, 245);
            this.dataGridView1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(949, 458);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 28);
            this.button1.TabIndex = 66;
            this.button1.Text = "Volver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(405, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 33);
            this.label5.TabIndex = 67;
            this.label5.Text = "Carga de Envíos";
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
            this.iconPictureBox1456VG.IconSize = 82;
            this.iconPictureBox1456VG.Location = new System.Drawing.Point(927, 32);
            this.iconPictureBox1456VG.Name = "iconPictureBox1456VG";
            this.iconPictureBox1456VG.Size = new System.Drawing.Size(85, 82);
            this.iconPictureBox1456VG.TabIndex = 68;
            this.iconPictureBox1456VG.TabStop = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(449, 444);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(133, 42);
            this.button4.TabIndex = 77;
            this.button4.Text = "Cerrar Lista de Carga";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // LCP456VG
            // 
            this.LCP456VG.AutoSize = true;
            this.LCP456VG.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LCP456VG.Location = new System.Drawing.Point(19, 99);
            this.LCP456VG.Name = "LCP456VG";
            this.LCP456VG.Size = new System.Drawing.Size(87, 15);
            this.LCP456VG.TabIndex = 81;
            this.LCP456VG.Text = "Listas de Carga:";
            // 
            // cmbEstadoEnv
            // 
            this.cmbEstadoEnv.FormattingEnabled = true;
            this.cmbEstadoEnv.Location = new System.Drawing.Point(22, 126);
            this.cmbEstadoEnv.Name = "cmbEstadoEnv";
            this.cmbEstadoEnv.Size = new System.Drawing.Size(148, 21);
            this.cmbEstadoEnv.TabIndex = 82;
            // 
            // CargaEnvíos_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(1034, 498);
            this.Controls.Add(this.cmbEstadoEnv);
            this.Controls.Add(this.LCP456VG);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.iconPictureBox1456VG);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CargaEnvíos_456VG";
            this.Text = "Carga de Envíos";
            this.Load += new System.EventHandler(this.CargaEnvíos_456VG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1456VG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1456VG;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label LCP456VG;
        private System.Windows.Forms.ComboBox cmbEstadoEnv;
    }
}