﻿namespace Proyecto_EnviosYA
{
    partial class IniciarSesion_456VG
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
            this.checkVer = new System.Windows.Forms.CheckBox();
            this.btningresar = new System.Windows.Forms.Button();
            this.lblcambiaridioma = new System.Windows.Forms.Label();
            this.cmbcambiaridioma = new System.Windows.Forms.ComboBox();
            this.lblcontraseña = new System.Windows.Forms.Label();
            this.lbldni = new System.Windows.Forms.Label();
            this.txtcontraseña = new System.Windows.Forms.TextBox();
            this.txtdni = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // checkVer
            // 
            this.checkVer.AutoSize = true;
            this.checkVer.Location = new System.Drawing.Point(465, 336);
            this.checkVer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkVer.Name = "checkVer";
            this.checkVer.Size = new System.Drawing.Size(43, 19);
            this.checkVer.TabIndex = 23;
            this.checkVer.Text = "Ver";
            this.checkVer.UseVisualStyleBackColor = true;
            // 
            // btningresar
            // 
            this.btningresar.Location = new System.Drawing.Point(322, 381);
            this.btningresar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btningresar.Name = "btningresar";
            this.btningresar.Size = new System.Drawing.Size(104, 38);
            this.btningresar.TabIndex = 22;
            this.btningresar.Text = "Ingresar";
            this.btningresar.UseVisualStyleBackColor = true;
            this.btningresar.Click += new System.EventHandler(this.btningresar_Click);
            // 
            // lblcambiaridioma
            // 
            this.lblcambiaridioma.AutoSize = true;
            this.lblcambiaridioma.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcambiaridioma.Location = new System.Drawing.Point(628, 62);
            this.lblcambiaridioma.Name = "lblcambiaridioma";
            this.lblcambiaridioma.Size = new System.Drawing.Size(100, 17);
            this.lblcambiaridioma.TabIndex = 20;
            this.lblcambiaridioma.Text = "Cambiar Idioma:";
            // 
            // cmbcambiaridioma
            // 
            this.cmbcambiaridioma.FormattingEnabled = true;
            this.cmbcambiaridioma.Items.AddRange(new object[] {
            "Inglés",
            "Español"});
            this.cmbcambiaridioma.Location = new System.Drawing.Point(631, 85);
            this.cmbcambiaridioma.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbcambiaridioma.Name = "cmbcambiaridioma";
            this.cmbcambiaridioma.Size = new System.Drawing.Size(121, 23);
            this.cmbcambiaridioma.TabIndex = 19;
            // 
            // lblcontraseña
            // 
            this.lblcontraseña.AutoSize = true;
            this.lblcontraseña.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcontraseña.Location = new System.Drawing.Point(296, 306);
            this.lblcontraseña.Name = "lblcontraseña";
            this.lblcontraseña.Size = new System.Drawing.Size(76, 17);
            this.lblcontraseña.TabIndex = 18;
            this.lblcontraseña.Text = "Contraseña:";
            // 
            // lbldni
            // 
            this.lbldni.AutoSize = true;
            this.lbldni.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldni.Location = new System.Drawing.Point(296, 223);
            this.lbldni.Name = "lbldni";
            this.lbldni.Size = new System.Drawing.Size(32, 17);
            this.lbldni.TabIndex = 17;
            this.lbldni.Text = "DNI:";
            // 
            // txtcontraseña
            // 
            this.txtcontraseña.Location = new System.Drawing.Point(299, 329);
            this.txtcontraseña.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtcontraseña.Multiline = true;
            this.txtcontraseña.Name = "txtcontraseña";
            this.txtcontraseña.PasswordChar = '*';
            this.txtcontraseña.Size = new System.Drawing.Size(149, 31);
            this.txtcontraseña.TabIndex = 21;
            // 
            // txtdni
            // 
            this.txtdni.Location = new System.Drawing.Point(299, 246);
            this.txtdni.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdni.Multiline = true;
            this.txtdni.Name = "txtdni";
            this.txtdni.Size = new System.Drawing.Size(149, 31);
            this.txtdni.TabIndex = 16;
            // 
            // IniciarSesion_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(800, 519);
            this.Controls.Add(this.checkVer);
            this.Controls.Add(this.btningresar);
            this.Controls.Add(this.lblcambiaridioma);
            this.Controls.Add(this.cmbcambiaridioma);
            this.Controls.Add(this.lblcontraseña);
            this.Controls.Add(this.lbldni);
            this.Controls.Add(this.txtcontraseña);
            this.Controls.Add(this.txtdni);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "IniciarSesion_456VG";
            this.Text = "Iniciar Sesion";
            this.Load += new System.EventHandler(this.IniciarSesion_456VG_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkVer;
        private System.Windows.Forms.Button btningresar;
        private System.Windows.Forms.Label lblcambiaridioma;
        private System.Windows.Forms.ComboBox cmbcambiaridioma;
        private System.Windows.Forms.Label lblcontraseña;
        private System.Windows.Forms.Label lbldni;
        private System.Windows.Forms.TextBox txtcontraseña;
        private System.Windows.Forms.TextBox txtdni;
    }
}