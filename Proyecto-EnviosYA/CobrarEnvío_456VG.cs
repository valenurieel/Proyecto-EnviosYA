using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _456VG_BLL;
using _456VG_Servicios;
using _456VG_BE;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Proyecto_EnviosYA
{
    public partial class CobrarEnvío_456VG : Form
    {
        BLLFactura_456VG BLLFac = new BLLFactura_456VG();
        BLLEnvio_456VG BLLEnv = new BLLEnvio_456VG();
        private List<BEEnvío_456VG> _envios;
        private BEEnvío_456VG envioCargado;
        private BECliente_456VG clienteCargado;
        private BEPaquete_456VG paqueteCargado;
        private string dniRemitenteSeleccionado;
        private string cliente;
        public CobrarEnvío_456VG()
        {
            InitializeComponent();
        }
        private void CobrarEnvío_456VG_Load(object sender, EventArgs e)
        {
            var envios = BLLEnv.leerEntidades456VG().Where(w => !w.Pagado456VG).ToList();
            _envios = envios;
            var datos = envios.Select(w => new {
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
                Pagado = w.Pagado456VG ? "Sí" : "No",
                IdEnvio = w.id_envio456VG
            }).ToList();
            dataGridView1456VG.DataSource = datos;
            dataGridView1456VG.Columns["Paquete"].HeaderText = "Código Paquete";
            dataGridView1456VG.Columns["Importe"].HeaderText = "Importe $";
            dataGridView1456VG.Columns["Remitente"].HeaderText = "Remitente";
            dataGridView1456VG.Columns["DNI_Remitente"].HeaderText = "DNI Remitente";
            dataGridView1456VG.Columns["Destinatario"].HeaderText = "Destinatario";
            dataGridView1456VG.Columns["DNI_Dest"].HeaderText = "DNI Destinatario";
            dataGridView1456VG.Columns["Prov"].HeaderText = "Provincia";
            dataGridView1456VG.Columns["Localidad"].HeaderText = "Localidad";
            dataGridView1456VG.Columns["Domicilio"].HeaderText = "Domicilio";
            dataGridView1456VG.Columns["TipoEnvio"].HeaderText = "Tipo Envío";
            dataGridView1456VG.Columns["Pagado"].HeaderText = "¿Pagado?";
            dataGridView1456VG.Columns["IdEnvio"].Visible = false;
        }
        private void dataGridView1456VG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int idEnvioSeleccionado = (int)dataGridView1456VG.Rows[e.RowIndex].Cells["IdEnvio"].Value;
            envioCargado = _envios.FirstOrDefault(env => env.id_envio456VG == idEnvioSeleccionado);
            if (envioCargado == null) return;
            paqueteCargado = envioCargado.Paquete;
            clienteCargado = envioCargado.Cliente;
            dniRemitenteSeleccionado = envioCargado.DNICli456VG;
            cliente = $"{clienteCargado.Nombre456VG} {clienteCargado.Apellido456VG}";
            lblImporte456VG.Text = envioCargado.Importe456VG.ToString("N2", CultureInfo.GetCultureInfo("es-AR"));
        }
        private void btnAggTarj456VG_Click(object sender, EventArgs e)
        {
            if (envioCargado == null)
            {
                MessageBox.Show("Debe seleccionar un envío para cobrar.", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (envioCargado.Pagado456VG) //en el caso que muestre los pagados tmb (opcional)
            {
                MessageBox.Show("El Envío ya se ha PAGADO.", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNTarj456VG.Text) ||
                string.IsNullOrWhiteSpace(txtTitular456VG.Text) ||
                string.IsNullOrWhiteSpace(txtCVC456VG.Text) ||
                string.IsNullOrWhiteSpace(txtDNI456VG.Text) ||
                dateTimePicker1456VG.Value == null)
            {
                MessageBox.Show("Todos los campos son obligatorios.",
                                "Validación", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            var cardPattern = @"^\d{4}-\d{4}-\d{4}-\d{4}$";
            if (!Regex.IsMatch(txtNTarj456VG.Text, cardPattern))
            {
                MessageBox.Show("El Número de Tarjeta debe ser VÁLIDO.",
                                "Verificación de Tarjeta",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var cvcPattern = @"^\d{3,4}$";
            if (!Regex.IsMatch(txtCVC456VG.Text, cvcPattern))
            {
                MessageBox.Show("El CVC debe tener 3 o 4 dígitos.",
                                "Verificación CVC",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dateTimePicker1456VG.Value.Date <= DateTime.Today)
            {
                MessageBox.Show("La Fecha de Vencimiento debe ser MAYOR al Hoy.",
                                "Verificación Fecha",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtDNI456VG.Text.Trim() != dniRemitenteSeleccionado)
            {
                MessageBox.Show("El DNI ingresado no coincide con el remitente.",
                                "Validación DNI",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtTitular456VG.Text.Trim() != cliente)
            {
                MessageBox.Show("El titular de la tarjeta no coincide con el remitente.",
                                "Validación Titular",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            envioCargado.Pagado456VG = true;
            var upd = BLLEnv.actualizarEntidad456VG(envioCargado);
            if (!upd.resultado)
            {
                MessageBox.Show($"Error al actualizar envío: {upd.mensaje}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
                MessageBox.Show($"Error al crear la Factura: {resFact.mensaje}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show($"Se han cobrado {lblImporte456VG.Text} a {cliente}",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Ya puede Imprimir la Factura del Envío",
                             "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CobrarEnvío_456VG_Load(sender, e);
            limpiar();
        }
        private void limpiar()
        {
            cmbMedPago456VG.Text = "";
            txtNTarj456VG.Text = "";
            txtTitular456VG.Text = "";
            txtCVC456VG.Text = "";
            txtDNI456VG.Text = "";
            lblImporte456VG.Text = "IMPORTE";
            dateTimePicker1456VG.Value = DateTime.Today;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}