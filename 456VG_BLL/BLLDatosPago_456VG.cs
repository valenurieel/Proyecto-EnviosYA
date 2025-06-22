using _456VG_BE;
using _456VG_DAL;
using _456VG_Servicios;
using System;
using System.Collections.Generic;

namespace _456VG_BLL
{
    public class BLLDatosPago_456VG : IEntidades_456VG<BEDatosPago_456VG>
    {
        private readonly DALDatosPago_456VG dal;

        public BLLDatosPago_456VG()
        {
            dal = new DALDatosPago_456VG();
        }
        public List<BEDatosPago_456VG> leerEntidades456VG()
        {
            return dal.leerEntidades456VG();
        }
        public Resultado_456VG<BEDatosPago_456VG> crearEntidad456VG(BEDatosPago_456VG datosPago)
        {
            return dal.crearEntidad456VG(datosPago);
        }
        public Resultado_456VG<BEDatosPago_456VG> eliminarEntidad456VG(BEDatosPago_456VG obj)
        {
            return dal.eliminarEntidad456VG(obj);
        }
        public Resultado_456VG<BEDatosPago_456VG> actualizarEntidad456VG(BEDatosPago_456VG obj)
        {
            return dal.actualizarEntidad456VG(obj);
        }
        public BEDatosPago_456VG LeerPorDni456VG(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return null;
            return dal.LeerPorDni456VG(dni);
        }
    }
}
