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
            // Nos suscribimos para que, al cambiar de idioma, se actualicen los controles
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }

        public void ActualizarIdioma_456VG()
        {
            // Traduce todos los controles visibles (labels, botones, etc.)
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            // Usaremos el título de la ventana traducido:
            string titulo = lng.ObtenerTexto_456VG("CerrarSesión_456VG.Text");

            if (SessionManager_456VG.ObtenerInstancia456VG().Usuario != null)
            {
                // 1) Cerrar sesión en SessionManager
                SessionManager_456VG.ObtenerInstancia456VG().CerrarSesion456VG();

                // 2) Mostrar mensaje de “Sesión cerrada” en el idioma activo
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CerrarSesión_456VG.Msg.SesionCerrada"),
                    titulo,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // 3) Deshabilitar opciones del menú principal, si existe
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
                // No había sesión iniciada: mostrar advertencia en el idioma activo
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CerrarSesión_456VG.Msg.NoSesion"),
                    titulo,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            // Llamar al método chau() del menú principal para restablecer el lblBienvenido
            InvocarChauEnMenu();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CerrarSesión_456VG_Load(object sender, EventArgs e)
        {
            // Cuando se abra el formulario, traducir todos los controles
            ActualizarIdioma_456VG();
        }

        private void InvocarChauEnMenu()
        {
            // Si el MenuPrincipal_456VG está abierto, llamamos a su chau()
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
