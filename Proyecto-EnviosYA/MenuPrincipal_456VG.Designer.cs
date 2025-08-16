namespace Proyecto_EnviosYA
{
    partial class MenuPrincipal_456VG
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal_456VG));
            this.menuStrip1456VG = new System.Windows.Forms.MenuStrip();
            this.MenuAdministrador = new System.Windows.Forms.ToolStripMenuItem();
            this.GestióndeUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.GestióndePerfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.BitacoraEventos = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMaestro = new System.Windows.Forms.ToolStripMenuItem();
            this.GestióndeClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.IniciarSesión = new System.Windows.Forms.ToolStripMenuItem();
            this.CambiarContraseña = new System.Windows.Forms.ToolStripMenuItem();
            this.CerrarSesión = new System.Windows.Forms.ToolStripMenuItem();
            this.CambiarIdioma = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuRecepción = new System.Windows.Forms.ToolStripMenuItem();
            this.CrearEnvío = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEnvíos = new System.Windows.Forms.ToolStripMenuItem();
            this.CobrarEnvío = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.FacturasIMP = new System.Windows.Forms.ToolStripMenuItem();
            this.SeguimientoEnvíos = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.iconPictureBox1456VG = new FontAwesome.Sharp.IconPictureBox();
            this.lblBienvenido456VG = new System.Windows.Forms.Label();
            this.lblBienvenidoDefault = new System.Windows.Forms.Label();
            this.menuStrip1456VG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1456VG)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1456VG
            // 
            this.menuStrip1456VG.BackColor = System.Drawing.Color.IndianRed;
            this.menuStrip1456VG.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1456VG.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1456VG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAdministrador,
            this.MenuMaestro,
            this.MenuUsuarios,
            this.MenuRecepción,
            this.MenuEnvíos,
            this.MenuReportes,
            this.MenuAyuda,
            this.MenuSalir});
            this.menuStrip1456VG.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1456VG.Name = "menuStrip1456VG";
            this.menuStrip1456VG.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1456VG.Size = new System.Drawing.Size(127, 652);
            this.menuStrip1456VG.TabIndex = 0;
            this.menuStrip1456VG.Text = "menuStrip1";
            this.menuStrip1456VG.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1456VG_ItemClicked);
            // 
            // MenuAdministrador
            // 
            this.MenuAdministrador.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GestióndeUsuarios,
            this.GestióndePerfiles,
            this.BitacoraEventos});
            this.MenuAdministrador.Name = "MenuAdministrador";
            this.MenuAdministrador.Size = new System.Drawing.Size(112, 22);
            this.MenuAdministrador.Text = "Administrador";
            this.MenuAdministrador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GestióndeUsuarios
            // 
            this.GestióndeUsuarios.Name = "GestióndeUsuarios";
            this.GestióndeUsuarios.Size = new System.Drawing.Size(195, 22);
            this.GestióndeUsuarios.Text = "Gestión de Usuarios";
            this.GestióndeUsuarios.Click += new System.EventHandler(this.usuariosToolStripMenuItem_Click);
            // 
            // GestióndePerfiles
            // 
            this.GestióndePerfiles.Name = "GestióndePerfiles";
            this.GestióndePerfiles.Size = new System.Drawing.Size(195, 22);
            this.GestióndePerfiles.Text = "Gestión de Perfiles";
            this.GestióndePerfiles.Click += new System.EventHandler(this.perfilesToolStripMenuItem456VG_Click);
            // 
            // BitacoraEventos
            // 
            this.BitacoraEventos.Name = "BitacoraEventos";
            this.BitacoraEventos.Size = new System.Drawing.Size(195, 22);
            this.BitacoraEventos.Text = "Bitácora de Eventos";
            this.BitacoraEventos.Click += new System.EventHandler(this.bitácoraDeEventosToolStripMenuItem_Click);
            // 
            // MenuMaestro
            // 
            this.MenuMaestro.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GestióndeClientes});
            this.MenuMaestro.Name = "MenuMaestro";
            this.MenuMaestro.Size = new System.Drawing.Size(112, 22);
            this.MenuMaestro.Text = "Maestro";
            this.MenuMaestro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GestióndeClientes
            // 
            this.GestióndeClientes.Name = "GestióndeClientes";
            this.GestióndeClientes.Size = new System.Drawing.Size(190, 22);
            this.GestióndeClientes.Text = "Gestión de Clientes";
            this.GestióndeClientes.Click += new System.EventHandler(this.clientesToolStripMenuItem456VG_Click);
            // 
            // MenuUsuarios
            // 
            this.MenuUsuarios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IniciarSesión,
            this.CambiarContraseña,
            this.CerrarSesión,
            this.CambiarIdioma});
            this.MenuUsuarios.Name = "MenuUsuarios";
            this.MenuUsuarios.Size = new System.Drawing.Size(112, 22);
            this.MenuUsuarios.Text = "Usuario";
            this.MenuUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IniciarSesión
            // 
            this.IniciarSesión.Name = "IniciarSesión";
            this.IniciarSesión.Size = new System.Drawing.Size(198, 22);
            this.IniciarSesión.Text = "Iniciar Sesión";
            this.IniciarSesión.Click += new System.EventHandler(this.iniciarSesiónToolStripMenuItem_Click);
            // 
            // CambiarContraseña
            // 
            this.CambiarContraseña.Name = "CambiarContraseña";
            this.CambiarContraseña.Size = new System.Drawing.Size(198, 22);
            this.CambiarContraseña.Text = "Cambiar Contraseña";
            this.CambiarContraseña.Click += new System.EventHandler(this.cambiarClaveToolStripMenuItem_Click);
            // 
            // CerrarSesión
            // 
            this.CerrarSesión.Name = "CerrarSesión";
            this.CerrarSesión.Size = new System.Drawing.Size(198, 22);
            this.CerrarSesión.Text = "Cerrar Sesión";
            this.CerrarSesión.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // CambiarIdioma
            // 
            this.CambiarIdioma.Name = "CambiarIdioma";
            this.CambiarIdioma.Size = new System.Drawing.Size(198, 22);
            this.CambiarIdioma.Text = "Cambiar Idioma";
            this.CambiarIdioma.Click += new System.EventHandler(this.cambiarIdiomaToolStripMenuItem456VG_Click);
            // 
            // MenuRecepción
            // 
            this.MenuRecepción.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CrearEnvío});
            this.MenuRecepción.Name = "MenuRecepción";
            this.MenuRecepción.Size = new System.Drawing.Size(112, 22);
            this.MenuRecepción.Text = "Recepción";
            this.MenuRecepción.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CrearEnvío
            // 
            this.CrearEnvío.Name = "CrearEnvío";
            this.CrearEnvío.Size = new System.Drawing.Size(144, 22);
            this.CrearEnvío.Text = "Crear Envío";
            this.CrearEnvío.Click += new System.EventHandler(this.paquetesToolStripMenuItem456VG_Click);
            // 
            // MenuEnvíos
            // 
            this.MenuEnvíos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CobrarEnvío});
            this.MenuEnvíos.Name = "MenuEnvíos";
            this.MenuEnvíos.Size = new System.Drawing.Size(112, 22);
            this.MenuEnvíos.Text = "Envíos";
            this.MenuEnvíos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MenuEnvíos.Click += new System.EventHandler(this.envíosToolStripMenuItem456VG_Click);
            // 
            // CobrarEnvío
            // 
            this.CobrarEnvío.Name = "CobrarEnvío";
            this.CobrarEnvío.Size = new System.Drawing.Size(153, 22);
            this.CobrarEnvío.Text = "Cobrar Envío";
            this.CobrarEnvío.Click += new System.EventHandler(this.cobrarEnvíoToolStripMenuItem_Click);
            // 
            // MenuReportes
            // 
            this.MenuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FacturasIMP,
            this.SeguimientoEnvíos});
            this.MenuReportes.Name = "MenuReportes";
            this.MenuReportes.Size = new System.Drawing.Size(112, 22);
            this.MenuReportes.Text = "Reportes";
            this.MenuReportes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FacturasIMP
            // 
            this.FacturasIMP.Name = "FacturasIMP";
            this.FacturasIMP.Size = new System.Drawing.Size(193, 22);
            this.FacturasIMP.Text = "Facturas";
            this.FacturasIMP.Click += new System.EventHandler(this.facturasIMPToolStripMenuItem456VG_Click);
            // 
            // SeguimientoEnvíos
            // 
            this.SeguimientoEnvíos.Name = "SeguimientoEnvíos";
            this.SeguimientoEnvíos.Size = new System.Drawing.Size(193, 22);
            this.SeguimientoEnvíos.Text = "Seguimiento Envíos";
            this.SeguimientoEnvíos.Click += new System.EventHandler(this.seguimientoEnvíosToolStripMenuItem_Click);
            // 
            // MenuAyuda
            // 
            this.MenuAyuda.Name = "MenuAyuda";
            this.MenuAyuda.Size = new System.Drawing.Size(112, 22);
            this.MenuAyuda.Text = "Ayuda";
            this.MenuAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MenuSalir
            // 
            this.MenuSalir.Name = "MenuSalir";
            this.MenuSalir.Size = new System.Drawing.Size(112, 22);
            this.MenuSalir.Text = "Salir";
            this.MenuSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MenuSalir.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
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
            this.iconPictureBox1456VG.IconSize = 153;
            this.iconPictureBox1456VG.Location = new System.Drawing.Point(341, 71);
            this.iconPictureBox1456VG.Name = "iconPictureBox1456VG";
            this.iconPictureBox1456VG.Size = new System.Drawing.Size(635, 153);
            this.iconPictureBox1456VG.TabIndex = 2;
            this.iconPictureBox1456VG.TabStop = false;
            // 
            // lblBienvenido456VG
            // 
            this.lblBienvenido456VG.AutoSize = true;
            this.lblBienvenido456VG.Font = new System.Drawing.Font("Open Sans SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenido456VG.Location = new System.Drawing.Point(139, 616);
            this.lblBienvenido456VG.Name = "lblBienvenido456VG";
            this.lblBienvenido456VG.Size = new System.Drawing.Size(99, 18);
            this.lblBienvenido456VG.TabIndex = 3;
            this.lblBienvenido456VG.Text = "¡Bienvenida/o!";
            this.lblBienvenido456VG.Click += new System.EventHandler(this.lblBienvenido_Click);
            // 
            // lblBienvenidoDefault
            // 
            this.lblBienvenidoDefault.AutoSize = true;
            this.lblBienvenidoDefault.Font = new System.Drawing.Font("Open Sans SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenidoDefault.Location = new System.Drawing.Point(139, 616);
            this.lblBienvenidoDefault.Name = "lblBienvenidoDefault";
            this.lblBienvenidoDefault.Size = new System.Drawing.Size(99, 18);
            this.lblBienvenidoDefault.TabIndex = 4;
            this.lblBienvenidoDefault.Text = "¡Bienvenida/o!";
            // 
            // MenuPrincipal_456VG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(1187, 652);
            this.Controls.Add(this.lblBienvenidoDefault);
            this.Controls.Add(this.lblBienvenido456VG);
            this.Controls.Add(this.iconPictureBox1456VG);
            this.Controls.Add(this.menuStrip1456VG);
            this.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1456VG;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MenuPrincipal_456VG";
            this.Text = "Envios YA";
            this.Load += new System.EventHandler(this.MenuPrincipal_456VG_Load);
            this.menuStrip1456VG.ResumeLayout(false);
            this.menuStrip1456VG.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1456VG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1456VG;
        private System.Windows.Forms.ToolStripMenuItem MenuAdministrador;
        private System.Windows.Forms.ToolStripMenuItem GestióndeUsuarios;
        private System.Windows.Forms.ToolStripMenuItem GestióndePerfiles;
        private System.Windows.Forms.ToolStripMenuItem MenuMaestro;
        private System.Windows.Forms.ToolStripMenuItem MenuUsuarios;
        private System.Windows.Forms.ToolStripMenuItem MenuRecepción;
        private System.Windows.Forms.ToolStripMenuItem CrearEnvío;
        private System.Windows.Forms.ToolStripMenuItem MenuEnvíos;
        private System.Windows.Forms.ToolStripMenuItem MenuReportes;
        private System.Windows.Forms.ToolStripMenuItem FacturasIMP;
        private System.Windows.Forms.ToolStripMenuItem MenuAyuda;
        private System.Windows.Forms.ToolStripMenuItem GestióndeClientes;
        private System.Windows.Forms.ToolStripMenuItem IniciarSesión;
        private System.Windows.Forms.ToolStripMenuItem CambiarContraseña;
        private System.Windows.Forms.ToolStripMenuItem CerrarSesión;
        private System.Windows.Forms.ToolStripMenuItem CambiarIdioma;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1456VG;
        private System.Windows.Forms.Label lblBienvenido456VG;
        private System.Windows.Forms.ToolStripMenuItem MenuSalir;
        private System.Windows.Forms.Label lblBienvenidoDefault;
        private System.Windows.Forms.ToolStripMenuItem SeguimientoEnvíos;
        private System.Windows.Forms.ToolStripMenuItem CobrarEnvío;
        private System.Windows.Forms.ToolStripMenuItem BitacoraEventos;
    }
}

