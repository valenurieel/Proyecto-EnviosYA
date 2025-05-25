using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
using System;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class CrearEnvío_456VG : Form
    {
        BLLEnvio_456VG BLLEnv = new BLLEnvio_456VG();
        BLLPaquete_456VG BLLPaque = new BLLPaquete_456VG();

        public CrearEnvío_456VG()
        {
            InitializeComponent();
        }

        private void iconPictureBox1456VG_Click(object sender, EventArgs e)
        {

        }

        private void btnCrearEnvío456VG_Click(object sender, EventArgs e)
        {
                string dni = txtDNID456VG.Text;
                string name = txtNomD456VG.Text;
                string ape = txtApeD456VG.Text;
                string telef = txtTelD456VG.Text;
                float codpostal = Convert.ToInt32(txtCP456VG.Text);
                string domicilio = txtDom456VG.Text;
                string localidad = txtLoc456VG.Text;
                string prov = txtProv456VG.Text;
                string dnicli = txtDNICli456VG.Text;
                string namecli = txtNomCli456VG.Text;
                string apecli = txtApeCli456VG.Text;
                string telcli = txtTelCli456VG.Text;
                float peso = Convert.ToSingle(txtPeso456VG.Text);
                float ancho = Convert.ToSingle(txtAncho456VG.Text);
                float alto = Convert.ToSingle(txtAlto456VG.Text);
                float largo = Convert.ToSingle(txtLargo456VG.Text);
                string tipoenvio = cmbTipEnvio456VG.SelectedItem?.ToString();
                bool pagado = false;
                BEEnvío_456VG envio = new BEEnvío_456VG(
                    dnicli, namecli, apecli, telcli,
                    dni, name, ape, telef,
                    codpostal, domicilio, localidad, prov,
                    peso, ancho, alto, largo,
                    tipoenvio, 0f, pagado
                );
                envio.Importe456VG = envio.CalcularImporte();
                Resultado_456VG<BEEnvío_456VG> resultado = BLLEnv.crearEntidad456VG(envio);
                if (resultado.resultado)
                {
                    MessageBox.Show("Envío registrado correctamente.");
                }
                else
                {
                    MessageBox.Show($"Error al registrar el envío: {resultado.mensaje}");
                }
        }
        private void btnRegCli456VG_Click(object sender, EventArgs e)
        {
            RegistrarCliente_456VG fr = new RegistrarCliente_456VG();
            fr.Show();
        }
    }
}
