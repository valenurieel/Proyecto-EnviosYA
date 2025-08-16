using _456VG_BE;
using _456VG_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class BitácoraEventos_456VG : Form
    {
        BLLEventoBitacora_456VG bll = new BLLEventoBitacora_456VG();
        BLLUsuario_456VG blluser = new BLLUsuario_456VG();
        private bool _muteSelectionChanged = false;
        public BitácoraEventos_456VG()
        {
            InitializeComponent();
            ConfigurarGrilla();
            CargarCombos();
            CargarTodo();
        }
        private static readonly string[] MODULOS_456VG = new[]
        {
            "Administrador",
            "Maestro",
            "Usuario",
            "Recepción",
            "Envíos",
            "Reportes",
            "Ayuda",
            "Salir"
        };
        private static readonly string[] ACCIONES_456VG = new[]
        {
            "Iniciar Sesión",
            "Cerrar Sesión",
            "Cambiar Contraseña",
            "Crear Envío",
            "Cargar Paquete",
            "Modificar Cliente",
            "Añadir Cliente",
            "Activar Cliente",
            "Desactivar Cliente",
            "Cobrar Envío",
            "Modificar Usuario",
            "Añadir Usuario",
            "Activar Usuario",
            "Desactivar Usuario",
            "Desbloquear Usuario",
            "Eliminar Perfil",
            "Crear Perfil",
            "Agregar Permiso - Perfil",
            "Quitar Permiso - Perfil",
            "Agregar Familia - Perfil",
            "Quitar Familia - Perfil",
            "Eliminar Familia",
            "Crear Familia",
            "Agregar Familia - Familia",
            "Quitar Familia - Familia",
            "Agregar Permiso - Familia",
            "Quitar Permiso - Familia",
        };
        private void SetPersonaDeRegistro(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                name.Text = "";
                ape.Text = "";
                return;
            }
            var res = blluser.recuperarUsuarioPorDNI456VG(dni);
            if (res != null && res.resultado && res.entidad != null)
            {
                name.Text = res.entidad.Nombre456VG;
                ape.Text = res.entidad.Apellido456VG;
            }
            else
            {
                name.Text = "";
                ape.Text = "";
            }
        }
        private void BitácoraEventos_456VG_Load(object sender, EventArgs e)
        {
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ConfigurarGrilla()
        {
            dgvBitacora.AutoGenerateColumns = true;
            dgvBitacora.ReadOnly = true;
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.AllowUserToDeleteRows = false;
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBitacora.MultiSelect = false;
        }
        private void CargarCombos()
        {
            var dnis = bll.GetUsers456VG();
            cmbLogin.BeginUpdate();
            cmbLogin.Items.Clear();
            if (dnis != null && dnis.Count > 0)
                cmbLogin.Items.AddRange(dnis.Cast<object>().ToArray());
            cmbLogin.SelectedIndex = -1;
            cmbLogin.EndUpdate();
            cmbLogin.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbLogin.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbModulo.BeginUpdate();
            cmbModulo.Items.Clear();
            cmbModulo.Items.AddRange(MODULOS_456VG.Cast<object>().ToArray());
            cmbModulo.SelectedIndex = -1;
            cmbModulo.EndUpdate();
            cmbModulo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbModulo.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbAccion.BeginUpdate();
            cmbAccion.Items.Clear();
            cmbAccion.Items.AddRange(ACCIONES_456VG.Cast<object>().ToArray());
            cmbAccion.SelectedIndex = -1;
            cmbAccion.EndUpdate();
            cmbAccion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAccion.AutoCompleteSource = AutoCompleteSource.ListItems;
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
        private void btnCargPaq456VG_Click(object sender, EventArgs e)
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
            CargarTodo();
            dgvBitacora.ClearSelection();
            dgvBitacora.CurrentCell = null;
            _muteSelectionChanged = false;
        }
        private void CargarTodo()
        {
            var datos = bll.GetBitacora456VG(null, null, null, null, null, null);
            BindGrid(datos);
        }
        private void BindGrid(List<BEEventoBitacora_456VG> datos)
        {
            _muteSelectionChanged = true;
            var vista = datos.Select(x => new
            {
                DNI = x.Usuario456VG,
                Fecha = x.Fecha456VG,
                Módulo = x.Modulo456VG,
                Acción = x.Accion456VG,
                Criticidad = x.Criticidad456VG
            }).ToList();
            dgvBitacora.DataSource = vista;
            if (dgvBitacora.Columns.Contains("Fecha"))
            {
                var colFecha = dgvBitacora.Columns["Fecha"];
                colFecha.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                colFecha.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                colFecha.MinimumWidth = 120;
            }
            dgvBitacora.ClearSelection();
            dgvBitacora.CurrentCell = null;
            _muteSelectionChanged = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int? crit = null;
            if (cmbCriticidad.SelectedIndex >= 0)
                crit = (int)cmbCriticidad.SelectedValue;
            if (dtpDesde.Checked && dtpHasta.Checked)
            {
                var d = dtpDesde.Value.Date;
                var h = dtpHasta.Value.Date;
                if (d > h)
                {
                    MessageBox.Show(
                        "La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.",
                        "Filtros de bitácora",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    dtpDesde.Focus();
                    return;
                }
            }
            DateTime? desde = dtpDesde.Checked ? (DateTime?)dtpDesde.Value.Date : null;
            DateTime? hasta = dtpHasta.Checked ? (DateTime?)dtpHasta.Value.Date.AddDays(1).AddTicks(-1) : null;
            string modulo = string.IsNullOrWhiteSpace(cmbModulo.Text) ? null : cmbModulo.Text.Trim();
            string accion = string.IsNullOrWhiteSpace(cmbAccion.Text) ? null : cmbAccion.Text.Trim();
            string dni = string.IsNullOrWhiteSpace(cmbLogin.Text) ? null : cmbLogin.Text.Trim();
            var datos = bll.GetBitacora456VG(crit, desde, hasta, modulo, dni, accion);
            BindGrid(datos);
        }
        private void dgvBitacora_SelectionChanged(object sender, EventArgs e)
        {
            if (_muteSelectionChanged) return;
            if (dgvBitacora.SelectedRows.Count == 0) return;
            var row = dgvBitacora.SelectedRows[0];
            string dni = row.Cells["DNI"]?.Value?.ToString();
            string modulo = row.Cells["Módulo"]?.Value?.ToString();
            string accion = row.Cells["Acción"]?.Value?.ToString();
            string critStr = row.Cells["Criticidad"]?.Value?.ToString();
            string fechaStr = row.Cells["Fecha"]?.Value?.ToString();
            SetComboByText(cmbLogin, dni);
            SetComboByText(cmbModulo, modulo);
            SetComboByText(cmbAccion, accion);
            if (int.TryParse(critStr, out int critNum))
                cmbCriticidad.SelectedValue = critNum;
            else
                cmbCriticidad.SelectedIndex = -1;
            if (DateTime.TryParse(fechaStr, out DateTime fecha))
            {
                dtpDesde.Value = fecha;
                dtpDesde.Checked = true;
                dtpHasta.Value = fecha;
                dtpHasta.Checked = true;
            }
            SetPersonaDeRegistro(dni);
        }
        private void SetComboByText(ComboBox cmb, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                cmb.SelectedIndex = -1;
                return;
            }
            int index = -1;
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                var itemText = cmb.GetItemText(cmb.Items[i]);
                if (string.Equals(itemText, text, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            if (index >= 0)
            {
                cmb.SelectedIndex = index;
            }
            else
            {
                if (cmb.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    cmb.Items.Add(text);
                    cmb.SelectedIndex = cmb.Items.Count - 1;
                }
                else
                {
                    cmb.Text = text;
                }
            }
        }
    }
}
