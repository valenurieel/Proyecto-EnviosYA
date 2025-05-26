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
    public class BLLCliente_456VG:ICrud_456VG<BECliente_456VG>
    {
        DALCliente_456VG dal;
        public BLLCliente_456VG()
        {
            dal = new DALCliente_456VG();
        }
        public List<BECliente_456VG> leerEntidades456VG()
        {
            return dal.leerEntidades456VG();
        }
        public Resultado_456VG<BECliente_456VG> crearEntidad456VG(BECliente_456VG obj)
        {
            return dal.crearEntidad456VG(obj);
        }
        public Resultado_456VG<BECliente_456VG> eliminarEntidad456VG(BECliente_456VG obj)
        {
            return dal.eliminarEntidad456VG(obj);
        }
        public Resultado_456VG<BECliente_456VG> actualizarEntidad456VG(BECliente_456VG obj)
        {
            return dal.actualizarEntidad456VG(obj);
        }
        public Resultado_456VG<BECliente_456VG> ObtenerClientePorDNI456VG(string dni)
        {
            return dal.ObtenerClientePorDNI456VG(dni);
        }
    }
}
