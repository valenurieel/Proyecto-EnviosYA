﻿using System;
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
        private SessionManager_456VG() { }
        public static SessionManager_456VG ObtenerInstancia()
        {
            if (Instancia == null)
            {
                Instancia = new SessionManager_456VG();
            }
            return Instancia;
        }
        public void IniciarSesion(BEUsuario_456VG userNuevo)
        {
            if (Instancia != null)
            {
                _user = userNuevo;
                inicióSesion = true;
                return;
            }
        }
        public static bool verificarInicioSesion()
        {
            return inicióSesion;
        }
        public void CerrarSesion()
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
        public static BEUsuario_456VG Obtenerdatosuser()
        {
            return _user;
        }
    }
}
