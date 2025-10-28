using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using _456VG_BLL;
using _456VG_Servicios;
using _456VG_BE;

namespace Proyecto_EnviosYA
{
    public partial class CobrarEnvío_456VG : Form, IObserver_456VG
    {
        BLLFactura_456VG BLLFac = new BLLFactura_456VG();
        BLLEnvio_456VG BLLEnv = new BLLEnvio_456VG();
        BLLDatosPago_456VG BLLDatosPago = new BLLDatosPago_456VG();
        private List<BEEnvío_456VG> _envios;
        private BEEnvío_456VG envioCargado;
        private BECliente_456VG clienteCargado;
        private string dniRemitenteSeleccionado;
        private string clienteNombreCompleto;
        public CobrarEnvío_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
            ConfigurarDataGridView();
            ConfigurarDataGridViewDetallePaquetes();
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
            TraducirEncabezadosDataGrid();
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            cmbMedPago456VG.Items.Clear();
            cmbMedPago456VG.Items.Add(lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Combo.MercadoPago"));
            cmbMedPago456VG.Items.Add(lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Combo.Debito"));
            cmbMedPago456VG.Items.Add(lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Combo.Credito"));
            cmbMedPago456VG.SelectedIndex = -1;
        }
        private void CobrarEnvío_456VG_Load(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            ActualizarIdioma_456VG();
            var envios = BLLEnv.leerEntidades456VG()
                               .Where(w => !w.Pagado456VG)
                               .ToList();
            _envios = envios;
            var datos = envios.Select(w =>
            {
                return new
                {
                    Envío = w.CodEnvio456VG,
                    Importe = w.Importe456VG.ToString("N2", CultureInfo.GetCultureInfo("es-AR")),
                    Remitente = $"{w.Cliente.Nombre456VG} {w.Cliente.Apellido456VG}",
                    DNI_Remitente = w.Cliente.DNI456VG,
                    Destinatario = $"{w.NombreDest456VG} {w.ApellidoDest456VG}",
                    DNI_Dest = w.DNIDest456VG,
                    Prov = w.Provincia456VG,
                    Localidad = w.Localidad456VG,
                    Domicilio = w.Domicilio456VG,
                    TipoEnvio = w.tipoenvio456VG,
                    IdEnvio = w.CodEnvio456VG
                };
            }).ToList();
            dataGridView1456VG.DataSource = datos;
            TraducirEncabezadosDataGrid();
            dgvPaquetesDetalle.DataSource = null;
        }
        private void TraducirEncabezadosDataGrid()
        {
            if (dataGridView1456VG.Columns.Count == 0) return;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            dataGridView1456VG.Columns["Envío"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Envío");
            dataGridView1456VG.Columns["Importe"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Importe");
            dataGridView1456VG.Columns["Remitente"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Remitente");
            dataGridView1456VG.Columns["DNI_Remitente"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.DNI_Remitente");
            dataGridView1456VG.Columns["Destinatario"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Destinatario");
            dataGridView1456VG.Columns["DNI_Dest"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.DNI_Dest");
            dataGridView1456VG.Columns["Prov"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Provincia");
            dataGridView1456VG.Columns["Localidad"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Localidad");
            dataGridView1456VG.Columns["Domicilio"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Domicilio");
            dataGridView1456VG.Columns["TipoEnvio"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.TipoEnvio");
            dgvPaquetesDetalle.Columns["CodPaq"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.CodPaq");
            dgvPaquetesDetalle.Columns["Peso"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Peso");
            dgvPaquetesDetalle.Columns["Ancho"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Ancho");
            dgvPaquetesDetalle.Columns["Largo"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Largo");
            dgvPaquetesDetalle.Columns["Alto"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Alto");
            dataGridView1456VG.Columns["IdEnvio"].Visible = false;
        }
        private void ConfigurarDataGridView()
        {
            dataGridView1456VG.AutoGenerateColumns = false;
            dataGridView1456VG.Columns.Clear();
            dataGridView1456VG.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Envío",
                HeaderText = "Código Envío",
                DataPropertyName = "Envío",
                ReadOnly = true,
                Width = 90
            });
            dataGridView1456VG.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Importe",
                HeaderText = "Importe $",
                DataPropertyName = "Importe",
                ReadOnly = true,
                Width = 70
            });
            dataGridView1456VG.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Remitente",
                HeaderText = "Remitente",
                DataPropertyName = "Remitente",
                ReadOnly = true,
                Width = 100
            });
            dataGridView1456VG.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DNI_Remitente",
                HeaderText = "DNI Remitente",
                DataPropertyName = "DNI_Remitente",
                ReadOnly = true,
                Width = 80
            });
            dataGridView1456VG.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Destinatario",
                HeaderText = "Destinatario",
                DataPropertyName = "Destinatario",
                ReadOnly = true,
                Width = 100
            });
            dataGridView1456VG.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DNI_Dest",
                HeaderText = "DNI Destinatario",
                DataPropertyName = "DNI_Dest",
                ReadOnly = true,
                Width = 80
            });
            dataGridView1456VG.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Prov",
                HeaderText = "Provincia",
                DataPropertyName = "Prov",
                ReadOnly = true,
                Width = 90
            });
            dataGridView1456VG.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Localidad",
                HeaderText = "Localidad",
                DataPropertyName = "Localidad",
                ReadOnly = true,
                Width = 90
            });
            dataGridView1456VG.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Domicilio",
                HeaderText = "Domicilio",
                DataPropertyName = "Domicilio",
                ReadOnly = true,
                Width = 90
            });
            dataGridView1456VG.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoEnvio",
                HeaderText = "Tipo de Envío",
                DataPropertyName = "TipoEnvio",
                ReadOnly = true,
                Width = 60
            });
            var colIdEnvio = new DataGridViewTextBoxColumn
            {
                Name = "IdEnvio",
                HeaderText = "IdEnvio",
                DataPropertyName = "IdEnvio",
                ReadOnly = true,
                Visible = false
            };
            dataGridView1456VG.Columns.Add(colIdEnvio);
        }
        private void ConfigurarDataGridViewDetallePaquetes()
        {
            dgvPaquetesDetalle.AutoGenerateColumns = false;
            dgvPaquetesDetalle.Columns.Clear();
            dgvPaquetesDetalle.AllowUserToAddRows = false;
            dgvPaquetesDetalle.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodPaq",
                HeaderText = "Código Paquete",
                DataPropertyName = "CodPaq456VG",
                ReadOnly = true,
                Width = 100
            });
            dgvPaquetesDetalle.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Peso",
                HeaderText = "Peso (kg)",
                DataPropertyName = "Peso456VG",
                ReadOnly = true,
                Width = 90
            });
            dgvPaquetesDetalle.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Ancho",
                HeaderText = "Ancho (cm)",
                DataPropertyName = "Ancho456VG",
                ReadOnly = true,
                Width = 90
            });
            dgvPaquetesDetalle.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Largo",
                HeaderText = "Largo (cm)",
                DataPropertyName = "Largo456VG",
                ReadOnly = true,
                Width = 90
            });
            dgvPaquetesDetalle.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Alto",
                HeaderText = "Alto (cm)",
                DataPropertyName = "Alto456VG",
                ReadOnly = true,
                Width = 90
            });
            dgvPaquetesDetalle.DataSource = null;
        }
        private void dataGridView1456VG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (e.RowIndex < 0)
            {
                lblImporte456VG.Text = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.LblImporteDefault");
                envioCargado = null;
                dgvPaquetesDetalle.DataSource = null;
                return;
            }
            string codEnvioSeleccionado = dataGridView1456VG
                .Rows[e.RowIndex]
                .Cells["IdEnvio"]
                .Value as string;
            envioCargado = _envios.FirstOrDefault(env => env.CodEnvio456VG == codEnvioSeleccionado);
            if (envioCargado == null)
            {
                lblImporte456VG.Text = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.LblImporteDefault");
                dgvPaquetesDetalle.DataSource = null;
                return;
            }
            clienteCargado = envioCargado.Cliente;
            dniRemitenteSeleccionado = clienteCargado.DNI456VG;
            clienteNombreCompleto = $"{clienteCargado.Nombre456VG} {clienteCargado.Apellido456VG}";
            if (envioCargado.Pagado456VG)
            {
                lblImporte456VG.Text = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.LblImporteDefault");
                dgvPaquetesDetalle.DataSource = null;
                return;
            }
            lblImporte456VG.Text = envioCargado.Importe456VG
                                   .ToString("N2", CultureInfo.GetCultureInfo("es-AR"));
            dgvPaquetesDetalle.DataSource = envioCargado.Paquetes;
        }
        private void dataGridView1456VG_SelectionChanged(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (dataGridView1456VG.SelectedRows.Count == 0)
            {
                lblImporte456VG.Text = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.LblImporteDefault");
                envioCargado = null;
                dgvPaquetesDetalle.DataSource = null;
            }
        }
        private void btnAggTarj456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (envioCargado == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.SeleccionarEnvio"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.InformacionTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (envioCargado.Pagado456VG)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.EnvioPagado"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.InformacionTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNTarj456VG.Text) ||
                string.IsNullOrWhiteSpace(txtTitular456VG.Text) ||
                string.IsNullOrWhiteSpace(txtCVC456VG.Text) ||
                string.IsNullOrWhiteSpace(txtDNI456VG.Text) ||
                dateTimePicker1456VG.Value == null ||
                cmbMedPago456VG.SelectedIndex < 0)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.CamposObligatorios"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ValidacionTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var cardPattern = @"^\d{4}-\d{4}-\d{4}-\d{4}$";
            if (!Regex.IsMatch(txtNTarj456VG.Text.Trim(), cardPattern))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.TarjetaInvalida"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.VerificacionTarjetaTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var cvcPattern = @"^\d{3,4}$";
            if (!Regex.IsMatch(txtCVC456VG.Text.Trim(), cvcPattern))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.CVCInvalido"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.VerificacionCVCTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (dateTimePicker1456VG.Value.Date <= DateTime.Today)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.FechaInvalida"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.VerificacionFechaTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (txtDNI456VG.Text.Trim() != dniRemitenteSeleccionado)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.DNIInvalido"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ValidacionDNITitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (txtTitular456VG.Text.Trim() != clienteNombreCompleto)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.TitularInvalido"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ValidacionTitularTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            envioCargado.Pagado456VG = true;
            var upd = BLLEnv.actualizarEntidad456VG(envioCargado);
            if (!upd.resultado)
            {
                MessageBox.Show(
                    string.Format(
                        lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ErrorActualizarEnvio"),
                        upd.mensaje
                    ),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ErrorTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            var datosPagoExistente = BLLDatosPago.LeerPorDni456VG(dniRemitenteSeleccionado);
            if (datosPagoExistente == null)
            {
                var nuevoPago = new BEDatosPago_456VG(
                    clienteCargado,
                    cmbMedPago456VG.SelectedItem.ToString().Trim(),
                    txtNTarj456VG.Text.Trim(),
                    txtTitular456VG.Text.Trim(),
                    dateTimePicker1456VG.Value.Date,
                    txtCVC456VG.Text.Trim()
                );
                var resPago = BLLDatosPago.crearEntidad456VG(nuevoPago);
                if (!resPago.resultado)
                {
                    MessageBox.Show(
                        string.Format(
                            lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ErrorGuardarPago"),
                            resPago.mensaje
                        ),
                        lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ErrorTitle"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }
                BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
                string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                blleven.AddBitacora456VG(dni: dniLog, modulo: "Envíos", accion: "Cobrar Envío", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Peligro);
            }
            var fact = new BEFactura_456VG(envioCargado, datosPagoExistente, DateTime.Now, false);
            var resFact = BLLFac.crearEntidad456VG(fact);
            new BLLDigitoVerificador_456VG().ActualizarDV456VG();
            MessageBox.Show(
                string.Format(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.CobroExitoso"),
                    lblImporte456VG.Text,
                    clienteNombreCompleto
                ),
                lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ExitoTitle"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            MessageBox.Show(
                lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.PuedeImprimirFactura"),
                lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ExitoTitle"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            CobrarEnvío_456VG_Load(sender, e);
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            cmbMedPago456VG.Text = "";
            txtNTarj456VG.Text = "";
            txtTitular456VG.Text = "";
            txtCVC456VG.Text = "";
            txtDNI456VG.Text = "";
            lblImporte456VG.Text = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.LblImporteDefault");
            dateTimePicker1456VG.Value = DateTime.Today;
            envioCargado = null;
            clienteCargado = null;
            dniRemitenteSeleccionado = null;
            clienteNombreCompleto = null;
            dgvPaquetesDetalle.DataSource = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtDNI456VG_Leave_1(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string dniIngresado = txtDNI456VG.Text.Trim();
            if (string.IsNullOrEmpty(dniIngresado))
                return;
            var datosPago = BLLDatosPago.LeerPorDni456VG(dniIngresado);
            if (datosPago != null)
            {
                cmbMedPago456VG.SelectedItem = datosPago.MedioPago456VG;
                txtNTarj456VG.Text = datosPago.NumTarjeta456VG;
                txtTitular456VG.Text = datosPago.Titular456VG;
                dateTimePicker1456VG.Value = datosPago.FechaVencimiento456VG.Date;
                txtCVC456VG.Text = datosPago.CVC456VG;
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.SinDatosPago"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.InformacionTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                cmbMedPago456VG.SelectedIndex = -1;
                txtNTarj456VG.Clear();
                txtTitular456VG.Clear();
                dateTimePicker1456VG.Value = DateTime.Today;
                txtCVC456VG.Clear();
            }
        }
        private void dgvPaquetesDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void txtDNI456VG_TextChanged_1(object sender, EventArgs e)
        {
        }
    }
}
