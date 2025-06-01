using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BECliente_456VG
    {
        public string DNI456VG { get; set; }
        public string Nombre456VG { get; set; }
        public string Apellido456VG { get; set; }
        public string Teléfono456VG { get; set; }
        public string Domicilio456VG { get; set; }
        public DateTime FechaNacimiento456VG { get; set; }
        public bool Activo456VG { get; set; }

        public BECliente_456VG(string dni, string name, string ape, string tel, string dom, DateTime fechanac, bool act)
        {
            this.DNI456VG = dni;
            this.Nombre456VG = name;
            this.Apellido456VG = ape;
            this.Teléfono456VG = tel;
            this.Domicilio456VG = dom;
            this.FechaNacimiento456VG = fechanac;
            this.Activo456VG = act;
        }

        public BECliente_456VG(string dni, string name, string ape, string tel, string dom, DateTime fechanac)
        {
            this.DNI456VG = dni;
            this.Nombre456VG = name;
            this.Apellido456VG = ape;
            this.Teléfono456VG = tel;
            this.Domicilio456VG = dom;
            this.FechaNacimiento456VG = fechanac;
        }
    }
}
