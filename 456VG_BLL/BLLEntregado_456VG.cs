using _456VG_BE;
using _456VG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BLL
{
    public class BLLEntregado_456VG
    {
        DALEntregado_456VG dal;
        public BLLEntregado_456VG()
        {
            dal = new DALEntregado_456VG();
        }
        public bool RegistrarEntrega456VG(BEEntregado_456VG entrega)
        {
            return dal.InsertarEntrega456VG(entrega);
        }
        public int ObtenerIntentosPorEnvio456VG(string codEnvio)
        {
            return dal.ContarIntentosPorEnvio456VG(codEnvio);
        }
    }
}
