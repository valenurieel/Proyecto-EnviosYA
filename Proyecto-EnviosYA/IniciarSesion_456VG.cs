using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using _456VG_BLL;
using _456VG_Servicios;
using _456VG_BE;

namespace Proyecto_EnviosYA
{
    public partial class IniciarSesion_456VG : Form
    {
        public IniciarSesion_456VG()
        {
            InitializeComponent();
        }
        private void label456VG()
        {
            MenuPrincipal_456VG menu = Application.OpenForms.OfType<MenuPrincipal_456VG>().FirstOrDefault();
            if (menu != null)
            {
                menu.bienvenido456VG();
            }
        }
        BLLUsuario_456VG BLLUsuario = new BLLUsuario_456VG();
        private Dictionary<string, int> intentosFallidosPorUsuario = new Dictionary<string, int>();
        private void btningresar_Click(object sender, EventArgs e)
        {
            string dni = txtdni456VG.Text.Trim();
            string contraseña = txtcontraseña456VG.Text.Trim();
            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Complete los campos");
                return;
            }
            if (SessionManager_456VG.ObtenerInstancia456VG().Usuario != null)
            {
                MessageBox.Show("Ya hay una sesión activa.");
                return;
            }
            Resultado_456VG<BEUsuario_456VG> resultUsuario = BLLUsuario.recuperarUsuarioPorDNI456VG(dni);
            if (!resultUsuario.resultado || resultUsuario.entidad == null)
            {
                MessageBox.Show("El DNI ingresado no es correcto o no existe.");
                return;
            }
            BEUsuario_456VG usuario = resultUsuario.entidad;
            if (usuario.Bloqueado456VG)
            {
                MessageBox.Show("El usuario está BLOQUEADO. Contacte a un Administrador.");
                return;
            }
            if (!usuario.Activo456VG)
            {
                MessageBox.Show("El usuario está DESACTIVADO. Contacte a un Administrador.");
                return;
            }
            HashSHA256_456VG hasheador = new HashSHA256_456VG();
            bool contraseñaCorrecta = hasheador.VerificarPassword456VG(contraseña, usuario.Contraseña456VG, usuario.Salt456VG);
            if (!contraseñaCorrecta)
            {
                if (!intentosFallidosPorUsuario.ContainsKey(dni))
                    intentosFallidosPorUsuario[dni] = 0;
                intentosFallidosPorUsuario[dni]++;
                MessageBox.Show("La contraseña es incorrecta.");
                if (intentosFallidosPorUsuario[dni] == 2)
                {
                    MessageBox.Show("Atención: Te queda una última oportunidad para ingresar la contraseña correcta.");
                }
                if (intentosFallidosPorUsuario[dni] >= 3)
                {
                    usuario.Bloqueado456VG = true;
                    BLLUsuario.bloquearUsuario456VG(usuario);
                    MessageBox.Show("Usuario BLOQUEADO por superar los intentos fallidos. Contacte a un Administrador.");
                    this.Close();
                }
                return;
            }
            SessionManager_456VG.ObtenerInstancia456VG().IniciarSesion456VG(usuario);
            intentosFallidosPorUsuario.Clear();
            MessageBox.Show("Sesión iniciada exitosamente");
            MenuPrincipal_456VG menu = Application.OpenForms.OfType<MenuPrincipal_456VG>().FirstOrDefault();
            if (menu != null)
            {
                menu.HabilitarOpcionesMenu456VG();
            }
            label456VG();
            this.Hide();
        }
        private void checkVer_CheckedChanged(object sender, EventArgs e)
        {
            txtcontraseña456VG.PasswordChar = checkVer456VG.Checked ? '\0' : '*';
        }
        private void btnCancelar456VG_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
