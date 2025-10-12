using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEChofer_456VG
    {
        public string DNIChofer456VG { get; set; }
        public string Nombre456VG { get; set; }
        public string Apellido456VG { get; set; }
        public string Teléfono456VG { get; set; }
        public bool Registro456VG { get; set; }
        public DateTime VencimientoRegistro456VG { get; set; }
        public DateTime FechaNacimiento456VG { get; set; }
        public bool Disponible456VG { get; set; }
        public bool Activo456VG { get; set; }
        public BEChofer_456VG(string dni, string name, string ape, string tel, bool reg, DateTime venc, DateTime fnac, bool disp, bool act)
        {
            this.DNIChofer456VG = dni;
            this.Nombre456VG = name;
            this.Apellido456VG = ape;
            this.Teléfono456VG = tel;
            this.Registro456VG = reg;
            this.VencimientoRegistro456VG = venc;
            this.FechaNacimiento456VG = fnac;
            this.Disponible456VG = disp;
            this.Activo456VG = act;
        }
        public BEChofer_456VG(string dni, string name, string ape, string tel, bool reg, DateTime venc, DateTime fnac, bool disp)
        {
            this.DNIChofer456VG = dni;
            this.Nombre456VG = name;
            this.Apellido456VG = ape;
            this.Teléfono456VG = tel;
            this.Registro456VG = reg;
            this.VencimientoRegistro456VG = venc;
            this.FechaNacimiento456VG = fnac;
            this.Disponible456VG = disp;
        }
        public BEChofer_456VG(string dni, string name, string ape, string tel, bool reg, DateTime venc, DateTime fnac)
        {
            this.DNIChofer456VG = dni;
            this.Nombre456VG = name;
            this.Apellido456VG = ape;
            this.Teléfono456VG = tel;
            this.Registro456VG = reg;
            this.VencimientoRegistro456VG = venc;
            this.FechaNacimiento456VG = fnac;
        }
    }
}
