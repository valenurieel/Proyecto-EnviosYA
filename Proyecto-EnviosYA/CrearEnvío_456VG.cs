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
        public CrearEnvío_456VG()
        {
            InitializeComponent();
        }

        private void iconPictureBox1456VG_Click(object sender, EventArgs e)
        {

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
                paqueteCargado.dnicliente456VG,
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
            envio.Cliente = paqueteCargado.Cliente;
            envio.Importe456VG = envio.CalcularImporte();
            var resEnv = BLLEnv.crearEntidad456VG(envio);
            if (!resEnv.resultado)
            {
                MessageBox.Show($"Error al crear el envío: {resEnv.mensaje}");
                return;
            }
            paqueteCargado.Enviado456VG = true;
            var upd = BLLPaque.actualizarEntidad456VG(paqueteCargado);
            MessageBox.Show("Envío creado exitosamente.");
        }
        private void btnRegCli456VG_Click(object sender, EventArgs e)
        {
            string dniCli = txtDNICli456VG.Text.Trim();
            var resCli = BLLCliente.ObtenerClientePorDNI456VG(dniCli);
            if (!resCli.resultado)
            {
                MessageBox.Show("Debe ser Cliente para cargar un Paquete.", "Cliente NO EXISTENTE",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            paqueteCargado = new BEPaquete_456VG(
                dniCli,
                Convert.ToSingle(txtPeso456VG.Text),
                Convert.ToSingle(txtAncho456VG.Text),
                Convert.ToSingle(txtLargo456VG.Text),
                Convert.ToSingle(txtAlto456VG.Text),
                false
            );
            paqueteCargado.Cliente = resCli.entidad;
            var resp = BLLPaque.crearEntidad456VG(paqueteCargado);
            if (!resp.resultado)
                MessageBox.Show($"Error al cargar el paquete: {resp.mensaje}");
            else
                MessageBox.Show($"Paquete cargado exitosamente.");
        }
        private void txtDNICli456VG_Leave(object sender, EventArgs e)
        {
            string dni = txtDNICli456VG.Text.Trim();
            if (string.IsNullOrEmpty(dni))
                return;
            var resultado = BLLCliente.ObtenerClientePorDNI456VG(dni);
            if (resultado.resultado && resultado.entidad != null)
            {
                txtNomCli456VG.Text = resultado.entidad.Nombre456VG;
                txtApeCli456VG.Text = resultado.entidad.Apellido456VG;
                txtTelCli456VG.Text = resultado.entidad.Teléfono456VG;
            }
            else
            {
                txtNomCli456VG.Clear();
                txtApeCli456VG.Clear();
                txtTelCli456VG.Clear();
                MessageBox.Show("No se encontró un cliente con ese DNI.", "Cliente NO ENCONTRADO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}