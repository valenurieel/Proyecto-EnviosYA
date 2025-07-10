using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using _456VG_BLL;
using _456VG_Servicios;
using _456VG_BE;

namespace Proyecto_EnviosYA
{
    public partial class IniciarSesion_456VG : Form, IObserver_456VG
    {
        BLLUsuario_456VG BLLUsuario = new BLLUsuario_456VG();
        private Dictionary<string, int> intentosFallidosPorUsuario = new Dictionary<string, int>();
        public event EventHandler LoginExitoso;
        public IniciarSesion_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
        }
        private void btningresar_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string dni = txtdni456VG.Text.Trim();
            string contraseña = txtcontraseña456VG.Text.Trim();
            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.CompleteCampos"),
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (SessionManager_456VG.ObtenerInstancia456VG().Usuario != null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.SesionActiva"),
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }
            Resultado_456VG<BEUsuario_456VG> resultUsuario = BLLUsuario.recuperarUsuarioPorDNI456VG(dni);
            if (!resultUsuario.resultado || resultUsuario.entidad == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.DniIncorrecto"),
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            BEUsuario_456VG usuario = resultUsuario.entidad;
            if (usuario.Bloqueado456VG)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.UsuarioBloqueado"),
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop
                );
                return;
            }
            if (!usuario.Activo456VG)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.UsuarioDesactivado"),
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            HashSHA256_456VG hasheador = new HashSHA256_456VG();
            bool contraseñaCorrecta = hasheador.VerificarPassword456VG(contraseña, usuario.Contraseña456VG, usuario.Salt456VG);
            if (!contraseñaCorrecta)
            {
                if (!intentosFallidosPorUsuario.ContainsKey(dni))
                    intentosFallidosPorUsuario[dni] = 0;
                intentosFallidosPorUsuario[dni]++;
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.ContraseñaIncorrecta"),
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                if (intentosFallidosPorUsuario[dni] == 2)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.UltimaOportunidad"),
                        lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                if (intentosFallidosPorUsuario[dni] >= 3)
                {
                    usuario.Bloqueado456VG = true;
                    BLLUsuario.bloquearUsuario456VG(usuario);
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.UsuarioBloqueadoIntentos"),
                        lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop
                    );
                    this.Close();
                }
                return;
            }
            string idiomaUsuario = BLLUsuario.RecuperarIdioma456VG(usuario.DNI456VG);
            SessionManager_456VG.IdiomaTemporal_456VG = idiomaUsuario;
            Lenguaje_456VG.ObtenerInstancia_456VG().IdiomaActual_456VG = idiomaUsuario;
            BEUsuario_456VG usuarioConPermisos = BLLUsuario.recuperarUsuarioConPerfil456VG(usuario.DNI456VG);
            if (usuarioConPermisos == null || usuarioConPermisos.Rol456VG == null || usuarioConPermisos.Rol456VG.Permisos456VG == null || !usuarioConPermisos.Rol456VG.Permisos456VG.Any())
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.PerfilInexistente"),
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                SessionManager_456VG.ObtenerInstancia456VG().CerrarSesion456VG();
                Lenguaje_456VG.ObtenerInstancia_456VG().IdiomaActual_456VG = "ES";
                SessionManager_456VG.IdiomaTemporal_456VG = "ES";
                var menu = Application.OpenForms.OfType<MenuPrincipal_456VG>().FirstOrDefault();
                if (menu != null)
                {
                    menu.deshabilitados();
                    menu.chau();
                }
                this.Close();
                return;
            }
            MessageBox.Show(
                lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.SesionIniciada"),
                lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            SessionManager_456VG.ObtenerInstancia456VG().IniciarSesion456VG(usuarioConPermisos);
            intentosFallidosPorUsuario.Clear();
            LoginExitoso?.Invoke(this, EventArgs.Empty);
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
        private void IniciarSesion_456VG_Load(object sender, EventArgs e)
        {
            txtcontraseña456VG.Text = "123";
            txtdni456VG.Text = "45984456";
            ActualizarIdioma_456VG();
        }
    }
}
