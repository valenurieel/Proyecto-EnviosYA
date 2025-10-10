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
    public partial class CargaEnvios_456VG : Form, IObserver_456VG
    {
        private readonly BLLListaCarga_456VG _bllLista = new BLLListaCarga_456VG();
        private readonly BLLDetalleListaCarga_456VG _bllDet = new BLLDetalleListaCarga_456VG();
        private List<BEListaCarga_456VG> _listas = new List<BEListaCarga_456VG>();
        private List<BEDetalleListaCarga_456VG> _detalles = new List<BEDetalleListaCarga_456VG>();
        private BEListaCarga_456VG _listaSeleccionada = null;
        private readonly BLLEnvio_456VG _bllEnvio = new BLLEnvio_456VG();
        private readonly BLLEventoBitacora_456VG bllBitacora = new BLLEventoBitacora_456VG();
        public CargaEnvios_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        private void CargaEnvios_456VG_Load(object sender, EventArgs e)
        {
            CargarCombosListasAbiertas();
            ConfigurarGrillas();
            ActualizarIdioma_456VG();
            dataGridLista.DataSource = null;
            dataGridDetalles.DataSource = null;
        }
        public void ActualizarIdioma_456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            TraducirColumnasGrillas456VG();
            lng.CambiarIdiomaControles_456VG(this);
        }
        private void TraducirCol(DataGridView grid, string colName, string key)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (grid.Columns.Contains(colName))
                grid.Columns[colName].HeaderText = lng.ObtenerTexto_456VG(key);
        }
        private void TraducirColumnasGrillas456VG()
        {
            TraducirCol(dataGridLista, "CodLista", "CargaEnvios_456VG.Columna.CodLista");
            TraducirCol(dataGridLista, "FechaCreacion", "CargaEnvios_456VG.Columna.FechaCreacion");
            TraducirCol(dataGridLista, "FechaSalida", "CargaEnvios_456VG.Columna.FechaSalida");
            TraducirCol(dataGridLista, "CantEnvios", "CargaEnvios_456VG.Columna.CantEnvios");
            TraducirCol(dataGridLista, "CantPaquetes", "CargaEnvios_456VG.Columna.CantPaquetes");
            TraducirCol(dataGridLista, "Transporte", "CargaEnvios_456VG.Columna.Transporte");
            TraducirCol(dataGridLista, "Chofer", "CargaEnvios_456VG.Columna.Chofer");
            TraducirCol(dataGridDetalles, "CodEnvio", "CargaEnvios_456VG.Columna.CodEnvio");
            TraducirCol(dataGridDetalles, "CantPaquetes", "CargaEnvios_456VG.Columna.CantPaquetes");
            TraducirCol(dataGridDetalles, "CodigosPaquetes", "CargaEnvios_456VG.Columna.CodigosPaquetes");
            TraducirCol(dataGridDetalles, "Estado", "CargaEnvios_456VG.Columna.Estado");
        }
        private void CargarCombosListasAbiertas()
        {
            _listas = _bllLista.leerEntidades456VG()
                .Where(l => l.EstadoLista456VG == "Abierta")
                .ToList();
            cmbListas.DataSource = _listas;
            cmbListas.DisplayMember = "CodLista456VG";
            cmbListas.ValueMember = "CodLista456VG";
            cmbListas.SelectedIndex = -1;
        }
        private void ConfigurarGrillas()
        {
            dataGridLista.AutoGenerateColumns = false;
            dataGridLista.Columns.Clear();
            dataGridLista.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodLista", HeaderText = "Código Lista", DataPropertyName = "CodLista456VG" });
            dataGridLista.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaCreacion", HeaderText = "Fecha Lista Creada", DataPropertyName = "FechaCreacion456VG" });
            dataGridLista.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaSalida", HeaderText = "Fecha Lista Salida", DataPropertyName = "FechaSalida456VG" });
            dataGridLista.Columns.Add(new DataGridViewTextBoxColumn { Name = "CantEnvios", HeaderText = "Cantidad Envíos", DataPropertyName = "CantEnvios456VG" });
            dataGridLista.Columns.Add(new DataGridViewTextBoxColumn { Name = "CantPaquetes", HeaderText = "Cantidad Paquetes", DataPropertyName = "CantPaquetes456VG" });
            dataGridLista.Columns.Add(new DataGridViewTextBoxColumn { Name = "Transporte", HeaderText = "Patente Transporte", DataPropertyName = "PatenteTransporte" });
            dataGridLista.Columns.Add(new DataGridViewTextBoxColumn { Name = "Chofer", HeaderText = "DNI Chofer", DataPropertyName = "DniChofer" });
            dataGridDetalles.AutoGenerateColumns = false;
            dataGridDetalles.Columns.Clear();
            dataGridDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Código Envío",
                DataPropertyName = "CodEnvio",
                Name = "CodEnvio"
            });
            dataGridDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Cant. Paquetes",
                DataPropertyName = "CantPaquetes",
                Name = "CantPaquetes"
            });
            dataGridDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Códigos Paquetes",
                DataPropertyName = "CodigosPaquetes",
                Name = "CodigosPaquetes"
            });
            dataGridDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Name = "Estado"
            });
        }
        private void cmbListas_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridLista.DataSource = null;
            dataGridDetalles.DataSource = null;
            if (cmbListas.SelectedItem is BEListaCarga_456VG lista)
            {
                _listaSeleccionada = lista;
                MostrarLista(lista);
                MostrarDetalles(lista);
            }
        }
        private void MostrarLista(BEListaCarga_456VG lista)
        {
            var data = new[]
            {
                new
                {
                    lista.CodLista456VG,
                    lista.FechaCreacion456VG,
                    lista.FechaSalida456VG,
                    lista.CantEnvios456VG,
                    lista.CantPaquetes456VG,
                    PatenteTransporte = lista.Transporte?.Patente456VG ?? "-",
                    DniChofer = lista.Chofer?.DNIChofer456VG ?? "-"
                }
            }.ToList();
            dataGridLista.DataSource = data;
        }
        private void MostrarDetalles(BEListaCarga_456VG lista)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            _detalles = _bllDet.leerEntidades456VG()
                .Where(d => d.Lista != null && d.Lista.CodLista456VG == lista.CodLista456VG)
                .ToList();
            var detallesData = _detalles.Select(d => new
            {
                CodEnvio = d.Envio?.CodEnvio456VG ?? "-",
                CantPaquetes = d.CantPaquetes456VG,
                CodigosPaquetes = d.Paquetes != null && d.Paquetes.Count > 0
                    ? string.Join(", ", d.Paquetes.Select(p => p.CodPaq456VG))
                    : "-",
                Estado = d.EstadoCargado == "Cargado"
            ? lng.ObtenerTexto_456VG("CargaEnvios_456VG.Estado.Cargado")
            : lng.ObtenerTexto_456VG("CargaEnvios_456VG.Estado.Pendiente")
            }).ToList();
            dataGridDetalles.DataSource = detallesData;
        }
        private void dataGridDetalles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridDetalles.Columns[e.ColumnIndex].Name == "Cargado")
            {
                bool marcado = (bool)dataGridDetalles.Rows[e.RowIndex].Cells["Cargado"].Value;
                var codEnvio = dataGridDetalles.Rows[e.RowIndex].Cells["CodEnvio"].Value?.ToString();
                var detalle = _detalles.FirstOrDefault(d => d.Envio?.CodEnvio456VG == codEnvio);
                if (detalle != null)
                {
                    detalle.EstadoCargado = marcado ? "Cargado" : "Pendiente";
                    var resDet = _bllDet.actualizarEntidad456VG(detalle);
                    string nuevoEstadoEnvio = marcado ? "En Tránsito" : "Pendiente de Entrega";
                    _bllEnvio.actualizarEstadoEnvio456VG(codEnvio, nuevoEstadoEnvio);
                    if (!resDet.resultado)
                        MessageBox.Show($"Error al actualizar detalle: {resDet.mensaje}");
                }
            }
        }
        private void btnMarcarCargado_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (dataGridDetalles.CurrentRow == null)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("CargaEnvios_456VG.Msg.SeleccionarEnvio"));
                return;
            }
            string codEnvio = dataGridDetalles.CurrentRow.Cells["CodEnvio"].Value?.ToString();
            var detalle = _detalles.FirstOrDefault(d => d.Envio?.CodEnvio456VG == codEnvio);
            if (detalle == null)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("CargaEnvios_456VG.Msg.DetalleNoEncontrado"));
                return;
            }
            if (detalle.EstadoCargado == "Cargado")
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("CargaEnvios_456VG.Msg.EnvioYaCargado"));
                return;
            }
            detalle.EstadoCargado = "Cargado";
            _bllDet.actualizarEntidad456VG(detalle);
            _bllEnvio.actualizarEstadoEnvio456VG(codEnvio, "En Tránsito");
            MessageBox.Show(string.Format(lng.ObtenerTexto_456VG("CargaEnvios_456VG.Msg.EnvioMarcado"), codEnvio));
            MostrarDetalles(_listaSeleccionada);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (_listaSeleccionada == null)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("CargaEnvios_456VG.Msg.SeleccionarLista"));
                return;
            }
            bool todosCargados = _detalles.All(d => d.EstadoCargado == "Cargado");
            if (!todosCargados)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("CargaEnvios_456VG.Msg.NoCerrarPendientes"));
                return;
            }
            _listaSeleccionada.EstadoLista456VG = "Cerrada";
            var resultado = _bllLista.actualizarEntidad456VG(_listaSeleccionada);
            if (resultado.resultado)
            {
                MessageBox.Show(string.Format(lng.ObtenerTexto_456VG("CargaEnvios_456VG.Msg.ListaCerrada"), _listaSeleccionada.CodLista456VG));
                CargarCombosListasAbiertas();
                string dniUsuario = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                bllBitacora.AddBitacora456VG(dniUsuario, "Recepción", "Carga de Envío", BEEventoBitacora_456VG.NVCriticidad456VG.Crítico);
            }
            else
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("CargaEnvios_456VG.Msg.ErrorCerrarLista") + resultado.mensaje);
            }
        }
    }
}
