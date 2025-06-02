using _456VG_BE;
using _456VG_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using _456VG_Servicios;

namespace Proyecto_EnviosYA
{
    public partial class GestióndeUsuarios_456VG : Form, IObserver_456VG
    {
        BLLUsuario_456VG BLLUser = new BLLUsuario_456VG();

        public GestióndeUsuarios_456VG()
        {
            InitializeComponent();
            // Nos suscribimos para que, cuando cambie el idioma, se actualicen controles
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }

        public void ActualizarIdioma_456VG()
        {
            // 1) Traduce todos los controles visibles (botones, labels fijos, etc.)
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);

            // 2) Después, actualizamos el texto dinámico "Modo Consulta" y los encabezados
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                  .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Consulta");
            TraducirEncabezadosDataGrid();

            // 3) REPOBLAR el ComboBox de Roles (cada vez que cambia el idioma)
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            cmbrol456VG.Items.Clear();
            cmbrol456VG.Items.Add(lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Combo.Administrador"));
            cmbrol456VG.Items.Add(lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Combo.EmpleadoAdministrativo"));
            cmbrol456VG.Items.Add(lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Combo.Cajero"));

            cmbrol456VG.SelectedIndex = -1;
        }


        private void TraducirEncabezadosDataGrid()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            foreach (DataGridViewColumn col in dataGridView1456VG.Columns)
            {
                string clave = $"GestióndeUsuarios_456VG.Columna.{col.Name}";
                col.HeaderText = lng.ObtenerTexto_456VG(clave);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // (Vacío en el original)
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            limpiar456VG();

            // Modo Añadir (texto obtenido del JSON)
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                  .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Añadir");

            txtdni456VG.Enabled = true;
            txtnombre456VG.Enabled = true;
            txtapellido456VG.Enabled = true;
            txtemail456VG.Enabled = true;
            txttelef456VG.Enabled = true;
            txtNameUser456VG.Enabled = false;
            txtdomicilio456VG.Enabled = true;
            cmbrol456VG.Enabled = true;

            btnAñadir456VG.Enabled = false;
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
                Activo = u.Activo456VG,
            }).ToList();

            dataGridView1456VG.DataSource = listaParaMostrar;
            TraducirEncabezadosDataGrid();
        }

        private void GestióndeUsuarios_456VG_Load(object sender, EventArgs e)
        {
            // 1) Traduce todos los controles estáticos del formulario
            ActualizarIdioma_456VG();

            // 2) Texto inicial de “Modo Consulta”
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                  .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Consulta");

            // 3) Habilito/Deshabilito controles según tu lógica original
            txtdni456VG.Enabled = true;
            txtnombre456VG.Enabled = true;
            txtapellido456VG.Enabled = true;
            txtemail456VG.Enabled = true;
            txttelef456VG.Enabled = true;
            txtNameUser456VG.Enabled = true;
            txtdomicilio456VG.Enabled = true;
            cmbrol456VG.Enabled = true;

            btnAñadir456VG.Enabled = true;
            btnModif456VG.Enabled = true;
            btnDesbloq456VG.Enabled = true;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = false;
            btnVolver456VG.Enabled = true;
            btnActivoDesac.Enabled = true;

            limpiar456VG();
            useractivos456VG();
            radioButton1456VG.Checked = true;
            radioButton1456VG.Enabled = true;
            radioButton2456VG.Enabled = true;
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

            // Modo Desbloquear
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                    .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Desbloquear");

            txtdni456VG.Enabled = false;
            txtnombre456VG.Enabled = false;
            txtapellido456VG.Enabled = false;
            txtemail456VG.Enabled = false;
            txttelef456VG.Enabled = false;
            txtNameUser456VG.Enabled = false;
            txtdomicilio456VG.Enabled = false;
            cmbrol456VG.Enabled = false;

            btnAñadir456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnDesbloq456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;

            radioButton1456VG.Checked = false;
            radioButton2456VG.Checked = false;
            radioButton1456VG.Enabled = false;
            radioButton2456VG.Enabled = false;

            List<BEUsuario_456VG> listaUsuarios = BLLUser.leerEntidades456VG();
            var bloqueados = listaUsuarios.Where(u => u.Bloqueado456VG).ToList();
            if (bloqueados.Count == 0)
            {
                // Mensaje traducido: "No hay usuarios bloqueados"
                MessageBox.Show(
                    Lenguaje_456VG.ObtenerInstancia_456VG()
                      .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.NoHayUsuariosBloqueados"),
                    Lenguaje_456VG.ObtenerInstancia_456VG()
                      .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                GestióndeUsuarios_456VG_Load(null, null);
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
                Activo = u.Activo456VG,
            }).ToList();

            dataGridView1456VG.DataSource = listaParaMostrar;
            TraducirEncabezadosDataGrid();
        }

