using _456VG_BE;
using _456VG_DAL;
using _456VG_Servicios;
using System.Collections.Generic;

namespace _456VG_BLL
{
    public class BLLChofer_456VG : IEntidades_456VG<BEChofer_456VG>
    {
        private readonly DALChofer_456VG dal;
        public BLLChofer_456VG()
        {
            dal = new DALChofer_456VG();
        }
        public List<BEChofer_456VG> leerEntidades456VG() => dal.leerEntidades456VG();
        public Resultado_456VG<BEChofer_456VG> crearEntidad456VG(BEChofer_456VG obj) => dal.crearEntidad456VG(obj);
        public Resultado_456VG<BEChofer_456VG> eliminarEntidad456VG(BEChofer_456VG obj) => dal.eliminarEntidad456VG(obj);
        public Resultado_456VG<BEChofer_456VG> actualizarEntidad456VG(BEChofer_456VG obj) => dal.actualizarEntidad456VG(obj);
    }
}
