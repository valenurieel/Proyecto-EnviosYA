using _456VG_BE;
using _456VG_BLL;
using _456VG_DAL;
using _456VG_Servicios;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class MenuPrincipal_456VG : Form, IObserver_456VG
    {
        BLLUsuario_456VG bllUsuario = new BLLUsuario_456VG();
        public MenuPrincipal_456VG()
        {
            InitializeComponent();
            QuestPDF.Settings.License = LicenseType.Community;
            BasedeDatos_456VG db = new BasedeDatos_456VG();
            db.ConfigurarEntornoInicial456VG();
            //BLLDigitoVerificador_456VG blld = new BLLDigitoVerificador_456VG();
            //blld.ActualizarDV456VG();
            deshabilitados();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
            Lenguaje_456VG.ObtenerInstancia_456VG().IdiomaActual_456VG = "ES";
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
            bienvenido456VG();
        }
        public void deshabilitados()
        {
            foreach (ToolStripMenuItem item in menuStrip1456VG.Items)
            {
                formManager.DeshabilitarTodosLosMenus(item);
            }
            MenuUsuarios.Enabled = true;
            CerrarSesión.Enabled = true;
            IniciarSesión.Enabled = true;
            MenuAyuda.Enabled = true;
            MenuSalir.Enabled = true;
        }
        public void chau()
        {
            lblBienvenido456VG.Visible = false;
            lblBienvenidoDefault.Visible = true;
            lblBienvenidoDefault.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                        .ObtenerTexto_456VG("MenuPrincipal_456VG.lblBienvenidoDefault");
        }
        public void bienvenido456VG()
        {
            BEUsuario_456VG usuarioLogueado = SessionManager_456VG.ObtenerInstancia456VG().Usuario;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (usuarioLogueado != null)
            {
                lblBienvenidoDefault.Visible = false;
                string plantilla = lng.ObtenerTexto_456VG("MenuPrincipal_456VG.lblBienvenido456VG");
                lblBienvenido456VG.Text = string.Format(plantilla, usuarioLogueado.NombreUsuario456VG);
                lblBienvenido456VG.Visible = true;
            }
            else
            {
                lblBienvenido456VG.Visible = false;
                lblBienvenidoDefault.Text = lng.ObtenerTexto_456VG("MenuPrincipal_456VG.lblBienvenidoDefault");
                lblBienvenidoDefault.Visible = true;
            }
        }
        Forms_456VG formManager = new Forms_456VG();
        private void AplicarPermisos(object sender, EventArgs e)
        {
            List<Permiso_456VG> permisos = bllUsuario.obtenerPermisosUsuario456VG(
                SessionManager_456VG.Obtenerdatosuser456VG().DNI456VG
            );
            foreach (ToolStripMenuItem item in menuStrip1456VG.Items)
            {
                formManager.HabilitarMenusPorPermisos(item, permisos);
            }
            bienvenido456VG();
        }
        private void MenuPrincipal_456VG_Load(object sender, EventArgs e)
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
            bienvenido456VG();
        }
        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmLogin = new IniciarSesion_456VG();
            frmLogin.LoginExitoso += AplicarPermisos;
            frmLogin.ShowDialog();
        }
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestióndeUsuarios_456VG fRM = new GestióndeUsuarios_456VG();
            fRM.ShowDialog();
        }
        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarSesión_456VG fRM = new CerrarSesión_456VG();
            fRM.ShowDialog();
        }
        private void cambiarClaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarContraseña_456VG fr = new CambiarContraseña_456VG();
            fr.ShowDialog();
        }
        private void lblBienvenido_Click(object sender, EventArgs e)
        {
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void paquetesToolStripMenuItem456VG_Click(object sender, EventArgs e)
        {
            CrearEnvío_456VG fr = new CrearEnvío_456VG();
            fr.ShowDialog();
        }
        private void clientesToolStripMenuItem456VG_Click(object sender, EventArgs e)
        {
            GestiondeClientes_456VG fr = new GestiondeClientes_456VG();
            fr.ShowDialog();
        }
        private void cambiarIdiomaToolStripMenuItem456VG_Click(object sender, EventArgs e)
        {
            CambiarIdioma_456VG fr = new CambiarIdioma_456VG();
            fr.ShowDialog();
        }
        private void perfilesToolStripMenuItem456VG_Click(object sender, EventArgs e)
        {
            Perfiles_456VG perfiles_456VG = new Perfiles_456VG();
            perfiles_456VG.ShowDialog();
        }
        private void facturasIMPToolStripMenuItem456VG_Click(object sender, EventArgs e)
        {
            FacturasIMP_456VG fact = new FacturasIMP_456VG();
            fact.ShowDialog();
        }
        private void envíosToolStripMenuItem456VG_Click(object sender, EventArgs e)
        {
        }
        private void cobrarEnvíoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CobrarEnvío_456VG fr = new CobrarEnvío_456VG();
            fr.ShowDialog();
        }
        private void menuStrip1456VG_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void bitácoraDeEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitácoraEventos_456VG fr = new BitácoraEventos_456VG();
            fr.ShowDialog();
        }
        private void backupRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupRestore_456VG fr = new BackupRestore_456VG();
            fr.ShowDialog();
        }
        private void AsignacióndeEnvío_Click(object sender, EventArgs e)
        {
            ListaCarga_456VG fr = new ListaCarga_456VG();
            fr.ShowDialog();
        }
        private void CargaEnvíos_Click(object sender, EventArgs e)
        {
            CargaEnvios_456VG rf = new CargaEnvios_456VG();
            rf.ShowDialog();
        }
        private void EntregadeEnvío_Click(object sender, EventArgs e)
        {
            EntregaEnvios_456VG fr = new EntregaEnvios_456VG();
            fr.ShowDialog();
        }
        private void GestióndeChoferes_Click(object sender, EventArgs e)
        {
            GestiondeChoferes_456VG fr = new GestiondeChoferes_456VG();
            fr.ShowDialog();
        }
        private void GestióndeTransportes_Click(object sender, EventArgs e)
        {
            GestiondeTransporte_456VG fr = new GestiondeTransporte_456VG();
            fr.ShowDialog();
        }
        private void bitácoraDeCambiosChoferesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitácoraCambios_456VG fr = new BitácoraCambios_456VG();
            fr.ShowDialog();
        }

        private void ReporteInteligente_Click(object sender, EventArgs e)
        {
            ReporteInteligente_456VG fr = new ReporteInteligente_456VG();
            fr.ShowDialog();
        }
    }
}
