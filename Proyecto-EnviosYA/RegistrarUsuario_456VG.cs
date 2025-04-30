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
        BLLUsuario_456VG BLLUser = new BLLUsuario_456VG();
        public RegistrarUsuario_456VG()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            label13456VG.Text = "Modo Añadir";
            txtdni456VG.Enabled = true;
            txtnombre456VG.Enabled = true;
            txtapellido456VG.Enabled = true;
            txtemail456VG.Enabled = true;
            txttelef456VG.Enabled = true;
            txtNameUser456VG.Enabled = false;
            txtdomicilio456VG.Enabled = true;
            cmbrol456VG.Enabled = true;
            rdbutonSI456VG.Enabled = false;
            rdbutonNO456VG.Enabled = false;
            btnAñadir456VG.Enabled = false;
            btnElim456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnDesbloq456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
        }
        private void allusers456VG()
        {
            List<BEUsuario_456VG> listuser = BLLUser.leerEntidades456VG();
            var listaParaMostrar = listuser.Select(u => new
            {
                DNI = u.DNI456VG,
                Nombre = u.Nombre456VG,
                Apellido = u.Apellido456VG,
                Email = u.Email456VG,
                Telefono = u.Teléfono456VG,
                NombreUsuario = u.NombreUsuario456VG,
                Domicilio = u.Domicilio456VG,
                Rol = u.Rol456VG,
                Bloqueado = u.Bloqueado456VG,
                Activo = u.Activo456VG
            }).ToList();
            dataGridView1456VG.DataSource = listaParaMostrar;
        }
        private void RegistrarUsuario_456VG_Load(object sender, EventArgs e)
        {
            label13456VG.Text = "Modo Consulta";
            txtdni456VG.Enabled = true;
            txtnombre456VG.Enabled = true;
            txtapellido456VG.Enabled = true;
            txtemail456VG.Enabled = true;
            txttelef456VG.Enabled = true;
            txtNameUser456VG.Enabled = true;
            txtdomicilio456VG.Enabled = true;
            cmbrol456VG.Enabled = true;
            rdbutonSI456VG.Enabled = true;
            rdbutonNO456VG.Enabled = true;
            btnAñadir456VG.Enabled = true;
            btnElim456VG.Enabled = true;
            btnModif456VG.Enabled = true;
            btnDesbloq456VG.Enabled = true;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = false;
            btnVolver456VG.Enabled = true;
            limpiar456VG();
            allusers456VG();
        }
        private void limpiar456VG()
        {
            txtdni456VG.Text = "";
            txtnombre456VG.Text = "";
            txtapellido456VG.Text = "";
            txtemail456VG.Text = "";
            txttelef456VG.Text = "";
            txtNameUser456VG.Text = "";
            txtdomicilio456VG.Text = "";
            cmbrol456VG.SelectedIndex = -1;
            rdbutonSI456VG.Checked = false;
            rdbutonNO456VG.Checked = false;
        }
        private void btnDesbloq_Click(object sender, EventArgs e)
        {
            label13456VG.Text = "Modo Desbloquear";
            txtdni456VG.Enabled = false;
            txtnombre456VG.Enabled = false;
            txtapellido456VG.Enabled = false;
            txtemail456VG.Enabled = false;
            txttelef456VG.Enabled = false;
            txtNameUser456VG.Enabled = false;
            txtdomicilio456VG.Enabled = false;
            cmbrol456VG.Enabled = false;
            rdbutonSI456VG.Enabled = false;
            rdbutonNO456VG.Enabled = false;
            btnAñadir456VG.Enabled = false;
            btnElim456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnDesbloq456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            List<BEUsuario_456VG> listaUsuarios = BLLUser.leerEntidades456VG();
            var bloqueados = listaUsuarios.Where(u => u.Bloqueado456VG).ToList();
            if (bloqueados.Count == 0)
            {
                MessageBox.Show("No hay usuarios bloqueados");
                dataGridView1456VG.DataSource = null;
                return;
            }
            var listaParaMostrar = bloqueados.Select(u => new
            {
                DNI = u.DNI456VG,
                Nombre = u.Nombre456VG,
                Apellido = u.Apellido456VG,
                Email = u.Email456VG,
                Telefono = u.Teléfono456VG,
                NombreUsuario = u.NombreUsuario456VG,
                Domicilio = u.Domicilio456VG,
                Rol = u.Rol456VG,
                Bloqueado = u.Bloqueado456VG,
                Activo = u.Activo456VG
            }).ToList();
            dataGridView1456VG.DataSource = listaParaMostrar;
        }

        private void btnElim_Click(object sender, EventArgs e)
        {
            label13456VG.Text = "Modo Eliminar";
        }

        private void btnModif_Click(object sender, EventArgs e)
        {
            label13456VG.Text = "Modo Modificar";
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if(label13456VG.Text == "Modo Desbloquear")
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un Usuario Bloqueado.");
                    return;
                }
                string dniSeleccionado = dataGridView1456VG.SelectedRows[0].Cells["DNI"].Value.ToString();
                bool estaBloqueado = Convert.ToBoolean(dataGridView1456VG.SelectedRows[0].Cells["Bloqueado"].Value);
                if (!estaBloqueado)
                {
                    MessageBox.Show("El Usuario NO se encuentra Bloqueado.");
                    return;
                }
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro de Desbloquear este Usuario?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (confirmacion == DialogResult.No)
                {
                    RegistrarUsuario_456VG_Load(null, null);
                    return;
                }
                var resultado = BLLUser.desbloquearUsuario456VG(dniSeleccionado);
                if (resultado.resultado)
                {
                    MessageBox.Show("Usuario desbloqueado exitosamente.");
                    RegistrarUsuario_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Error: " + resultado.mensaje);
                }
            }
            if(label13456VG.Text == "Modo Añadir")
            {
                string emailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
                bool isValid = Regex.IsMatch(txtemail456VG.Text, emailPattern);
                if (!isValid)
                {
                    MessageBox.Show("Debe ingresar un email válido");
                    return;
                }
                string dni = this.txtdni456VG.Text;
                string name = txtnombre456VG.Text;
                string ape = txtapellido456VG.Text;
                string email = txtemail456VG.Text;
                string telef = txttelef456VG.Text;
                string nameuser = this.txtdni456VG.Text + txtapellido456VG.Text;
                string domicilio = txtdomicilio456VG.Text;
                string pass = this.txtdni456VG.Text + txtnombre456VG.Text;
                string rol = cmbrol456VG.SelectedItem?.ToString();
                bool bloqueado = false;
                bool activo = true;
                BEUsuario_456VG usernew = new BEUsuario_456VG(dni, name, ape, email, telef, nameuser, pass, domicilio, rol, bloqueado, activo);
                Resultado_456VG<BEUsuario_456VG> resultado = BLLUser.crearEntidad456VG(usernew);
                if (resultado.resultado)
                {
                    MessageBox.Show("Usuario registrado correctamente.");
                    RegistrarUsuario_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show($"Error al registrar el usuario: {resultado.mensaje}");
                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de Cancelar la Operación?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirmacion == DialogResult.Yes)
            {
                RegistrarUsuario_456VG_Load(null, null);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            allusers456VG();
        }
        private void useractivos456VG()
        {
            List<BEUsuario_456VG> listaUsuariosActivos = BLLUser.leerEntidades456VG().Where(u => u.Activo456VG).ToList();
            var listaParaMostrar = listaUsuariosActivos.Select(u => new
            {
                DNI = u.DNI456VG,
                Nombre = u.Nombre456VG,
                Apellido = u.Apellido456VG,
                Email = u.Email456VG,
                Telefono = u.Teléfono456VG,
                NombreUsuario = u.NombreUsuario456VG,
                Domicilio = u.Domicilio456VG,
                Rol = u.Rol456VG,
                Bloqueado = u.Bloqueado456VG,
                Activo = u.Activo456VG
            }).ToList();
            dataGridView1456VG.DataSource = listaParaMostrar;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            useractivos456VG();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
