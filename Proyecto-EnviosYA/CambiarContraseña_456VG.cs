﻿using System;
using System.Linq;
using System.Windows.Forms;
using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;

namespace Proyecto_EnviosYA
{
    public partial class CambiarContraseña_456VG : Form, IObserver_456VG
    {
        BLLUsuario_456VG BLLUser = new BLLUsuario_456VG();
        BEUsuario_456VG user = SessionManager_456VG.Obtenerdatosuser456VG();
        HashSHA256_456VG hash = new HashSHA256_456VG();
        public CambiarContraseña_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
        }
        private void label()
        {
            MenuPrincipal_456VG menu = Application.OpenForms
                                          .OfType<MenuPrincipal_456VG>()
                                          .FirstOrDefault();
            if (menu != null)
            {
                menu.chau();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (string.IsNullOrWhiteSpace(txtContraAct456VG.Text) ||
                string.IsNullOrWhiteSpace(txtContraNew456VG.Text) ||
                string.IsNullOrWhiteSpace(txtContraConfirm456VG.Text))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.CompleteCampos"),
                    lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.ErrorTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            bool contraseñaCorrecta = hash.VerificarPassword456VG(
                txtContraAct456VG.Text,
                user.Contraseña456VG,
                user.Salt456VG
            );
            if (!contraseñaCorrecta)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.ContraseñaActualIncorrecta"),
                    lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.ErrorTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            if (txtContraAct456VG.Text == txtContraNew456VG.Text)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.NuevaIgualActual"),
                    lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.ErrorTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            if (txtContraNew456VG.Text != txtContraConfirm456VG.Text)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.ContraseñasNoCoinciden"),
                    lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.ErrorTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            try
            {
                var resultado = BLLUser.cambiarContraseña456VG(user, txtContraNew456VG.Text);
                if (resultado.resultado)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.ExitoMensaje"),
                        lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.CambioExitosoTitle"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    SessionManager_456VG.ObtenerInstancia456VG().CerrarSesion456VG();
                    MenuPrincipal_456VG menu = Application.OpenForms
                                                  .OfType<MenuPrincipal_456VG>()
                                                  .FirstOrDefault();
                    if (menu != null)
                    {
                        menu.deshabilitados();
                    }
                    this.Hide();
                    IniciarSesion_456VG frm = new IniciarSesion_456VG
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    frm.ShowDialog();
                    label();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        string.Format(
                            lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.ErrorAlCambiar"),
                            resultado.mensaje
                        ),
                        lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.ErrorTitle"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format(
                        lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.ErrorInesperado"),
                        ex.Message
                    ),
                    lng.ObtenerTexto_456VG("CambiarContraseña_456VG.Msg.ErrorTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void CambiarContraseña_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void checkVer456VG_CheckedChanged(object sender, EventArgs e)
        {
            if (checkVer456VG.Checked)
            {
                txtContraAct456VG.PasswordChar = '\0';
                txtContraNew456VG.PasswordChar = '\0';
                txtContraConfirm456VG.PasswordChar = '\0';
            }
            else
            {
                txtContraAct456VG.PasswordChar = '*';
                txtContraNew456VG.PasswordChar = '*';
                txtContraConfirm456VG.PasswordChar = '*';
            }
        }
    }
}
