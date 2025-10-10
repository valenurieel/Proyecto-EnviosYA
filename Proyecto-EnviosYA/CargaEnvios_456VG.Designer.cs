namespace Proyecto_EnviosYA
{
    partial class CargaEnvios_456VG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CargaEnvios_456VG));
            this.dataGridLista = new System.Windows.Forms.DataGridView();
            this.iconPictureBox1456VG = new FontAwesome.Sharp.IconPictureBox();
            this.cmbListas = new System.Windows.Forms.ComboBox();
            this.dataGridDetalles = new System.Windows.Forms.DataGridView();
            this.btnMarcarCargado = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1456VG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetalles)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridLista
            // 
            this.dataGridLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridLista.Location = new System.Drawing.Point(12, 169);
            this.dataGridLista.Name = "dataGridLista";
            this.dataGridLista.Size = new System.Drawing.Size(759, 178);
            this.dataGridLista.TabIndex = 5;
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
            this.iconPictureBox1456VG.Location = new System.Drawing.Point(776, 32);
            this.iconPictureBox1456VG.Name = "iconPictureBox1456VG";
            this.iconPictureBox1456VG.Size = new System.Drawing.Size(85, 82);
            this.iconPictureBox1456VG.TabIndex = 68;
            this.iconPictureBox1456VG.TabStop = false;
            // 
            // cmbListas
            // 
            this.cmbListas.FormattingEnabled = true;
            this.cmbListas.Location = new System.Drawing.Point(22, 126);
            this.cmbListas.Name = "cmbListas";
            this.cmbListas.Size = new System.Drawing.Size(148, 21);
            this.cmbListas.TabIndex = 82;
            this.cmbListas.SelectedIndexChanged += new System.EventHandler(this.cmbListas_SelectedIndexChanged);
            // 
            // dataGridDetalles
            // 
            this.dataGridDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDetalles.Location = new System.Drawing.Point(12, 362);
            this.dataGridDetalles.Name = "dataGridDetalles";
            this.dataGridDetalles.Size = new System.Drawing.Size(759, 178);
            this.dataGridDetalles.TabIndex = 83;
            this.dataGridDetalles.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDetalles_CellValueChanged);
            // 
            // btnMarcarCargado
            // 
            this.btnMarcarCargado.Location = new System.Drawing.Point(783, 432);
            this.btnMarcarCargado.Name = "btnMarcarCargado";
            this.btnMarcarCargado.Size = new System.Drawing.Size(86, 42);
            this.btnMarcarCargado.TabIndex = 84;
            this.btnMarcarCargado.Text = "Envío Cargado";
            this.btnMarcarCargado.UseVisualStyleBackColor = true;
            this.btnMarcarCargado.Click += new System.EventHandler(this.btnMarcarCargado_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(317, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 33);
            this.label1.TabIndex = 85;
            this.label1.Text = "Carga de Envíos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 86;
            this.label2.Text = "Listas de Carga:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(794, 564);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 29);
            this.button1.TabIndex = 87;
            this.button1.Text = "Volver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(349, 558);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 35);
            this.button2.TabIndex = 88;
            this.button2.Text = "Cerrar Lista de Carga";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CargaEnvios_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(887, 612);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMarcarCargado);
            this.Controls.Add(this.dataGridDetalles);
            this.Controls.Add(this.cmbListas);
            this.Controls.Add(this.iconPictureBox1456VG);
            this.Controls.Add(this.dataGridLista);
            this.Name = "CargaEnvios_456VG";
            this.Text = "Carga de Envíos";
            this.Load += new System.EventHandler(this.CargaEnvios_456VG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1456VG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetalles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridLista;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1456VG;
        private System.Windows.Forms.ComboBox cmbListas;
        private System.Windows.Forms.DataGridView dataGridDetalles;
        private System.Windows.Forms.Button btnMarcarCargado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}