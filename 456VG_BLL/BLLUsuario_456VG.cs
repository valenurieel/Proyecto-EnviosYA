using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _456VG_DAL;
using _456VG_BE;
using _456VG_Servicios;

namespace _456VG_BLL
{
    public class BLLUsuario_456VG: IEntidades_456VG<BEUsuario_456VG>
    {
        DALUsuario_456VG dal;
        public BLLUsuario_456VG()
        {
            dal = new DALUsuario_456VG();
        }
        public List<BEUsuario_456VG> leerEntidades()
        {
            return dal.leerEntidades();
        }
        public Resultado_456VG<BEUsuario_456VG> crearEntidad(BEUsuario_456VG obj)
        {
            return dal.crearEntidad(obj);
        }
        public Resultado_456VG<BEUsuario_456VG> eliminarEntidad(BEUsuario_456VG obj)
        {
            return dal.eliminarEntidad(obj);
        }
        public Resultado_456VG<BEUsuario_456VG> actualizarEntidad(BEUsuario_456VG obj)
        {
            return dal.actualizarEntidad(obj);
        }
        public Resultado_456VG<BEUsuario_456VG> recuperarUsuario(string DNI, string Contraseña)
        {
            return dal.recuperarUsuario(DNI, Contraseña);
        }
    }
}
