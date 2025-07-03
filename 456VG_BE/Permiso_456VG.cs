using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class Permiso_456VG : IPerfil_456VG
    {
        public int CodPermisos456VG { get; set; }
        public string Nombre456VG { get; set; }
        public int CodPermiso456VG
        {
            get => CodPermisos456VG;
            set => CodPermisos456VG = value;
        }
    }
}
