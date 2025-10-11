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
    public partial class ListaCarga_456VG : Form, IObserver_456VG
    {
        BLLEnvio_456VG _bllEnv = new BLLEnvio_456VG();
        BLLTransporte_456VG _bllTrans = new BLLTransporte_456VG();
        BLLChofer_456VG _bllChof = new BLLChofer_456VG();
        BLLListaCarga_456VG _bllLista = new BLLListaCarga_456VG();
        BLLDetalleListaCarga_456VG _bllDet = new BLLDetalleListaCarga_456VG();
        BLLEventoBitacora_456VG _bllEvent = new BLLEventoBitacora_456VG();
        private List<BEEnvío_456VG> _enviosFiltrados = new List<BEEnvío_456VG>();
        private readonly List<BEEnvío_456VG> _enviosSeleccionados = new List<BEEnvío_456VG>();
        private BETransporte_456VG _transporteSel = null;
        private BEChofer_456VG _choferSel = null;
        private string _listaTipoEnvio = null;
        private string _listaZona = null;
        public ListaCarga_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
            chkFiltrarFecha.CheckedChanged += chkFiltrarFecha_CheckedChanged;
        }
        private void chkFiltrarFecha_CheckedChanged(object sender, EventArgs e) => RefrescarFiltrosYGrilla456VG();

        private void label5_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ConfigurarGrids456VG()
        {
            dataEnv.AutoGenerateColumns = false;
            dataEnv.Columns.Clear();
            dataEnv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataEnv.MultiSelect = false;
            dataEnv.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoEnvio", DataPropertyName = "CodigoEnvio", Width = 120 });
            dataEnv.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoEnvio", DataPropertyName = "TipoEnvio", Width = 70 });
            dataEnv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Direccion", DataPropertyName = "Direccion", Width = 200 });
            dataEnv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Paquetes", DataPropertyName = "Paquetes", Width = 60 });
            dataEnv.Columns.Add(new DataGridViewTextBoxColumn { Name = "PesoTotal", DataPropertyName = "PesoTotal", Width = 80 });
            dataEnv.Columns.Add(new DataGridViewTextBoxColumn { Name = "VolTotal", DataPropertyName = "VolTotal", Width = 80 });
            dataEnv.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaProg", DataPropertyName = "FechaProg", Width = 100 });
            dataEnv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", DataPropertyName = "Estado", Width = 100 });
            dataTrans.AutoGenerateColumns = false;
            dataTrans.Columns.Clear();
            dataTrans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataTrans.MultiSelect = false;
            dataTrans.Columns.Add(new DataGridViewTextBoxColumn { Name = "Patente", DataPropertyName = "Patente", Width = 120 });
            dataTrans.Columns.Add(new DataGridViewTextBoxColumn { Name = "Marca", DataPropertyName = "Marca", Width = 140 });
            dataTrans.Columns.Add(new DataGridViewTextBoxColumn { Name = "Anio", DataPropertyName = "Anio", Width = 90 });
            dataTrans.Columns.Add(new DataGridViewTextBoxColumn { Name = "Capacidad Peso", DataPropertyName = "CapPeso", Width = 100 });
            dataTrans.Columns.Add(new DataGridViewTextBoxColumn { Name = "Capacidad Vol", DataPropertyName = "CapVol", Width = 100 });
            dataTrans.Columns.Add(new DataGridViewTextBoxColumn { Name = "Disponible", DataPropertyName = "Disponible", Width = 80 });
            dataChof.AutoGenerateColumns = false;
            dataChof.Columns.Clear();
            dataChof.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataChof.MultiSelect = false;
            dataChof.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNI", DataPropertyName = "DNI", Width = 80 });
            dataChof.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", Width = 100 });
            dataChof.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido", DataPropertyName = "Apellido", Width = 100 });
            dataChof.Columns.Add(new DataGridViewTextBoxColumn { Name = "Telefono", DataPropertyName = "Telefono", Width = 100 });
            dataChof.Columns.Add(new DataGridViewTextBoxColumn { Name = "Registro", DataPropertyName = "Registro", Width = 80 });
            dataChof.Columns.Add(new DataGridViewTextBoxColumn { Name = "VencimientoRegistro", DataPropertyName = "VencimientoRegistro", Width = 115 });
            dataChof.Columns.Add(new DataGridViewTextBoxColumn { Name = "Disponible", DataPropertyName = "Disponible", Width = 80 });
        }
        public void ActualizarIdioma_456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            lng.CambiarIdiomaControles_456VG(this);
            TraducirColumnasGrillas456VG();
            CargarCombos456VG();
            RefrescarFiltrosYGrilla456VG();
        }
        private void ListaCarga_456VG_Load(object sender, EventArgs e)
        {
            ConfigurarGrids456VG();
            ActualizarIdioma_456VG();
            CargarTransporteChofer456VG();
            CargarCombos456VG();
            RefrescarFiltrosYGrilla456VG();
        }
        private void CargarCombos456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            cmbEstadoEnv.Items.Clear();
            cmbEstadoEnv.Items.Add("Pendiente de Entrega");
            cmbEstadoEnv.Items.Add("Reasignación");
            cmbEstadoEnv.Items.Add("En Espera");
            cmbEstadoEnv.Items.Add("En Tránsito");
            cmbEstadoEnv.Items.Add("Entregado");
            cmbEstadoEnv.Items.Add("Retiro por Sucursal");
            cmbEstadoEnv.SelectedIndex = 0;
            cmbZonaEnv.Items.Clear();
            cmbZonaEnv.Items.Add("Alta");
            cmbZonaEnv.Items.Add("Media");
            cmbZonaEnv.Items.Add("Baja");
            cmbZonaEnv.SelectedIndex = -1;
            cmbTipEnv.Items.Clear();
            cmbTipEnv.Items.Add("Normal");
            cmbTipEnv.Items.Add("Express");
            cmbTipEnv.SelectedIndex = -1;
            cmbEstadoEnv.Items[0] = lng.ObtenerTexto_456VG("ListaCarga_456VG.Combo.Pendiente");
            cmbEstadoEnv.Items[1] = lng.ObtenerTexto_456VG("ListaCarga_456VG.Combo.Reasignacion");
            cmbEstadoEnv.Items[2] = lng.ObtenerTexto_456VG("ListaCarga_456VG.Combo.EnEspera");
            cmbEstadoEnv.Items[3] = lng.ObtenerTexto_456VG("ListaCarga_456VG.Combo.EnTransito");
            cmbEstadoEnv.Items[4] = lng.ObtenerTexto_456VG("ListaCarga_456VG.Combo.Entregado");
            cmbEstadoEnv.Items[5] = lng.ObtenerTexto_456VG("ListaCarga_456VG.Combo.Retiro");
            cmbZonaEnv.Items[0] = lng.ObtenerTexto_456VG("ListaCarga_456VG.Combo.ZonaAlta");
            cmbZonaEnv.Items[1] = lng.ObtenerTexto_456VG("ListaCarga_456VG.Combo.ZonaMedia");
            cmbZonaEnv.Items[2] = lng.ObtenerTexto_456VG("ListaCarga_456VG.Combo.ZonaBaja");
            cmbTipEnv.Items[0] = lng.ObtenerTexto_456VG("ListaCarga_456VG.Combo.normal");
            cmbTipEnv.Items[1] = lng.ObtenerTexto_456VG("ListaCarga_456VG.Combo.express");
        }
        private void CargarTransporteChofer456VG()
        {
            var listaT = (_bllTrans.leerEntidades456VG() ?? new List<BETransporte_456VG>())
                .Where(t => t.Activo456VG && t.Disponible456VG)
                .ToList();
            var gridT = listaT.Select(t => new
            {
                Patente = t.Patente456VG,
                Marca = t.Marca456VG,
                Anio = t.Año456VG,
                CapPeso = t.CapacidadPeso456VG,
                CapVol = t.CapacidadVolumen456VG,
                Disponible = t.Disponible456VG ? "Sí" : "No",
                Ref = t
            }).ToList();
            dataTrans.DataSource = gridT;
            dataTrans.ClearSelection();
            var listaC = (_bllChof.leerEntidades456VG() ?? new List<BEChofer_456VG>())
                .Where(c => c.Activo456VG && c.Disponible456VG)
                .ToList();
            var gridC = listaC.Select(c => new
            {
                DNI = c.DNIChofer456VG,
                Nombre = c.Nombre456VG,
                Apellido = c.Apellido456VG,
                Telefono = c.Teléfono456VG,
                Registro = c.Registro456VG ? "Sí" : "No",
                VencimientoRegistro = c.VencimientoRegistro456VG.ToString("dd/MM/yyyy"),
                Disponible = c.Disponible456VG ? "Sí" : "No",
                Ref = c
            }).ToList();
            dataChof.DataSource = gridC;
            dataChof.ClearSelection();
        }
        private void btnImprimir456VG_Click(object sender, EventArgs e)
        {
            if (dataEnv.CurrentRow == null) return;
            var cod = dataEnv.CurrentRow.Cells["CodigoEnvio"]?.Value?.ToString();
            var item = _enviosFiltrados.FirstOrDefault(x => x.CodEnvio456VG == cod);
            if (item == null) return;
            if (_enviosSeleccionados.Count == 0)
            {
                _listaZona = ObtenerZona(item.Provincia456VG);
                _listaTipoEnvio = NormalizarTipo(item.tipoenvio456VG);
            }
            else
            {
                var zona = ObtenerZona(item.Provincia456VG);
                var tipo = NormalizarTipo(item.tipoenvio456VG);
                if (!string.Equals(zona, _listaZona, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        Traducir("ListaCarga_456VG.Msg.ZonaNoCoincide"),
                        Traducir("ListaCarga_456VG.Titulo.Validacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
            }
            if (!(item.EstadoEnvio456VG == "Pendiente de Entrega" || item.EstadoEnvio456VG == "Reasignación"))
            {
                MessageBox.Show("Estos Envíos no pueden ser Seleccionados.", "Estado inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_enviosSeleccionados.Any(e2 => e2.CodEnvio456VG == item.CodEnvio456VG))
            {
                MessageBox.Show(Traducir("ListaCarga_456VG.Msg.EnvioYaAgregado"), "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _enviosSeleccionados.Add(item);
            item.EstadoEnvio456VG = "En Espera";
            _bllEnv.actualizarEstadoEnvio456VG(item.CodEnvio456VG, "En Espera");
            MessageBox.Show(Traducir("ListaCarga_456VG.Msg.EnvioAgregado"), "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarFiltrosYGrilla456VG();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
                if (_enviosSeleccionados.Count == 0)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("ListaCarga_456VG.Msg.NoHayEnvios"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_transporteSel == null)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("ListaCarga_456VG.Msg.AsignarTransporte"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_choferSel == null)
                {
                    MessageBox.Show(lng.ObtenerTexto_456VG("ListaCarga_456VG.Msg.AsignarChofer"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int cantEnv = _enviosSeleccionados.Count;
                int cantPaq = _enviosSeleccionados.Sum(r => r.Paquetes?.Count ?? 0);
                float pesoTot = _enviosSeleccionados.Sum(r => r.Paquetes?.Sum(p => p.Peso456VG) ?? 0f);
                float volTot = _enviosSeleccionados.Sum(r => r.Paquetes?.Sum(p => (p.Alto456VG * p.Ancho456VG * p.Largo456VG) / 1000f) ?? 0f);
                DateTime fechaCre = DateTime.Now;
                DateTime fechaSalida = dateTimePicker1.Value.Date;
                string estadoLista = "Abierta";
                var lista = new BEListaCarga_456VG(
                    fechacre: fechaCre,
                    tzona: _listaZona,
                    tenv: _listaTipoEnvio,
                    canenv: cantEnv,
                    cantpaq: cantPaq,
                    peso: pesoTot,
                    vol: volTot,
                    chof: _choferSel,
                    trans: _transporteSel,
                    fsalida: fechaSalida,
                    estado: estadoLista
                );
                var resLista = _bllLista.crearEntidad456VG(lista);
                if (!resLista.resultado)
                {
                    MessageBox.Show($"Error al crear la lista: {resLista.mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                bool detallesOk = true;
                foreach (var env in _enviosSeleccionados)
                {
                    System.Threading.Thread.Sleep(5);
                    var detalle = new BEDetalleListaCarga_456VG(
                        lista,
                        env,
                        env.Paquetes ?? new List<BEPaquete_456VG>(),
                        env.Paquetes?.Count ?? 0,
                        "Pendiente"
                    );
                    var resDet = _bllDet.crearEntidad456VG(detalle);
                    if (!resDet.resultado)
                    {
                        detallesOk = false;
                        MessageBox.Show($"Error al insertar detalle de envío {env.CodEnvio456VG}: {resDet.mensaje}");
                    }
                }
                if (!detallesOk)
                {
                    MessageBox.Show("La lista fue creada, pero algunos detalles no se registraron correctamente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                _bllEvent.AddBitacora456VG(
                    dni: dniLog,
                    modulo: "Recepción",
                    accion: "Generar Lista Carga",
                    crit: BEEventoBitacora_456VG.NVCriticidad456VG.Crítico
                );
                MessageBox.Show(
                    string.Format(lng.ObtenerTexto_456VG("ListaCarga_456VG.Msg.ListaGenerada"), lista.CodLista456VG),
                    "OK",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                _enviosSeleccionados.Clear();
                _transporteSel = null;
                _choferSel = null;
                _listaTipoEnvio = null;
                _listaZona = null;
                btnAsigTrans.Enabled = true;
                btnAsigChof.Enabled = true;
                ListaCarga_456VG_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAsigTrans_Click(object sender, EventArgs e)
        {
            if (dataTrans.CurrentRow == null) return;
            var patente = dataTrans.CurrentRow.Cells["Patente"]?.Value?.ToString();
            var todosT = _bllTrans.leerEntidades456VG() ?? new List<BETransporte_456VG>();
            _transporteSel = todosT.FirstOrDefault(t => t.Patente456VG == patente);
            if (_transporteSel != null)
            {
                _transporteSel.Disponible456VG = false;
                _bllTrans.actualizarEntidad456VG(_transporteSel);
                CargarTransporteChofer456VG();
                btnAsigTrans.Enabled = false;
                MessageBox.Show(Traducir("ListaCarga_456VG.Msg.TransporteAsignado"), "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnAsigChof_Click(object sender, EventArgs e)
        {
            if (dataChof.CurrentRow == null) return;
            var dni = dataChof.CurrentRow.Cells["DNI"]?.Value?.ToString();
            var todosC = _bllChof.leerEntidades456VG() ?? new List<BEChofer_456VG>();
            _choferSel = todosC.FirstOrDefault(c => c.DNIChofer456VG == dni);
            if (_choferSel != null)
            {
                _choferSel.Disponible456VG = false;
                _bllChof.actualizarEntidad456VG(_choferSel);
                CargarTransporteChofer456VG();
                btnAsigChof.Enabled = false;
                MessageBox.Show(Traducir("ListaCarga_456VG.Msg.ChoferAsignado"), "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private string MapearZonaComboConProvincia456VG(string provincia)
        {
            var zona = ObtenerZona(provincia);
            if (zona == "Zona Alta") return "Alta";
            if (zona == "Zona Baja") return "Baja";
            if (zona == "Buenos Aires") return "Media";
            return "Baja";
        }
        private void dataEnv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void dataTrans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void dataChof_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private string NormalizarTipo(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo)) return "Normal";
            return (tipo.Equals("express", StringComparison.OrdinalIgnoreCase) ? "Express" : "Normal");
        }
        private string ObtenerZona(string provincia)
        {
            if (string.IsNullOrWhiteSpace(provincia)) return "Zona Baja";
            var p = provincia.Trim().ToLower();
            if (p == "buenos aires") return "Buenos Aires";
            var zonaAlta = new HashSet<string> {
                "mendoza","san luis","cordoba","córdoba","tucuman","tucumán","san juan","la rioja",
                "santa fe","entre rios","entre ríos","corrientes","misiones","jujuy","salta","formosa",
                "chaco","santiago del estero","catamarca"
            };
            return zonaAlta.Contains(p) ? "Zona Alta" : "Zona Baja";
        }
        private void RefrescarFiltrosYGrilla456VG()
        {
            var todos = _bllEnv.leerEntidades456VG() ?? new List<BEEnvío_456VG>();
            string estadoFiltro = cmbEstadoEnv.SelectedItem as string;
            string zonaFiltro = cmbZonaEnv.SelectedItem as string;
            string tipoFiltro = cmbTipEnv.SelectedItem as string;
            DateTime fechaProg = dateTimePicker1.Value.Date;
            bool filtrarFecha = chkFiltrarFecha.Checked;
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            _enviosFiltrados = todos
                .Where(e =>
                    (string.IsNullOrEmpty(estadoFiltro) || lng.ObtenerTexto_456VG($"ListaCarga_456VG.Combo.{e.EstadoEnvio456VG.Replace(" ", "").ToLower()}") == estadoFiltro) &&
                    (string.IsNullOrEmpty(zonaFiltro) || lng.ObtenerTexto_456VG($"ListaCarga_456VG.Combo.Zona{MapearZonaComboConProvincia456VG(e.Provincia456VG)}") == zonaFiltro) &&
                    (string.IsNullOrEmpty(tipoFiltro) || lng.ObtenerTexto_456VG($"ListaCarga_456VG.Combo.{e.tipoenvio456VG.ToLower()}") == tipoFiltro) &&
                    (!filtrarFecha || e.FechaEntregaProgramada456VG.Date == fechaProg)
                )
                .ToList();
            var lista = _enviosFiltrados.Select(e => new
            {
                CodigoEnvio = e.CodEnvio456VG,
                TipoEnvio = lng.ObtenerTexto_456VG($"ListaCarga_456VG.Combo.{e.tipoenvio456VG.ToLower()}"),
                Direccion = $"{e.Domicilio456VG}, {e.Localidad456VG}, {e.Provincia456VG}",
                Paquetes = e.Paquetes?.Count ?? 0,
                PesoTotal = e.Paquetes?.Sum(p => p.Peso456VG) ?? 0f,
                VolTotal = e.Paquetes?.Sum(p => (p.Alto456VG * p.Ancho456VG * p.Largo456VG) / 1000f) ?? 0f,
                FechaProg = e.FechaEntregaProgramada456VG.ToString("dd/MM/yyyy"),
                Estado = lng.ObtenerTexto_456VG($"ListaCarga_456VG.Combo.{e.EstadoEnvio456VG.Replace(" ", "").ToLower()}"),
                Ref = e
            }).ToList();
            dataEnv.DataSource = lista;
            dataEnv.ClearSelection();
        }
        private void TraducirColumnasGrillas456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (dataEnv.Columns.Contains("CodigoEnvio")) dataEnv.Columns["CodigoEnvio"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.CodigoEnvio");
            if (dataEnv.Columns.Contains("TipoEnvio")) dataEnv.Columns["TipoEnvio"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.TipoEnvio");
            if (dataEnv.Columns.Contains("Direccion")) dataEnv.Columns["Direccion"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.Direccion");
            if (dataEnv.Columns.Contains("Paquetes")) dataEnv.Columns["Paquetes"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.Paquetes");
            if (dataEnv.Columns.Contains("PesoTotal")) dataEnv.Columns["PesoTotal"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.PesoTotal");
            if (dataEnv.Columns.Contains("VolTotal")) dataEnv.Columns["VolTotal"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.VolTotal");
            if (dataEnv.Columns.Contains("FechaProg")) dataEnv.Columns["FechaProg"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.FechaProg");
            if (dataEnv.Columns.Contains("Estado")) dataEnv.Columns["Estado"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.Estado");
            if (dataTrans.Columns.Contains("Patente")) dataTrans.Columns["Patente"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.Patente");
            if (dataTrans.Columns.Contains("Marca")) dataTrans.Columns["Marca"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.Marca");
            if (dataTrans.Columns.Contains("Anio")) dataTrans.Columns["Anio"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.ano");
            if (dataTrans.Columns.Contains("Capacidad Peso")) dataTrans.Columns["Capacidad Peso"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.CapPeso");
            if (dataTrans.Columns.Contains("Capacidad Vol")) dataTrans.Columns["Capacidad Vol"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.CapVol");
            if (dataTrans.Columns.Contains("Disponible")) dataTrans.Columns["Disponible"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.Disponible");
            if (dataChof.Columns.Contains("DNI")) dataChof.Columns["DNI"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.DNI");
            if (dataChof.Columns.Contains("Nombre")) dataChof.Columns["Nombre"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.Nombre");
            if (dataChof.Columns.Contains("Apellido")) dataChof.Columns["Apellido"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.Apellido");
            if (dataChof.Columns.Contains("Telefono")) dataChof.Columns["Telefono"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.Teléfono");
            if (dataChof.Columns.Contains("Registro")) dataChof.Columns["Registro"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.Registro");
            if (dataChof.Columns.Contains("VencimientoRegistro")) dataChof.Columns["VencimientoRegistro"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.VencimientoRegistro");
            if (dataChof.Columns.Contains("Disponible")) dataChof.Columns["Disponible"].HeaderText = lng.ObtenerTexto_456VG("ListaCarga_456VG.Columna.Disponible");
        }
        private string Traducir(string clave)
        {
            return Lenguaje_456VG.ObtenerInstancia_456VG().ObtenerTexto_456VG(clave);
        }
        private void cmbEstadoEnv_SelectedIndexChanged(object sender, EventArgs e) => RefrescarFiltrosYGrilla456VG();
        private void cmbZonaEnv_SelectedIndexChanged(object sender, EventArgs e) => RefrescarFiltrosYGrilla456VG();
        private void cmbTipEnv_SelectedIndexChanged(object sender, EventArgs e) => RefrescarFiltrosYGrilla456VG();
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) => RefrescarFiltrosYGrilla456VG();
        private void button2_Click(object sender, EventArgs e)
        {
            cmbEstadoEnv.SelectedIndex = -1;
            cmbZonaEnv.SelectedIndex = -1;
            cmbTipEnv.SelectedIndex = -1;
            dateTimePicker1.Checked = false;
        }
        private void dataTrans_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }
        private void dataChof_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }
    }
}
