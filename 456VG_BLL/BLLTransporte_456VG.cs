using _456VG_BE;
using _456VG_DAL;
using _456VG_Servicios;
using System.Collections.Generic;

namespace _456VG_BLL
{
    public class BLLTransporte_456VG : IEntidades_456VG<BETransporte_456VG>
    {
        private readonly DALTransporte_456VG dal;
        public BLLTransporte_456VG()
        {
            dal = new DALTransporte_456VG();
        }
        public List<BETransporte_456VG> leerEntidades456VG() => dal.leerEntidades456VG();
        public Resultado_456VG<BETransporte_456VG> crearEntidad456VG(BETransporte_456VG obj) => dal.crearEntidad456VG(obj);
        public Resultado_456VG<BETransporte_456VG> eliminarEntidad456VG(BETransporte_456VG obj) => dal.eliminarEntidad456VG(obj);
        public Resultado_456VG<BETransporte_456VG> actualizarEntidad456VG(BETransporte_456VG obj) => dal.actualizarEntidad456VG(obj);
        public Resultado_456VG<BETransporte_456VG> ActivarDesactivarEntidad456VG(string patente, bool nuevoEstado)
        {
            return dal.ActDesacTrans456(patente, nuevoEstado);
        }
    }
}
