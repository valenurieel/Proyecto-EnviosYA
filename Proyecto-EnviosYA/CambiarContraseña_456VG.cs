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
        BEUsuario_456VG user = SessionManager_456VG.Obtenerdatosuser456VG();
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
            bool contraseñaCorrecta = hash.VerificarPassword456VG(txtContraAct456VG.Text, user.Contraseña456VG, user.Salt456VG);
            if (!contraseñaCorrecta)
            {
                MessageBox.Show("La Contraseña actual ingresada NO es correcta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtContraAct456VG.Text == txtContraNew456VG.Text)
            {
                MessageBox.Show("La Nueva Contraseña NO puede ser la Contraseña actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtContraNew456VG.Text != txtContraConfirm456VG.Text)
            {
                MessageBox.Show("Las Nuevas Contraseñas NO coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var resultado = BLLUser.cambiarContraseña456VG(user, txtContraNew456VG.Text);
                if (resultado.resultado)
                {
                    MessageBox.Show("Su Contraseña ha sido cambiada correctamente.\nPor favor, vuelva a iniciar sesión con su Nueva Contraseña.",
                                    "Cambio exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SessionManager_456VG.ObtenerInstancia456VG().CerrarSesion456VG();
                    MenuPrincipal_456VG menu = Application.OpenForms.OfType<MenuPrincipal_456VG>().FirstOrDefault();
                    if (menu != null)
                    {
                        menu.deshabilitados();
                    }
                    this.Hide();
                    IniciarSesion_456VG frm = new IniciarSesion_456VG();
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                    label();
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Error al cambiar la Contraseña: {resultado.mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
