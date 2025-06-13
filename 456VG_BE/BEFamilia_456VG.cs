using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEFamilia_456VG
    {
        public int id_permiso456VG { get; set; }
        public string nombre456VG { get; set; }
        public bool is_perfil456VG { get; set; } = false;  // siempre false para familias
        // Si quieres devolver hijos ya sea planos o anidados:
        public List<BEPermisoComp_456VG> Hijos { get; set; } = new List<BEPermisoComp_456VG>();

        public BEFamilia_456VG() { }

        public BEFamilia_456VG(int id, string nombre)
        {
            id_permiso456VG = id;
            nombre456VG = nombre;
        }
    }
}
