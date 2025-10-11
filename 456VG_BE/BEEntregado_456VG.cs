using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEEntregado_456VG
    {
        public string CodEntrega456VG { get; set; }
        public BEEnvío_456VG Envio { get; set; }
        public DateTime FechaIntentoEntrega456VG { get; set; }
        public int CantIntento456VG { get; set; }
        public bool Entregado456VG { get; set; }
        public string Motivo456VG { get; set; }
        public string NombreEncargado456VG { get; set; }
        public BEEntregado_456VG(string cod, BEEnvío_456VG env, DateTime fec, int cantint, bool entr, string mot, string name)
        {
            this.CodEntrega456VG = cod;
            this.Envio = env;
            this.FechaIntentoEntrega456VG= fec;
            this.CantIntento456VG= cantint;
            this.Entregado456VG = entr;
            this.Motivo456VG= mot;
            this.NombreEncargado456VG= name;
        }
        public static string GenerateCodEntrega456VG(string codenvio, string nombreEncargado, string dniEncargado)
        {
            string codEnvio = (codenvio ?? "").Length >= 3
                ? codenvio.Substring(codenvio.Length - 3).ToUpper()
                : (codenvio ?? "").ToUpper().PadRight(3, 'X');
            string dni = (dniEncargado ?? "").Length >= 3
                ? dniEncargado.Substring(0, 3).ToUpper()
                : (dniEncargado ?? "").ToUpper().PadRight(3, 'X');
            string nombre = (nombreEncargado ?? "").Length >= 3
                ? nombreEncargado.Substring(0, 3).ToUpper()
                : (nombreEncargado ?? "").ToUpper().PadRight(3, 'X');
            string sufijoTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return $"{codEnvio}{dni}{nombre}{sufijoTime}";
        }
    }
}
