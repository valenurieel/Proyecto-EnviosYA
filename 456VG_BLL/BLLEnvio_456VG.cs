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
    public class BLLEnvio_456VG: IEntidades_456VG<BEEnvío_456VG>
    {
        DALEnvio_456VG dal;
        public BLLEnvio_456VG()
        {
            dal = new DALEnvio_456VG();
        }
        public List<BEEnvío_456VG> leerEntidades456VG()
        {
            return dal.leerEntidades456VG();
        }
        public Resultado_456VG<BEEnvío_456VG> crearEntidad456VG(BEEnvío_456VG obj)
        {
            return dal.crearEntidad456VG(obj);
        }
        public Resultado_456VG<BEEnvío_456VG> eliminarEntidad456VG(BEEnvío_456VG obj)
        {
            return dal.eliminarEntidad456VG(obj);
        }
        public Resultado_456VG<BEEnvío_456VG> actualizarEntidad456VG(BEEnvío_456VG obj)
        {
            return dal.actualizarEntidad456VG(obj);
        }
    }
}
