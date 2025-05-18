using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _456VG_BE;
using _456VG_DAL;
using _456VG_Servicios;

namespace Proyecto_EnviosYA
{
    public partial class MenuPrincipal_456VG : Form
    {
        public MenuPrincipal_456VG()
        {
            InitializeComponent();
            BasedeDatos_456VG bd = new BasedeDatos_456VG();
            bd.scriptInicio456VG();
            deshabilitados();
        }
        public void HabilitarOpcionesMenu456VG()
        {
            cambiarIdiomaToolStripMenuItem456VG.Enabled = true;
            cambiarClaveToolStripMenuItem456VG.Enabled = true;
        }
        public void deshabilitados()
        {
            perfilesToolStripMenuItem456VG.Enabled = false;
            maestroToolStripMenuItem456VG.Enabled = false;
            cambiarIdiomaToolStripMenuItem456VG.Enabled = false;
            recepciónToolStripMenuItem.Enabled = false;
            envíosToolStripMenuItem456VG.Enabled = false;
            reportesToolStripMenuItem456VG.Enabled = false;
            cambiarClaveToolStripMenuItem456VG.Enabled = false;
        }
        public void chau()
        {
            if (SessionManager_456VG.ObtenerInstancia456VG().Usuario == null)
            {
                lblBienvenido456VG.Text = "¡Bienvenida/o!";
            }
        }
        public void bienvenido456VG()
        {
            BEUsuario_456VG usuarioLogueado = SessionManager_456VG.ObtenerInstancia456VG().Usuario;
            if (usuarioLogueado != null)
            {
                lblBienvenido456VG.Text = "¡Bienvenida/o " + usuarioLogueado.NombreUsuario456VG + "!";
            }
        }
        private void MenuPrincipal_456VG_Load(object sender, EventArgs e)
        {
            bienvenido456VG();
        }
        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IniciarSesion_456VG fRM = new IniciarSesion_456VG();
            fRM.ShowDialog();
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
        private void crearEnvíoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CobrarEnvío_456VG fr = new CobrarEnvío_456VG();
            fr.ShowDialog();
        }
    }
}
