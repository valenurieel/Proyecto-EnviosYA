using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class SeguimientoEnvíos_456VG : Form, IObserver_456VG
    {
        BLLSeguimientoEnvío_456VG bllSeg_456VG = new BLLSeguimientoEnvío_456VG();
        ArchivoIMP_456VG archivo_456VG = new ArchivoIMP_456VG();
        private List<BEEnvío_456VG> _enviosElegibles = new List<BEEnvío_456VG>();
        BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
        BLLEnvio_456VG bllEnv_456VG = new BLLEnvio_456VG();
        public SeguimientoEnvíos_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        private void btnImprimir456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string codEnvio = ObtenerCodigoSeleccionado456VG();
            if (string.IsNullOrWhiteSpace(codEnvio))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.SeleccioneEnvio"),
                                lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.TituloInfo"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                var resSeg = bllSeg_456VG.CrearParaEnvio456VG(codEnvio);
                if (!resSeg.resultado || resSeg.entidad == null)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.NoSePudoPreparar") + " " + resSeg.mensaje,
                                    lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.TituloInfo"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var envioCompleto = bllEnv_456VG.leerEntidades456VG().FirstOrDefault(x => x.CodEnvio456VG == codEnvio);
                if (envioCompleto == null)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.EnvioCompletoNoEncontrado"),
                                    lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.TituloError"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                archivo_456VG.GenerarSeguimientoEnvioPDF_456VG(resSeg.entidad, envioCompleto);
                bool marcado = bllSeg_456VG.MarcarImpresoPorEnvio456VG(codEnvio);
                if (!marcado)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.NoSePudoMarcarImpreso"),
                                    lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.TituloInfo"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                blleven.AddBitacora456VG(dni: dniLog, modulo: "Reportes", accion: "Imprimir Seguimiento", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Información);
                MessageBox.Show(lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.ImpresionOk"),
                                lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.TituloInfo"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarElegibles456VG();
                var abrir = MessageBox.Show(lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.DeseaAbrirPDF"),
                                            lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.TituloAbrirPDF"),
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (abrir == DialogResult.Yes) archivo_456VG.AbrirUltimoPDF_456VG();
            }
            catch (Exception ex)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.ErrorImpresion") + " " + ex.Message,
                                lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Msg.TituloError"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private string ObtenerCodigoSeleccionado456VG()
        {
            if (dataGridView1.CurrentRow == null) return null;
            var val = dataGridView1.CurrentRow.Cells["CodigoEnvio"]?.Value?.ToString();
            return string.IsNullOrWhiteSpace(val) ? null : val;
        }
        private void CargarElegibles456VG()
        {
            var todos = bllEnv_456VG.leerEntidades456VG() ?? new List<BEEnvío_456VG>();
            _enviosElegibles = todos.Where(e => e.Pagado456VG && !bllSeg_456VG.ExistePorEnvioImpreso456VG(e.CodEnvio456VG)).ToList();
            var lista = _enviosElegibles.Select(e => new
            {
                CodigoEnvio = e.CodEnvio456VG,
                CodigosPaquetes = (e.Paquetes != null && e.Paquetes.Count > 0) ? string.Join(", ", e.Paquetes.Select(p => p.CodPaq456VG)) : "(sin paquetes)",
                CodigoSeguimiento = bllSeg_456VG.ObtenerCodSeguimientoNoImpresoPorEnvio456VG(e.CodEnvio456VG) ?? "—",
                DireccionEntrega = $"{e.Domicilio456VG}, {e.Localidad456VG}, {e.Provincia456VG}",
                DniDest = e.DNIDest456VG,
                Destinatario = $"{e.NombreDest456VG} {e.ApellidoDest456VG}"
            }).ToList();
            dataGridView1.DataSource = lista;
            dataGridView1.ClearSelection();
        }
        private void ConfigurarGrid456VG()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoEnvio",
                HeaderText = "Código de Envío",
                DataPropertyName = "CodigoEnvio",
                Width = 130
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigosPaquetes",
                HeaderText = "Código de Paquete/s",
                DataPropertyName = "CodigosPaquetes",
                Width = 135
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoSeguimiento",
                HeaderText = "Código Seguimiento",
                DataPropertyName = "CodigoSeguimiento",
                Width = 130
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DireccionEntrega",
                HeaderText = "Dirección de Entrega",
                DataPropertyName = "DireccionEntrega",
                Width = 140
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DniDest",
                HeaderText = "DNI",
                DataPropertyName = "DniDest",
                Width = 80
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Destinatario",
                HeaderText = "Nombre y Apellido",
                DataPropertyName = "Destinatario",
                Width = 110
            });
        }
        private void TraducirEncabezadosDataGrid()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (dataGridView1.Columns.Count == 0) return;
            dataGridView1.Columns["CodigoEnvio"].HeaderText = lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Columna.CodigoEnvio");
            dataGridView1.Columns["CodigosPaquetes"].HeaderText = lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Columna.CodigosPaquetes");
            dataGridView1.Columns["CodigoSeguimiento"].HeaderText = lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Columna.CodigoSeguimiento");
            dataGridView1.Columns["DireccionEntrega"].HeaderText = lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Columna.DireccionEntrega");
            dataGridView1.Columns["DniDest"].HeaderText = lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Columna.DniDest");
            dataGridView1.Columns["Destinatario"].HeaderText = lng.ObtenerTexto_456VG("SeguimientoEnvíos_456VG.Columna.Destinatario");
        }
        public void ActualizarIdioma_456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            lng.CambiarIdiomaControles_456VG(this);
            TraducirEncabezadosDataGrid();
        }
        private void SeguimientoEnvíos_456VG_Load(object sender, EventArgs e)
        {
            ConfigurarGrid456VG();
            CargarElegibles456VG();
            ActualizarIdioma_456VG();
        }
    }
}
