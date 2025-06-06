using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
using System;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class RegistrarCliente_456VG : Form, IObserver_456VG
    {
        BLLCliente_456VG BLLCliente = new BLLCliente_456VG();
        public RegistrarCliente_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
        }
        private void btnRegCli456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string dniCli = txtDNICli456VG.Text.Trim();
            string nomCli = txtNomCli456VG.Text.Trim();
            string apeCli = txtApeCli456VG.Text.Trim();
            string telCli = txtTelCli456VG.Text.Trim();
            string domCli = txtDomiCli456VG.Text.Trim();
            DateTime fechaNacCli = dateTimePicker1456VG.Value;
            if (string.IsNullOrWhiteSpace(dniCli) ||
                string.IsNullOrWhiteSpace(nomCli) ||
                string.IsNullOrWhiteSpace(apeCli) ||
                string.IsNullOrWhiteSpace(telCli) ||
                string.IsNullOrWhiteSpace(domCli))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("RegistrarCliente_456VG.Msg.CamposObligatorios"),
                    lng.ObtenerTexto_456VG("RegistrarCliente_456VG.Msg.ErrorRegistroTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var existe = BLLCliente.ObtenerClientePorDNI456VG(dniCli);
            if (existe.resultado && existe.entidad != null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("RegistrarCliente_456VG.Msg.ClienteExiste"),
                    lng.ObtenerTexto_456VG("RegistrarCliente_456VG.Msg.ErrorRegistroTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            BECliente_456VG cliente = new BECliente_456VG(
                dniCli,
                nomCli,
                apeCli,
                telCli,
                domCli,
                fechaNacCli,
                true
            );
            var resp = BLLCliente.crearEntidad456VG(cliente);
            if (resp.resultado)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("RegistrarCliente_456VG.Msg.RegistroExitoso"),
                    lng.ObtenerTexto_456VG("RegistrarCliente_456VG.Msg.RegistroExitosoTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    string.Format(
                        lng.ObtenerTexto_456VG("RegistrarCliente_456VG.Msg.ErrorRegistro"),
                        resp.mensaje
                    ),
                    lng.ObtenerTexto_456VG("RegistrarCliente_456VG.Msg.ErrorRegistroTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void RegistrarCliente_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
