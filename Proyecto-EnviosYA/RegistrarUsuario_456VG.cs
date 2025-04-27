using _456VG_BE;
using _456VG_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using _456VG_Servicios;

namespace Proyecto_EnviosYA
{
    public partial class RegistrarUsuario_456VG : Form
    {
        public RegistrarUsuario_456VG()
        {
            InitializeComponent();
        }
        BLLUsuario_456VG BLLUser = new BLLUsuario_456VG();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string emailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
            bool isValid = Regex.IsMatch(txtemail.Text, emailPattern);
            if (!isValid)
            {
                MessageBox.Show("Debe ingresar un email válido");
                return;
            }
            string dni = this.txtdni.Text;
            string name = txtnombre.Text;
            string ape = txtapellido.Text;
            string email = txtemail.Text;
            string telef = txttelef.Text;
            string nameuser = this.txtdni.Text+txtapellido.Text;
            string domicilio = txtdomicilio.Text;
            string pass = this.txtdni.Text+txtnombre.Text;
            string rol = cmbrol.SelectedItem?.ToString();
            bool bloqueado = false;
            bool activo = true;
            BEUsuario_456VG usernew = new BEUsuario_456VG(dni, name, ape, email, telef, nameuser, pass, domicilio, rol, bloqueado, activo);
            Resultado_456VG<BEUsuario_456VG> resultado = BLLUser.crearEntidad(usernew);
            if (resultado.resultado)
            {
                MessageBox.Show("Usuario registrado correctamente.");
                this.Close();
            }
            else
            {
                MessageBox.Show($"Error al registrar el usuario: {resultado.mensaje}");
            }
        }
    }
}
