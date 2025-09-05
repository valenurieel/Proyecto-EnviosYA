using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BESeguimientoEnvío_456VG
    {
        public BEEnvío_456VG Envio { get; set; }
        public string CodSeguimientoEnvío456VG { get; set; }
        public DateTime FechaEmitido456VG { get; set; }
        public bool Impreso456VG { get; set; }
        public BESeguimientoEnvío_456VG(BEEnvío_456VG env, string codseg, DateTime fecha, bool imp)
        {
            this.Envio = env;
            this.CodSeguimientoEnvío456VG = codseg;
            this.FechaEmitido456VG = fecha;
            this.Impreso456VG = imp;
        }
        public BESeguimientoEnvío_456VG(BEEnvío_456VG env, DateTime fecha, bool imp)
        {
            this.Envio = env;
            this.FechaEmitido456VG = fecha;
            this.Impreso456VG = imp;
            this.CodSeguimientoEnvío456VG = GenerateCodSeguimientoEnvio456VG();
        }
        private string GenerateCodSeguimientoEnvio456VG()
        {
            string codEnvio = (Envio?.CodEnvio456VG ?? "").Trim().ToUpper();
            string tresCodEnvio = codEnvio.Length >= 3 ? codEnvio.Substring(0, 3) : codEnvio.PadRight(3, 'X');
            string dni = (Envio?.Cliente?.DNI456VG ?? "").Trim().ToUpper();
            string tresDni = dni.Length >= 3 ? dni.Substring(0, 3) : dni.PadRight(3, 'X');
            DateTime fecha = FechaEmitido456VG == default(DateTime) ? DateTime.Now : FechaEmitido456VG;
            string fechaFormateada = fecha.ToString("yyyyMMddHHmmssfff");
            return $"{tresCodEnvio}{tresDni}{fechaFormateada}";
        }
    }
}
