using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEPerfil_456VG
    {
        public string Nombre456VG { get; set; }
        public List<IPerfil_456VG> Permisos456VG { get; set; } = new List<IPerfil_456VG>();
        public List<Permiso_456VG> obtenerPermisos456VG()
        {
            List<Permiso_456VG> permisosFinales = new List<Permiso_456VG>();
            foreach (var permiso in Permisos456VG)
            {
                if (permiso is Permiso_456VG simple)
                {
                    permisosFinales.Add(simple);
                }
                else if (permiso is FamiliaPermiso_456VG familia)
                {
                    var hijos = familia.ObtenerTodosLosPermisos_456VG()
                                       .OfType<Permiso_456VG>()
                                       .ToList();
                    permisosFinales.AddRange(hijos);
                }
            }
            return permisosFinales
                .Distinct()
                .ToList();
        }
    }
}