using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _456VG_BE;
using _456VG_DAL;
using _456VG_Servicios;

namespace _456VG_BLL
{
    public class BLLFactura_456VG : IEntidades_456VG<BEFactura_456VG>
    {
        DALFactura_456VG dal;
        public BLLFactura_456VG()
        {
            dal = new DALFactura_456VG();
        }
        public List<BEFactura_456VG> leerEntidades456VG()
        {
            return dal.leerEntidades456VG();
        }
        public Resultado_456VG<BEFactura_456VG> crearEntidad456VG(BEFactura_456VG obj)
        {
            return dal.crearEntidad456VG(obj);
        }
        public Resultado_456VG<BEFactura_456VG> eliminarEntidad456VG(BEFactura_456VG obj)
        {
            return dal.eliminarEntidad456VG(obj);
        }
        public Resultado_456VG<BEFactura_456VG> actualizarEntidad456VG(BEFactura_456VG obj)
        {
            return dal.actualizarEntidad456VG(obj);
        }
    }
}
