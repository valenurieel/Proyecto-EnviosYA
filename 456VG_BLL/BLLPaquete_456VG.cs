using _456VG_BE;
using _456VG_DAL;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BLL
{
    public class BLLPaquete_456VG:IEntidades_456VG<BEPaquete_456VG>
    {
        DALPaquete_456VG dal;
        public BLLPaquete_456VG()
        {
            dal = new DALPaquete_456VG();
        }
        public List<BEPaquete_456VG> leerEntidades456VG()
        {
            return dal.leerEntidades456VG();
        }
        public Resultado_456VG<BEPaquete_456VG> crearEntidad456VG(BEPaquete_456VG obj)
        {
            return dal.crearEntidad456VG(obj);
        }
        public Resultado_456VG<BEPaquete_456VG> eliminarEntidad456VG(BEPaquete_456VG obj)
        {
            return dal.eliminarEntidad456VG(obj);
        }
        public Resultado_456VG<BEPaquete_456VG> actualizarEntidad456VG(BEPaquete_456VG obj)
        {
            return dal.actualizarEntidad456VG(obj);
        }
    }
}
