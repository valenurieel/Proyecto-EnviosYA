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
    public partial class ReasignarEntrega_456VG : Form, IObserver_456VG
    {
        private readonly BLLEnvio_456VG bllEnvio = new BLLEnvio_456VG();
        private readonly BLLEventoBitacora_456VG bllBitacora = new BLLEventoBitacora_456VG();
        private readonly BLLEntregado_456VG bllEntrega = new BLLEntregado_456VG();
        private BEEnvío_456VG envioActual;
        private int intentosPrevios = 0;
        private readonly BLLListaCarga_456VG bllLista = new BLLListaCarga_456VG();
        public ReasignarEntrega_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
            ConfigurarGrillas();
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
        }
        private void ConfigurarGrillas()
        {
            dataGridEnvio.AutoGenerateColumns = false;
            dataGridEnvio.Columns.Clear();
            dataGridEnvio.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodEnvio", HeaderText = "Código Envío", DataPropertyName = "CodEnvio" });
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
        private void ReasignarEntrega_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
            limpiar();
        }
        private void limpiar()
        {
            txtCodEnvio.Clear();
            txtMotivo.Clear();
            dataGridEnvio.DataSource = null;
            dataGridPaquetes.DataSource = null;
            envioActual = null;
            intentosPrevios = 0;
        }
        private void btnImprimir456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (envioActual == null)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("ReasignarEntrega_456VG.Msg.VerificarPrimero"));
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("ReasignarEntrega_456VG.Msg.MotivoObligatorio"));
                return;
            }
            var usuario = SessionManager_456VG.ObtenerInstancia456VG().Usuario;
            string codEntrega = BEEntregado_456VG.GenerateCodEntrega456VG(envioActual.CodEnvio456VG, usuario.Nombre456VG, usuario.DNI456VG);
            int nuevosIntentos = intentosPrevios + 1;
            string nuevoEstado;
            DateTime? nuevaFecha = envioActual.FechaEntregaProgramada456VG;
            if (nuevosIntentos >= 2)
            {
                nuevoEstado = "Retiro por Sucursal";
                nuevaFecha = null;
            }
            else
            {
                nuevoEstado = "Reasignación";
                nuevaFecha = DateTime.Now.AddDays(7);
            }
            var entrega = new BEEntregado_456VG(
                codEntrega,
                envioActual,
                DateTime.Now,
                nuevosIntentos,
                false,
                txtMotivo.Text.Trim(),
                $"{usuario.Nombre456VG} {usuario.Apellido456VG}"
            );
            bool ok = bllEntrega.RegistrarEntrega456VG(entrega);
            if (ok)
            {
                BLLDigitoVerificador_456VG bllDV = new BLLDigitoVerificador_456VG();
                bllDV.ActualizarDV456VG();
                envioActual.EstadoEnvio456VG = nuevoEstado;
                envioActual.FechaEntregaProgramada456VG = nuevaFecha ?? default;
                bllEnvio.actualizarEstadoYFechaEnvio456VG(envioActual.CodEnvio456VG, nuevoEstado, nuevaFecha);
                bllBitacora.AddBitacora456VG(usuario.DNI456VG, "Envíos", "Reasignación de Envío", BEEventoBitacora_456VG.NVCriticidad456VG.Crítico);
                bool liberado = bllLista.VerificarYLiberarRecursosPorEntrega456VG(envioActual.CodEnvio456VG);
                MessageBox.Show(nuevosIntentos >= 2
                    ? lng.ObtenerTexto_456VG("ReasignarEntrega_456VG.Msg.RetiroSucursal")
                    : lng.ObtenerTexto_456VG("ReasignarEntrega_456VG.Msg.Reasignado"));
                limpiar();
            }
            else
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("ReasignarEntrega_456VG.Msg.ErrorRegistro"));
            }
        }
        private void txtCodEnvio_Leave(object sender, EventArgs e)
        {
            string codEnvio = txtCodEnvio.Text.Trim();
            if (string.IsNullOrWhiteSpace(codEnvio)) return;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            envioActual = bllEnvio.leerEntidades456VG().FirstOrDefault(a => a.CodEnvio456VG == codEnvio);
            if (envioActual == null)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("ReasignarEntrega_456VG.Msg.EnvioNoEncontrado"));
                limpiar();
                return;
            }
            switch (envioActual.EstadoEnvio456VG)
            {
                case "Entregado":
                    MessageBox.Show(lng.ObtenerTexto_456VG("ReasignarEntrega_456VG.Msg.YaEntregado"));
                    limpiar();
                    return;
                case "Retiro por Sucursal":
                    MessageBox.Show(lng.ObtenerTexto_456VG("ReasignarEntrega_456VG.Msg.EstadoSucursal"));
                    limpiar();
                    return;
                case "Pendiente de Entrega":
                case "En Tránsito":
                case "Reasignación":
                    break;
                default:
                    MessageBox.Show(lng.ObtenerTexto_456VG("EntregaEnvios_456VG.Msg.EstadoDesconocido"));
                    limpiar();
                    return;
            }
            dataGridEnvio.DataSource = new[]
            {
            new {
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
            dataGridPaquetes.DataSource = envioActual.Paquetes;
            intentosPrevios = ObtenerIntentosPrevios(envioActual.CodEnvio456VG);
        }
        private int ObtenerIntentosPrevios(string codEnvio)
        {
            try
            {
                return bllEntrega.ObtenerIntentosPorEnvio456VG(codEnvio);
            }
            catch
            {
                return 0;
            }
        }
    }
}
