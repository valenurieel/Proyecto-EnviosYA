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
    public class BLLFamilia_456VG
    {
        DALFamilia_456VG dal;
        public BLLFamilia_456VG()
        {
            dal = new DALFamilia_456VG();
        }
        public List<BEFamilia_456VG> leerEntidades456VG()
                    => dal.leerEntidades456VG();

        public Resultado_456VG<BEFamilia_456VG> crearEntidad456VG(BEFamilia_456VG obj)
            => dal.crearEntidad456VG(obj);

        public Resultado_456VG<BEFamilia_456VG> actualizarEntidad456VG(BEFamilia_456VG obj)
            => dal.actualizarEntidad456VG(obj);

        public Resultado_456VG<BEFamilia_456VG> eliminarEntidad456VG(BEFamilia_456VG obj)
            => dal.eliminarEntidad456VG(obj);

        public List<BEPermisoComp_456VG> ObtenerRelacionesDeFamilia456VG(int idFamiliaPadre)
            => dal.ObtenerRelacionesDeFamilia456VG(idFamiliaPadre);

        public Resultado_456VG<BEPermisoComp_456VG> AgregarHijo456VG(int idPadre, int idHijo)
            => dal.AgregarHijo456VG(idPadre, idHijo);
        public Resultado_456VG<int> EliminarRelacion456VG(int idPadre, int idHijo)
        {
            return dal.EliminarRelacion456VG(idPadre, idHijo);
        }
    }
}
