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

        // Lista que contendrá los paquetes pendientes (no enviados) del cliente cargado
        private List<BEPaquete_456VG> _paquetesPendientes = new List<BEPaquete_456VG>();

        // Cliente que se carga al escribir el DNI
        private BECliente_456VG clienteCargado;

        public CrearEnvío_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);

            // Configuramos el DataGridView para mostrar los paquetes pendientes
            ConfigurarDataGridViewPaquetes();
        }

        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
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

            // Limpiar la lista de paquetes pendientes y refrescar el DataGrid
            _paquetesPendientes.Clear();
            ((BindingSource)dgvPaquetes.DataSource).ResetBindings(false);
        }

        private void ConfigurarDataGridViewPaquetes()
        {
            dgvPaquetes.AutoGenerateColumns = false;
            dgvPaquetes.Columns.Clear();

            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodPaq",
                HeaderText = "Cod. Paq",
                DataPropertyName = "CodPaq456VG",
                ReadOnly = true,
                Width = 140
            });
            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Peso",
                HeaderText = "Peso",
                DataPropertyName = "Peso456VG",
                ReadOnly = true,
                Width = 80
            });
            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Ancho",
                HeaderText = "Ancho",
                DataPropertyName = "Ancho456VG",
                ReadOnly = true,
                Width = 80
            });
            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Largo",
                HeaderText = "Largo",
                DataPropertyName = "Largo456VG",
                ReadOnly = true,
                Width = 80
            });
            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Alto",
                HeaderText = "Alto",
                DataPropertyName = "Alto456VG",
                ReadOnly = true,
                Width = 80
            });

            // Ligamos la lista _paquetesPendientes al DataGridView mediante un BindingSource
            dgvPaquetes.DataSource = new BindingSource { DataSource = _paquetesPendientes };
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

            if (!float.TryParse(txtPeso456VG.Text, out float peso)) peso = 0f;
            if (!float.TryParse(txtAncho456VG.Text, out float ancho)) ancho = 0f;
            if (!float.TryParse(txtLargo456VG.Text, out float largo)) largo = 0f;
            if (!float.TryParse(txtAlto456VG.Text, out float alto)) alto = 0f;

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

            // Agregamos el paquete recién creado a la lista de pendientes y refrescamos el DataGrid
            _paquetesPendientes.Add(nuevoPaquete);
            ((BindingSource)dgvPaquetes.DataSource).ResetBindings(false);

            // Limpiamos solo los campos de dimensiones para poder agregar otro paquete sin scroll
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

            string tipoEnvio = cmbTipEnvio456VG.SelectedItem?.ToString()
                                ?? lng.ObtenerTexto_456VG("CrearEnvío_456VG.Combo.normal");

            // Construimos el objeto BEEnvío_456VG con todos los paquetes pendientes
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
                false  // inicialmente no pagado
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

            // Marcamos cada paquete como enviado en la BD
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

            // Limpiamos todos los campos y la lista de paquetes pendientes
            limpiar();
        }

        private void txtDNICli456VG_Leave(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string dni = txtDNICli456VG.Text.Trim();
            if (string.IsNullOrEmpty(dni)) return;

            // 1) Buscamos el cliente por DNI
            var resCli = BLLCliente.ObtenerClientePorDNI456VG(dni);
            if (resCli.resultado && resCli.entidad != null)
            {
                clienteCargado = resCli.entidad;
                txtNomCli456VG.Text = clienteCargado.Nombre456VG;
                txtApeCli456VG.Text = clienteCargado.Apellido456VG;
                txtTelCli456VG.Text = clienteCargado.Teléfono456VG;

                // 2) Cargamos los paquetes pendientes (Enviado456VG == false) para ese cliente
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
                // Si el cliente no existe, limpiamos todo
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

                // Abrir el formulario de registro de cliente si lo deseas
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
            this.Close();
        }

        private void txtDNICli456VG_TextChanged(object sender, EventArgs e)
        {
            // Este evento no se usa, pero el controlador existe en el diseñador
        }
    }
}
