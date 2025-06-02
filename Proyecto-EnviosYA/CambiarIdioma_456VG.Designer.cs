namespace Proyecto_EnviosYA
{
    partial class CambiarIdioma_456VG
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
            this.button1456VG = new System.Windows.Forms.Button();
            this.label1456VG = new System.Windows.Forms.Label();
            this.box456VG = new System.Windows.Forms.ComboBox();
            this.button2456VG = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1456VG
            // 
            this.button1456VG.Location = new System.Drawing.Point(141, 156);
            this.button1456VG.Name = "button1456VG";
            this.button1456VG.Size = new System.Drawing.Size(113, 32);
            this.button1456VG.TabIndex = 6;
            this.button1456VG.Text = "Cambiar Idioma";
            this.button1456VG.UseVisualStyleBackColor = true;
            this.button1456VG.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1456VG
            // 
            this.label1456VG.AutoSize = true;
            this.label1456VG.Font = new System.Drawing.Font("Open Sans", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1456VG.Location = new System.Drawing.Point(122, 84);
            this.label1456VG.Name = "label1456VG";
            this.label1456VG.Size = new System.Drawing.Size(63, 20);
            this.label1456VG.TabIndex = 7;
            this.label1456VG.Text = "Idioma:";
            // 
            // box456VG
            // 
            this.box456VG.FormattingEnabled = true;
            this.box456VG.Items.AddRange(new object[] {
            "Español",
            "Inglés"});
            this.box456VG.Location = new System.Drawing.Point(125, 113);
            this.box456VG.Name = "box456VG";
            this.box456VG.Size = new System.Drawing.Size(141, 21);
            this.box456VG.TabIndex = 5;
            // 
            // button2456VG
            // 
            this.button2456VG.Location = new System.Drawing.Point(287, 247);
            this.button2456VG.Name = "button2456VG";
            this.button2456VG.Size = new System.Drawing.Size(94, 27);
            this.button2456VG.TabIndex = 63;
            this.button2456VG.Text = "Volver";
            this.button2456VG.UseVisualStyleBackColor = true;
            this.button2456VG.Click += new System.EventHandler(this.button2456VG_Click);
            // 
            // CambiarIdioma_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(412, 286);
            this.Controls.Add(this.button2456VG);
            this.Controls.Add(this.button1456VG);
            this.Controls.Add(this.label1456VG);
            this.Controls.Add(this.box456VG);
            this.Name = "CambiarIdioma_456VG";
            this.Text = "Cambiar Idioma";
            this.Load += new System.EventHandler(this.CambiarIdioma_456VG_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1456VG;
        private System.Windows.Forms.Label label1456VG;
        private System.Windows.Forms.ComboBox box456VG;
        private System.Windows.Forms.Button button2456VG;
    }
}