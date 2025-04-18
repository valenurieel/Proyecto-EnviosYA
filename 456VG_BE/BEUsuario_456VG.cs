using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEUsuario_456VG
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_nacimiento { get; set; }
        public string Teléfono { get; set; }
        public string Contraseña { get; set; }
        public string Salt { get; set; }
        public string Domicilio { get; set; }

        public BEUsuario_456VG(string DNI, string Nombre, string Apellido, DateTime Fecha_nacimiento, string Teléfono,string Contraseña, string Salt, string Domicilio)
        {
            this.DNI = DNI;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Fecha_nacimiento = Fecha_nacimiento;
            this.Teléfono = Teléfono;
            this.Contraseña = Contraseña;
            this.Salt = Salt;
            this.Domicilio = Domicilio;
        }
        public BEUsuario_456VG(string DNI, string Nombre, string Apellido, DateTime Fecha_nacimiento, string Teléfono, string Contraseña, string Domicilio)
        {
            this.DNI = DNI;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Fecha_nacimiento = Fecha_nacimiento;
            this.Teléfono = Teléfono;
            this.Contraseña = Contraseña;
            this.Domicilio = Domicilio;
        }
    }
}
