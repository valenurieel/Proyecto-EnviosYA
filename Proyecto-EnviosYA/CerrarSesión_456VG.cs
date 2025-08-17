using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;

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
                var bllUsuario = new BLLUsuario_456VG();
                var usuarioActual = SessionManager_456VG.Obtenerdatosuser456VG();
                var idiomaFinal = SessionManager_456VG.IdiomaTemporal_456VG;
                bllUsuario.modificarIdioma456VG(usuarioActual, idiomaFinal);
                BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
                string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                blleven.AddBitacora456VG(dni: dniLog, modulo: "Usuario", accion: "Cerrar Sesión", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Crítico);
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
                Lenguaje_456VG.ObtenerInstancia_456VG().IdiomaActual_456VG = "ES";
                SessionManager_456VG.IdiomaTemporal_456VG = "ES";
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
