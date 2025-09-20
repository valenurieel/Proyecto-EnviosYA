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
    public class BLLSeguimientoEnvío_456VG
    {
        DALSeguimientoEnvío_456VG dal;
        public BLLSeguimientoEnvío_456VG()
        {
            dal = new DALSeguimientoEnvío_456VG();
        }
        public Resultado_456VG<BESeguimientoEnvío_456VG> CrearParaEnvio(string codEnvio)
            => dal.CrearParaEnvio456VG(codEnvio);

        public List<string> LeerCodEnviosElegibles()
            => dal.LeerCodEnviosElegibles456VG();

        public bool MarcarImpresoPorEnvio(string codEnvio)
            => dal.MarcarImpresoPorEnvio456VG(codEnvio);
        public string ObtenerCodSeguimientoNoImpresoPorEnvio(string codEnvio)
            => dal.ObtenerCodSeguimientoNoImpresoPorEnvio456VG(codEnvio);
        public bool ExistePorEnvioImpreso456VG(string codEnvio)
        {
            return dal.ExistePorEnvioImpreso456VG(codEnvio);
        }
    }
}
