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
    public partial class EntregaEnvios_456VG : Form, IObserver_456VG
    {
        private readonly BLLEnvio_456VG bllEnvio = new BLLEnvio_456VG();
        private readonly BLLEventoBitacora_456VG bllBitacora = new BLLEventoBitacora_456VG();
        private BEEnvío_456VG envioActual;
        private readonly BLLEntregado_456VG bllEntrega = new BLLEntregado_456VG();
        private readonly BLLListaCarga_456VG bllLista = new BLLListaCarga_456VG();
        public EntregaEnvios_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
            ConfigurarGrillas();
        }
        public void ActualizarIdioma_456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            lng.CambiarIdiomaControles_456VG(this);
            TraducirColumnas();
        }
        private void TraducirColumnas()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (dataGridEnvio.Columns.Count > 0)
            {
                dataGridEnvio.Columns["CodEnvio"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.CodEnvio");
                dataGridEnvio.Columns["Remitente"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.Remitente");
                dataGridEnvio.Columns["DNIRemitente"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.DNIRemitente");
                dataGridEnvio.Columns["Destinatario"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.Destinatario");
                dataGridEnvio.Columns["DNIDest"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.DNIDest");
                dataGridEnvio.Columns["Provincia"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.Provincia");
                dataGridEnvio.Columns["Localidad"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.Localidad");
                dataGridEnvio.Columns["Domicilio"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.Domicilio");
                dataGridEnvio.Columns["TipoEnvio"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.TipoEnvio");
            }
            if (dataGridPaquetes.Columns.Count > 0)
            {
                dataGridPaquetes.Columns["CodPaq"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.CodPaq");
                dataGridPaquetes.Columns["Peso"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.Peso");
                dataGridPaquetes.Columns["Ancho"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.Ancho");
                dataGridPaquetes.Columns["Largo"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.Largo");
                dataGridPaquetes.Columns["Alto"].HeaderText = lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Columna.Alto");
            }
        }
        private void ConfigurarGrillas()
        {
            dataGridEnvio.AutoGenerateColumns = false;
            dataGridEnvio.Columns.Clear();
            dataGridEnvio.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodEnvio",
                HeaderText = "Código Envío",
                DataPropertyName = "CodEnvio"
            });
            dataGridEnvio.Columns.Add(new DataGridViewTextBoxColumn { Name = "Remitente", HeaderText = "Remitente", DataPropertyName = "Remitente" });
            dataGridEnvio.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNIRemitente", HeaderText = "DNI Remitente", DataPropertyName = "DNIRemitente" });
            dataGridEnvio.Columns.Add(new DataGridViewTextBoxColumn { Name = "Destinatario", HeaderText = "Destinatario", DataPropertyName = "Destinatario" });
            dataGridEnvio.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNIDest", HeaderText = "DNI Destinatario", DataPropertyName = "DNIDest" });
            dataGridEnvio.Columns.Add(new DataGridViewTextBoxColumn { Name = "Provincia", HeaderText = "Provincia", DataPropertyName = "Provincia" });
            dataGridEnvio.Columns.Add(new DataGridViewTextBoxColumn { Name = "Localidad", HeaderText = "Localidad", DataPropertyName = "Localidad" });
            dataGridEnvio.Columns.Add(new DataGridViewTextBoxColumn { Name = "Domicilio", HeaderText = "Domicilio", DataPropertyName = "Domicilio" });
            dataGridEnvio.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoEnvio", HeaderText = "Tipo Envío", DataPropertyName = "TipoEnvio" });
            dataGridEnvio.ReadOnly = true;
            dataGridEnvio.AllowUserToAddRows = false;
            dataGridEnvio.AllowUserToDeleteRows = false;
            dataGridEnvio.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridPaquetes.AutoGenerateColumns = false;
            dataGridPaquetes.Columns.Clear();
            dataGridPaquetes.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodPaq", HeaderText = "Código Paquete", DataPropertyName = "CodPaq456VG" });
            dataGridPaquetes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Peso", HeaderText = "Peso (kg)", DataPropertyName = "Peso456VG" });
            dataGridPaquetes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ancho", HeaderText = "Ancho (cm)", DataPropertyName = "Ancho456VG" });
            dataGridPaquetes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Largo", HeaderText = "Largo (cm)", DataPropertyName = "Largo456VG" });
            dataGridPaquetes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Alto", HeaderText = "Alto (cm)", DataPropertyName = "Alto456VG" });
            dataGridPaquetes.ReadOnly = true;
            dataGridPaquetes.AllowUserToAddRows = false;
            dataGridPaquetes.AllowUserToDeleteRows = false;
            dataGridPaquetes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ReasignarEntrega_456VG fr = new ReasignarEntrega_456VG();
            fr.ShowDialog();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string codEnvio = txtCodEnvio.Text.Trim();
            string dniDest = txtDNIDest.Text.Trim();
            string nombre = txtNombreDest.Text.Trim();
            string apellido = txtApellidoDest.Text.Trim();
            if (string.IsNullOrWhiteSpace(codEnvio))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.CodigoObligatorio"));
                return;
            }
            envioActual = bllEnvio.leerEntidades456VG().FirstOrDefault(a =>a.CodEnvio456VG == codEnvio);
            if (envioActual == null)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.EnvioNoEncontrado"));
                return;
            }
            if (!string.IsNullOrEmpty(dniDest) && envioActual.DNIDest456VG != dniDest)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.DNIIncorrecto"));
                return;
            }
            if (!string.IsNullOrEmpty(nombre) && !envioActual.NombreDest456VG.Equals(nombre, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.NombreIncorrecto"));
                return;
            }
            if (!string.IsNullOrEmpty(apellido) && !envioActual.ApellidoDest456VG.Equals(apellido, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.ApellidoIncorrecto"));
                return;
            }
            var datos = new[]
            {
                new
                {
                    CodEnvio = envioActual.CodEnvio456VG,
                    Remitente = $"{envioActual.Cliente.Nombre456VG} {envioActual.Cliente.Apellido456VG}",
                    DNIRemitente = envioActual.Cliente.DNI456VG,
                    Destinatario = $"{envioActual.NombreDest456VG} {envioActual.ApellidoDest456VG}",
                    DNIDest = envioActual.DNIDest456VG,
                    Provincia = envioActual.Provincia456VG,
                    Localidad = envioActual.Localidad456VG,
                    Domicilio = envioActual.Domicilio456VG,
                    TipoEnvio = envioActual.tipoenvio456VG
                }
            }.ToList();
            dataGridEnvio.DataSource = datos;
            dataGridPaquetes.DataSource = envioActual.Paquetes;
        }
        private void limpiar()
        {
            txtCodEnvio.Clear();
            txtDNIDest.Clear();
            txtNombreDest.Clear();
            txtApellidoDest.Clear();
            dataGridEnvio.DataSource = null;
            dataGridPaquetes.DataSource = null;
        }
        private void EntregaEnvios_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
            limpiar();
        }
        private void btnImprimir456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (envioActual == null)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.VerificarPrimero"));
                return;
            }
            switch (envioActual.EstadoEnvio456VG)
            {
                case "Pendiente de Entrega":
                    MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.EstadoPendiente"));
                    return;
                case "En Espera":
                    MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.EstadoEspera"));
                    return;
                case "Entregado":
                    MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.YaEntregado"));
                    return;
                case "En Tránsito":
                    break;
                case "Reasignación":
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.NoEntregarReasignado"),
                        lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Titulo.Validacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                case "Retiro por Sucursal":
                    if (MessageBox.Show(
                        lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.ConfirmarSucursal"),
                        lng.ObtenerTexto_456VG("EntregaEnvios_456VG.TituloConfirmacion"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    break;
                default:
                    MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.EstadoDesconocido"));
                    return;
            }
            string motivoEntrega = envioActual.EstadoEnvio456VG == "Retiro por Sucursal"
                    ? "Entregado Exitosamente por Sucursal"
                    : "Entregado Exitosamente";
            var usuario = SessionManager_456VG.ObtenerInstancia456VG().Usuario;
            string codEntrega = BEEntregado_456VG.GenerateCodEntrega456VG(
                envioActual.CodEnvio456VG,
                usuario.Nombre456VG,
                usuario.DNI456VG
            );
            var entrega = new BEEntregado_456VG(
                codEntrega,
                envioActual,
                DateTime.Now,
                0,
                true,
                motivoEntrega,
                $"{usuario.Nombre456VG} {usuario.Apellido456VG}"
            );
            bool ok = bllEntrega.RegistrarEntrega456VG(entrega);
            if (ok)
            {
                bllEnvio.actualizarEstadoEnvio456VG(envioActual.CodEnvio456VG, "Entregado");
                new BLLDigitoVerificador_456VG().ActualizarDV456VG();
                bllBitacora.AddBitacora456VG(usuario.DNI456VG, "Envíos", "Entrega de Envío", BEEventoBitacora_456VG.NVCriticidad456VG.Crítico);
                bool liberado = bllLista.VerificarYLiberarRecursosPorEntrega456VG(envioActual.CodEnvio456VG);
                MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.EntregaExitosa"));
                limpiar();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
