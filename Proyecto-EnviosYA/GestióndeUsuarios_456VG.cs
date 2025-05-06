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
    public partial class GestióndeUsuarios_456VG : Form
    {
        BLLUsuario_456VG BLLUser = new BLLUsuario_456VG();
        public GestióndeUsuarios_456VG()
        {
            InitializeComponent();
            dataGridView1456VG.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1456VG_CellFormatting);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            limpiar456VG();
            label13456VG.Text = "Modo Añadir";
            txtdni456VG.Enabled = true;
            txtnombre456VG.Enabled = true;
            txtapellido456VG.Enabled = true;
            txtemail456VG.Enabled = true;
            txttelef456VG.Enabled = true;
            txtNameUser456VG.Enabled = false;
            txtdomicilio456VG.Enabled = true;
            cmbrol456VG.Enabled = true;
            btnAñadir456VG.Enabled = false;
            btnElim456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnDesbloq456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;
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
            btnAñadir456VG.Enabled = true;
            btnElim456VG.Enabled = true;
            btnModif456VG.Enabled = true;
            btnDesbloq456VG.Enabled = true;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = false;
            btnVolver456VG.Enabled = true;
            btnActivoDesac.Enabled = true;
            limpiar456VG();
            useractivos456VG();
            radioButton1456VG.Checked = true;
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
        }
        private void btnDesbloq_Click(object sender, EventArgs e)
        {
            limpiar456VG();
            label13456VG.Text = "Modo Desbloquear";
            txtdni456VG.Enabled = false;
            txtnombre456VG.Enabled = false;
            txtapellido456VG.Enabled = false;
            txtemail456VG.Enabled = false;
            txttelef456VG.Enabled = false;
            txtNameUser456VG.Enabled = false;
            txtdomicilio456VG.Enabled = false;
            cmbrol456VG.Enabled = false;
            btnAñadir456VG.Enabled = false;
            btnElim456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnDesbloq456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;
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
            limpiar456VG();
            label13456VG.Text = "Modo Eliminar";
            txtdni456VG.Enabled = false;
            txtnombre456VG.Enabled = false;
            txtapellido456VG.Enabled = false;
            txtemail456VG.Enabled = false;
            txttelef456VG.Enabled = false;
            txtNameUser456VG.Enabled = false;
            txtdomicilio456VG.Enabled = false;
            cmbrol456VG.Enabled = false;
            btnAñadir456VG.Enabled = false;
            btnElim456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnDesbloq456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;
            allusers456VG();
            radioButton2456VG.Checked = true;
        }
        private void btnModif_Click(object sender, EventArgs e)
        {
            label13456VG.Text = "Modo Modificar";
            limpiar456VG();
            txtdni456VG.Enabled = false;
            txtnombre456VG.Enabled = true;
            txtapellido456VG.Enabled = true;
            txtemail456VG.Enabled = true;
            txttelef456VG.Enabled = true;
            txtNameUser456VG.Enabled = true;
            txtdomicilio456VG.Enabled = true;
            cmbrol456VG.Enabled = false;
            btnAñadir456VG.Enabled = false;
            btnElim456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnDesbloq456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;
            allusers456VG();
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
            if(label13456VG.Text == "Modo Consulta")
            {
                radioButton1456VG.Checked = false;
                radioButton2456VG.Checked = false;
                List<BEUsuario_456VG> listaUsuarios = BLLUser.leerEntidades456VG();
                var filtro = listaUsuarios.Where(u =>
                    (string.IsNullOrWhiteSpace(txtdni456VG.Text) || u.DNI456VG.ToLower().Contains(txtdni456VG.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(txtnombre456VG.Text) || u.Nombre456VG.ToLower().Contains(txtnombre456VG.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(txtapellido456VG.Text) || u.Apellido456VG.ToLower().Contains(txtapellido456VG.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(txtemail456VG.Text) || u.Email456VG.ToLower().Contains(txtemail456VG.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(txttelef456VG.Text) || u.Teléfono456VG.ToLower().Contains(txttelef456VG.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(txtNameUser456VG.Text) || u.NombreUsuario456VG.ToLower().Contains(txtNameUser456VG.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(txtdomicilio456VG.Text) || u.Domicilio456VG.ToLower().Contains(txtdomicilio456VG.Text.ToLower())) &&
                    (cmbrol456VG.SelectedIndex == -1 || u.Rol456VG.ToLower().Contains(cmbrol456VG.SelectedItem.ToString().ToLower()))
                ).Select(u => new
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
                if (filtro.Count == 0)
                {
                    MessageBox.Show("No se encontraron usuarios con esos criterios.", "Resultado vacío", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    useractivos456VG();
                }
                else
                {
                    dataGridView1456VG.DataSource = filtro;
                }
            }
            if (label13456VG.Text == "Modo Eliminar")
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un Usuario para eliminar.");
                    return;
                }
                DataGridViewRow fila = dataGridView1456VG.SelectedRows[0];
                string dni = fila.Cells["DNI"].Value.ToString();
                var resultadoRecuperar = BLLUser.recuperarUsuarioPorDNI456VG(dni);
                if (!resultadoRecuperar.resultado)
                {
                    MessageBox.Show("Error al recuperar los datos del usuario: " + resultadoRecuperar.mensaje);
                    return;
                }
                BEUsuario_456VG usuarioAEliminar = resultadoRecuperar.entidad;
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro de eliminar el Usuario seleccionado?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (confirmacion == DialogResult.No)
                {
                    RegistrarUsuario_456VG_Load(null, null);
                    return;
                }
                var resultado = BLLUser.eliminarEntidad456VG(usuarioAEliminar);
                if (resultado.resultado)
                {
                    MessageBox.Show("Usuario eliminado correctamente.");
                    RegistrarUsuario_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Error al eliminar usuario: " + resultado.mensaje);
                }
            }
            if (label13456VG.Text == "Modo Modificar")
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un Usuario para Modificar.");
                    return;
                }
                string dniSeleccionado = dataGridView1456VG.SelectedRows[0].Cells["DNI"].Value.ToString();
                BEUsuario_456VG usuarioAActualizar = new BEUsuario_456VG
                (
                    dniSeleccionado,
                    txtnombre456VG.Text.Trim(),
                    txtapellido456VG.Text.Trim(),
                    txtemail456VG.Text.Trim(),
                    txttelef456VG.Text.Trim(),
                    txtNameUser456VG.Text.Trim(),
                    txtdomicilio456VG.Text.Trim()
                );
                Resultado_456VG<BEUsuario_456VG> resultado = BLLUser.actualizarEntidad456VG(usuarioAActualizar);
                if (resultado.resultado)
                {
                    MessageBox.Show("Usuario actualizado correctamente.");
                    RegistrarUsuario_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Error al actualizar el usuario: " + resultado.mensaje);
                }
            }
            if (label13456VG.Text == "Modo Activar / Desactivar")
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un Usuario para Activar o Desactivar.");
                    return;
                }
                string dniSeleccionado = dataGridView1456VG.SelectedRows[0].Cells["DNI"].Value.ToString();
                bool estadoActivo = Convert.ToBoolean(dataGridView1456VG.SelectedRows[0].Cells["Activo"].Value);
                bool nuevoEstadoActivo = !estadoActivo;
                var resultado = BLLUser.ActDesacUsuario456(dniSeleccionado, nuevoEstadoActivo);
                if (resultado.resultado)
                {
                    string estado = nuevoEstadoActivo ? "Activado" : "Desactivado";
                    MessageBox.Show($"Usuario {estado} correctamente.");
                    RegistrarUsuario_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Error: " + resultado.mensaje);
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
            //radioButton2456VG.Checked = true;
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
        private void dataGridView1456VG_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1456VG.SelectedRows.Count > 0)
            {
                string dniSeleccionado = dataGridView1456VG.SelectedRows[0].Cells["DNI"].Value.ToString();
                var resultadoRecuperar = BLLUser.recuperarUsuarioPorDNI456VG(dniSeleccionado);
                if (resultadoRecuperar.resultado)
                {
                    BEUsuario_456VG usuarioSeleccionado = resultadoRecuperar.entidad;
                    txtdni456VG.Text = usuarioSeleccionado.DNI456VG;
                    txtnombre456VG.Text = usuarioSeleccionado.Nombre456VG;
                    txtapellido456VG.Text = usuarioSeleccionado.Apellido456VG;
                    txtemail456VG.Text = usuarioSeleccionado.Email456VG;
                    txttelef456VG.Text = usuarioSeleccionado.Teléfono456VG;
                    txtNameUser456VG.Text = usuarioSeleccionado.NombreUsuario456VG;
                    txtdomicilio456VG.Text = usuarioSeleccionado.Domicilio456VG;
                    cmbrol456VG.SelectedItem = usuarioSeleccionado.Rol456VG;
                }
                else
                {
                    MessageBox.Show("Error al recuperar los datos del usuario: " + resultadoRecuperar.mensaje);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            limpiar456VG();
            label13456VG.Text = "Modo Activar / Desactivar";
            txtdni456VG.Enabled = false;
            txtnombre456VG.Enabled = false;
            txtapellido456VG.Enabled = false;
            txtemail456VG.Enabled = false;
            txttelef456VG.Enabled = false;
            txtNameUser456VG.Enabled = false;
            txtdomicilio456VG.Enabled = false;
            cmbrol456VG.Enabled = false;
            btnAñadir456VG.Enabled = false;
            btnElim456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnDesbloq456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;
            allusers456VG();
            radioButton2456VG.Checked = true;
        }
        private void dataGridView1456VG_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1456VG.Columns[e.ColumnIndex].Name == "Activo")
            {
                bool isActive = Convert.ToBoolean(e.Value);
                if (!isActive)
                {
                    dataGridView1456VG.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    dataGridView1456VG.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
