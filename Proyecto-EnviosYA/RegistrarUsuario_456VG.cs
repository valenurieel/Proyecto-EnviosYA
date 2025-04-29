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
            label13.Text = "Modo Añadir";
            txtdni.Enabled = true;
            txtnombre.Enabled = true;
            txtapellido.Enabled = true;
            txtemail.Enabled = true;
            txttelef.Enabled = true;
            txtNameUser.Enabled = false;
            txtdomicilio.Enabled = true;
            cmbrol.Enabled = true;
            rdbutonSI.Enabled = false;
            rdbutonNO.Enabled = false;
            btnAñadir.Enabled = false;
            btnElim.Enabled = false;
            btnModif.Enabled = false;
            btnDesbloq.Enabled = false;
            btnAplicar.Enabled = true;
            btnCancelar.Enabled = true;
            btnVolver.Enabled = false;
        }
        private void allusers()
        {
            List<BEUsuario_456VG> listuser = BLLUser.leerEntidades();
            var listaParaMostrar = listuser.Select(u => new
            {
                DNI = u.DNI,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Email = u.Email,
                Telefono = u.Teléfono,
                NombreUsuario = u.NombreUsuario,
                Domicilio = u.Domicilio,
                Rol = u.Rol,
                Bloqueado = u.Bloqueado,
                Activo = u.Activo
            }).ToList();
            dataGridView1.DataSource = listaParaMostrar;
        }
        private void RegistrarUsuario_456VG_Load(object sender, EventArgs e)
        {
            label13.Text = "Modo Consulta";
            txtdni.Enabled = true;
            txtnombre.Enabled = true;
            txtapellido.Enabled = true;
            txtemail.Enabled = true;
            txttelef.Enabled = true;
            txtNameUser.Enabled = true;
            txtdomicilio.Enabled = true;
            cmbrol.Enabled = true;
            rdbutonSI.Enabled = true;
            rdbutonNO.Enabled = true;
            btnAñadir.Enabled = true;
            btnElim.Enabled = true;
            btnModif.Enabled = true;
            btnDesbloq.Enabled = true;
            btnAplicar.Enabled = true;
            btnCancelar.Enabled = false;
            btnVolver.Enabled = true;
            limpiar();
            allusers();
        }
        private void limpiar()
        {
            txtdni.Text = "";
            txtnombre.Text = "";
            txtapellido.Text = "";
            txtemail.Text = "";
            txttelef.Text = "";
            txtNameUser.Text = "";
            txtdomicilio.Text = "";
            cmbrol.SelectedItem = null;
            rdbutonSI.Checked = false;
            rdbutonNO.Checked = false;
        }
        private void btnDesbloq_Click(object sender, EventArgs e)
        {
            label13.Text = "Modo Desbloquear";
            txtdni.Enabled = false;
            txtnombre.Enabled = false;
            txtapellido.Enabled = false;
            txtemail.Enabled = false;
            txttelef.Enabled = false;
            txtNameUser.Enabled = false;
            txtdomicilio.Enabled = false;
            cmbrol.Enabled = false;
            rdbutonSI.Enabled = false;
            rdbutonNO.Enabled = false;
            btnAñadir.Enabled = false;
            btnElim.Enabled = false;
            btnModif.Enabled = false;
            btnDesbloq.Enabled = false;
            btnAplicar.Enabled = true;
            btnCancelar.Enabled = true;
            btnVolver.Enabled = false;
            List<BEUsuario_456VG> listaUsuarios = BLLUser.leerEntidades();
            var bloqueados = listaUsuarios.Where(u => u.Bloqueado).ToList();
            if (bloqueados.Count == 0)
            {
                MessageBox.Show("No hay usuarios bloqueados");
                dataGridView1.DataSource = null;
                return;
            }
            var listaParaMostrar = bloqueados.Select(u => new
            {
                DNI = u.DNI,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Email = u.Email,
                Telefono = u.Teléfono,
                NombreUsuario = u.NombreUsuario,
                Domicilio = u.Domicilio,
                Rol = u.Rol,
                Bloqueado = u.Bloqueado,
                Activo = u.Activo
            }).ToList();
            dataGridView1.DataSource = listaParaMostrar;
        }

        private void btnElim_Click(object sender, EventArgs e)
        {
            label13.Text = "Modo Eliminar";
        }

        private void btnModif_Click(object sender, EventArgs e)
        {
            label13.Text = "Modo Modificar";
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if(label13.Text == "Modo Desbloquear")
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un Usuario Bloqueado.");
                    return;
                }
                string dniSeleccionado = dataGridView1.SelectedRows[0].Cells["DNI"].Value.ToString();
                bool estaBloqueado = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells["Bloqueado"].Value);
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
                var resultado = BLLUser.desbloquearUsuario(dniSeleccionado);
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
            if(label13.Text == "Modo Añadir")
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
                string nameuser = this.txtdni.Text + txtapellido.Text;
                string domicilio = txtdomicilio.Text;
                string pass = this.txtdni.Text + txtnombre.Text;
                string rol = cmbrol.SelectedItem?.ToString();
                bool bloqueado = false;
                bool activo = true;
                BEUsuario_456VG usernew = new BEUsuario_456VG(dni, name, ape, email, telef, nameuser, pass, domicilio, rol, bloqueado, activo);
                Resultado_456VG<BEUsuario_456VG> resultado = BLLUser.crearEntidad(usernew);
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
            allusers();
        }
        private void useractivos()
        {
            List<BEUsuario_456VG> listaUsuariosActivos = BLLUser.leerEntidades().Where(u => u.Activo).ToList();
            var listaParaMostrar = listaUsuariosActivos.Select(u => new
            {
                DNI = u.DNI,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Email = u.Email,
                Telefono = u.Teléfono,
                NombreUsuario = u.NombreUsuario,
                Domicilio = u.Domicilio,
                Rol = u.Rol,
                Bloqueado = u.Bloqueado,
                Activo = u.Activo
            }).ToList();
            dataGridView1.DataSource = listaParaMostrar;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            useractivos();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
