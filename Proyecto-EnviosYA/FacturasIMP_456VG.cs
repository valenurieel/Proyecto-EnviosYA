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
        private readonly BLLFactura_456VG bllFactura = new BLLFactura_456VG();

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
            var facturas = bllFactura.leerEntidades456VG();
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
            if (lista.Count == 0)
            {
                var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.NoFacturasImprimir"),
                    lng.ObtenerTexto_456VG("FacturasIMP_456VG.Msg.TituloInfo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }
            dataGridView1.DataSource = lista;
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
