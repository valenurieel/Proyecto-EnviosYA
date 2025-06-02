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
        // Lógica de negocio para usuarios
        BLLUsuario_456VG BLLUsuario = new BLLUsuario_456VG();

        // Para contar intentos fallidos por DNI
        private Dictionary<string, int> intentosFallidosPorUsuario = new Dictionary<string, int>();

        public IniciarSesion_456VG()
        {
            InitializeComponent();
            // Nos suscribimos para que, al cambiar idioma, se recarguen los textos
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }

        public void ActualizarIdioma_456VG()
        {
            // Traducir todos los controles visibles (labels, botones, checkbox, etc.)
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
        }

        private void label456VG()
        {
            // Cuando inicie sesión, actualizo el lblBienvenido del menú principal
            MenuPrincipal_456VG menu = Application.OpenForms
                                          .OfType<MenuPrincipal_456VG>()
                                          .FirstOrDefault();
            if (menu != null)
            {
                menu.bienvenido456VG();
            }
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            string dni = txtdni456VG.Text.Trim();
            string contraseña = txtcontraseña456VG.Text.Trim();

            // 1) Validar campos vacíos
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

            // 2) Verificar si ya hay sesión activa
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

            // 3) Recuperar usuario por DNI
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

            // 4) Verificar si está bloqueado
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

            // 5) Verificar si está desactivado
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

            // 6) Verificar contraseña
            HashSHA256_456VG hasheador = new HashSHA256_456VG();
            bool contraseñaCorrecta = hasheador.VerificarPassword456VG(
                contraseña,
                usuario.Contraseña456VG,
                usuario.Salt456VG
            );

            if (!contraseñaCorrecta)
            {
                // Incrementar contador de fallos para este DNI
                if (!intentosFallidosPorUsuario.ContainsKey(dni))
                    intentosFallidosPorUsuario[dni] = 0;
                intentosFallidosPorUsuario[dni]++;

                // Mostrar aviso de contraseña incorrecta
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.ContraseñaIncorrecta"),
                    lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // Si ya hubo 2 fallos, advertir última oportunidad
                if (intentosFallidosPorUsuario[dni] == 2)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.UltimaOportunidad"),
                        lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }

                // Si acumula 3 o más, bloqueamos al usuario
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

            // 7) Si la contraseña es correcta: iniciar sesión, limpiar contadores y notificar
            SessionManager_456VG.ObtenerInstancia456VG().IniciarSesion456VG(usuario);
            intentosFallidosPorUsuario.Clear();

            MessageBox.Show(
                lng.ObtenerTexto_456VG("IniciarSesion_456VG.Msg.SesionIniciada"),
                lng.ObtenerTexto_456VG("IniciarSesion_456VG.Text"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Habilitar opciones del menú principal
            MenuPrincipal_456VG menu = Application.OpenForms
                                          .OfType<MenuPrincipal_456VG>()
                                          .FirstOrDefault();
            if (menu != null)
            {
                menu.HabilitarOpcionesMenu456VG();
            }

            // Actualizar etiqueta “Bienvenido” en el menú
            label456VG();

            // Ocultar este formulario
            this.Hide();
        }

        private void checkVer_CheckedChanged(object sender, EventArgs e)
        {
            // Mostrar / Ocultar texto de contraseña
            txtcontraseña456VG.PasswordChar = checkVer456VG.Checked ? '\0' : '*';
        }

        private void btnCancelar456VG_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IniciarSesion_456VG_Load(object sender, EventArgs e)
        {
            // Al cargar, traducir todos los controles
            ActualizarIdioma_456VG();
        }
    }
}
