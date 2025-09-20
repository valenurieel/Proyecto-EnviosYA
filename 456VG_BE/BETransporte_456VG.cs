using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BETransporte_456VG
    {
        public string Patente456VG { get; set; }
        public string Marca456VG { get; set; }
        public int Año456VG { get; set; }
        public float CapacidadPeso456VG { get; set; }
        public float CapacidadVolumen456VG { get; set; }
        public bool Disponible456VG { get; set; }
        public bool Activo456VG { get; set; }
        public BETransporte_456VG(string pat, string mar, int año, float cpeso, float cvol, bool disp, bool act)
        {
            this.Patente456VG = pat;
            this.Marca456VG = mar;
            this.Año456VG = año;
            this.CapacidadPeso456VG = cpeso;
            this.CapacidadVolumen456VG = cvol;
            this.Disponible456VG = disp;
            this.Activo456VG = act;
        }
        public BETransporte_456VG(string pat, string mar, int año, float cpeso, float cvol, bool disp)
        {
            this.Patente456VG = pat;
            this.Marca456VG = mar;
            this.Año456VG = año;
            this.CapacidadPeso456VG = cpeso;
            this.CapacidadVolumen456VG = cvol;
            this.Disponible456VG = disp;
        }
    }
}
