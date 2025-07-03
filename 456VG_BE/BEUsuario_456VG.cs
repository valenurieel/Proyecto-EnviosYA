using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEUsuario_456VG
    {
        public string DNI456VG { get; set; }
        public string Nombre456VG { get; set; }
        public string Apellido456VG { get; set; }
        public string Email456VG { get; set; }
        public string Teléfono456VG { get; set; }
        public string NombreUsuario456VG { get; set; }
        public string Contraseña456VG { get; set; }
        public string Salt456VG { get; set; }
        public string Domicilio456VG { get; set; }
        public BEPerfil_456VG Rol456VG { get; set; } //Perfil 
        public bool Bloqueado456VG { get; set; }
        public bool Activo456VG { get; set; }
        public string Idioma456VG { get; set; } //Idioma guardado
        public BEUsuario_456VG(string DNI, string Nombre, string Apellido, string Email, string Teléfono, string NombreUsuario, string Contraseña, string Salt, string Domicilio, BEPerfil_456VG Rol, bool Bloqueado, bool Activo, string idioma)
        {
            this.DNI456VG = DNI;
            this.Nombre456VG = Nombre;
            this.Apellido456VG = Apellido;
            this.Email456VG = Email;
            this.Teléfono456VG = Teléfono;
            this.NombreUsuario456VG = NombreUsuario;
            this.Contraseña456VG = Contraseña;
            this.Salt456VG = Salt;
            this.Domicilio456VG = Domicilio;
            this.Rol456VG = Rol;
            this.Bloqueado456VG = Bloqueado;
            this.Activo456VG = Activo;
            this.Idioma456VG = idioma;
        }
        public BEUsuario_456VG(string DNI, string Nombre, string Apellido, string Email, string Teléfono, string NombreUsuario, string Contraseña, string Domicilio, BEPerfil_456VG Rol, bool Bloqueado, bool Activo, string idioma)
        {
            this.DNI456VG = DNI;
            this.Nombre456VG = Nombre;
            this.Apellido456VG = Apellido;
            this.Email456VG = Email;
            this.Teléfono456VG = Teléfono;
            this.NombreUsuario456VG = NombreUsuario;
            this.Contraseña456VG = Contraseña;
            this.Domicilio456VG = Domicilio;
            this.Rol456VG = Rol;
            this.Bloqueado456VG = Bloqueado;
            this.Activo456VG = Activo;
            this.Idioma456VG = idioma;
        }
        public BEUsuario_456VG(string dni, string nombre, string apellido, string email, string telefono, string nombreUsuario, string domicilio, BEPerfil_456VG rol)
        {
            this.DNI456VG = dni;
            this.Nombre456VG = nombre;
            this.Apellido456VG = apellido;
            this.Email456VG = email;
            this.Teléfono456VG = telefono;
            this.NombreUsuario456VG = nombreUsuario;
            this.Domicilio456VG = domicilio;
            this.Rol456VG = rol;
        }
    }
}
