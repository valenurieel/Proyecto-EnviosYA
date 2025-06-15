using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class GestiondeClientes_456VG : Form, IObserver_456VG
    {
        BLLCliente_456VG BLLCli = new BLLCliente_456VG();
        public GestiondeClientes_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
        }
        private void dataGridView1456VG_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1456VG.Columns[e.ColumnIndex].Name == "Activo")
            {
                bool isActive = Convert.ToBoolean(e.Value);
                dataGridView1456VG.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                    isActive ? Color.White : Color.Red;
            }
        }
        private void btnAñadir456VG_Click(object sender, EventArgs e)
        {
            limpiar456VG();
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                     .ObtenerTexto_456VG("GestiondeClientes_456VG.Modo.Añadir");
            txtdni456VG.Enabled = true;
            txtnombre456VG.Enabled = true;
            txtapellido456VG.Enabled = true;
            txttelef456VG.Enabled = true;
            txtdomicilio456VG.Enabled = true;
            dateTimePicker1456VG.Enabled = true;
            btnAñadir456VG.Enabled = false;
            btnModif456VG.Enabled = false;
            btnAplicar456VG.Enabled = true;
            btnCancelar456VG.Enabled = true;
            btnVolver456VG.Enabled = false;
            btnActivoDesac.Enabled = false;
        }
        private void btnActivoDesac_Click(object sender, EventArgs e)
        {
            limpiar456VG();
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                     .ObtenerTexto_456VG("GestiondeClientes_456VG.Modo.ActDesac");
            txtdni456VG.Enabled = false;
            txtnombre456VG.Enabled = false;
            txtapellido456VG.Enabled = false;
            txttelef456VG.Enabled = false;
            txtdomicilio456VG.Enabled = false;
            dateTimePicker1456VG.Enabled = false;
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
        private void btnModif456VG_Click(object sender, EventArgs e)
        {
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                     .ObtenerTexto_456VG("GestiondeClientes_456VG.Modo.Modificar");
            limpiar456VG();
            txtdni456VG.Enabled = false;
            txtnombre456VG.Enabled = true;
            txtapellido456VG.Enabled = true;
            txttelef456VG.Enabled = true;
            txtdomicilio456VG.Enabled = true;
            dateTimePicker1456VG.Enabled = true;
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
        private void btnDesbloq456VG_Click(object sender, EventArgs e)
        {
        }
        private void btnAplicar456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Modo.Añadir"))
            {
                string dni = txtdni456VG.Text;
                string name = txtnombre456VG.Text;
                string ape = txtapellido456VG.Text;
                string telef = txttelef456VG.Text;
                string domicilio = txtdomicilio456VG.Text;
                DateTime fechaNacimiento = dateTimePicker1456VG.Value;
                bool activo = true;
                BECliente_456VG clinew = new BECliente_456VG(
                    dni, name, ape, telef, domicilio, fechaNacimiento, activo
                );
                Resultado_456VG<BECliente_456VG> resultado = BLLCli.crearEntidad456VG(clinew);
                if (resultado.resultado)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.ClienteRegistradoOK"),
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    GestiondeClientes_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                        string.Format(
                            lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.ErrorRegistrar"),
                            resultado.mensaje
                        ),
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Modo.Consulta"))
            {
                radioButton1456VG.Checked = false;
                radioButton2456VG.Checked = false;
                List<BECliente_456VG> listaCli = BLLCli.leerEntidades456VG();
                var filtro = listaCli.Where(u =>
                    (string.IsNullOrWhiteSpace(txtdni456VG.Text) || u.DNI456VG.ToLower().Contains(txtdni456VG.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(txtnombre456VG.Text) || u.Nombre456VG.ToLower().Contains(txtnombre456VG.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(txtapellido456VG.Text) || u.Apellido456VG.ToLower().Contains(txtapellido456VG.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(txttelef456VG.Text) || u.Teléfono456VG.ToLower().Contains(txttelef456VG.Text.ToLower())) &&
                    (string.IsNullOrWhiteSpace(txtdomicilio456VG.Text) || u.Domicilio456VG.ToLower().Contains(txtdomicilio456VG.Text.ToLower()))
                )
                .Select(u => new
                {
                    DNI = u.DNI456VG,
                    Nombre = u.Nombre456VG,
                    Apellido = u.Apellido456VG,
                    Telefono = u.Teléfono456VG,
                    Domicilio = u.Domicilio456VG,
                    FechaNacimiento = u.FechaNacimiento456VG.ToShortDateString(),
                    Activo = u.Activo456VG
                })
                .ToList();
                if (filtro.Count == 0)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.NoClientesCriterio"),
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.ResultadoVacio"),
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
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Modo.Modificar"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.SeleccioneClienteModificar"),
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                string dniSeleccionado = dataGridView1456VG.SelectedRows[0].Cells["DNI"].Value.ToString();
                BECliente_456VG cliAActualizar = new BECliente_456VG(
                    dniSeleccionado,
                    txtnombre456VG.Text.Trim(),
                    txtapellido456VG.Text.Trim(),
                    txttelef456VG.Text.Trim(),
                    txtdomicilio456VG.Text.Trim(),
                    dateTimePicker1456VG.Value.Date
                );
                Resultado_456VG<BECliente_456VG> resultado = BLLCli.actualizarEntidad456VG(cliAActualizar);
                if (resultado.resultado)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.ClienteActualizadoOK"),
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    GestiondeClientes_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                        string.Format(
                            lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.ErrorActualizar"),
                            resultado.mensaje
                        ),
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            if (label13456VG.Text == lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Modo.ActDesac"))
            {
                if (dataGridView1456VG.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.SeleccioneClienteActDesac"),
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                string dniSeleccionado = dataGridView1456VG.SelectedRows[0].Cells["DNI"].Value.ToString();
                bool estadoActivo = Convert.ToBoolean(
                    dataGridView1456VG.SelectedRows[0].Cells["Activo"].Value
                );
                bool nuevoEstadoActivo = !estadoActivo;

                var resultado = BLLCli.ActDesacCli456(dniSeleccionado, nuevoEstadoActivo);
                if (resultado.resultado)
                {
                    MessageBox.Show(
                        string.Format(
                            lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.ClienteActDesacOK"),
                            (nuevoEstadoActivo ? lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Columna.Activo")
                                                : lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Columna.Activo"))
                        ),
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    GestiondeClientes_456VG_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                        string.Format(
                            lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.ErrorActDesac"),
                            resultado.mensaje
                        ),
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Text"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }
        private void btnCancelar456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            DialogResult confirmacion = MessageBox.Show(
                lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.ConfirmCancel"),
                lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.ConfirmTitle"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirmacion == DialogResult.Yes)
            {
                GestiondeClientes_456VG_Load(null, null);
            }
        }
        private void btnVolver456VG_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void radioButton1456VG_CheckedChanged(object sender, EventArgs e)
        {
            useractivos456VG();
        }
        private void radioButton2456VG_CheckedChanged(object sender, EventArgs e)
        {
            allusers456VG();
        }
        private void useractivos456VG()
        {
            List<BECliente_456VG> listaCliActivos = BLLCli.leerEntidades456VG()
                                                 .Where(u => u.Activo456VG)
                                                 .ToList();
            var listaParaMostrar = listaCliActivos.Select(u => new
            {
                DNI = u.DNI456VG,
                Nombre = u.Nombre456VG,
                Apellido = u.Apellido456VG,
                Telefono = u.Teléfono456VG,
                Domicilio = u.Domicilio456VG,
                FechaNacimiento = u.FechaNacimiento456VG.ToShortDateString(),
                Activo = u.Activo456VG
            }).ToList();
            dataGridView1456VG.DataSource = listaParaMostrar;
            TraducirEncabezadosDataGrid();
        }
        private void allusers456VG()
        {
            List<BECliente_456VG> listcli = BLLCli.leerEntidades456VG();
            var listaParaMostrar = listcli.Select(u => new
            {
                DNI = u.DNI456VG,
                Nombre = u.Nombre456VG,
                Apellido = u.Apellido456VG,
                Telefono = u.Teléfono456VG,
                Domicilio = u.Domicilio456VG,
                FechaNacimiento = u.FechaNacimiento456VG.ToShortDateString(),
                Activo = u.Activo456VG
            }).ToList();
            dataGridView1456VG.DataSource = listaParaMostrar;
            TraducirEncabezadosDataGrid();
        }
        private void GestiondeClientes_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
            label13456VG.Text = Lenguaje_456VG.ObtenerInstancia_456VG()
                                   .ObtenerTexto_456VG("GestiondeClientes_456VG.Modo.Consulta");
            txtdni456VG.Enabled = true;
            txtnombre456VG.Enabled = true;
            txtapellido456VG.Enabled = true;
            txttelef456VG.Enabled = true;
            txtdomicilio456VG.Enabled = true;
            dateTimePicker1456VG.Enabled = true;
            btnAñadir456VG.Enabled = true;
            btnModif456VG.Enabled = true;
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
            txttelef456VG.Text = "";
            txtdomicilio456VG.Text = "";
            dateTimePicker1456VG.Value = DateTime.Now;
        }
        private void dataGridView1456VG_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1456VG.SelectedRows.Count == 0) return;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string dniSeleccionado = dataGridView1456VG.SelectedRows[0].Cells["DNI"].Value.ToString();
            var resultadoRecuperar = BLLCli.recuperarClientePorDNI456VG(dniSeleccionado);
            if (!resultadoRecuperar.resultado)
            {
                MessageBox.Show(
                    string.Format(
                        lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Msg.ErrorRecuperar"),
                        resultadoRecuperar.mensaje
                    ),
                    lng.ObtenerTexto_456VG("GestiondeClientes_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            BECliente_456VG clienteSeleccionado = resultadoRecuperar.entidad;
            txtdni456VG.Text = clienteSeleccionado.DNI456VG;
            txtnombre456VG.Text = clienteSeleccionado.Nombre456VG;
            txtapellido456VG.Text = clienteSeleccionado.Apellido456VG;
            txttelef456VG.Text = clienteSeleccionado.Teléfono456VG;
            dateTimePicker1456VG.Value = clienteSeleccionado.FechaNacimiento456VG;
            txtdomicilio456VG.Text = clienteSeleccionado.Domicilio456VG;
        }
        private void TraducirEncabezadosDataGrid()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            foreach (DataGridViewColumn col in dataGridView1456VG.Columns)
            {
                string clave = $"GestiondeClientes_456VG.Columna.{col.Name}";
                col.HeaderText = lng.ObtenerTexto_456VG(clave);
            }
        }
        private void checkVer456VG_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1456VG.SelectedRows.Count == 0) return;
            if (checkVer456VG.Checked)
            {
                var aes = new HashSHA256_456VG();
                try
                {
                    string cifrado = txtdomicilio456VG.Text;
                    string desencriptado = aes.DecryptAes(cifrado);
                    txtdomicilio456VG.Text = desencriptado;
                }
                catch
                {
                }
            }
            else
            {
                if (dataGridView1456VG.SelectedRows.Count > 0)
                {
                    string dni = dataGridView1456VG.SelectedRows[0].Cells["DNI"].Value.ToString();
                    var resultado = BLLCli.recuperarClientePorDNI456VG(dni);
                    if (resultado.resultado)
                    {
                        txtdomicilio456VG.Text = resultado.entidad.Domicilio456VG;
                    }
                }
            }
        }
    }
}
