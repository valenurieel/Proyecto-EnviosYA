using System;
using System.Collections.Generic;
using System.Data;
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
        private BLLFactura_456VG BLLFac = new BLLFactura_456VG();
        private BLLEnvio_456VG BLLEnv = new BLLEnvio_456VG();
        private List<BEEnvío_456VG> _envios;
        private BEEnvío_456VG envioCargado;
        private BECliente_456VG clienteCargado;
        private BEPaquete_456VG paqueteCargado;
        private string dniRemitenteSeleccionado;
        private string cliente;

        public CobrarEnvío_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }

        public void ActualizarIdioma_456VG()
        {
            // 1) Traduce todos los controles visibles (labels, botones, etc.)
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);

            // 2) Luego traducimos también los encabezados de DataGridView
            TraducirEncabezadosDataGrid();

            // 3) Finalmente, repoblamos el ComboBox de medios de pago
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            // Limpiamos los ítems actuales
            cmbMedPago456VG.Items.Clear();

            // Agregamos cada ítem traducido
            cmbMedPago456VG.Items.Add(lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Combo.MercadoPago"));
            cmbMedPago456VG.Items.Add(lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Combo.Debito"));
            cmbMedPago456VG.Items.Add(lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Combo.Credito"));

            // (Opcional) Si quieres que no quede ningún elemento seleccionado por defecto:
            cmbMedPago456VG.SelectedIndex = -1;
        }


        private void CobrarEnvío_456VG_Load(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            ActualizarIdioma_456VG();

            // 1) Recuperar todos los envíos no pagados
            var envios = BLLEnv.leerEntidades456VG().Where(w => !w.Pagado456VG).ToList();
            _envios = envios;

            // 2) Generar la lista anónima para el DataGridView,
            //    traduciéndole el “Sí/No” de la columna Pagado
            var datos = envios.Select(w => new
            {
                Paquete = w.Paquete.CodPaq456VG,
                Importe = w.Importe456VG.ToString("N2", CultureInfo.GetCultureInfo("es-AR")),
                Remitente = $"{w.Cliente.Nombre456VG} {w.Cliente.Apellido456VG}",
                DNI_Remitente = w.DNICli456VG,
                Destinatario = $"{w.NombreDest456VG} {w.ApellidoDest456VG}",
                DNI_Dest = w.DNIDest456VG,
                Prov = w.Provincia456VG,
                Localidad = w.Localidad456VG,
                Domicilio = w.Domicilio456VG,
                TipoEnvio = w.tipoenvio456VG,
                Pagado = w.Pagado456VG
                                   ? lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Pagado.Sí")
                                   : lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Pagado.No"),
                IdEnvio = w.id_envio456VG
            }).ToList();

            // 3) Asignar al DataSource
            dataGridView1456VG.DataSource = datos;

            // 4) Traducir encabezados (porque acabamos de volver a generar el DataSource)
            TraducirEncabezadosDataGrid();
        }

        private void TraducirEncabezadosDataGrid()
        {
            if (dataGridView1456VG.Columns.Count == 0) return;

            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            // Cada columna usa su clave en el JSON
            dataGridView1456VG.Columns["Paquete"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Paquete");
            dataGridView1456VG.Columns["Importe"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Importe");
            dataGridView1456VG.Columns["Remitente"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Remitente");
            dataGridView1456VG.Columns["DNI_Remitente"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.DNI_Remitente");
            dataGridView1456VG.Columns["Destinatario"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Destinatario");
            dataGridView1456VG.Columns["DNI_Dest"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.DNI_Dest");
            dataGridView1456VG.Columns["Prov"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Provincia");
            dataGridView1456VG.Columns["Localidad"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Localidad");
            dataGridView1456VG.Columns["Domicilio"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Domicilio");
            dataGridView1456VG.Columns["TipoEnvio"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.TipoEnvio");
            dataGridView1456VG.Columns["Pagado"].HeaderText = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Columna.Pagado");
            dataGridView1456VG.Columns["IdEnvio"].Visible = false;
        }

        private void dataGridView1456VG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            // 1) Si no es una fila válida, dejamos el texto por defecto.
            if (e.RowIndex < 0)
            {
                lblImporte456VG.Text = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.LblImporteDefault");
                envioCargado = null;
                return;
            }

            // 2) Recuperamos el id del envío en esa fila
            int idEnvioSeleccionado = (int)dataGridView1456VG
                .Rows[e.RowIndex]
                .Cells["IdEnvio"]
                .Value;

            envioCargado = _envios.FirstOrDefault(env => env.id_envio456VG == idEnvioSeleccionado);

            // 3) Si no lo encontramos (no debería ocurrir normalmente) o está null, devolvemos el valor por defecto
            if (envioCargado == null)
            {
                lblImporte456VG.Text = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.LblImporteDefault");
                return;
            }

            // 4) Guardamos referencias a paquete/cliente
            paqueteCargado = envioCargado.Paquete;
            clienteCargado = envioCargado.Cliente;
            dniRemitenteSeleccionado = envioCargado.DNICli456VG;
            cliente = $"{clienteCargado.Nombre456VG} {clienteCargado.Apellido456VG}";

            // 5) Si el envío ya está pagado, volvemos al valor por defecto
            if (envioCargado.Pagado456VG)
            {
                lblImporte456VG.Text = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.LblImporteDefault");
                return;
            }

            // 6) Finalmente, si no está pagado, mostramos su importe
            lblImporte456VG.Text = envioCargado.Importe456VG.ToString("N2", CultureInfo.GetCultureInfo("es-AR"));
        }


        private void btnAggTarj456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            // 1) Verificar que se haya seleccionado un envío
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

            // 2) Si ya está pagado, avisar
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

            // 3) Validar que todos los campos estén completos
            if (string.IsNullOrWhiteSpace(txtNTarj456VG.Text) ||
                string.IsNullOrWhiteSpace(txtTitular456VG.Text) ||
                string.IsNullOrWhiteSpace(txtCVC456VG.Text) ||
                string.IsNullOrWhiteSpace(txtDNI456VG.Text) ||
                dateTimePicker1456VG.Value == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.CamposObligatorios"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ValidacionTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 4) Validar formato de tarjeta: "1234-1234-1234-1234"
            var cardPattern = @"^\d{4}-\d{4}-\d{4}-\d{4}$";
            if (!Regex.IsMatch(txtNTarj456VG.Text, cardPattern))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.TarjetaInvalida"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.VerificacionTarjetaTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 5) Validar CVC (3 o 4 dígitos)
            var cvcPattern = @"^\d{3,4}$";
            if (!Regex.IsMatch(txtCVC456VG.Text, cvcPattern))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.CVCInvalido"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.VerificacionCVCTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 6) Validar que la fecha de vencimiento sea mayor a la de hoy
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

            // 7) Validar que el DNI ingresado coincida con el remitente
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

            // 8) Validar que el titular coincida con el remitente
            if (txtTitular456VG.Text.Trim() != cliente)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.TitularInvalido"),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ValidacionTitularTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 9) Marcar el envío como pagado y actualizarlo en la base
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

            // 10) Crear la factura correspondiente
            var fact = new BEFactura_456VG(
                envioCargado.id_envio456VG,
                envioCargado.id_paquete456VG,
                clienteCargado.DNI456VG,
                DateTime.Now
            );
            fact.Envio = envioCargado;
            fact.Paquete = paqueteCargado;
            fact.Cliente = clienteCargado;

            var resFact = BLLFac.crearEntidad456VG(fact);
            if (!resFact.resultado)
            {
                MessageBox.Show(
                    string.Format(
                        lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ErrorCrearFactura"),
                        resFact.mensaje
                    ),
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ErrorTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // 11) Mensaje de cobro exitoso (usamos formato {valor} a {cliente})
            MessageBox.Show(
                string.Format(
                    lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.CobroExitoso"),
                    lblImporte456VG.Text,
                    cliente
                ),
                lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ExitoTitle"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // 12) Mensaje adicional para indicar que ya puede imprimir
            MessageBox.Show(
                lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.PuedeImprimirFactura"),
                lng.ObtenerTexto_456VG("CobrarEnvío_456VG.Msg.ExitoTitle"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // 13) Recargar la grilla y limpiar campos
            CobrarEnvío_456VG_Load(sender, e);
            limpiar();
        }

        private void limpiar()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            cmbMedPago456VG.Text = "";
            txtNTarj456VG.Text = "";
            txtTitular456VG.Text = "";
            txtCVC456VG.Text = "";
            txtDNI456VG.Text = "";
            // Aquí corregimos la llamada a ObtenerTexto_456VG
            lblImporte456VG.Text = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.LblImporteDefault");
            dateTimePicker1456VG.Value = DateTime.Today;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1456VG_SelectionChanged(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            // Si no hay ninguna fila seleccionada, devolvemos el valor por defecto
            if (dataGridView1456VG.SelectedRows.Count == 0)
            {
                lblImporte456VG.Text = lng.ObtenerTexto_456VG("CobrarEnvío_456VG.LblImporteDefault");
                envioCargado = null;
            }
        }
    }
}
