using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEPerfil_456VG
    {
        public int CodPermiso456VG { get; set; }
        public string nombre456VG { get; set; }
        public string permiso456VG { get; set; }
        public bool IsPerfil456VG { get; set; }
        public BEPerfil_456VG(int id_permiso, string nombre, string permiso, bool is_perfil)
        {
            this.CodPermiso456VG = id_permiso;
            this.nombre456VG = nombre;
            this.permiso456VG = permiso;
            this.IsPerfil456VG = is_perfil;
        }
        public BEPerfil_456VG(string nombre, string permiso, bool is_perfil)
        {
            this.nombre456VG = nombre;
            this.permiso456VG = permiso;
            this.IsPerfil456VG = is_perfil;
        }
    }
}
