using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
using System;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class CrearEnvío_456VG : Form, IObserver_456VG
    {
        BLLEnvio_456VG BLLEnv = new BLLEnvio_456VG();
        BLLPaquete_456VG BLLPaque = new BLLPaquete_456VG();
        BLLCliente_456VG BLLCliente = new BLLCliente_456VG();

        private BEPaquete_456VG paqueteCargado;
        private BECliente_456VG clienteCargado;

        public CrearEnvío_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }

        public void ActualizarIdioma_456VG()
        {
            // Traduce todos los controles visibles (labels, botones, combo, etc.)
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);

            // Traducir manualmente ítems del ComboBox “Tipo de Envío”
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            cmbTipEnvio456VG.Items.Clear();
            cmbTipEnvio456VG.Items.Add(lng.ObtenerTexto_456VG("CrearEnvío_456VG.Combo.normal"));
            cmbTipEnvio456VG.Items.Add(lng.ObtenerTexto_456VG("CrearEnvío_456VG.Combo.express"));
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
            txtApeD456VG.Clear();
            txtNomCli456VG.Clear();
            txtApeCli456VG.Clear();
            txtTelD456VG.Clear();
            txtTelCli456VG.Clear();
            txtDNICli456VG.Clear();
            txtPeso456VG.Clear();
            txtProv456VG.Clear();
            cmbTipEnvio456VG.SelectedIndex = -1;
        }

        private void btnCrearEnvío456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            // 1) Verificar que se haya cargado un paquete
            if (paqueteCargado == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.PaqueteNoExistente"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.PaqueteNoExistenteTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 2) Construir objeto BEEnvío_456VG
            var envio = new BEEnvío_456VG(
                paqueteCargado.id_paquete456VG,
                clienteCargado.DNI456VG,
                txtDNID456VG.Text.Trim(),
                txtNomD456VG.Text.Trim(),
                txtApeD456VG.Text.Trim(),
                txtTelD456VG.Text.Trim(),
                float.TryParse(txtCP456VG.Text, out float cp) ? cp : 0f,
                txtDom456VG.Text.Trim(),
                txtLoc456VG.Text.Trim(),
                txtProv456VG.Text.Trim(),
                cmbTipEnvio456VG.SelectedItem?.ToString()
                    ?? lng.ObtenerTexto_456VG("CrearEnvío_456VG.Combo.normal"),
                0m,
                false
            )
            {
                Paquete = paqueteCargado,
                Cliente = clienteCargado
            };

            envio.Importe456VG = envio.CalcularImporte456VG();

            // 3) Intentar crear el envío
            var resEnv = BLLEnv.crearEntidad456VG(envio);
            if (!resEnv.resultado)
            {
                MessageBox.Show(
                    string.Format(
                        lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ErrorCrearEnvio"),
                        resEnv.mensaje
                    ),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // 4) Marcar paquete como enviado
            paqueteCargado.Enviado456VG = true;
            BLLPaque.actualizarEntidad456VG(paqueteCargado);

            // 5) Mostrar mensaje de éxito y limpiar campos
            MessageBox.Show(
                lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.EnvioCreadoOK"),
                lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            limpiar();
        }

        private void btnRegCli456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            // 1) Verificar que exista un cliente cargado
            if (clienteCargado == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ClienteNoExistente"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ClienteNoExistenteTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 2) Crear objeto BEPaquete_456VG con datos del cliente y dimensiones
            if (!float.TryParse(txtPeso456VG.Text, out float peso)) peso = 0f;
            if (!float.TryParse(txtAncho456VG.Text, out float ancho)) ancho = 0f;
            if (!float.TryParse(txtLargo456VG.Text, out float largo)) largo = 0f;
            if (!float.TryParse(txtAlto456VG.Text, out float alto)) alto = 0f;

            paqueteCargado = new BEPaquete_456VG(
                clienteCargado.DNI456VG,
                peso,
                ancho,
                largo,
                alto,
                false
            )
            {
                Cliente = clienteCargado
            };

            // 3) Intentar guardar el paquete
            var resp = BLLPaque.crearEntidad456VG(paqueteCargado);
            if (!resp.resultado)
            {
                MessageBox.Show(
                    string.Format(
                        lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ErrorCargarPaquete"),
                        resp.mensaje
                    ),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.PaqueteCargadoOK"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void txtDNICli456VG_Leave(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
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
                txtNomCli456VG.Clear();
                txtApeCli456VG.Clear();
                txtTelCli456VG.Clear();

                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ClienteNoEncontrado"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ClienteNoEncontradoTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Abrir formulario de registro de cliente
                RegistrarCliente_456VG regcli = new RegistrarCliente_456VG();
                regcli.ShowDialog();
            }
        }

        private void CrearEnvío_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // “Volver” / “Back”
            this.Close();
        }

        // Este evento no se utiliza, pero debe existir si está en el diseñador
        private void txtDNICli456VG_TextChanged(object sender, EventArgs e) { }
    }
}
