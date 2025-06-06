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
        public string RecuperarIdioma456VG(string dniUsuario)
        {
            return dal.RecuperarIdioma456VG(dniUsuario);
        }
        public List<BEUsuario_456VG> leerEntidades456VG()
        {
            return dal.leerEntidades456VG();
        }
        public Resultado_456VG<BEUsuario_456VG> crearEntidad456VG(BEUsuario_456VG obj)
        {
            return dal.crearEntidad456VG(obj);
        }
        public Resultado_456VG<BEUsuario_456VG> eliminarEntidad456VG(BEUsuario_456VG obj)
        {
            return dal.eliminarEntidad456VG(obj);
        }
        public bool modificarIdioma456VG(BEUsuario_456VG obj, string nuevoIdioma)
        {
            bool result = dal.modificarIdioma456VG(obj, nuevoIdioma);
            return result;
        }
        public Resultado_456VG<BEUsuario_456VG> actualizarEntidad456VG(BEUsuario_456VG obj)
        {
            return dal.actualizarEntidad456VG(obj);
        }
        //public Resultado_456VG<BEUsuario_456VG> recuperarUsuario456VG(string DNI, string Contraseña)
        //{
        //    return dal.recuperarUsuario456VG(DNI, Contraseña);
        //}
        public Resultado_456VG<BEUsuario_456VG> recuperarUsuarioPorDNI456VG(string DNI)
        {
            return dal.recuperarUsuarioPorDNI456VG(DNI);
        }
        public Resultado_456VG<bool> bloquearUsuario456VG(BEUsuario_456VG usuario)
        {
            return dal.bloquearUsuario456VG(usuario);
        }
        public Resultado_456VG<bool> cambiarContraseña456VG(BEUsuario_456VG usuario, string nuevaContraseña)
        {
            return dal.cambiarContraseña456VG(usuario, nuevaContraseña);
        }
        public Resultado_456VG<BEUsuario_456VG> desbloquearUsuario456VG(string DNI)
        {
            return dal.desbloquearUsuario456VG(DNI);
        }
        public Resultado_456VG<BEUsuario_456VG> ActDesacUsuario456(string dni, bool nuevoEstadoActivo)
        {
            return dal.ActDesacUsuario456(dni, nuevoEstadoActivo);
        }
    }
}
