using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEReporteInteligente_456VG
    {
        public string CodReporte { get; set; }
        public int Año { get; set; }
        public int Mes { get; set; }
        public decimal FacturacionMensual { get; set; }
        public decimal VariacionVsAnterior { get; set; }
        public decimal VariacionAnual { get; set; }
        public string TendenciaAnual { get; set; }
        public int MesesInactividad { get; set; }
        public string PeriodoReferencia { get; set; }
        public BEReporteInteligente_456VG(string cod, int año, int mes, decimal factmen, decimal vs, decimal anual, string tende, int inactividad, string refPeriodo)
        {
            this.CodReporte = cod;
            this.Año = año;
            this.Mes = mes;
            this.FacturacionMensual = factmen;
            this.VariacionVsAnterior = vs;
            this.VariacionAnual = anual;
            this.TendenciaAnual = tende;
            this.MesesInactividad = inactividad;
            this.PeriodoReferencia = refPeriodo;
        }
        public BEReporteInteligente_456VG(int año, int mes, decimal factmen, decimal vs, decimal anual, string tende, int inactividad, string refPeriodo)
        {
            this.Año = año;
            this.Mes = mes;
            this.FacturacionMensual = factmen;
            this.VariacionVsAnterior = vs;
            this.VariacionAnual = anual;
            this.TendenciaAnual = tende;
            this.MesesInactividad = inactividad;
            this.PeriodoReferencia = refPeriodo;
        }
    }
}
