using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
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
    public partial class BitácoraCambios_456VG : Form, IObserver_456VG
    {
        private readonly BLLChoferes_C_456VG bllChoferC = new BLLChoferes_C_456VG();
        private readonly BLLChofer_456VG bllChofer = new BLLChofer_456VG();
        private bool _muteSelection = false;
        public BitácoraCambios_456VG()
        {
            InitializeComponent();
            ConfigurarGrilla456VG();
            CargarCambios456VG();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        public void ActualizarIdioma_456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            lng.CambiarIdiomaControles_456VG(this);
            TraducirEncabezadosGrilla456VG();
        }
        private void TraducirEncabezadosGrilla456VG()
        {
            if (dgvBitacora?.Columns == null) return;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            foreach (DataGridViewColumn col in dgvBitacora.Columns)
            {
                string clave = $"BitácoraCambios_456VG.Columna.{col.Name}";
                col.HeaderText = lng.ObtenerTexto_456VG(clave);
            }
        }
        private void BitácoraCambios_456VG_Load(object sender, EventArgs e)
        {
            CargarCombos456VG();
            ActualizarIdioma_456VG();
            CargarCambios456VG();
        }
        private void ConfigurarGrilla456VG()
        {
            dgvBitacora.AutoGenerateColumns = true;
            dgvBitacora.ReadOnly = true;
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBitacora.MultiSelect = false;
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.AllowUserToDeleteRows = false;
            dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void CargarCombos456VG()
        {
            var choferes = bllChofer.leerEntidades456VG();
            cmbLogin.BeginUpdate();
            cmbLogin.DataSource = choferes.Select(c => c.DNIChofer456VG).Distinct().ToList();
            cmbLogin.SelectedIndex = -1;
            cmbLogin.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbLogin.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbLogin.EndUpdate();
            cmbModulo.BeginUpdate();
            cmbModulo.DataSource = choferes.Select(c => c.Nombre456VG).Distinct().ToList();
            cmbModulo.SelectedIndex = -1;
            cmbModulo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbModulo.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbModulo.EndUpdate();
        }
        private void CargarCambios456VG()
        {
            var fechaInicio = new DateTime(1900, 1, 1);
            var fechaFin = DateTime.Now.AddDays(1);
            var lista = bllChoferC.LeerCambios456VG(fechaInicio, fechaFin);
            MostrarCambiosEnGrilla456VG(lista);
        }
        private void MostrarCambiosEnGrilla456VG(List<BEChoferes_C_456VG> lista)
        {
            _muteSelection = true;
            var vista = lista.Select(c => new
            {
                DNI = c.DNIChofer456VG,
                Nombre = c.Nombre456VG,
                Apellido = c.Apellido456VG,
                Teléfono = c.Telefono456VG,
                Registro = c.Registro456VG ? "Sí" : "No",
                VencimientoRegistro = c.VencimientoRegistro456VG.ToShortDateString(),
                FechaNacimiento = c.FechaNacimiento456VG.ToShortDateString(),
                Disponible = c.Disponible456VG ? "Sí" : "No",
                Activo = c.Activo456VG ? "Sí" : "No",
                Fecha = c.Fecha456VG.ToString("dd/MM/yyyy"),
                Hora = c.Hora456VG
            }).ToList();
            dgvBitacora.DataSource = vista;
            TraducirEncabezadosGrilla456VG();
            dgvBitacora.ClearSelection();
            _muteSelection = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (dgvBitacora.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("BitácoraCambios_456VG.Msg.Seleccionar"),
                    lng.ObtenerTexto_456VG("General.TítuloAviso"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string activoStr = dgvBitacora.SelectedRows[0].Cells["Activo"].Value?.ToString() ?? "";
            bool yaActivo = activoStr.Equals("Sí", StringComparison.OrdinalIgnoreCase)
                            || activoStr.Equals("Si", StringComparison.OrdinalIgnoreCase)
                            || activoStr.Equals("True", StringComparison.OrdinalIgnoreCase)
                            || activoStr.Equals("1", StringComparison.OrdinalIgnoreCase);
            if (yaActivo)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("BitácoraCambios_456VG.Msg.YaActivo"),
                    lng.ObtenerTexto_456VG("General.TítuloAviso"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string dniSel = dgvBitacora.SelectedRows[0].Cells["DNI"].Value.ToString();
            string fechaStr = dgvBitacora.SelectedRows[0].Cells["Fecha"].Value.ToString();
            string horaStr = dgvBitacora.SelectedRows[0].Cells["Hora"].Value.ToString();
            DateTime fechaSel;
            if (!DateTime.TryParseExact($"{fechaStr} {horaStr}", "dd/MM/yyyy HH:mm:ss.fff",
                null, System.Globalization.DateTimeStyles.None, out fechaSel))
            {
                fechaSel = DateTime.ParseExact($"{fechaStr} {horaStr}", "dd/MM/yyyy HH:mm:ss", null);
            }
            var res = bllChoferC.CambiarEstadoChofer456VG(dniSel, fechaSel);
            if (res.resultado)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("BitácoraCambios_456VG.Msg.Activado"),
                    lng.ObtenerTexto_456VG("General.TítuloInfo"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCambios456VG();
                BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
                string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                blleven.AddBitacora456VG(dni: dniLog, modulo: "Maestros", accion: "Registro Activado de Cambios", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Peligro);
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("BitácoraCambios_456VG.Msg.ErrorCambio") + ": " + res.mensaje,
                    lng.ObtenerTexto_456VG("General.TítuloError"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarFiltros456VG()
        {
            cmbLogin.SelectedIndex = -1;
            cmbModulo.SelectedIndex = -1;
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;
            CargarCambios456VG();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            DateTime fechaIni = dtpDesde.Value.Date;
            DateTime fechaFin = dtpHasta.Value.Date;
            if (fechaIni > fechaFin)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("BitácoraCambios_456VG.Msg.FechaInvalida"),
                    lng.ObtenerTexto_456VG("General.TítuloAviso"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                dtpDesde.Value = DateTime.Now;
                dtpDesde.Focus();
                return;
            }
            string dni = cmbLogin.SelectedIndex >= 0 ? cmbLogin.SelectedItem.ToString() : null;
            string nombre = cmbModulo.SelectedIndex >= 0 ? cmbModulo.SelectedItem.ToString() : null;
            DateTime fechaIniReal = fechaIni;
            DateTime fechaFinReal = fechaFin.AddDays(1).AddTicks(-1);
            var datos = bllChoferC.LeerCambios456VG(fechaIniReal, fechaFinReal, dni, nombre);
            if (datos == null || datos.Count == 0)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("BitácoraCambios_456VG.Msg.SinResultados"),
                    lng.ObtenerTexto_456VG("General.TítuloAviso"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }
            MostrarCambiosEnGrilla456VG(datos);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            LimpiarFiltros456VG();
        }
        private void dgvBitacora_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
