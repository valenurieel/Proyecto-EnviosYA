using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;

namespace Proyecto_EnviosYA
{
    public partial class CambiarContraseña_456VG : Form
    {
        BLLUsuario_456VG BLLUser = new BLLUsuario_456VG();
        BEUsuario_456VG user = SessionManager_456VG.Obtenerdatosuser();
        HashSHA256_456VG hash = new HashSHA256_456VG();
        public CambiarContraseña_456VG()
        {
            InitializeComponent();
        }
        private void label()
        {
            MenuPrincipal_456VG menu = Application.OpenForms.OfType<MenuPrincipal_456VG>().FirstOrDefault();
            if (menu != null)
            {
                menu.chau();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContraAct456VG.Text) ||
                string.IsNullOrWhiteSpace(txtContraNew456VG.Text) ||
                string.IsNullOrWhiteSpace(txtContraConfirm456VG.Text))
            {
                MessageBox.Show("Complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool contraseñaCorrecta = hash.VerificarPassword(txtContraAct456VG.Text, user.Contraseña, user.Salt);
            if (!contraseñaCorrecta)
            {
                MessageBox.Show("La contraseña actual ingresada no es correcta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtContraAct456VG.Text == txtContraNew456VG.Text)
            {
                MessageBox.Show("La nueva contraseña no puede ser la contraseña actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtContraNew456VG.Text != txtContraConfirm456VG.Text)
            {
                MessageBox.Show("Las nuevas contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var resultado = BLLUser.cambiarContraseña(user, txtContraNew456VG.Text);
                if (resultado.resultado)
                {
                    MessageBox.Show("Su contraseña ha sido cambiada correctamente.\nPor favor, vuelva a iniciar sesión con su nueva contraseña.",
                                    "Cambio exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SessionManager_456VG.ObtenerInstancia().CerrarSesion();
                    IniciarSesion_456VG frm = new IniciarSesion_456VG();
                    frm.Show();
                    label();
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Error al cambiar la contraseña: {resultado.mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CambiarContraseña_456VG_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
