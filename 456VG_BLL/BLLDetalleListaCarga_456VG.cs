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
    public class BLLDetalleListaCarga_456VG
    {
        private DALDetalleListaCarga_456VG dal = new DALDetalleListaCarga_456VG();
        public Resultado_456VG<BEDetalleListaCarga_456VG> crearEntidad456VG(BEDetalleListaCarga_456VG detalle)
        {
            return dal.InsertarDetalle456VG(detalle);
        }
        public List<BEDetalleListaCarga_456VG> leerEntidades456VG()
        {
            return dal.ObtenerDetalles456VG();
        }
        public Resultado_456VG<bool> actualizarEntidad456VG(BEDetalleListaCarga_456VG detalle)
        {
            return dal.ActualizarDetalle456VG(detalle);
        }
    }
}
