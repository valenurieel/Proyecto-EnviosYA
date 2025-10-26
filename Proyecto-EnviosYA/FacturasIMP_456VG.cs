using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class FacturasIMP_456VG : Form, IObserver_456VG
    {
        BLLFactura_456VG bllFactura = new BLLFactura_456VG();
        ArchivoIMP_456VG archivo = new ArchivoIMP_456VG();
        private List<BEFactura_456VG> facturasCargadas = new List<BEFactura_456VG>();
        public FacturasIMP_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        private void FacturasIMP_456VG_Load(object sender, EventArgs e)
        {
            ConfigurarDataGrid456VG();
            CargarFacturas456VG();
            ActualizarIdioma_456VG();
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
            TraducirEncabezadosDataGrid();
        }
        private void CargarFacturas456VG()
        {
            var facturas = bllFactura.leerEntidades456VG()
                                     .Where(f => !f.Impreso456VG)
                                     .ToList();
            facturasCargadas = facturas;
            var lista = facturas.Select(f => new
            {
                CódigoFactura = f.CodFactura456VG,
                CódigoEnvío = f.Envio.CodEnvio456VG,
                DNICLiente = f.Envio.Cliente.DNI456VG,
                Cliente = $"{f.Envio.Cliente.Nombre456VG} {f.Envio.Cliente.Apellido456VG}",
                CantidadPaquetes = f.Envio.Paquetes?.Count ?? 0,
                MedioPago = f.DatosPago?.MedioPago456VG ?? "Sin datos",
                Importe = f.Envio.Importe456VG,
                Fecha = f.FechaEmision456VG.ToShortDateString(),
                Hora = f.HoraEmision456VG.ToString(@"hh\:mm")
            }).ToList();
            dataGridView1.DataSource = lista;
            dataGridView1.ClearSelection();
        }
        private void ConfigurarDataGrid456VG()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CódigoFactura",
                DataPropertyName = "CódigoFactura",
                HeaderText = "Código Factura",
                Width = 110
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CódigoEnvío",
                DataPropertyName = "CódigoEnvío",
                HeaderText = "Código Envío",
                Width = 110
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DNICLiente",
                DataPropertyName = "DNICLiente",
                HeaderText = "DNI Cliente",
                Width = 80
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                DataPropertyName = "Cliente",
                HeaderText = "Cliente",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadPaquetes",
                DataPropertyName = "CantidadPaquetes",
                HeaderText = "Paquetes",
                Width = 60
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MedioPago",
                DataPropertyName = "MedioPago",
                HeaderText = "Pago",
                Width = 75
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Importe",
                DataPropertyName = "Importe",
                HeaderText = "Importe$",
                Width = 70
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                DataPropertyName = "Fecha",
                HeaderText = "Fecha",
                Width = 70
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Hora",
                DataPropertyName = "Hora",
                HeaderText = "Hora",
                Width = 50
            });
        }
        private void TraducirEncabezadosDataGrid()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (dataGridView1.Columns.Count == 0) return;
            dataGridView1.Columns["CódigoFactura"].HeaderText = lng.ObtenerTexto_456VG("FacturasIMP_456VG.Columna.CódigoFactura");
            dataGridView1.Columns["CódigoEnvío"].HeaderText = lng.ObtenerTexto_456VG("FacturasIMP_456VG.Columna.CódigoEnvío");
            dataGridView1.Columns["DNICLiente"].HeaderText = lng.ObtenerTexto_456VG("FacturasIMP_456VG.Columna.DNICLiente");
            dataGridView1.Columns["Cliente"].HeaderText = lng.ObtenerTexto_456VG("FacturasIMP_456VG.Columna.Cliente");
            dataGridView1.Columns["CantidadPaquetes"].HeaderText = lng.ObtenerTexto_456VG("FacturasIMP_456VG.Columna.CantidadPaquetes");
            dataGridView1.Columns["MedioPago"].HeaderText = lng.ObtenerTexto_456VG("FacturasIMP_456VG.Columna.MedioPago");
            dataGridView1.Columns["Importe"].HeaderText = lng.ObtenerTexto_456VG("FacturasIMP_456VG.Columna.Importe");
            dataGridView1.Columns["Fecha"].HeaderText = lng.ObtenerTexto_456VG("FacturasIMP_456VG.Columna.Fecha");
            dataGridView1.Columns["Hora"].HeaderText = lng.ObtenerTexto_456VG("FacturasIMP_456VG.Columna.Hora");
        }
        private void btnImprimir456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.CurrentRow == null || !dataGridView1.CurrentRow.Selected)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.SeleccioneFactura"),
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.TituloInfo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            int filaSeleccionada = dataGridView1.CurrentRow.Index;
            if (filaSeleccionada < 0 || filaSeleccionada >= facturasCargadas.Count)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.FacturaNoEncontrada"),
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.TituloError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            var facturaSeleccionada = facturasCargadas[filaSeleccionada];
            try
            {
                archivo.GenerarFacturaDetalladaPDF_456VG(facturaSeleccionada);
                facturaSeleccionada.Impreso456VG = true;
                bllFactura.actualizarEntidad456VG(facturaSeleccionada);
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.ImpresionExitosa"),
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.TituloInfo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
                string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                blleven.AddBitacora456VG(dni: dniLog, modulo: "Reportes", accion: "Imprimir Factura", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Información);
                CargarFacturas456VG();
                var resultado = MessageBox.Show(
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.DeseaAbrirPDF"),
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.TituloAbrirPDF"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (resultado == DialogResult.Yes)
                {
                    archivo.AbrirUltimoPDF_456VG();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.ErrorGenerarPDF") + "\n" + ex.Message,
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.TituloError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
