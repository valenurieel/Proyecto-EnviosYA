using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEChoferes_C_456VG
    {
        public string DNIChofer456VG { get; set; }
        public string Nombre456VG { get; set; }
        public string Apellido456VG { get; set; }
        public string Telefono456VG { get; set; }
        public bool Registro456VG { get; set; }
        public DateTime VencimientoRegistro456VG { get; set; }
        public DateTime FechaNacimiento456VG { get; set; }
        public bool Disponible456VG { get; set; }
        public bool Activo456VG { get; set; }
        public DateTime Fecha456VG { get; set; }
        public string Hora456VG => Fecha456VG.ToString("HH:mm:ss");
        public BEChoferes_C_456VG(string dni, string nombre, string apellido, string telefono, bool registro, DateTime vencimiento, DateTime nacimiento, bool disponible, bool activo, DateTime fecha)
        {
            DNIChofer456VG = dni;
            Nombre456VG = nombre;
            Apellido456VG = apellido;
            Telefono456VG = telefono;
            Registro456VG = registro;
            VencimientoRegistro456VG = vencimiento;
            FechaNacimiento456VG = nacimiento;
            Disponible456VG = disponible;
            Activo456VG = activo;
            Fecha456VG = fecha;
        }
    }
}
