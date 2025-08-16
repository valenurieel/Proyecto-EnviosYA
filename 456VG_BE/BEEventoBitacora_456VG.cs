using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _456VG_BE.BEEventoBitacora_456VG;

namespace _456VG_BE
{
    public class BEEventoBitacora_456VG
    {
        public int CodBitacora456VG { get; set; }
        public string Usuario456VG { get; set; } = string.Empty;
        public DateTime Fecha456VG { get; set; } = DateTime.Now;
        public string Modulo456VG { get; set; } = string.Empty;
        public string Accion456VG { get; set; } = string.Empty;
        public int Criticidad456VG { get; set; } = (int)NVCriticidad456VG.Información;
        public BEEventoBitacora_456VG(int cod, string user, string mod, string acc, NVCriticidad456VG criti, DateTime? Fecha = null)
        {
            this.CodBitacora456VG = cod;
            this.Usuario456VG = user;
            this.Modulo456VG = mod;
            this.Accion456VG = acc;
            this.Criticidad456VG= (int)criti;
            Fecha456VG = Fecha ?? DateTime.Now;
        }
        public BEEventoBitacora_456VG(string user, string mod, string acc, NVCriticidad456VG criti, DateTime? Fecha = null)
        {
            this.Usuario456VG = user;
            this.Modulo456VG = mod;
            this.Accion456VG = acc;
            this.Criticidad456VG = (int)criti;
            Fecha456VG = Fecha ?? DateTime.Now;
        }
        public enum NVCriticidad456VG
        {
            Crítico = 1,
            Peligro = 2,
            Atención = 3,
            Información = 4
        }
    }
}
