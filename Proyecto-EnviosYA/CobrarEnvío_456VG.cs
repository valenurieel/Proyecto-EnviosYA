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
        private List<BEFactura_456VG> _facturas;
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
            _facturas = BLLFac.leerEntidades456VG() ?? new List<BEFactura_456VG>();
            var datos = _facturas
                .Where(f => !f.Envio.Pagado456VG)
                .Select(f => new
                {
                    Paquete = f.Paquete.CodPaq456VG,
                    Importe = f.Envio.Importe456VG
                                       .ToString("N2", CultureInfo.GetCultureInfo("es-AR")),
                    Remitente = $"{f.Cliente.Nombre456VG} {f.Cliente.Apellido456VG}",
                    DNI_Remitente = f.DNICli456VG,
                    Destinatario = $"{f.Envio.NombreDest456VG} {f.Envio.ApellidoDest456VG}",
                    DNI_Dest = f.Envio.DNIDest456VG,
                    Prov = f.Envio.Provincia456VG,
                    Localidad = f.Envio.Localidad456VG,
                    Domicilio = f.Envio.Domicilio456VG,
                    TipoEnvio = f.Envio.tipoenvio456VG,
                    Pagado = f.Envio.Pagado456VG ? "Sí" : "No",
                    FechaEmisión = f.FechaEmision456VG.ToString("dd/MM/yyyy"),
                })
                .ToList();
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
            dataGridView1456VG.Columns["FechaEmisión"].HeaderText = "Fecha Emisión";
        }
        private void dataGridView1456VG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var factura = _facturas[e.RowIndex];
            envioCargado = factura.Envio;
            paqueteCargado = factura.Paquete;
            clienteCargado = factura.Cliente;
            lblImporte456VG.Text = envioCargado
                .Importe456VG
                .ToString("N2", CultureInfo.GetCultureInfo("es-AR"));
            dniRemitenteSeleccionado = factura.DNICli456VG;
            cliente = clienteCargado.Nombre456VG + " " +clienteCargado.Apellido456VG;
        }

        private void btnAggTarj456VG_Click(object sender, EventArgs e)
        {
            if(envioCargado.Pagado456VG == false)
            {
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
                if (dateTimePicker1456VG.Value.Date < DateTime.Today)
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
                    MessageBox.Show("El DNI ingresado no coincide con el remitente.",
                                    "Validación DNI",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                envioCargado.Pagado456VG = true;
                var upd = BLLEnv.actualizarEntidad456VG(envioCargado);
                MessageBox.Show("Cobrando...", "En Proceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show($"Se han cobrado {lblImporte456VG.Text} a {cliente}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El Envío ya se ha PAGADO.", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}