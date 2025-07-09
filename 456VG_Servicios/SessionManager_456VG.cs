using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _456VG_BE;

namespace _456VG_Servicios
{
    public sealed class SessionManager_456VG
    {
        private static SessionManager_456VG Instancia = null;
        private static BEUsuario_456VG _user = null;
        private static bool inicióSesion = false;
        private static string _idiomaTemporal_456VG = "ES";
        private SessionManager_456VG() { }
        public static SessionManager_456VG ObtenerInstancia456VG()
        {
            if (Instancia == null)
            {
                Instancia = new SessionManager_456VG();
            }
            return Instancia;
        }
        public static string IdiomaTemporal_456VG
        {
            get => _idiomaTemporal_456VG;
            set => _idiomaTemporal_456VG = value;
        }
        public void IniciarSesion456VG(BEUsuario_456VG userNuevo)
        {
            _user = userNuevo;
            inicióSesion = true;
        }
        public static bool verificarInicioSesion456VG()
        {
            return inicióSesion;
        }
        public void CerrarSesion456VG()
        {
            if (Instancia != null)
            {
                _user = null;
                inicióSesion = false;
            }
        }
        public BEUsuario_456VG Usuario
        {
            get { return _user; }
        }
        public static BEUsuario_456VG Obtenerdatosuser456VG()
        {
            return _user;
        }
    }
}
