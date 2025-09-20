using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class CrearEnvío_456VG : Form, IObserver_456VG
    {
        private BLLEnvio_456VG BLLEnv = new BLLEnvio_456VG();
        private BLLPaquete_456VG BLLPaque = new BLLPaquete_456VG();
        private BLLCliente_456VG BLLCliente = new BLLCliente_456VG();
        private List<BEPaquete_456VG> _paquetesPendientes = new List<BEPaquete_456VG>();
        private BECliente_456VG clienteCargado;
        public CrearEnvío_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
            ConfigurarDataGridViewPaquetes();
        }
        public void ActualizarIdioma_456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            lng.CambiarIdiomaControles_456VG(this);
            cmbTipEnvio456VG.Items.Clear();
            cmbTipEnvio456VG.Items.Add(lng.ObtenerTexto_456VG("CrearEnvío_456VG.Combo.normal"));
            cmbTipEnvio456VG.Items.Add(lng.ObtenerTexto_456VG("CrearEnvío_456VG.Combo.express"));
            TraducirEncabezadosDataGridPaquetes();
        }
        private void ConfigurarDataGridViewPaquetes()
        {
            dgvPaquetes.AutoGenerateColumns = false;
            dgvPaquetes.Columns.Clear();
            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodPaq",
                HeaderText = "Código Paquete",
                DataPropertyName = "CodPaq456VG",
                ReadOnly = true,
                Width = 120
            });
            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Peso",
                HeaderText = "Peso (kg)",
                DataPropertyName = "Peso456VG",
                ReadOnly = true,
                Width = 80
            });
            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Ancho",
                HeaderText = "Ancho (cm)",
                DataPropertyName = "Ancho456VG",
                ReadOnly = true,
                Width = 80
            });
            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Largo",
                HeaderText = "Largo (cm)",
                DataPropertyName = "Largo456VG",
                ReadOnly = true,
                Width = 80
            });
            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Alto",
                HeaderText = "Alto (cm)",
                DataPropertyName = "Alto456VG",
                ReadOnly = true,
                Width = 80
            });
            dgvPaquetes.DataSource = new BindingSource { DataSource = _paquetesPendientes };
        }
        private void TraducirEncabezadosDataGridPaquetes()
        {
            if (dgvPaquetes.Columns.Count == 0) return;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            dgvPaquetes.Columns["CodPaq"].HeaderText = lng.ObtenerTexto_456VG("CrearEnvío_456VG.Columna.CodPaq");
            dgvPaquetes.Columns["Peso"].HeaderText = lng.ObtenerTexto_456VG("CrearEnvío_456VG.Columna.Peso");
            dgvPaquetes.Columns["Ancho"].HeaderText = lng.ObtenerTexto_456VG("CrearEnvío_456VG.Columna.Ancho");
            dgvPaquetes.Columns["Largo"].HeaderText = lng.ObtenerTexto_456VG("CrearEnvío_456VG.Columna.Largo");
            dgvPaquetes.Columns["Alto"].HeaderText = lng.ObtenerTexto_456VG("CrearEnvío_456VG.Columna.Alto");
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
            _paquetesPendientes.Clear();
            ((BindingSource)dgvPaquetes.DataSource).ResetBindings(false);
        }
        private void btnRegCli456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
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
            if (string.IsNullOrWhiteSpace(txtPeso456VG.Text) ||
                string.IsNullOrWhiteSpace(txtAncho456VG.Text) ||
                string.IsNullOrWhiteSpace(txtLargo456VG.Text) ||
                string.IsNullOrWhiteSpace(txtAlto456VG.Text))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.FaltanDatosPaquete"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.DatosPaqueteTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (!float.TryParse(txtPeso456VG.Text.Trim(), out float peso) || peso <= 0f)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.PesoInvalido"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ValidacionTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtPeso456VG.Focus();
                return;
            }
            if (!float.TryParse(txtAncho456VG.Text.Trim(), out float ancho) || ancho <= 0f)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.AnchoInvalido"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ValidacionTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtAncho456VG.Focus();
                return;
            }
            if (!float.TryParse(txtLargo456VG.Text.Trim(), out float largo) || largo <= 0f)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.LargoInvalido"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ValidacionTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtLargo456VG.Focus();
                return;
            }
            if (!float.TryParse(txtAlto456VG.Text.Trim(), out float alto) || alto <= 0f)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.AltoInvalido"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ValidacionTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtAlto456VG.Focus();
                return;
            }
            bool enviadoPaq = false;
            var nuevoPaquete = new BEPaquete_456VG(
                clienteCargado,
                peso,
                ancho,
                largo,
                alto,
                enviadoPaq
            );
            var resp = BLLPaque.crearEntidad456VG(nuevoPaquete);
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
                return;
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
            BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
            string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
            blleven.AddBitacora456VG(dni: dniLog, modulo: "Recepción", accion: "Cargar Paquete", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Peligro);
            _paquetesPendientes.Add(nuevoPaquete);
            ((BindingSource)dgvPaquetes.DataSource).ResetBindings(false);
            txtPeso456VG.Clear();
            txtAncho456VG.Clear();
            txtLargo456VG.Clear();
            txtAlto456VG.Clear();
            txtPeso456VG.Focus();
        }
        private void btnCrearEnvío456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (_paquetesPendientes == null || _paquetesPendientes.Count == 0)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.PaqueteNoExistente"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.PaqueteNoExistenteTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDNID456VG.Text) ||
                string.IsNullOrWhiteSpace(txtNomD456VG.Text) ||
                string.IsNullOrWhiteSpace(txtApeD456VG.Text) ||
                string.IsNullOrWhiteSpace(txtTelD456VG.Text) ||
                string.IsNullOrWhiteSpace(txtCP456VG.Text) ||
                string.IsNullOrWhiteSpace(txtDom456VG.Text) ||
                string.IsNullOrWhiteSpace(txtLoc456VG.Text) ||
                string.IsNullOrWhiteSpace(txtProv456VG.Text) ||
                cmbTipEnvio456VG.SelectedItem == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.FaltanDatosEnvio"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (!float.TryParse(txtCP456VG.Text, out float codPostal))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.CPInvalido"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            if (txtDNID456VG.Text.Length != 8 || !txtDNID456VG.Text.All(char.IsDigit))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.DNIDestinatarioInvalido"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDNID456VG.Focus();
                return;
            }
            if (DNIEstaRegistradoEnSistema(txtDNID456VG.Text.Trim()))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.DNIDestinatarioRepetido"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtDNID456VG.Focus();
                return;
            }
            if (txtCP456VG.Text.Length != 4 || !txtCP456VG.Text.All(char.IsDigit))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.CPInvalido"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCP456VG.Focus();
                return;
            }
            if (txtTelD456VG.Text.Length != 10 || !txtTelD456VG.Text.All(char.IsDigit))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.TelefonoDestinatarioInvalido"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelD456VG.Focus();
                return;
            }
            var provinciaIngresada = txtProv456VG.Text.Trim().ToLower();
            var provinciasValidas = new HashSet<string>
            {
                "mendoza", "san luis", "cordoba", "córdoba", "tucuman", "tucumán", "san juan", "la rioja",
                "santa fe", "entre rios", "entre ríos", "corrientes", "misiones", "jujuy", "salta", "formosa",
                "chaco", "santiago del estero", "catamarca",
                "buenos aires",
                "tierra del fuego", "neuquen", "neuquén", "chubut", "santa cruz", "la pampa", "rio negro"
            };
            if (!provinciasValidas.Contains(provinciaIngresada))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ProvinciaNoValida"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtProv456VG.Focus();
                return;
            }
            string normalTxt = lng.ObtenerTexto_456VG("CrearEnvío_456VG.Combo.normal");
            string expressTxt = lng.ObtenerTexto_456VG("CrearEnvío_456VG.Combo.express");
            string tipoEnvio = cmbTipEnvio456VG.SelectedItem != null
                                ? cmbTipEnvio456VG.SelectedItem.ToString()
                                : normalTxt;
            DateTime fechaEntrega = DateTime.Now.AddDays(
                string.Equals(tipoEnvio, expressTxt, StringComparison.OrdinalIgnoreCase) ? 1 : 3
            );
            string estado = "Pendiente de Entrega";
            var envio = new BEEnvío_456VG(
                clienteCargado,
                new List<BEPaquete_456VG>(_paquetesPendientes),
                txtDNID456VG.Text.Trim(),
                txtNomD456VG.Text.Trim(),
                txtApeD456VG.Text.Trim(),
                txtTelD456VG.Text.Trim(),
                codPostal,
                txtDom456VG.Text.Trim(),
                txtLoc456VG.Text.Trim(),
                txtProv456VG.Text.Trim(),
                tipoEnvio,
                false,
                estado,
                fechaEntrega
            );
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
            foreach (var paq in _paquetesPendientes)
            {
                paq.Enviado456VG = true;
                BLLPaque.actualizarEntidad456VG(paq);
            }
            MessageBox.Show(
                lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.EnvioCreadoOK"),
                lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
            string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
            blleven.AddBitacora456VG(dni: dniLog, modulo: "Recepción", accion: "Crear Envío", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Peligro);
            limpiar();
        }
        private void txtDNICli456VG_Leave(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string dni = txtDNICli456VG.Text.Trim();
            if (dni.Length != 8 || !dni.All(char.IsDigit))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.DNIClienteInvalido"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDNICli456VG.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dni)) return;
            var resCli = BLLCliente.ObtenerClientePorDNI456VG(dni);
            if (resCli.resultado && resCli.entidad != null)
            {
                clienteCargado = resCli.entidad;
                txtNomCli456VG.Text = clienteCargado.Nombre456VG;
                txtApeCli456VG.Text = clienteCargado.Apellido456VG;
                txtTelCli456VG.Text = clienteCargado.Teléfono456VG;
                _paquetesPendientes.Clear();
                var todosLosPaquetesDelCliente = BLLPaque.leerEntidades456VG()
                    .Where(p =>
                        p.Cliente != null
                        && p.Cliente.DNI456VG == clienteCargado.DNI456VG
                        && !p.Enviado456VG
                    )
                    .ToList();
                _paquetesPendientes.AddRange(todosLosPaquetesDelCliente);
                ((BindingSource)dgvPaquetes.DataSource).ResetBindings(false);
            }
            else
            {
                clienteCargado = null;
                txtNomCli456VG.Clear();
                txtApeCli456VG.Clear();
                txtTelCli456VG.Clear();
                _paquetesPendientes.Clear();
                ((BindingSource)dgvPaquetes.DataSource).ResetBindings(false);
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ClienteNoEncontrado"),
                    lng.ObtenerTexto_456VG("CrearEnvío_456VG.Msg.ClienteNoEncontradoTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                RegistrarCliente_456VG regcli = new RegistrarCliente_456VG();
                regcli.ShowDialog();
            }
        }
        private bool DNIEstaRegistradoEnSistema(string dni)
        {
            return new BLLUsuario_456VG().leerEntidades456VG().Any(u => u.DNI456VG == dni)
                || new BLLCliente_456VG().leerEntidades456VG().Any(c => c.DNI456VG == dni);
        }
        private void CrearEnvío_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
            txtNomCli456VG.Enabled = false;
            txtApeCli456VG.Enabled = false;
            txtTelCli456VG.Enabled = false;
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
