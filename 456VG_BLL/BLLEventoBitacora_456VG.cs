using _456VG_BE;
using _456VG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BLL
{
    public class BLLEventoBitacora_456VG
    {
        DALEventoBitacora_456VG dal;
        public BLLEventoBitacora_456VG()
        {
            dal = new DALEventoBitacora_456VG();
        }
        public void AddBitacora456VG(string dni, string modulo, string accion, BEEventoBitacora_456VG.NVCriticidad456VG crit, DateTime? fecha = null)
        {
            dal.Insertar456VG(new BEEventoBitacora_456VG(dni, modulo, accion, crit, fecha));
        }
        public List<BEEventoBitacora_456VG> GetBitacora456VG(int? criticidad, DateTime? desde, DateTime? hasta, string modulo = null, string dni = null, string accion = null)
        {
            return dal.Seleccionar456VG(criticidad, desde, hasta, modulo, dni, accion);
        }
        public List<string> TraerUsers456VG()
        {
            return dal.SelectCmbUsers456VG();
        }
    }
}
