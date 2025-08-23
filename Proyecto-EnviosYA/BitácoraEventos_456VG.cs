using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class BitácoraEventos_456VG : Form, IObserver_456VG
    {
        BLLEventoBitacora_456VG bll = new BLLEventoBitacora_456VG();
        BLLUsuario_456VG blluser = new BLLUsuario_456VG();
        private bool _muteSelectionChanged = false;
        private string T(string key) => Lenguaje_456VG.ObtenerInstancia_456VG().ObtenerTexto_456VG(key);
        public BitácoraEventos_456VG()
        {
            InitializeComponent();
            ConfigurarGrilla456VG();
            CargarCombos456VG();
            CargarTodo456VG();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        private static readonly Dictionary<string, string> MOD_KEY456VG = new Dictionary<string, string>
        {
            ["Administrador"] = "BitácoraEventos_456VG.Combo.Administrador",
            ["Maestro"] = "BitácoraEventos_456VG.Combo.Maestro",
            ["Usuario"] = "BitácoraEventos_456VG.Combo.Usuario",
            ["Recepción"] = "BitácoraEventos_456VG.Combo.Recepción",
            ["Envíos"] = "BitácoraEventos_456VG.Combo.Envíos",
            ["Reportes"] = "BitácoraEventos_456VG.Combo.Reportes",
            ["Ayuda"] = "BitácoraEventos_456VG.Combo.Ayuda",
            ["Salir"] = "BitácoraEventos_456VG.Combo.Salir",
        };
        private static readonly Dictionary<string, string> ACC_KEY456VG = new Dictionary<string, string>
        {
            ["Iniciar Sesión"] = "BitácoraEventos_456VG.Combo.IniciarSesión",
            ["Cerrar Sesión"] = "BitácoraEventos_456VG.Combo.CerrarSesión",
            ["Cambiar Contraseña"] = "BitácoraEventos_456VG.Combo.CambiarContraseña",
            ["Crear Envío"] = "BitácoraEventos_456VG.Combo.CrearEnvío",
            ["Cargar Paquete"] = "BitácoraEventos_456VG.Combo.CargarPaquete",
            ["Modificar Cliente"] = "BitácoraEventos_456VG.Combo.ModificarCliente",
            ["Añadir Cliente"] = "BitácoraEventos_456VG.Combo.AñadirCliente",
            ["Activar Cliente"] = "BitácoraEventos_456VG.Combo.ActivarCliente",
            ["Desactivar Cliente"] = "BitácoraEventos_456VG.Combo.DesactivarCliente",
            ["Cobrar Envío"] = "BitácoraEventos_456VG.Combo.CobrarEnvío",
            ["Modificar Usuario"] = "BitácoraEventos_456VG.Combo.ModificarUsuario",
            ["Añadir Usuario"] = "BitácoraEventos_456VG.Combo.AñadirUsuario",
            ["Activar Usuario"] = "BitácoraEventos_456VG.Combo.ActivarUsuario",
            ["Desactivar Usuario"] = "BitácoraEventos_456VG.Combo.DesactivarUsuario",
            ["Desbloquear Usuario"] = "BitácoraEventos_456VG.Combo.DesbloquearUsuario",
            ["Eliminar Perfil"] = "BitácoraEventos_456VG.Combo.EliminarPerfil",
            ["Crear Perfil"] = "BitácoraEventos_456VG.Combo.CrearPerfil",
            ["Agregar Permiso - Perfil"] = "BitácoraEventos_456VG.Combo.AgregarPermisoPerfil",
            ["Quitar Permiso - Perfil"] = "BitácoraEventos_456VG.Combo.QuitarPermisoPerfil",
            ["Agregar Familia - Perfil"] = "BitácoraEventos_456VG.Combo.AgregarFamiliaPerfil",
            ["Quitar Familia - Perfil"] = "BitácoraEventos_456VG.Combo.QuitarFamiliaPerfil",
            ["Eliminar Familia"] = "BitácoraEventos_456VG.Combo.EliminarFamilia",
            ["Crear Familia"] = "BitácoraEventos_456VG.Combo.CrearFamilia",
            ["Agregar Familia - Familia"] = "BitácoraEventos_456VG.Combo.AgregarFamiliaFamilia",
            ["Quitar Familia - Familia"] = "BitácoraEventos_456VG.Combo.QuitarFamiliaFamilia",
            ["Agregar Permiso - Familia"] = "BitácoraEventos_456VG.Combo.AgregarPermisoFamilia",
            ["Quitar Permiso - Familia"] = "BitácoraEventos_456VG.Combo.QuitarPermisoFamilia",
            ["Backup"] = "BitácoraEventos_456VG.Combo.Backup",
            ["Restaurar"] = "BitácoraEventos_456VG.Combo.Restaurar",
            ["Imprimir Factura"] = "BitácoraEventos_456VG.Combo.ImprimirFactura",
        };
        private string TradModulo456VG(string orig) => MOD_KEY456VG.TryGetValue(orig ?? "", out var k) ? T(k) : orig;
        private string TradAccion456VG(string orig) => ACC_KEY456VG.TryGetValue(orig ?? "", out var k) ? T(k) : orig;
        //destraduce para poder usar el valor original en base
        private string ModuloOriginal456VG(string texto) => MOD_KEY456VG.FirstOrDefault(kv => string.Equals(TradModulo456VG(kv.Key), texto, StringComparison.OrdinalIgnoreCase)).Key ?? texto;
        //destraduce para poder usar el valor original en base
        private string AccionOriginal456VG(string texto) => ACC_KEY456VG.FirstOrDefault(kv => string.Equals(TradAccion456VG(kv.Key), texto, StringComparison.OrdinalIgnoreCase)).Key ?? texto;
        //traduce grilla de DGV

        private void TraducirGrilla456VG()
        {
            if (dgvBitacora.Columns.Contains("Login"))
                dgvBitacora.Columns["Login"].HeaderText = T("BitácoraEventos_456VG.Columna.Login");
            if (dgvBitacora.Columns.Contains("Fecha"))
                dgvBitacora.Columns["Fecha"].HeaderText = T("BitácoraEventos_456VG.Columna.Fecha");
            if (dgvBitacora.Columns.Contains("Módulo"))
                dgvBitacora.Columns["Módulo"].HeaderText = T("BitácoraEventos_456VG.Columna.Módulo");
            if (dgvBitacora.Columns.Contains("Acción"))
                dgvBitacora.Columns["Acción"].HeaderText = T("BitácoraEventos_456VG.Columna.Acción");
            if (dgvBitacora.Columns.Contains("Criticidad"))
                dgvBitacora.Columns["Criticidad"].HeaderText = T("BitácoraEventos_456VG.Columna.Criticidad");
        }
        //para seleccionar un item de un cmb traducido
        private void SelecCMBTexto456VG(ComboBox cmb, string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { cmb.SelectedIndex = -1; return; }
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (cmb.Items[i] is string orig)
                {
                    string display = orig;
                    if (cmb == cmbModulo) display = TradModulo456VG(orig);
                    else if (cmb == cmbAccion) display = TradAccion456VG(orig);
                    if (string.Equals(orig, text, StringComparison.OrdinalIgnoreCase) || string.Equals(display, text, StringComparison.OrdinalIgnoreCase))
                    {
                        cmb.SelectedIndex = i;
                        return;
                    }
                }
            }
            if (cmb.DropDownStyle != ComboBoxStyle.DropDownList) cmb.Text = text; else cmb.SelectedIndex = -1;
        }
        //carga Nombre y Apellido en los labels
        private void PersonaNomApePorDNI456VG(string dni)
        {
            name.Text = ""; ape.Text = "";
            if (string.IsNullOrWhiteSpace(dni)) return;
            var res = blluser.recuperarUsuarioPorDNI456VG(dni);
            if (res.resultado && res.entidad != null)
            {
                name.Text = res.entidad.Nombre456VG;
                ape.Text = res.entidad.Apellido456VG;
            }
        }
        public void ActualizarIdioma_456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            lng.CambiarIdiomaControles_456VG(this);
            if (dgvBitacora.DataSource != null)
            {
                int? crit = (cmbCriticidad.SelectedIndex >= 0) ? (int?)cmbCriticidad.SelectedValue : null;
                DateTime? desde = dtpDesde.Checked ? (DateTime?)dtpDesde.Value.Date : null;
                DateTime? hasta = dtpHasta.Checked ? (DateTime?)dtpHasta.Value.Date.AddDays(1).AddTicks(-1) : null;
                string modulo = cmbModulo.SelectedItem as string;
                string accion = cmbAccion.SelectedItem as string;
                string dni = (cmbLogin.SelectedIndex >= 0) ? (string)cmbLogin.SelectedValue : null;
                var datos = bll.GetBitacora456VG(crit, desde, hasta, modulo, dni, accion);
                ArmadoDGV456VG(datos);
            }
            else TraducirGrilla456VG();
        }
        private void BitácoraEventos_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ConfigurarGrilla456VG()
        {
            dgvBitacora.AutoGenerateColumns = true;
            dgvBitacora.ReadOnly = true;
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.AllowUserToDeleteRows = false;
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBitacora.MultiSelect = false;
        }
        //carga los cmb y los pone traducidos
        private void CargarCombos456VG()
        {
            var usuarios = blluser.leerEntidades456VG()
                                  .GroupBy(u => u.DNI456VG)
                                  .Select(g => new { Value = g.Key, Text = g.First().NombreUsuario456VG })
                                  .OrderBy(x => x.Text)
                                  .ToList();
            cmbLogin.BeginUpdate();
            cmbLogin.DataSource = usuarios;
            cmbLogin.DisplayMember = "Text";
            cmbLogin.ValueMember = "Value";
            cmbLogin.SelectedIndex = -1;
            cmbLogin.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbLogin.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbLogin.EndUpdate();
            cmbModulo.BeginUpdate();
            cmbModulo.Items.Clear();
            cmbModulo.Items.AddRange(MOD_KEY456VG.Keys.Cast<object>().ToArray());
            cmbModulo.SelectedIndex = -1;
            cmbModulo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbModulo.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbModulo.FormattingEnabled = true;
            cmbModulo.Format -= FormatoCMBModulo456VG;
            cmbModulo.Format += FormatoCMBModulo456VG;
            cmbModulo.EndUpdate();
            cmbAccion.BeginUpdate();
            cmbAccion.Items.Clear();
            cmbAccion.Items.AddRange(ACC_KEY456VG.Keys.Cast<object>().ToArray());
            cmbAccion.SelectedIndex = -1;
            cmbAccion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAccion.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbAccion.FormattingEnabled = true;
            cmbAccion.Format -= FormatoCMBAccion456VG;
            cmbAccion.Format += FormatoCMBAccion456VG;
            cmbAccion.EndUpdate();
            var critSource = Enum.GetValues(typeof(BEEventoBitacora_456VG.NVCriticidad456VG))
                                 .Cast<BEEventoBitacora_456VG.NVCriticidad456VG>()
                                 .Select(v => new { Numero = (int)v, Texto = ((int)v).ToString() })
                                 .OrderBy(x => x.Numero)
                                 .ToList();
            cmbCriticidad.DataSource = critSource;
            cmbCriticidad.DisplayMember = "Texto";
            cmbCriticidad.ValueMember = "Numero";
            cmbCriticidad.SelectedIndex = -1;
        }
        //muestra los texts de los cmb traducidos
        private void FormatoCMBModulo456VG(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is string orig) e.Value = TradModulo456VG(orig);
        }
        //muestra los texts de los cmb traducidos
        private void FormatoCMBAccion456VG(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is string orig) e.Value = TradAccion456VG(orig);
        }
        //Carga datos de bitácora
        private void CargarTodo456VG()
        {
            var datos = bll.GetBitacora456VG(null, null, null, null, null, null);
            ArmadoDGV456VG(datos);
        }
        //aplica columnas en DGV y su traduccion
        private void ArmadoDGV456VG(List<BEEventoBitacora_456VG> datos)
        {
            _muteSelectionChanged = true;

            var mapa = blluser.leerEntidades456VG()
                              .ToDictionary(x => x.DNI456VG, x => x.NombreUsuario456VG);
            var vista = datos.Select(x => new
            {
                DNI = x.Usuario456VG,
                Login = mapa.ContainsKey(x.Usuario456VG) ? mapa[x.Usuario456VG] : x.Usuario456VG,
                Fecha = x.Fecha456VG,
                Módulo = TradModulo456VG(x.Modulo456VG ?? string.Empty),
                Acción = TradAccion456VG(x.Accion456VG ?? string.Empty),
                Criticidad = x.Criticidad456VG
            }).ToList();
            dgvBitacora.DataSource = vista;
            if (dgvBitacora.Columns.Contains("DNI"))
                dgvBitacora.Columns["DNI"].Visible = false;
            if (dgvBitacora.Columns.Contains("Fecha"))
                dgvBitacora.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            TraducirGrilla456VG();
            dgvBitacora.ClearSelection();
            dgvBitacora.CurrentCell = null;
            _muteSelectionChanged = false;
        }
        private void dgvBitacora_SelectionChanged(object sender, EventArgs e)
        {
            if (_muteSelectionChanged || dgvBitacora.SelectedRows.Count == 0) return;
            var row = dgvBitacora.SelectedRows[0];
            string dni = row.Cells["DNI"]?.Value?.ToString();
            string modulo = row.Cells["Módulo"]?.Value?.ToString();
            string accion = row.Cells["Acción"]?.Value?.ToString();
            string critStr = row.Cells["Criticidad"]?.Value?.ToString();
            string fechaStr = row.Cells["Fecha"]?.Value?.ToString();
            if (!string.IsNullOrEmpty(dni)) cmbLogin.SelectedValue = dni; else cmbLogin.SelectedIndex = -1;
            SelecCMBTexto456VG(cmbModulo, modulo);
            SelecCMBTexto456VG(cmbAccion, accion);
            if (int.TryParse(critStr, out int critNum)) cmbCriticidad.SelectedValue = critNum; else cmbCriticidad.SelectedIndex = -1;
            if (DateTime.TryParse(fechaStr, out DateTime fecha)) { dtpDesde.Value = fecha; dtpDesde.Checked = true; dtpHasta.Value = fecha; dtpHasta.Checked = true; }
            PersonaNomApePorDNI456VG(dni);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            _muteSelectionChanged = true;
            cmbLogin.SelectedIndex = -1;
            cmbModulo.SelectedIndex = -1;
            cmbAccion.SelectedIndex = -1;
            cmbCriticidad.SelectedIndex = -1;
            dtpDesde.Checked = false;
            dtpHasta.Checked = false;
            name.Text = "";
            ape.Text = "";
            CargarTodo456VG();
            dgvBitacora.ClearSelection();
            dgvBitacora.CurrentCell = null;
            _muteSelectionChanged = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int? crit = (cmbCriticidad.SelectedIndex >= 0) ? (int?)cmbCriticidad.SelectedValue : null;
            if (dtpDesde.Checked && dtpHasta.Checked && dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
                MessageBox.Show(lng.ObtenerTexto_456VG("BitácoraEventos_456VG.Msg.FechaInvalida"),
                                lng.ObtenerTexto_456VG("BitácoraEventos_456VG.Msg.FechaInvalidaTitle"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDesde.Focus();
                return;
            }
            DateTime? desde = dtpDesde.Checked ? dtpDesde.Value.Date : (DateTime?)null;
            DateTime? hasta = dtpHasta.Checked ? dtpHasta.Value.Date.AddDays(1).AddTicks(-1) : (DateTime?)null;
            string modulo = (cmbModulo.SelectedItem as string) ?? ModuloOriginal456VG(cmbModulo.Text?.Trim());
            if (string.IsNullOrWhiteSpace(cmbModulo.Text)) modulo = null;
            string accion = (cmbAccion.SelectedItem as string) ?? AccionOriginal456VG(cmbAccion.Text?.Trim());
            if (string.IsNullOrWhiteSpace(cmbAccion.Text)) accion = null;
            string dni = (cmbLogin.SelectedIndex >= 0) ? (string)cmbLogin.SelectedValue : null;
            var datos = bll.GetBitacora456VG(crit, desde, hasta, modulo, dni, accion);
            ArmadoDGV456VG(datos);
        }
    }
}
