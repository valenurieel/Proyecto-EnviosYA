using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEPermisoComp_456VG
    {
        public int id_permisopadre456VG { get; set; }
        public int id_permisohijo456VG { get; set; }
        public BEPermisoComp_456VG(int idp, int idh)
        {
            this.id_permisopadre456VG = idp;
            this.id_permisohijo456VG = idh;
        }
    }
}
