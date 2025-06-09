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
        public bool IsPerfil456VG { get; set; }  // Indica si el permiso es un perfil compuesto

        public List<Permiso_456VG> PermisosHijos456VG { get; set; }  // Lista de permisos hijos

        public Permiso_456VG(string nombre, string formulario, bool isPerfil = false)
        {
            Nombre456VG = nombre;
            NombreFormulario456VG = formulario;
            IsPerfil456VG = isPerfil;
            PermisosHijos456VG = new List<Permiso_456VG>();  // Inicializamos la lista de permisos hijos
        }

        // Método para verificar si este permiso incluye un permiso hijo
        public bool IncluyePermiso(string permisoRequerido)
        {
            if (Nombre456VG == permisoRequerido)
            {
                return true;  // Si este permiso es el requerido
            }

            // Recorrer los permisos hijos
            foreach (var permisoHijo in PermisosHijos456VG)
            {
                if (permisoHijo.IncluyePermiso(permisoRequerido))
                {
                    return true;  // Si alguno de los hijos incluye el permiso requerido
                }
            }
            return false;  // Si no se encuentra el permiso en este perfil ni en sus hijos
        }
    }
}
