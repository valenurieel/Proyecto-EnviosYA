namespace Proyecto_EnviosYA
{
    partial class CerrarSesión_456VG
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
            this.label1456VG = new System.Windows.Forms.Label();
            this.btnAceptar456VG = new System.Windows.Forms.Button();
            this.btnCancelar456VG = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1456VG
            // 
            this.label1456VG.AutoSize = true;
            this.label1456VG.Font = new System.Drawing.Font("Open Sans SemiBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1456VG.Location = new System.Drawing.Point(178, 112);
            this.label1456VG.Name = "label1456VG";
            this.label1456VG.Size = new System.Drawing.Size(378, 33);
            this.label1456VG.TabIndex = 0;
            this.label1456VG.Text = "¿Estas seguro de Cerrar Sesión?";
            // 
            // btnAceptar456VG
            // 
            this.btnAceptar456VG.Location = new System.Drawing.Point(184, 258);
            this.btnAceptar456VG.Name = "btnAceptar456VG";
            this.btnAceptar456VG.Size = new System.Drawing.Size(118, 45);
            this.btnAceptar456VG.TabIndex = 1;
            this.btnAceptar456VG.Text = "Aceptar";
            this.btnAceptar456VG.UseVisualStyleBackColor = true;
            this.btnAceptar456VG.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar456VG
            // 
            this.btnCancelar456VG.Location = new System.Drawing.Point(438, 258);
            this.btnCancelar456VG.Name = "btnCancelar456VG";
            this.btnCancelar456VG.Size = new System.Drawing.Size(118, 45);
            this.btnCancelar456VG.TabIndex = 2;
            this.btnCancelar456VG.Text = "Cancelar";
            this.btnCancelar456VG.UseVisualStyleBackColor = true;
            this.btnCancelar456VG.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // CerrarSesión_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(748, 448);
            this.Controls.Add(this.btnCancelar456VG);
            this.Controls.Add(this.btnAceptar456VG);
            this.Controls.Add(this.label1456VG);
            this.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "CerrarSesión_456VG";
            this.Text = "Cerrar Sesión";
            this.Load += new System.EventHandler(this.CerrarSesión_456VG_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1456VG;
        private System.Windows.Forms.Button btnAceptar456VG;
        private System.Windows.Forms.Button btnCancelar456VG;
    }
}