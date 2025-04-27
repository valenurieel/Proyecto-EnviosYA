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
            bd.scriptInicio();
        }
        public void chau()
        {
            if (SessionManager_456VG.ObtenerInstancia().Usuario == null)
            {
                lblBienvenido.Text = "¡Bienvenido!";
            }
        }
        public void bienvenido()
        {
            BEUsuario_456VG usuarioLogueado = SessionManager_456VG.ObtenerInstancia().Usuario;
            if (usuarioLogueado != null)
            {
                lblBienvenido.Text = "¡Bienvenido " + usuarioLogueado.Nombre + " " + usuarioLogueado.Apellido + "!";
            }
        }
        private void MenuPrincipal_456VG_Load(object sender, EventArgs e)
        {
            bienvenido();
        }

        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AbrirForm(new IniciarSesion_456VG());
            IniciarSesion_456VG fRM = new IniciarSesion_456VG();
            fRM.ShowDialog();
            //this.Hide();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistrarUsuario_456VG fRM = new RegistrarUsuario_456VG();
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
    }
}
