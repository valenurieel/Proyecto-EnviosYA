using _456VG_BE;
using _456VG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _456VG_Servicios;

namespace _456VG_BLL
{
    public class BLLPermisoComp_456VG
    {
        DALPermisoComp_456VG dal;
        public BLLPermisoComp_456VG()
        {
            dal = new DALPermisoComp_456VG();
        }
        public Resultado_456VG<BEPermisoComp_456VG> aggPermisos456VG(BEPermisoComp_456VG obj)
        {
            return dal.aggPermisos456VG(obj);
        }
        public List<BEPermisoComp_456VG> ListaPermisos456VG()
        {
            return dal.ListaPermisos456VG();
        }
    }
}