        private void btnModif_Click(object sender, EventArgs e)
        {
            // Modo Modificar
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                    .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Modificar");
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
            btnModif456VG.Enabled = false;
            btnDesbloq456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;

            radioButton1456VG.Enabled = false;
            radioButton1456VG.Checked = false;
            radioButton2456VG.Checked = true;

            allusers456VG();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            string modoActual = label13456VG.Text;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            // --- MODO Desbloquear ---
            if (modoActual == lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Desbloquear"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    // "Debe seleccionar un Usuario Bloqueado."
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.UsuarioNoBloqueado"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                string dniSeleccionado = dataGridView1456VG
                                            .SelectedRows[0]
                                            .Cells["DNI"]
                                            .Value
                                            .ToString();

                bool estaBloqueado = Convert.ToBoolean(
                    dataGridView1456VG
                      .SelectedRows[0]
                      .Cells["Bloqueado"]
                      .Value
                );

                if (!estaBloqueado)
                {
                    // "El Usuario NO se encuentra Bloqueado."
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.UsuarioNoBloqueado"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // "¿Está seguro de Desbloquear este Usuario?"
                DialogResult confirmacion = MessageBox.Show(
                    lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ConfirmDesbloquear"),
                    lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ConfirmTitle"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.No)
                {
                    GestióndeUsuarios_456VG_Load(null, null);
                    return;
                }

                var resultado = BLLUser.desbloquearUsuario456VG(dniSeleccionado);
                if (resultado.resultado)
                {
                    // "Usuario desbloqueado exitosamente."
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.UsuarioDesbloqueadoOK"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    GestióndeUsuarios_456VG_Load(null, null);
                    return;
                }
                else
                {
                    // "Error: {0}"
                    MessageBox.Show(
                        string.Format(
                          lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ErrorDesbloquear"),
                          resultado.mensaje
                        ),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

            // --- MODO Añadir ---
            if (modoActual == lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Añadir"))
            {
                string emailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
                bool isValid = Regex.IsMatch(txtemail456VG.Text, emailPattern);
                if (!isValid)
                {
                    // "Debe ingresar un email válido"
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.DebeEmailValido"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                string dni = txtdni456VG.Text;
                string name = txtnombre456VG.Text;
                string ape = txtapellido456VG.Text;
                string email = txtemail456VG.Text;
                string telef = txttelef456VG.Text;
                string nameuser = txtdni456VG.Text + txtapellido456VG.Text;
                string domicilio = txtdomicilio456VG.Text;
                string pass = txtdni456VG.Text + txtnombre456VG.Text;
                string rol = cmbrol456VG.SelectedItem?.ToString();
                bool bloqueado = false;
                bool activo = true;
                string idioma = "ES";

                BEUsuario_456VG usernew = new BEUsuario_456VG(
                    dni, name, ape, email, telef,
                    nameuser, pass, domicilio, rol,
                    bloqueado, activo, idioma
                );

                Resultado_456VG<BEUsuario_456VG> resultado = BLLUser.crearEntidad456VG(usernew);
                if (resultado.resultado)
                {
                    // "Usuario registrado correctamente."
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.UsuarioRegistradoOK"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    GestióndeUsuarios_456VG_Load(null, null);
                }
                else
                {
                    // "Error al registrar el usuario: {0}"
                    MessageBox.Show(
                        string.Format(
                          lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ErrorRegistrar"),
                          resultado.mensaje
                        ),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

            // --- MODO Consulta ---
            if (modoActual == lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Consulta"))
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
                    Activo = u.Activo456VG,
                }).ToList();

                if (filtro.Count == 0)
                {
                    // "No se encontraron usuarios con esos criterios."
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.NoUsuariosCriterio"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    useractivos456VG();
                }
                else
                {
                    dataGridView1456VG.DataSource = filtro;
                    TraducirEncabezadosDataGrid();
                }
            }

            // --- MODO Modificar ---
            if (modoActual == lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Modificar"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    // "Debe seleccionar un Usuario para Modificar."
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.SeleccioneUsuarioModificar"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                string dniSeleccionado = dataGridView1456VG
                                            .SelectedRows[0]
                                            .Cells["DNI"]
                                            .Value
                                            .ToString();

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
                    // "Usuario actualizado correctamente."
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.UsuarioActualizadoOK"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    GestióndeUsuarios_456VG_Load(null, null);
                }
                else
                {
                    // "Error al actualizar el usuario: {0}"
                    MessageBox.Show(
                        string.Format(
                          lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ErrorActualizar"),
                          resultado.mensaje
                        ),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

            // --- MODO Activar / Desactivar ---
            if (modoActual == lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.ActDesac"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    // "Debe seleccionar un Usuario para Activar o Desactivar."
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.SeleccioneUsuarioActDesac"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                string dniSeleccionado = dataGridView1456VG
                                            .SelectedRows[0]
                                            .Cells["DNI"]
                                            .Value
                                            .ToString();

                bool estadoActivo = Convert.ToBoolean(
                                            dataGridView1456VG
                                              .SelectedRows[0]
                                              .Cells["Activo"]
                                              .Value
                                         );
                bool nuevoEstadoActivo = !estadoActivo;

                var resultado = BLLUser.ActDesacUsuario456(dniSeleccionado, nuevoEstadoActivo);
                if (resultado.resultado)
                {
                    string claveEstado = nuevoEstadoActivo
                                           ? "GestióndeUsuarios_456VG.Msg.UsuarioActivado"
                                           : "GestióndeUsuarios_456VG.Msg.UsuarioDesactivado";

                    MessageBox.Show(
                        lng.ObtenerTexto_456VG(claveEstado),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    GestióndeUsuarios_456VG_Load(null, null);
                }
                else
                {
                    // "Error: {0}"
                    MessageBox.Show(
                        string.Format(
                          lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ErrorActDesac"),
                          resultado.mensaje
                        ),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            DialogResult confirmacion = MessageBox.Show(
                lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ConfirmCancel"),
                lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ConfirmTitle"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirmacion == DialogResult.Yes)
            {
                GestióndeUsuarios_456VG_Load(null, null);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            allusers456VG();
        }

        private void useractivos456VG()
        {
            List<BEUsuario_456VG> listaUsuariosActivos = BLLUser.leerEntidades456VG()
                .Where(u => u.Activo456VG).ToList();

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
                Activo = u.Activo456VG,
            }).ToList();

            dataGridView1456VG.DataSource = listaParaMostrar;
            TraducirEncabezadosDataGrid();
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
                string dniSeleccionado = dataGridView1456VG
                                            .SelectedRows[0]
                                            .Cells["DNI"]
                                            .Value
                                            .ToString();
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
                    // "Error al recuperar los datos del usuario: {0}"
                    MessageBox.Show(
                        string.Format(
                          Lenguaje_456VG.ObtenerInstancia_456VG()
                            .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ErrorRecuperar"),
                          resultadoRecuperar.mensaje
                        ),
                        Lenguaje_456VG.ObtenerInstancia_456VG()
                          .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiar456VG();
            // Modo Activar / Desactivar
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                  .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.ActDesac");

            txtdni456VG.Enabled = false;
            txtnombre456VG.Enabled = false;
            txtapellido456VG.Enabled = false;
            txtemail456VG.Enabled = false;
            txttelef456VG.Enabled = false;
            txtNameUser456VG.Enabled = false;
            txtdomicilio456VG.Enabled = false;
            cmbrol456VG.Enabled = false;

            btnAñadir456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnDesbloq456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;

            radioButton1456VG.Enabled = false;
            radioButton1456VG.Checked = false;
            radioButton2456VG.Checked = true;

            allusers456VG();
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

        private void dataGridView1456VG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
