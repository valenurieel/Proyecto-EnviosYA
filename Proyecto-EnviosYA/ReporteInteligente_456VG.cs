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
    public partial class ReporteInteligente_456VG : Form, IObserver_456VG
    {
        private readonly BLLReporteInteligente_456VG bll;
        private readonly ArchivoIMP_456VG archivo;
        private BEReporteInteligente_456VG reporteGenerado;
        public ReporteInteligente_456VG()
        {
            InitializeComponent();
            bll = new BLLReporteInteligente_456VG();
            archivo = new ArchivoIMP_456VG();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtReporte456VG_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnImprimir456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (reporteGenerado == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Msg.GenerarPrimero"),
                    lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Msg.TituloInfo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            archivo.GenerarReporteInteligentePDF_456VG(reporteGenerado);
            MessageBox.Show(
                lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Msg.PDFOK"),
                lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Msg.TituloInfo"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            var abrir = MessageBox.Show(
                lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Msg.DeseaAbrirPDF"),
                lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Msg.TituloAbrirPDF"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (abrir == DialogResult.Yes)
                archivo.AbrirUltimoPDF_456VG();
            new BLLEventoBitacora_456VG().AddBitacora456VG(
                SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG,
                "Reportes",
                "Imprimir Reporte Inteligente",
                BEEventoBitacora_456VG.NVCriticidad456VG.Información
            );
        }
        private string FormatearReporte(BEReporteInteligente_456VG r)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("==========================================");
            sb.AppendLine($"   {lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.PerformanceAnalysis")}");
            sb.AppendLine("==========================================");
            sb.AppendLine($"{lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.MesAnalizado").ToUpper()}: {r.Mes}/{r.Año}");
            sb.AppendLine($"{lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.FacturacionMensual").ToUpper()}: ${r.FacturacionMensual:N2}");
            sb.AppendLine("------------------------------------------");
            sb.AppendLine($"{lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.OpHistory")}:");
            if (r.MesesInactividad > 0)
            {
                sb.AppendLine(lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.InactivityState"));
                sb.AppendLine($"→ {lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.MonthsInactivity")}: {r.MesesInactividad}");
                sb.AppendLine($"→ {lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.ComparedLastClose")}: {r.PeriodoReferencia}");
            }
            else
            {
                sb.AppendLine(lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.ConstantState"));
                sb.AppendLine($"→ {lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.ComparedPrevMonth")}: {r.PeriodoReferencia}");
            }
            sb.AppendLine();
            string signo = r.VariacionVsAnterior >= 0 ? "+" : "";
            sb.AppendLine($"{lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.StrategicVar")}: {signo}{r.VariacionVsAnterior:N2}%");
            sb.AppendLine();
            sb.AppendLine($"{lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.TendenciaAnual").ToUpper()}:");
            sb.AppendLine($"→ {lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.Tendencia")}: {r.TendenciaAnual.ToUpper()}");
            sb.AppendLine($"→ {lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.VariacionAnualAcumulada")}: {r.VariacionAnual:N2}%");
            sb.AppendLine("==========================================");
            return sb.ToString();
        }
        private void btnGenerar456VG_Click(object sender, EventArgs e)
        {
            reporteGenerado = bll.GenerarReporte456VG();
            if (reporteGenerado == null)
            {
                var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
                txtReporte456VG.Text = lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Msg.SinDatos");
                return;
            }
            txtReporte456VG.Text = FormatearReporte(reporteGenerado);
        }
        private void ReporteInteligente_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
        }
    }
}
