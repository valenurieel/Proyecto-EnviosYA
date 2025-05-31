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
        BLLCliente_456VG BLLCliente = new BLLCliente_456VG();
        private BEPaquete_456VG paqueteCargado;
        private BECliente_456VG clienteCargado;
        public CrearEnvío_456VG()
        {
            InitializeComponent();
        }
        private void iconPictureBox1456VG_Click(object sender, EventArgs e)
        {

        }
        private void limpiar()
        {
            txtAlto456VG.Clear();
            txtAncho456VG.Clear();
            txtCP456VG.Clear();
            txtDNID456VG.Clear();
            txtDom456VG.Clear();
            txtLargo456VG.Clear();
            txtLoc456VG.Clear();
            txtNomD456VG.Clear();
            txtApeCli456VG.Clear();
            txtNomCli456VG.Clear();
            txtPeso456VG.Clear();
            txtProv456VG.Clear();
            txtTelD456VG.Clear();
            txtTelCli456VG.Text = "";
            txtApeD456VG.Text = "";
            txtDNICli456VG.Text = "";
            cmbTipEnvio456VG.SelectedIndex = -1;
        }
        private void btnCrearEnvío456VG_Click(object sender, EventArgs e)
        {
            if (paqueteCargado == null)
            {
                MessageBox.Show("Debe Cargar un Paquete.", "Paquete NO EXISTENTE",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var envio = new BEEnvío_456VG(
                paqueteCargado.id_paquete456VG,
                clienteCargado.DNI456VG,
                txtDNID456VG.Text.Trim(),
                txtNomD456VG.Text.Trim(),
                txtApeD456VG.Text.Trim(),
                txtTelD456VG.Text.Trim(),
                Convert.ToSingle(txtCP456VG.Text),
                txtDom456VG.Text.Trim(),
                txtLoc456VG.Text.Trim(),
                txtProv456VG.Text.Trim(),
                cmbTipEnvio456VG.SelectedItem?.ToString() ?? "normal",
                0m,
                false
            );
            envio.Paquete = paqueteCargado;
            envio.Cliente = clienteCargado;
            envio.Importe456VG = envio.CalcularImporte456VG();
            var resEnv = BLLEnv.crearEntidad456VG(envio);
            if (!resEnv.resultado)
            {
                MessageBox.Show($"Error al crear el Envío: {resEnv.mensaje}");
                return;
            }
            paqueteCargado.Enviado456VG = true;
            var upd = BLLPaque.actualizarEntidad456VG(paqueteCargado);
            MessageBox.Show("Envío creado exitosamente.");
            limpiar();
        }
        private void btnRegCli456VG_Click(object sender, EventArgs e)
        {
            if (clienteCargado == null)
            {
                MessageBox.Show(
                    "Debe ser Cliente para cargar un Paquete.",
                    "Cliente NO EXISTENTE",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            paqueteCargado = new BEPaquete_456VG(
                clienteCargado.DNI456VG,
                Convert.ToSingle(txtPeso456VG.Text),
                Convert.ToSingle(txtAncho456VG.Text),
                Convert.ToSingle(txtLargo456VG.Text),
                Convert.ToSingle(txtAlto456VG.Text),
                false
            );
            paqueteCargado.Cliente = clienteCargado;
            var resp = BLLPaque.crearEntidad456VG(paqueteCargado);
            if (!resp.resultado)
                MessageBox.Show($"Error al cargar el paquete: {resp.mensaje}");
            else
                MessageBox.Show("Paquete cargado exitosamente.");
        }
        private void txtDNICli456VG_Leave(object sender, EventArgs e)
        {
            string dni = txtDNICli456VG.Text.Trim();
            if (string.IsNullOrEmpty(dni)) return;

            var resCli = BLLCliente.ObtenerClientePorDNI456VG(dni);
            if (resCli.resultado && resCli.entidad != null)
            {
                clienteCargado = resCli.entidad;
                txtNomCli456VG.Text = clienteCargado.Nombre456VG;
                txtApeCli456VG.Text = clienteCargado.Apellido456VG;
                txtTelCli456VG.Text = clienteCargado.Teléfono456VG;
            }
            else
            {
                clienteCargado = null;
                txtNomCli456VG.Text = "";
                txtApeCli456VG.Text = "";
                txtTelCli456VG.Text = "";
                MessageBox.Show(
                    "No se encontró un cliente con ese DNI.",
                    "Cliente NO ENCONTRADO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                RegistrarCliente_456VG regcli = new RegistrarCliente_456VG();
                regcli.ShowDialog();
            }
        }
        private void CrearEnvío_456VG_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDNICli456VG_TextChanged(object sender, EventArgs e)
        {

        }
    }
}