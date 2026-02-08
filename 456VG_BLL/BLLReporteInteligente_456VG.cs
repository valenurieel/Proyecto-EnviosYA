using _456VG_BE;
using _456VG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BLL
{
    public class BLLReporteInteligente_456VG
    {
        private readonly DALReporteInteligente_456VG dal;
        public BLLReporteInteligente_456VG()
        {
            dal = new DALReporteInteligente_456VG();
        }
        public BEReporteInteligente_456VG GenerarReporte456VG()
        {
            var datos = dal.ObtenerFacturacionMensual();
            if (datos.Count == 0) return null;
            var hoy = DateTime.Now;
            int mesActual = hoy.Month;
            int añoActual = hoy.Year;
            var actual = datos.FirstOrDefault(x => x.Mes == mesActual && x.Año == añoActual);
            if (actual == null) return null;
            // Buscamos el último mes registrado antes del actual (lógica de memoria transaccional)
            var anterior = datos
                .Where(x => x.Año < añoActual || (x.Año == añoActual && x.Mes < mesActual))
                .OrderByDescending(x => x.Año)
                .ThenByDescending(x => x.Mes)
                .FirstOrDefault();
            decimal variacionMesAnterior = 100;
            int mesesInactividad = 0;
            string periodoRef = "Inicio de Actividad";
            if (anterior != null)
            {
                mesesInactividad = ((actual.Año - anterior.Año) * 12) + (actual.Mes - anterior.Mes) - 1;
                periodoRef = $"{anterior.Mes}/{anterior.Año}";
                if (anterior.FacturacionMensual > 0)
                {
                    variacionMesAnterior = ((actual.FacturacionMensual - anterior.FacturacionMensual)
                                           / anterior.FacturacionMensual) * 100;
                }
            }
            // Variación Anual (Primer dato histórico vs el último disponible)
            var primer = datos.First();
            var ultimo = datos.Last();
            decimal variacionAnual = primer.FacturacionMensual > 0
                ? ((ultimo.FacturacionMensual - primer.FacturacionMensual) / primer.FacturacionMensual) * 100
                : 0;
            string tendencia;
            if (Math.Abs((double)variacionAnual) <= 5) tendencia = "Estable";
            else if (variacionAnual > 0) tendencia = "Creciente";
            else tendencia = "Bajante";
            return new BEReporteInteligente_456VG(
                añoActual,
                mesActual,
                actual.FacturacionMensual,
                variacionMesAnterior,
                variacionAnual,
                tendencia,
                mesesInactividad,
                periodoRef
            );
        }
    }
}
