namespace Proyecto_EnviosYA
{
    partial class SeguimientoEnvíos_456VG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeguimientoEnvíos_456VG));
            this.button1 = new System.Windows.Forms.Button();
            this.btnImprimir456VG = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.iconPictureBox1456VG = new FontAwesome.Sharp.IconPictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1456VG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(707, 404);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 28);
            this.button1.TabIndex = 69;
            this.button1.Text = "Volver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnImprimir456VG
            // 
            this.btnImprimir456VG.Location = new System.Drawing.Point(355, 331);
            this.btnImprimir456VG.Name = "btnImprimir456VG";
            this.btnImprimir456VG.Size = new System.Drawing.Size(101, 35);
            this.btnImprimir456VG.TabIndex = 68;
            this.btnImprimir456VG.Text = "Imprimir";
            this.btnImprimir456VG.UseVisualStyleBackColor = true;
            this.btnImprimir456VG.Click += new System.EventHandler(this.btnImprimir456VG_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(238, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(384, 33);
            this.label5.TabIndex = 67;
            this.label5.Text = "Imprimir Seguimiento de Envío";
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
            this.iconPictureBox1456VG.Location = new System.Drawing.Point(695, 18);
            this.iconPictureBox1456VG.Name = "iconPictureBox1456VG";
            this.iconPictureBox1456VG.Size = new System.Drawing.Size(85, 82);
            this.iconPictureBox1456VG.TabIndex = 66;
            this.iconPictureBox1456VG.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 116);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(770, 173);
            this.dataGridView1.TabIndex = 65;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // SeguimientoEnvíos_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnImprimir456VG);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.iconPictureBox1456VG);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SeguimientoEnvíos_456VG";
            this.Text = "Seguimiento Envíos";
            this.Load += new System.EventHandler(this.SeguimientoEnvíos_456VG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1456VG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnImprimir456VG;
        private System.Windows.Forms.Label label5;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1456VG;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}