using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEPermisoComp_456VG
    {
        public int CodPermisoPadre456VG { get; set; }
        public int CodPermisoHijo456VG { get; set; }
        public BEPermisoComp_456VG(int idp, int idh)
        {
            this.CodPermisoPadre456VG = idp;
            this.CodPermisoHijo456VG = idh;
        }
    }
}
