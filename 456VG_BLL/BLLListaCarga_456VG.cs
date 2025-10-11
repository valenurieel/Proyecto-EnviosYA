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
    public class BLLListaCarga_456VG
    {
        private DALListaCarga_456VG dal = new DALListaCarga_456VG();
        public Resultado_456VG<BEListaCarga_456VG> crearEntidad456VG(BEListaCarga_456VG lista)
        {
            return dal.InsertarLista456VG(lista);
        }
        public List<BEListaCarga_456VG> leerEntidades456VG()
        {
            return dal.ObtenerListas456VG();
        }
        public Resultado_456VG<bool> actualizarEntidad456VG(BEListaCarga_456VG lista)
        {
            return dal.ActualizarLista456VG(lista);
        }
        public bool VerificarYLiberarRecursosPorEntrega456VG(string codEnvio)
        {
            return dal.VerificarYLiberarRecursosPorEntrega456VG(codEnvio);
        }
    }
}
