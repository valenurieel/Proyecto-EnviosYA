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
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            box456VG.Items.Clear();
            box456VG.Items.Add(lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Item.Español"));
            box456VG.Items.Add(lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Item.Inglés"));
            string claveEsp = lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Item.Español");
            string claveIng = lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Item.Inglés");
            box456VG.Text = (lng.IdiomaActual_456VG == "ES") ? claveEsp : claveIng;
        }
        private void CambiarIdioma_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
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
            string seleccion = box456VG.Text;
            string txtEspañol = lng.ObtenerTexto_456VG("CambiarIdioma_456VG.Item.Español");
            string nuevoIdioma = (seleccion == txtEspañol) ? "ES" : "EN";
            Lenguaje_456VG.ObtenerInstancia_456VG().IdiomaActual_456VG = nuevoIdioma;
            if (SessionManager_456VG.verificarInicioSesion456VG())
            {
                SessionManager_456VG.IdiomaTemporal_456VG = nuevoIdioma;
            }
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
