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
    public partial class GestiondeChoferes_456VG : Form, IObserver_456VG
    {
        BLLChofer_456VG BLLChof = new BLLChofer_456VG();
        BLLEventoBitacora_456VG blleven = new BLLEventoBitacora_456VG();
        private bool mostrarSoloActivos = true;
        public GestiondeChoferes_456VG()
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
            textBoxDNI.Clear();
            textBoxName.Clear();
            textBoxApe.Clear();
            textBoxTel.Clear();
            radioButtonSI.Checked = false;
            dateTimePicker1456VG.Value = DateTime.Today;
            dateTimePicker1.Value = DateTime.Today;
        }
        private void TraducirEncabezadosDataGrid()
        {
            if (dataGridView1456VG?.Columns == null) return;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            foreach (DataGridViewColumn col in dataGridView1456VG.Columns)
            {
                string clave = $"GestiondeChoferes_456VG.Columna.{col.Name}";
                col.HeaderText = lng.ObtenerTexto_456VG(clave);
            }
        }
        private void GestiondeChoferes_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                .ObtenerTexto_456VG("GestiondeChoferes_456VG.Modo.Consulta");
            textBoxDNI.Enabled = true;
            textBoxName.Enabled = true;
            textBoxApe.Enabled = true;
            textBoxTel.Enabled = true;
            dateTimePicker1456VG.Enabled = true;
            dateTimePicker1.Enabled = true;
            radioButtonSI.Enabled = true;
            btnAñadir456VG.Enabled = true;
            btnModif456VG.Enabled = true;
            btnActivoDesac.Enabled = true;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = false;
            btnVolver456VG.Enabled = true;
            radioButton1456VG.Enabled = true;
            radioButton2456VG.Enabled = true;
            limpiar456VG();
            useractivos456VG();
            mostrarSoloActivos = true;
            radioButton1456VG.Checked = true;
        }
        private void useractivos456VG()
        {
            List<BEChofer_456VG> lista = BLLChof.leerEntidades456VG().Where(c => c.Activo456VG).ToList();
            var show = lista.Select(c => new
            {
                DNIChofer = c.DNIChofer456VG,
                Nombre = c.Nombre456VG,
                Apellido = c.Apellido456VG,
                Telefono = c.Teléfono456VG,
                Registro = c.Registro456VG,
                VencimientoRegistro = c.VencimientoRegistro456VG.ToShortDateString(),
                FechaNacimiento = c.FechaNacimiento456VG.ToShortDateString(),
                Disponible = c.Disponible456VG,
                Activo = c.Activo456VG
            }).ToList();
            dataGridView1456VG.DataSource = show;
            TraducirEncabezadosDataGrid();
        }
        private void allusers456VG()
        {
            List<BEChofer_456VG> lista = BLLChof.leerEntidades456VG();
            var show = lista.Select(c => new
            {
                DNIChofer = c.DNIChofer456VG,
                Nombre = c.Nombre456VG,
                Apellido = c.Apellido456VG,
                Telefono = c.Teléfono456VG,
                Registro = c.Registro456VG,
                VencimientoRegistro = c.VencimientoRegistro456VG.ToShortDateString(),
                FechaNacimiento = c.FechaNacimiento456VG.ToShortDateString(),
                Disponible = c.Disponible456VG,
                Activo = c.Activo456VG
            }).ToList();
            dataGridView1456VG.DataSource = show;
            TraducirEncabezadosDataGrid();
        }
        private void radioButton1456VG_CheckedChanged(object sender, EventArgs e) => useractivos456VG();
        private void radioButton2456VG_CheckedChanged(object sender, EventArgs e) => allusers456VG();
        private void dataGridView1456VG_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            bool activo = Convert.ToBoolean(dataGridView1456VG.Rows[e.RowIndex].Cells["Activo"].Value);

            if (!activo)
                dataGridView1456VG.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            else
                dataGridView1456VG.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }
        private void btnAñadir456VG_Click(object sender, EventArgs e)
        {
            limpiar456VG();
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            label13456VG.Text = lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Modo.Añadir");
            textBoxDNI.Enabled = true;
            textBoxName.Enabled = true;
            textBoxApe.Enabled = true;
            textBoxTel.Enabled = true;
            dateTimePicker1456VG.Enabled = true;
            dateTimePicker1.Enabled = true;
            radioButtonSI.Enabled = true;
            btnAñadir456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;
        }
        private void btnModif456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            label13456VG.Text = lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Modo.Modificar");
            limpiar456VG();
            textBoxDNI.Enabled = false;
            textBoxName.Enabled = true;
            textBoxApe.Enabled = true;
            textBoxTel.Enabled = true;
            dateTimePicker1456VG.Enabled = true;
            dateTimePicker1.Enabled = true;
            radioButtonSI.Enabled = true;
            btnAñadir456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;
            radioButton1456VG.Enabled = false;
            radioButton1456VG.Checked = false;
            radioButton2456VG.Checked = true;
            allusers456VG();
        }
        private void btnActivoDesac_Click(object sender, EventArgs e)
        {
            limpiar456VG();
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            label13456VG.Text = lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Modo.ActDesac");
            textBoxDNI.Enabled = false;
            textBoxName.Enabled = false;
            textBoxApe.Enabled = false;
            textBoxTel.Enabled = false;
            dateTimePicker1456VG.Enabled = false;
            dateTimePicker1.Enabled = false;
            radioButtonSI.Enabled = false;
            radioButton1456VG.Enabled = true;
            btnAñadir456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;
            radioButton1456VG.Enabled = false;
            radioButton1456VG.Checked = false;
            radioButton2456VG.Checked = true;
            allusers456VG();
        }
        private void btnCancelar456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            DialogResult dr = MessageBox.Show(
                lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.ConfirmCancel"),
                lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.ConfirmTitle"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                GestiondeChoferes_456VG_Load(null, null);
        }
        private bool ValidarCamposAlta(string dni, string nom, string ape, string tel, bool reg, DateTime vencReg, DateTime fnac, bool act)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            dni = textBoxDNI.Text.Trim();
            nom = textBoxName.Text.Trim();
            ape = textBoxApe.Text.Trim();
            tel = textBoxTel.Text.Trim();
            reg = radioButtonSI.Checked;
            vencReg = dateTimePicker1.Value.Date;
            fnac = dateTimePicker1456VG.Value.Date;
            act = true;
            if (string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(ape) || string.IsNullOrWhiteSpace(tel))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.CamposObligatorios"),
                                lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dni.Length != 8 || !dni.All(char.IsDigit))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.DNIInvalido"),
                                lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tel.Length != 10 || !tel.All(char.IsDigit))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.TelefonoInvalido"),
                                lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (fnac > DateTime.Today)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.FechaNacimientoInvalida"),
                                lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!reg)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.RegistroObligatorio"),
                                lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (vencReg <= DateTime.Today.AddMonths(1))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.VencimientoMinimo"),
                                lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            bool dniTomado =
                new BLLUsuario_456VG().leerEntidades456VG().Any(u => u.DNI456VG == dni) ||
                new BLLCliente_456VG().leerEntidades456VG().Any(c => c.DNI456VG == dni) ||
                BLLChof.leerEntidades456VG().Any(ch => ch.DNIChofer456VG == dni);
            if (dniTomado)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.DNIRepetido"),
                                lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void btnVolver456VG_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAplicar456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Modo.Añadir"))
            {
                string dni = textBoxDNI.Text.Trim();
                string nom = textBoxName.Text.Trim();
                string ape = textBoxApe.Text.Trim();
                string tel = textBoxTel.Text.Trim();
                bool reg = radioButtonSI.Checked;
                DateTime vencReg = dateTimePicker1.Value.Date;
                DateTime fnac = dateTimePicker1456VG.Value.Date;
                bool disp = true;
                bool act = true;
                if (!ValidarCamposAlta(dni, nom, ape, tel, reg, vencReg, fnac, act))
                    return;
                var nuevo = new BEChofer_456VG(dni, nom, ape, tel, reg, vencReg, fnac, disp, act);
                var res = BLLChof.crearEntidad456VG(nuevo);
                if (res.resultado)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.ChoferRegistradoOK"),
                        lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                    blleven.AddBitacora456VG(
                        dni: dniLog,
                        modulo: "Maestro",
                        accion: "Añadir Chofer",
                        crit: BEEventoBitacora_456VG.NVCriticidad456VG.Peligro);
                    GestiondeChoferes_456VG_Load(null,null);
                }
                else
                {
                    MessageBox.Show(
                        string.Format(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.ErrorRegistrar"), res.mensaje),
                        lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Modo.Consulta"))
            {
                radioButton1456VG.Checked = false;
                radioButton2456VG.Checked = false;
                var lista = BLLChof.leerEntidades456VG();
                var filtro = lista.Where(c =>
                    (string.IsNullOrWhiteSpace(textBoxDNI.Text) || c.DNIChofer456VG.ToLower().Contains(textBoxDNI.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(textBoxName.Text) || c.Nombre456VG.ToLower().Contains(textBoxName.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(textBoxApe.Text) || c.Apellido456VG.ToLower().Contains(textBoxApe.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(textBoxTel.Text) || c.Teléfono456VG.ToLower().Contains(textBoxTel.Text.ToLower()))
                )
                .Select(c => new
                {
                    DNIChofer = c.DNIChofer456VG,
                    Nombre = c.Nombre456VG,
                    Apellido = c.Apellido456VG,
                    Telefono = c.Teléfono456VG,
                    Registro = c.Registro456VG,
                    VencimientoRegistro = c.VencimientoRegistro456VG.ToShortDateString(),
                    FechaNacimiento = c.FechaNacimiento456VG.ToShortDateString(),
                    Disponible = c.Disponible456VG,
                    Activo = c.Activo456VG
                }).ToList();
                if (filtro.Count == 0)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.NoChoferesCriterio"),
                                    lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.ResultadoVacio"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    useractivos456VG();
                }
                else
                {
                    dataGridView1456VG.DataSource = filtro;
                    TraducirEncabezadosDataGrid();
                }
                return;
            }
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Modo.Modificar"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.SeleccioneChoferModificar"),
                                    lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string dniSel = dataGridView1456VG.SelectedRows[0].Cells["DNIChofer"].Value.ToString();
                string nombre = textBoxName.Text.Trim();
                string apellido = textBoxApe.Text.Trim();
                string telefono = textBoxTel.Text.Trim();
                bool registro = radioButtonSI.Checked;
                DateTime vencimiento = dateTimePicker1.Value.Date;
                DateTime nacimiento = dateTimePicker1456VG.Value.Date;
                if (vencimiento <= DateTime.Today.AddMonths(1))
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.VencimientoMinimo"),
                                    lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var obj = new BEChofer_456VG(
                    dniSel,
                    nombre,
                    apellido,
                    telefono,
                    registro,
                    vencimiento,
                    nacimiento
                );
                var res = BLLChof.actualizarEntidad456VG(obj);
                if (res.resultado)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.ChoferActualizadoOK"),
                                    lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                    blleven.AddBitacora456VG(dni: dniLog, modulo: "Maestro", accion: "Modificar Chofer", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Peligro);
                    GestiondeChoferes_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show(string.Format(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.ErrorActualizar"), res.mensaje),
                                    lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Modo.ActDesac"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.SeleccioneChoferActDesac"),
                        lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                string dniSel = dataGridView1456VG.SelectedRows[0].Cells["DNIChofer"].Value.ToString();
                bool activo = Convert.ToBoolean(dataGridView1456VG.SelectedRows[0].Cells["Activo"].Value);
                bool nuevo = !activo;
                var dal = new _456VG_DAL.DALChofer_456VG();
                var res = dal.ActDesacChof456(dniSel, nuevo);
                if (res.resultado)
                {
                    MessageBox.Show(
                        nuevo
                            ? lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.ActivadoOK")
                            : lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.DesactivadoOK"),
                        lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    string accionLog = nuevo ? "Activar Chofer" : "Desactivar Chofer";
                    var dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                    blleven.AddBitacora456VG(
                        dni: dniLog,
                        modulo: "Maestro",
                        accion: accionLog,
                        crit: BEEventoBitacora_456VG.NVCriticidad456VG.Peligro
                    );
                    GestiondeChoferes_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                        string.Format(
                            lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.ErrorActDesac"),
                            res.mensaje
                        ),
                        lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }
        private void dataGridView1456VG_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1456VG.SelectedRows.Count == 0) return;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string dniSel = dataGridView1456VG.SelectedRows[0].Cells["DNIChofer"].Value.ToString();
            var dal = new _456VG_DAL.DALChofer_456VG();
            var r = dal.ObtenerChoferPorDNI456VG(dniSel);
            if (!r.resultado)
            {
                MessageBox.Show(string.Format(lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Msg.ErrorRecuperar"), r.mensaje),
                                lng.ObtenerTexto_456VG("GestiondeChoferes_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var ch = r.entidad;
            textBoxDNI.Text = ch.DNIChofer456VG;
            textBoxName.Text = ch.Nombre456VG;
            textBoxApe.Text = ch.Apellido456VG;
            textBoxTel.Text = ch.Teléfono456VG;
            radioButtonSI.Checked = ch.Registro456VG;
            dateTimePicker1.Value = ch.VencimientoRegistro456VG;
            dateTimePicker1456VG.Value = ch.FechaNacimiento456VG;
        }
        private void radioButton1456VG_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1456VG.Checked)
            {
                mostrarSoloActivos = true;
                useractivos456VG();
            }
        }
        private void radioButton2456VG_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton2456VG.Checked)
            {
                mostrarSoloActivos = false;
                allusers456VG();
            }
        }
    }
}
