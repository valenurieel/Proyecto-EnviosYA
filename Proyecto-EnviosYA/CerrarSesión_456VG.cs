using System;
using System.Linq;
using System.Windows.Forms;
using _456VG_Servicios;

namespace Proyecto_EnviosYA
{
    public partial class CerrarSesión_456VG : Form, IObserver_456VG
    {
        public CerrarSesión_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string titulo = lng.ObtenerTexto_456VG("CerrarSesión_456VG.Text");
            if (SessionManager_456VG.ObtenerInstancia456VG().Usuario != null)
            {
                SessionManager_456VG.ObtenerInstancia456VG().CerrarSesion456VG();
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CerrarSesión_456VG.Msg.SesionCerrada"),
                    titulo,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                MenuPrincipal_456VG menu = Application.OpenForms
                                              .OfType<MenuPrincipal_456VG>()
                                              .FirstOrDefault();
                if (menu != null)
                {
                    menu.deshabilitados();
                }
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CerrarSesión_456VG.Msg.NoSesion"),
                    titulo,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            InvocarChauEnMenu();
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CerrarSesión_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
        }
        private void InvocarChauEnMenu()
        {
            MenuPrincipal_456VG menu = Application.OpenForms
                                          .OfType<MenuPrincipal_456VG>()
                                          .FirstOrDefault();
            if (menu != null)
            {
                menu.chau();
            }
        }
    }
}
