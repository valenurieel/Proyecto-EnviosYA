using _456VG_BE;
using _456VG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BLL
{
    public class BLLDigitoVerificador_456VG
    {
        private readonly DALDigitoVerificador_456VG dal;
        public BLLDigitoVerificador_456VG()
        {
            dal = new DALDigitoVerificador_456VG();
        }
        public void ActualizarDV456VG()
        {
            dal.ActualizarDV456VG();
        }
        public BEDigitoVerificador_456VG LeerDV456VG()
        {
            return dal.LeerDV456VG();
        }
        public (string DVH, string DVV) CalcularDV456VG()
        {
            return dal.CalcularDVGeneral456VG();
        }
    }
}
