using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _456VG_BLL;
using _456VG_Servicios;
using _456VG_BE;
using System.Windows.Media.Animation;

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
        private void IniciarSesion_456VG_Load(object sender, EventArgs e)
        {

        }
        private int intentosFallidos456VG = 0;
        private BEUsuario_456VG usuarioActual456VG = null;
        private void btningresar_Click(object sender, EventArgs e)
        {
            if (txtdni456VG.Text == string.Empty || txtcontraseña456VG.Text == string.Empty)
            {
                MessageBox.Show("Complete los campos");
                return;
            }
            if (SessionManager_456VG.ObtenerInstancia456VG().Usuario != null)
            {
                MessageBox.Show("Ya hay una sesión activa.");
                return;
            }
            Resultado_456VG<BEUsuario_456VG> resultUsuario = BLLUsuario.recuperarUsuarioPorDNI456VG(txtdni456VG.Text.Trim());
            if (!resultUsuario.resultado || resultUsuario.entidad == null)
            {
                MessageBox.Show("El DNI ingresado no es correcto o no existe.");
                return;
            }
            usuarioActual456VG = resultUsuario.entidad;
            if (usuarioActual456VG.Bloqueado456VG)
            {
                MessageBox.Show("El usuario está BLOQUEADO. Contacte a un Administrador.");
                return;
            }
            if (!usuarioActual456VG.Activo456VG)
            {
                MessageBox.Show("El usuario está DESACTIVADO. Contacte a un Administrador.");
                return;
            }
            HashSHA256_456VG hasheador = new HashSHA256_456VG();
            bool contraseñaCorrecta = hasheador.VerificarPassword456VG(
                txtcontraseña456VG.Text.Trim(),
                usuarioActual456VG.Contraseña456VG,
                usuarioActual456VG.Salt456VG
            );
            if (!contraseñaCorrecta)
            {
                intentosFallidos456VG++;
                MessageBox.Show("La contraseña es incorrecta.");
                if (intentosFallidos456VG == 2)
                {
                    MessageBox.Show("Atención: Te queda una última oportunidad para ingresar la contraseña correcta.");
                }
                if (intentosFallidos456VG >= 3)
                {
                    usuarioActual456VG.Bloqueado456VG = true;
                    BLLUsuario.bloquearUsuario456VG(usuarioActual456VG);
                    MessageBox.Show("Usuario BLOQUEADO por superar los intentos fallidos. Contacte a un Administrador.");
                    this.Close();
                }
                return;
            }
            SessionManager_456VG.ObtenerInstancia456VG().IniciarSesion456VG(usuarioActual456VG);
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
            if (checkVer456VG.Checked)
            {
                txtcontraseña456VG.PasswordChar = '\0';
            }
            else
            {
                txtcontraseña456VG.PasswordChar = '*';
            }
        }

        private void btnCancelar456VG_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
