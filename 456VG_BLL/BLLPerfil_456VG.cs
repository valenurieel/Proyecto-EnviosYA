using _456VG_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _456VG_DAL;
using _456VG_Servicios;

namespace _456VG_BLL
{
    public class BLLPerfil_456VG
    {
        DALPerfil_456VG dal;
        public BLLPerfil_456VG()
        {
            dal = new DALPerfil_456VG();
        }
        public Resultado_456VG<BEPerfil_456VG> aggPerfil456VG(BEPerfil_456VG obj)
        {
            return dal.aggPerfil456VG(obj);
        }
        public List<BEPerfil_456VG> CargarCBPerfil456VG()
        {
            return dal.CargarCBPerfil456VG();
        }
        public List<BEPerfil_456VG> CargarCBPermisos456VG()
        {
            return dal.CargarCBPermisos456VG();
        }
        public Resultado_456VG<int> EliminarPerfil456VG(int idPerfil)
            => dal.EliminarPerfil456VG(idPerfil);
    }
}
