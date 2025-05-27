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

namespace Proyecto_EnviosYA
{
    public partial class CobrarEnvío_456VG : Form
    {
        BLLEnvio_456VG BLLEnv = new BLLEnvio_456VG();
        public CobrarEnvío_456VG()
        {
            InitializeComponent();
        }

        private void CobrarEnvío_456VG_Load(object sender, EventArgs e) //QUE LEA LA FACTURA A COBRAR, se tiene que armar con el envio Y LUEGO PUEDA IMPRIMIRLA.
        {
            var facturas = new BLLFactura_456VG().leerEntidades456VG() ?? new List<BEFactura_456VG>();
            var datos = facturas.Select(f => new
            {
                Paquete = f.Paquete.CodPaq456VG,
                Remitente = $"{f.Cliente.Nombre456VG} {f.Cliente.Apellido456VG}",
                DNI_Remitente = f.DNICli456VG,
                Destinatario = $"{f.Envio.NombreDest456VG} {f.Envio.ApellidoDest456VG}",
                DNI_Dest = f.Envio.DNIDest456VG,
                Prov = f.Envio.Provincia456VG,
                Localidad = f.Envio.Localidad456VG,
                Domicilio = f.Envio.Domicilio456VG,
                TipoEnvio = f.Envio.tipoenvio456VG,
                Importe = f.Envio.Importe456VG.ToString("N2", CultureInfo.GetCultureInfo("es-AR")),
                Pagado = f.Envio.Pagado456VG ? "Sí" : "No",
                FechaEmisión = f.FechaEmision456VG.ToString("dd/MM/yyyy"),
            }).ToList();
            dataGridView1456VG.DataSource = datos;
            dataGridView1456VG.Columns["Paquete"].HeaderText = "Código Paquete";
            dataGridView1456VG.Columns["Remitente"].HeaderText = "Remitente";
            dataGridView1456VG.Columns["DNI_Remitente"].HeaderText = "DNI Remitente";
            dataGridView1456VG.Columns["Destinatario"].HeaderText = "Destinatario";
            dataGridView1456VG.Columns["DNI_Dest"].HeaderText = "DNI Destinatario";
            dataGridView1456VG.Columns["TipoEnvio"].HeaderText = "Tipo Envío";
            dataGridView1456VG.Columns["Importe"].HeaderText = "Importe ($)";
            dataGridView1456VG.Columns["Pagado"].HeaderText = "¿Pagado?";
            dataGridView1456VG.Columns["FechaEmisión"].HeaderText = "Fecha Emisión";
        }
    }
}
