using _456VG_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_Servicios
{
    public class Permiso_456VG
    {
        public string Nombre456VG { get; set; }
        public string NombreFormulario456VG { get; set; }
        public bool IsPerfil456VG { get; set; }
        public List<Permiso_456VG> PermisosHijos456VG { get; set; }
        public Permiso_456VG(string nombre, string formulario, bool isPerfil = false)
        {
            Nombre456VG = nombre;
            NombreFormulario456VG = formulario;
            IsPerfil456VG = isPerfil;
            PermisosHijos456VG = new List<Permiso_456VG>();
        }
        //Ver si tiene Hijos
        public bool IncluyePermiso(string permisoRequerido)
        {
            if (Nombre456VG == permisoRequerido)
            {
                return true;
            }
            foreach (var permisoHijo in PermisosHijos456VG)
            {
                if (permisoHijo.IncluyePermiso(permisoRequerido))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
