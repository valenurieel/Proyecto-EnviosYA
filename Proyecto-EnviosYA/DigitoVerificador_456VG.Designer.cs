namespace Proyecto_EnviosYA
{
    partial class DigitoVerificador_456VG
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
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnrecalcular = new System.Windows.Forms.Button();
            this.btnrestaurar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.labelALGO = new System.Windows.Forms.Label();
            this.labelTablas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(73, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(451, 33);
            this.label5.TabIndex = 38;
            this.label5.Text = "Se ha encontrado una Inconsistencia";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(243, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 33);
            this.label1.TabIndex = 39;
            this.label1.Text = "¡ALERTA!";
            // 
            // btnrecalcular
            // 
            this.btnrecalcular.Location = new System.Drawing.Point(185, 185);
            this.btnrecalcular.Name = "btnrecalcular";
            this.btnrecalcular.Size = new System.Drawing.Size(228, 38);
            this.btnrecalcular.TabIndex = 40;
            this.btnrecalcular.Text = "Recalcular Digito Verificador";
            this.btnrecalcular.UseVisualStyleBackColor = true;
            this.btnrecalcular.Click += new System.EventHandler(this.btnrecalcular_Click);
            // 
            // btnrestaurar
            // 
            this.btnrestaurar.Location = new System.Drawing.Point(185, 240);
            this.btnrestaurar.Name = "btnrestaurar";
            this.btnrestaurar.Size = new System.Drawing.Size(228, 38);
            this.btnrestaurar.TabIndex = 41;
            this.btnrestaurar.Text = "Restaurar Base de Datos";
            this.btnrestaurar.UseVisualStyleBackColor = true;
            this.btnrestaurar.Click += new System.EventHandler(this.btnrestaurar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(228, 296);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 38);
            this.button2.TabIndex = 42;
            this.button2.Text = "Salir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelALGO
            // 
            this.labelALGO.AutoSize = true;
            this.labelALGO.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelALGO.Location = new System.Drawing.Point(12, 134);
            this.labelALGO.Name = "labelALGO";
            this.labelALGO.Size = new System.Drawing.Size(139, 20);
            this.labelALGO.TabIndex = 80;
            this.labelALGO.Text = "Tabla/s Afectada/s:";
            // 
            // labelTablas
            // 
            this.labelTablas.AutoSize = true;
            this.labelTablas.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTablas.Location = new System.Drawing.Point(157, 137);
            this.labelTablas.Name = "labelTablas";
            this.labelTablas.Size = new System.Drawing.Size(104, 17);
            this.labelTablas.TabIndex = 81;
            this.labelTablas.Text = "Tablas Afectadas";
            this.labelTablas.Click += new System.EventHandler(this.labelTablas_Click);
            // 
            // DigitoVerificador_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(613, 358);
            this.Controls.Add(this.labelTablas);
            this.Controls.Add(this.labelALGO);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnrestaurar);
            this.Controls.Add(this.btnrecalcular);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Name = "DigitoVerificador_456VG";
            this.Text = "DigitoVerificador_456VG";
            this.Load += new System.EventHandler(this.DigitoVerificador_456VG_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnrecalcular;
        private System.Windows.Forms.Button btnrestaurar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelALGO;
        private System.Windows.Forms.Label labelTablas;
    }
}