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

namespace Proyecto_EnviosYA
{
    public partial class IniciarSesion_456VG : Form
    {
        public IniciarSesion_456VG()
        {
            InitializeComponent();
        }
        BLLUsuario_456VG BLLUsuario = new BLLUsuario_456VG();
        private void IniciarSesion_456VG_Load(object sender, EventArgs e)
        {

        }
        private int intentosFallidos = 0;
        private BEUsuario_456VG usuarioActual = null;
        private void btningresar_Click(object sender, EventArgs e)
        {
            if (txtdni.Text == string.Empty || txtcontraseña.Text == string.Empty)
            {
                MessageBox.Show("Complete los campos");
                return;
            }
            if (SessionManager_456VG.ObtenerInstancia().Usuario != null)
            {
                MessageBox.Show("Ya hay una sesión activa.");
                return;
            }
            Resultado_456VG<BEUsuario_456VG> resultUsuario = BLLUsuario.recuperarUsuarioPorDNI(txtdni.Text.Trim());
            if (!resultUsuario.resultado || resultUsuario.entidad == null)
            {
                MessageBox.Show("El DNI ingresado no es correcto o no existe.");
                return;
            }
            usuarioActual = resultUsuario.entidad;

            if (usuarioActual.Bloqueado)
            {
                MessageBox.Show("El usuario está bloqueado. Contacte a un administrador.");
                return;
            }
            HashSHA256_456VG hasheador = new HashSHA256_456VG();
            bool contraseñaCorrecta = hasheador.VerificarPassword(
                txtcontraseña.Text.Trim(),    
                usuarioActual.Contraseña,     
                usuarioActual.Salt    
            );
            if (!contraseñaCorrecta)
            {
                intentosFallidos++;
                MessageBox.Show("La contraseña es incorrecta.");
                if (intentosFallidos == 2)
                {
                    MessageBox.Show("Atención: Te queda una última oportunidad para ingresar la contraseña correcta.");
                }
                if (intentosFallidos >= 3)
                {
                    usuarioActual.Bloqueado = true;
                    BLLUsuario.bloquearUsuario(usuarioActual);
                    MessageBox.Show("Usuario bloqueado por superar los intentos fallidos.");
                    this.Close();
                }
                return;
            }
            SessionManager_456VG.ObtenerInstancia().IniciarSesion(usuarioActual);
            MessageBox.Show("Sesión iniciada correctamente");
            this.Hide();
        }
    }
}
