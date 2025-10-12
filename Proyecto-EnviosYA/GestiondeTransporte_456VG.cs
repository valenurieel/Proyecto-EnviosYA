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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class GestiondeTransporte_456VG : Form, IObserver_456VG
    {
        private readonly BLLTransporte_456VG BLLTrans = new BLLTransporte_456VG();
        private readonly BLLEventoBitacora_456VG bllBitacora = new BLLEventoBitacora_456VG();
        private bool mostrarSoloActivos = true;
        public GestiondeTransporte_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
            TraducirEncabezadosDataGrid();
        }
        private void limpiar456VG()
        {
            txtdni456VG.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
        private void TraducirEncabezadosDataGrid()
        {
            if (dataGridView1456VG?.Columns == null) return;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            foreach (DataGridViewColumn col in dataGridView1456VG.Columns)
            {
                string clave = $"GestiondeTransportes_456VG.Columna.{col.Name}";
                col.HeaderText = lng.ObtenerTexto_456VG(clave);
            }
        }
        private void btnVolver456VG_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GestiondeTransporte_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                .ObtenerTexto_456VG("GestiondeTransportes_456VG.Modo.Consulta");
            txtdni456VG.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            btnAñadir456VG.Enabled = true;
            btnModif456VG.Enabled = true;
            btnActivoDesac.Enabled = true;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = false;
            btnVolver456VG.Enabled = true;
            radioButton1456VG.Enabled = true;
            radioButton2456VG.Enabled = true;
            limpiar456VG();
            CargarTransportesActivos();
            mostrarSoloActivos = true;
            radioButton1456VG.Checked = true;
            dataGridView1456VG.SelectionChanged += dataGridView1456VG_SelectionChanged;
        }
        private void CargarTransportesActivos()
        {
            List<BETransporte_456VG> lista = BLLTrans.leerEntidades456VG().Where(t => t.Activo456VG).ToList();
            dataGridView1456VG.DataSource = lista.Select(t => new
            {
                Patente = t.Patente456VG,
                Marca = t.Marca456VG,
                Año = t.Año456VG,
                CapacidadPeso = t.CapacidadPeso456VG,
                CapacidadVolumen = t.CapacidadVolumen456VG,
                Disponible = t.Disponible456VG,
                Activo = t.Activo456VG
            }).ToList();
            TraducirEncabezadosDataGrid();
        }
        private void CargarTodosTransportes()
        {
            List<BETransporte_456VG> lista = BLLTrans.leerEntidades456VG();
            dataGridView1456VG.DataSource = lista.Select(t => new
            {
                Patente = t.Patente456VG,
                Marca = t.Marca456VG,
                Año = t.Año456VG,
                CapacidadPeso = t.CapacidadPeso456VG,
                CapacidadVolumen = t.CapacidadVolumen456VG,
                Disponible = t.Disponible456VG,
                Activo = t.Activo456VG
            }).ToList();
            TraducirEncabezadosDataGrid();
        }
        private void dataGridView1456VG_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            bool activo = Convert.ToBoolean(dataGridView1456VG.Rows[e.RowIndex].Cells["Activo"].Value);
            dataGridView1456VG.Rows[e.RowIndex].DefaultCellStyle.BackColor = activo ? Color.White : Color.Red;
        }
        private void radioButton1456VG_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1456VG.Checked)
            {
                mostrarSoloActivos = true;
                CargarTransportesActivos();
            }
        }
        private void radioButton2456VG_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2456VG.Checked)
            {
                mostrarSoloActivos = false;
                CargarTodosTransportes();
            }
        }
        private void btnAñadir456VG_Click(object sender, EventArgs e)
        {
            limpiar456VG();
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            label13456VG.Text = lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Modo.Añadir");
        }
        private bool ValidarCampos()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string patente = txtdni456VG.Text.Trim();
            string marca = textBox1.Text.Trim();
            string anioStr = textBox2.Text.Trim();
            string pesoStr = textBox3.Text.Trim();
            string volStr = textBox4.Text.Trim();
            if (string.IsNullOrWhiteSpace(patente) || string.IsNullOrWhiteSpace(marca) ||
                string.IsNullOrWhiteSpace(anioStr) || string.IsNullOrWhiteSpace(pesoStr) || string.IsNullOrWhiteSpace(volStr))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.CamposObligatorios"),
                    lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            Regex regexPatente = new Regex(@"^[A-Z]{2}[0-9]{3}[A-Z]{2}$");
            if (!regexPatente.IsMatch(patente))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.PatenteInvalida"),
                    lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(anioStr, out int anio) || anio < 1900 || anio > DateTime.Now.Year)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.AñoInvalido"),
                    lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!float.TryParse(pesoStr, out _) || !float.TryParse(volStr, out _))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.CapacidadInvalida"),
                    lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void btnAplicar456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Modo.Añadir"))
            {
                if (!ValidarCampos()) return;
                var nuevo = new BETransporte_456VG(
                    txtdni456VG.Text.Trim(),
                    textBox1.Text.Trim(),
                    int.Parse(textBox2.Text.Trim()),
                    float.Parse(textBox3.Text.Trim()),
                    float.Parse(textBox4.Text.Trim()),
                    true,
                    true
                );
                var res = BLLTrans.crearEntidad456VG(nuevo);
                if (res.resultado)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.RegistroOK"),
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                    bllBitacora.AddBitacora456VG(dniLog, "Maestro", "Añadir Transporte", BEEventoBitacora_456VG.NVCriticidad456VG.Peligro);
                    GestiondeTransporte_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                        string.Format(lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.ErrorRegistrar"), res.mensaje),
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Modo.Modificar"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.SeleccioneModificar"),
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string patSel = dataGridView1456VG.SelectedRows[0].Cells["Patente"].Value.ToString();
                var obj = new BETransporte_456VG(
                    patSel,
                    textBox1.Text.Trim(),
                    int.Parse(textBox2.Text.Trim()),
                    float.Parse(textBox3.Text.Trim()),
                    float.Parse(textBox4.Text.Trim())
                );
                var res = BLLTrans.actualizarEntidad456VG(obj);
                if (res.resultado)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.ModificadoOK"),
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                    bllBitacora.AddBitacora456VG(dniLog, "Maestro", "Modificar Transporte", BEEventoBitacora_456VG.NVCriticidad456VG.Peligro);
                    GestiondeTransporte_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.ErrorActualizar"),
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Modo.ActDesac"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.SeleccioneActDesac"),
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                string patSel = dataGridView1456VG.SelectedRows[0].Cells["Patente"].Value.ToString();
                bool activo = Convert.ToBoolean(dataGridView1456VG.SelectedRows[0].Cells["Activo"].Value);
                bool nuevoEstado = !activo;
                var resultado = BLLTrans.ActivarDesactivarEntidad456VG(patSel, nuevoEstado);
                if (resultado.resultado)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.RegistroOK"),
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    string accionLog = nuevoEstado ? "Activar Transporte" : "Desactivar Transporte";
                    string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                    bllBitacora.AddBitacora456VG(
                        dni: dniLog,
                        modulo: "Maestro",
                        accion: accionLog,
                        BEEventoBitacora_456VG.NVCriticidad456VG.Peligro
                    );
                    GestiondeTransporte_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                        string.Format(
                            lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.ErrorActDesac"),
                            resultado.mensaje
                        ),
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Modo.Consulta"))
            {
                var lista = BLLTrans.leerEntidades456VG();
                var filtro = lista.Where(t =>
                    (string.IsNullOrWhiteSpace(txtdni456VG.Text) || t.Patente456VG.IndexOf(txtdni456VG.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrWhiteSpace(textBox1.Text) || t.Marca456VG.IndexOf(textBox1.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrWhiteSpace(textBox2.Text) || t.Año456VG.ToString().IndexOf(textBox2.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrWhiteSpace(textBox3.Text) || t.CapacidadPeso456VG.ToString().IndexOf(textBox3.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrWhiteSpace(textBox4.Text) || t.CapacidadVolumen456VG.ToString().IndexOf(textBox4.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                ).Select(t => new
                {
                    Patente = t.Patente456VG,
                    Marca = t.Marca456VG,
                    Año = t.Año456VG,
                    CapacidadPeso = t.CapacidadPeso456VG,
                    CapacidadVolumen = t.CapacidadVolumen456VG,
                    Disponible = t.Disponible456VG,
                    Activo = t.Activo456VG
                }).ToList();
                if (filtro.Count == 0)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Msg.NoTransportesCriterio"),
                        lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    CargarTransportesActivos();
                }
                else
                {
                    dataGridView1456VG.DataSource = filtro;
                    TraducirEncabezadosDataGrid();
                }
                return;
            }
        }
        private void btnCancelar456VG_Click(object sender, EventArgs e)
        {
            limpiar456VG();
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            label13456VG.Text = lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Modo.Consulta");
            btnAñadir456VG.Enabled = true;
            btnModif456VG.Enabled = true;
            btnActivoDesac.Enabled = true;
            btnAplicar456VG.Enabled = false;
            btnCancelar456VG.Enabled = false;
            txtdni456VG.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            radioButton1456VG.Enabled = true;
            radioButton2456VG.Enabled = true;
            if (mostrarSoloActivos)
                CargarTransportesActivos();
            else
                CargarTodosTransportes();
        }
        private void btnModif456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            label13456VG.Text = lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Modo.Modificar");
            txtdni456VG.Enabled = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            btnAñadir456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnActivoDesac.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            radioButton1456VG.Enabled = false;
            radioButton2456VG.Checked = true;
            CargarTodosTransportes();
        }
        private void btnActivoDesac_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            label13456VG.Text = lng.ObtenerTexto_456VG("GestiondeTransportes_456VG.Modo.ActDesac");
            txtdni456VG.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            btnAñadir456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnActivoDesac.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            radioButton2456VG.Checked = true;
            radioButton1456VG.Enabled = true;
            radioButton2456VG.Enabled = true;
            CargarTodosTransportes();
        }
        private void dataGridView1456VG_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1456VG.SelectedRows.Count == 0) return;
            var fila = dataGridView1456VG.SelectedRows[0];
            txtdni456VG.Text = fila.Cells["Patente"].Value.ToString();
            textBox1.Text = fila.Cells["Marca"].Value.ToString();
            textBox2.Text = fila.Cells["Año"].Value.ToString();
            textBox3.Text = fila.Cells["CapacidadPeso"].Value.ToString();
            textBox4.Text = fila.Cells["CapacidadVolumen"].Value.ToString();
        }
    }
}
