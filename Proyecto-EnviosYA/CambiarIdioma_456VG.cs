using System;
using System.Linq;
using System.Windows.Forms;
using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;

namespace Proyecto_EnviosYA
{
    public partial class CambiarIdioma_456VG : Form, IObserver_456VG
    {
        public CambiarIdioma_456VG()
        {
            InitializeComponent();
            // Nos suscribimos para que, cuando cambie el idioma, se actualicen controles
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }

        public void ActualizarIdioma_456VG()
        {
            // 1) Traduce todos los controles visibles (labels, botones, CheckBox, RadioButton...)
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);

            // 2) Poblar (o refrescar) los ítems del ComboBox con los valores traducidos
            //    Esto debe ocurrir *después* de traducir los controles, para que box456VG.Name+".Text" exista.
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            box456VG.Items.Clear();
            box456VG.Items.Add(lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Item.Español"));
            box456VG.Items.Add(lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Item.Inglés"));

            // 3) Opcionalmente, preseleccionamos el ítem según el idioma actual
            //    Si estoy en ES, selecciono “Español”; si en EN, selecciono “English”
            string claveEsp = lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Item.Español");
            string claveIng = lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Item.Inglés");
            box456VG.Text = (lng.IdiomaActual_456VG == "ES") ? claveEsp : claveIng;
        }

        private void CambiarIdioma_456VG_Load(object sender, EventArgs e)
        {
            // Al cargar, aplico idioma y lleno el ComboBox
            ActualizarIdioma_456VG();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            // 1) Validar que se haya seleccionado algo en el ComboBox
            if (string.IsNullOrWhiteSpace(box456VG.Text))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Msg.CompleteCampo"),
                    lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Msg.WarningTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 2) Determinar el código de idioma según la selección traducida
            string seleccion = box456VG.Text;
            string txtEspañol = lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Item.Español"); // podría ser “Español” o “Spanish”
            // Si la selección coincide con la cadena traducida “Español” en este idioma, guardo “ES”; de lo contrario “EN”
            string nuevoIdioma = (seleccion == txtEspañol) ? "ES" : "EN";

            // 3) Cambio el idioma globalmente (desencadena Notificar_456VG en todos los forms suscritos)
            Lenguaje_456VG.ObtenerInstancia_456VG().IdiomaActual_456VG = nuevoIdioma;

            // 4) Si ya hay sesión iniciada, guardo el idioma en la base de datos
            if (SessionManager_456VG.verificarInicioSesion456VG())
            {
                BEUsuario_456VG usuarioActual = SessionManager_456VG.Obtenerdatosuser456VG();
                BLLUsuario_456VG bllUsuario = new BLLUsuario_456VG();
                bllUsuario.modificarIdioma456VG(usuarioActual, nuevoIdioma);
            }

            // 5) Muestro mensaje de éxito (también extraído del JSON)
            MessageBox.Show(
                string.Format(
                    lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Msg.IdiomaActualizado"),
                    box456VG.Text
                ),
                lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Msg.IdiomaActualizadoTitle"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void button2456VG_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
