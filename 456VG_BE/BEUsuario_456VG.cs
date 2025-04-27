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
        public string Email { get; set; }
        public string Teléfono { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Salt { get; set; }
        public string Domicilio { get; set; }
        public string Rol { get; set; }
        public bool Bloqueado { get; set; }
        public bool Activo { get; set; }

        public BEUsuario_456VG(string DNI, string Nombre, string Apellido, string Email, string Teléfono, string NombreUsuario, string Contraseña, string Salt, string Domicilio, string Rol, bool Bloqueado, bool Activo)
        {
            this.DNI = DNI;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Email = Email;
            this.Teléfono = Teléfono;
            this.NombreUsuario = NombreUsuario;
            this.Contraseña = Contraseña;
            this.Salt = Salt;
            this.Domicilio = Domicilio;
            this.Rol = Rol;
            this.Bloqueado = Bloqueado;
            this.Activo = Activo;
        }
        public BEUsuario_456VG(string DNI, string Nombre, string Apellido, string Email, string Teléfono, string NombreUsuario, string Contraseña, string Domicilio, string Rol, bool Bloqueado, bool Activo)
        {
            this.DNI = DNI;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Email = Email;
            this.Teléfono = Teléfono;
            this.NombreUsuario = NombreUsuario;
            this.Contraseña = Contraseña;
            this.Domicilio = Domicilio;
            this.Rol = Rol;
            this.Bloqueado = Bloqueado;
            this.Activo = Activo;
        }
        //list Gestión de Usuarios
        public BEUsuario_456VG(string DNI, string Nombre, string Apellido, string Email, string Teléfono, string NombreUsuario, string Domicilio, string Rol, bool Bloqueado)
        {
            this.DNI = DNI;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Email = Email;
            this.Teléfono = Teléfono;
            this.NombreUsuario = NombreUsuario;
            this.Domicilio = Domicilio;
            this.Rol = Rol;
            this.Bloqueado = Bloqueado;
        }
    }
}
