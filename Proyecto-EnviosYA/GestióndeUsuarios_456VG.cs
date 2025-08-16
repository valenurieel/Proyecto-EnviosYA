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
        BLLPerfil_456VG bllPerfil = new BLLPerfil_456VG();
        private Dictionary<string, BEPerfil_456VG> dicPerfilesTraducidos_456VG;
        public GestióndeUsuarios_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                  .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Consulta");
            TraducirEncabezadosDataGrid();
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
        }
        private void CargarPerfilesEnComboBox456VG()
        {
            var perfiles = bllPerfil.CargarCBPerfil456VG();
            dicPerfilesTraducidos_456VG = new Dictionary<string, BEPerfil_456VG>();
            cmbrol456VG.DataSource = null;
            cmbrol456VG.Items.Clear();
            foreach (var perfil in perfiles)
            {
                string claveTrad = $"GestióndeUsuarios_456VG.Combo.{perfil.Nombre456VG.Replace(" ", "")}";
                string nombreTraducido = Lenguaje_456VG.ObtenerInstancia_456VG().ObtenerTexto_456VG(claveTrad);
                cmbrol456VG.Items.Add(nombreTraducido);
                dicPerfilesTraducidos_456VG[nombreTraducido] = perfil;
            }
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
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            limpiar456VG();
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
                Rol = Lenguaje_456VG.ObtenerInstancia_456VG()
        .ObtenerTexto_456VG($"GestióndeUsuarios_456VG.Combo.{u.Rol456VG.Nombre456VG.Replace(" ", "")}"),
                Bloqueado = u.Bloqueado456VG,
                Activo = u.Activo456VG,
            }).ToList();
            dataGridView1456VG.DataSource = listaParaMostrar;
            TraducirEncabezadosDataGrid();
        }
        private void GestióndeUsuarios_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
            CargarPerfilesEnComboBox456VG();
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                  .ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Consulta");
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
            CargarPerfilesEnComboBox456VG();
            cmbrol456VG.SelectedIndex = -1;
        }
        private void btnDesbloq_Click(object sender, EventArgs e)
        {
            limpiar456VG();
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
                Rol = Lenguaje_456VG.ObtenerInstancia_456VG()
        .ObtenerTexto_456VG($"GestióndeUsuarios_456VG.Combo.{u.Rol456VG.Nombre456VG.Replace(" ", "")}"),
                Bloqueado = u.Bloqueado456VG,
                Activo = u.Activo456VG,
            }).ToList();
            dataGridView1456VG.DataSource = listaParaMostrar;
            TraducirEncabezadosDataGrid();
        }
        private void btnModif_Click(object sender, EventArgs e)
        {
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
            cmbrol456VG.Enabled = true;
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
        private bool DNIEstaRegistradoEnSistema(string dni)
        {
            return new BLLUsuario_456VG().leerEntidades456VG().Any(u => u.DNI456VG == dni)
                || new BLLCliente_456VG().leerEntidades456VG().Any(c => c.DNI456VG == dni)
                || new BLLEnvio_456VG().leerEntidades456VG().Any(e => e.DNIDest456VG == dni);
        }
        private void btnAplicar_Click(object sender, EventArgs e)
        {
            string modoActual = label13456VG.Text;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (modoActual == lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Desbloquear"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
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
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.UsuarioNoBloqueado"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
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
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.UsuarioDesbloqueadoOK"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
                    string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                    blleven.AddBitacora456VG(dni: dniLog, modulo: "Administrador", accion: "Desbloquear Usuario", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Crítico);
                    GestióndeUsuarios_456VG_Load(null, null);
                    return;
                }
                else
                {
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
            if (modoActual == lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Añadir"))
            {
                string emailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
                bool isValid = Regex.IsMatch(txtemail456VG.Text, emailPattern);
                if (!isValid)
                {
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
                bool bloqueado = false;
                bool activo = true;
                string idioma = "ES";
                var nombreTraducido = cmbrol456VG.SelectedItem as string;
                if (nombreTraducido == null || !dicPerfilesTraducidos_456VG.ContainsKey(nombreTraducido))
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.SeleccionePerfil"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                var perfilSeleccionado = dicPerfilesTraducidos_456VG[nombreTraducido];
                if (dni.Length != 8 || !dni.All(char.IsDigit))
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.DNIInvalido"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                if (telef.Length != 10 || !telef.All(char.IsDigit))
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.TelefonoInvalido"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                if (DNIEstaRegistradoEnSistema(txtdni456VG.Text.Trim()))
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.DNIRepetido"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                BEUsuario_456VG usernew = new BEUsuario_456VG(
                    dni, name, ape, email, telef,
                    nameuser, pass, domicilio, perfilSeleccionado,
                    bloqueado, activo, idioma
                );
                Resultado_456VG<BEUsuario_456VG> resultado = BLLUser.crearEntidad456VG(usernew);
                if (resultado.resultado)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.UsuarioRegistradoOK"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
                    string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                    blleven.AddBitacora456VG(dni: dniLog, modulo: "Administrador", accion: "Añadir Usuario", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Crítico);
                    GestióndeUsuarios_456VG_Load(null, null);
                }
                else
                {
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
                    (cmbrol456VG.SelectedIndex == -1 || u.Rol456VG.Nombre456VG.ToLower().Contains(cmbrol456VG.SelectedItem.ToString().ToLower()))
                ).Select(u => new
                {
                    DNI = u.DNI456VG,
                    Nombre = u.Nombre456VG,
                    Apellido = u.Apellido456VG,
                    Email = u.Email456VG,
                    Telefono = u.Teléfono456VG,
                    NombreUsuario = u.NombreUsuario456VG,
                    Domicilio = u.Domicilio456VG,
                    Rol = Lenguaje_456VG.ObtenerInstancia_456VG()
        .ObtenerTexto_456VG($"GestióndeUsuarios_456VG.Combo.{u.Rol456VG.Nombre456VG.Replace(" ", "")}"),
                    Bloqueado = u.Bloqueado456VG,
                    Activo = u.Activo456VG,
                }).ToList();
                if (filtro.Count == 0)
                {
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
            if (modoActual == lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.Modificar"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
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
                var nombreTraducido = cmbrol456VG.SelectedItem as string;
                if (nombreTraducido == null || !dicPerfilesTraducidos_456VG.ContainsKey(nombreTraducido))
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.SeleccionePerfil"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                var perfilSeleccionado = dicPerfilesTraducidos_456VG[nombreTraducido];
                BEUsuario_456VG usuarioAActualizar = new BEUsuario_456VG
                (
                    dniSeleccionado,
                    txtnombre456VG.Text.Trim(),
                    txtapellido456VG.Text.Trim(),
                    txtemail456VG.Text.Trim(),
                    txttelef456VG.Text.Trim(),
                    txtNameUser456VG.Text.Trim(),
                    txtdomicilio456VG.Text.Trim(),
                    perfilSeleccionado
                );
                Resultado_456VG<BEUsuario_456VG> resultado = BLLUser.actualizarEntidad456VG(usuarioAActualizar);
                if (resultado.resultado)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.UsuarioActualizadoOK"),
                        lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
                    string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                    blleven.AddBitacora456VG(dni: dniLog, modulo: "Administrador", accion: "Modificar Usuario", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Crítico);
                    if (dniSeleccionado == SessionManager_456VG.Obtenerdatosuser456VG().DNI456VG)
                    {
                        MessageBox.Show(
                            lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.CambioRolSesion"),
                            lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Msg.ResultadoVacio"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        var idiomaFinal = SessionManager_456VG.IdiomaTemporal_456VG;
                        var usuarioActual = SessionManager_456VG.Obtenerdatosuser456VG();
                        new BLLUsuario_456VG().modificarIdioma456VG(usuarioActual, idiomaFinal);
                        SessionManager_456VG.ObtenerInstancia456VG().CerrarSesion456VG();
                        blleven.AddBitacora456VG(dni: dniLog, modulo: "Usuario", accion: "Cerrar Sesión", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Crítico);
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
                    GestióndeUsuarios_456VG_Load(null, null);
                }
                else
                {
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
            if (modoActual == lng.ObtenerTexto_456VG("GestióndeUsuarios_456VG.Modo.ActDesac"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
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
                    BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
                    string accionLog = nuevoEstadoActivo ? "Activar Cliente" : "Desactivar Cliente";
                    string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                    blleven.AddBitacora456VG(dni: dniLog, modulo: "Administrador", accionLog, crit: BEEventoBitacora_456VG.NVCriticidad456VG.Crítico);
                    GestióndeUsuarios_456VG_Load(null, null);
                }
                else
                {
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
                Rol = Lenguaje_456VG.ObtenerInstancia_456VG()
        .ObtenerTexto_456VG($"GestióndeUsuarios_456VG.Combo.{u.Rol456VG.Nombre456VG.Replace(" ", "")}"),
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
                    foreach (var kvp in dicPerfilesTraducidos_456VG)
                    {
                        if (kvp.Value.Nombre456VG == usuarioSeleccionado.Rol456VG.Nombre456VG)
                        {
                            cmbrol456VG.SelectedItem = kvp.Key;
                            break;
                        }
                    }
                }
                else
                {
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
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            CargarPerfilesEnComboBox456VG();
        }
    }
}
