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
    public class BLLChoferes_C_456VG
    {
        DALChoferes_C_456VG dal;
        public BLLChoferes_C_456VG()
        {
            dal = new DALChoferes_C_456VG();
        }
        public List<BEChoferes_C_456VG> LeerCambios456VG(DateTime ini, DateTime fin, string dni = null, string nombre = null)
        {
            return dal.LeerCambios456VG(ini, fin, dni, nombre);
        }
        public Resultado_456VG<BEChoferes_C_456VG> CambiarEstadoChofer456VG(string dni, DateTime fechaSeleccionada)
        {
            return dal.CambiarEstadoChofer456VG(dni, fechaSeleccionada);
        }
    }
}
