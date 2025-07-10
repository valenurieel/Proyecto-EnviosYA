using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;

namespace Proyecto_EnviosYA
{
    public partial class Perfiles_456VG : Form, IObserver_456VG
    {
        private readonly BLLPerfil_456VG bllp = new BLLPerfil_456VG();
        private Dictionary<string, string> dictPermisosTraducidos;
        private Dictionary<string, string> dictFamiliasTraducidas;
        private Dictionary<string, string> dictPerfilesTraducidos;
        public Perfiles_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        public void ActualizarIdioma_456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            lng.CambiarIdiomaControles_456VG(this);
            ActualizarTextosTree(treeView1456VG.Nodes);
        }
        private void TreeView1456VG_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text != "...")
                return;
            e.Node.Nodes.Clear();
            if (e.Node.Tag is FamiliaPermiso_456VG familia)
            {
                var familiaCompleta = bllp.ObtenerFamiliaCompleta456VG(familia.Nombre456VG);
                if (familiaCompleta != null)
                {
                    foreach (var hijo in familiaCompleta.Permisos456VG)
                    {
                        TreeNode nodoHijo = CrearNodoPermisoRecursivo456VG(hijo, true);
                        e.Node.Nodes.Add(nodoHijo);
                    }
                }
            }
        }
        private TreeNode CrearNodoPermisoRecursivo456VG(IPerfil_456VG permiso, bool expandirFamilia = false)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string claveJSON = permiso is FamiliaPermiso_456VG
                ? "Perfiles_456VG.Item.cmboxflia456VG." + permiso.Nombre456VG
                : "Perfiles_456VG.Item.comboBox1456VG." + permiso.Nombre456VG;
            string nombreTraducido = lng.ObtenerTexto_456VG(claveJSON);
            TreeNode nodo = new TreeNode(nombreTraducido) { Tag = permiso };
            if (permiso is FamiliaPermiso_456VG familia && expandirFamilia)
            {
                foreach (var hijo in familia.Permisos456VG)
                {
                    nodo.Nodes.Add(CrearNodoPermisoRecursivo456VG(hijo, true));
                }
            }
            return nodo;
        }
        private void ActualizarTextosTree(TreeNodeCollection nodes)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            foreach (TreeNode n in nodes)
            {
                n.Text = lng.ObtenerTexto_456VG(n.Name);
                if (n.Nodes.Count > 0)
                    ActualizarTextosTree(n.Nodes);
            }
        }
        private void button1456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string nombre = textBox1456VG.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NombrePerfilVacio"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bllp.CrearPerfil456VG(nombre))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PerfilCreadoExito"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox3456VG.Items.Add(nombre);
                textBox1456VG.Clear();
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PerfilYaExiste"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button4456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (comboBox3456VG.SelectedItem == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarPerfilPrimero"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var nombrePerfil = ObtenerClaveOriginal(dictPerfilesTraducidos, comboBox3456VG.SelectedItem);
            var perfil = bllp.ObtenerPerfilCompleto456VG(nombrePerfil);
            if (perfil == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorCargarPerfil"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            treeView1456VG.Nodes.Clear();
            TreeNode nodoPerfil = new TreeNode(perfil.Nombre456VG)
            {
                Tag = perfil
            };
            foreach (var permiso in perfil.Permisos456VG)
            {
                TreeNode nodoPermiso = CrearNodoPermisoRecursivo456VG(permiso, true);
                nodoPerfil.Nodes.Add(nodoPermiso);
            }
            treeView1456VG.Nodes.Add(nodoPerfil);
        }
        private void button3456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1456VG.SelectedNode;
            if (nodoSel == null || nodoSel.Parent != null || !(nodoSel.Tag is BEPerfil_456VG perfil))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloPermisoPerfilRaiz"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox1456VG.SelectedItem == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarPermiso"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var nombrePermiso = ObtenerClaveOriginal(dictPermisosTraducidos, comboBox1456VG.SelectedItem);
            int codPermiso = bllp.ObtenerCodPermisoPorNombre456VG(nombrePermiso);
            if (codPermiso <= 0)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorPermisoInvalido"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var permiso = new Permiso_456VG
            {
                CodPermiso456VG = codPermiso,
                Nombre456VG = nombrePermiso
            };
            if (!bllp.AgregarPermisoAPerfil456VG(perfil.Nombre456VG, permiso))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoYaAsignado"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoAgregado"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            button4456VG_Click(null, null);
        }
        private void button2456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (treeView1456VG.SelectedNode == null || !(treeView1456VG.SelectedNode.Tag is BEPerfil_456VG perfil))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarPerfilEliminar"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ConfirmarEliminarPerfil"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                string perfilAEliminar = perfil.Nombre456VG;
                string perfilLogueado = SessionManager_456VG.Obtenerdatosuser456VG().Rol456VG.Nombre456VG;
                bool eliminado = bllp.EliminarPerfil456VG(perfilAEliminar);
                if (eliminado)
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PerfilEliminadoExito"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarCombos456VG();
                    button6_Click(null, null);
                    if (perfilAEliminar == perfilLogueado)
                    {
                        MessageBox.Show(
                            lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PerfilActualEliminado"),
                            lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        var usuarioActual = SessionManager_456VG.Obtenerdatosuser456VG();
                        var idiomaFinal = SessionManager_456VG.IdiomaTemporal_456VG;
                        new BLLUsuario_456VG().modificarIdioma456VG(usuarioActual, idiomaFinal);
                        SessionManager_456VG.ObtenerInstancia456VG().CerrarSesion456VG();
                        Lenguaje_456VG.ObtenerInstancia_456VG().IdiomaActual_456VG = "ES";
                        SessionManager_456VG.IdiomaTemporal_456VG = "ES";
                        var menu = Application.OpenForms.OfType<MenuPrincipal_456VG>().FirstOrDefault();
                        if (menu != null)
                        {
                            menu.deshabilitados();
                            menu.chau();
                        }
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorEliminarPerfil"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button8456VG_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Perfiles_456VG_Load(object sender, EventArgs e)
        {
            dictPermisosTraducidos = new Dictionary<string, string>();
            dictFamiliasTraducidas = new Dictionary<string, string>();
            dictPerfilesTraducidos = new Dictionary<string, string>();
            ActualizarIdioma_456VG();
            CargarCombos456VG();
        }
        private void comboBox3456VG_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void cmboxflia456VG_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void aggflia456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1456VG.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is BEPerfil_456VG perfil))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarPerfilAgregarFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmboxflia456VG.SelectedItem == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var nombreFamilia = ObtenerClaveOriginal(dictFamiliasTraducidas, cmboxflia456VG.SelectedItem);
            var familia = bllp.ObtenerFamiliaCompleta456VG(nombreFamilia);
            if (familia == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorObtenerFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!bllp.AgregarFamiliaAPerfil456VG(perfil.Nombre456VG, familia))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.FamiliaYaAsignada"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.FamiliaAgregada"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            button4456VG_Click(null, null);
        }
        private void label6_Click(object sender, EventArgs e)
        {
        }
        private void BTNCrearFamilia456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string nombreFamilia = TXTFamilia456VG.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombreFamilia))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NombreFamiliaVacio"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bllp.CrearFamilia456VG(nombreFamilia))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.FamiliaCreadaOk"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CBFamilias456VG.Items.Add(nombreFamilia);
                cmboxflia456VG.Items.Add(nombreFamilia);
                TXTFamilia456VG.Clear();
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorCrearFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BTNEliminarFamilia456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1.SelectedNode;

            if (nodoSel == null || !(nodoSel.Tag is FamiliaPermiso_456VG familia))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarFamiliaEliminar"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nodoSel.Parent != null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloEliminarFamiliaRaiz"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirmar = MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ConfirmarEliminarFamiliaRaiz"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                if (bllp.EliminarFamilia456VG(familia.Nombre456VG))
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.FamiliaRaizEliminadaOk"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarCombos456VG();
                    RefrescarPerfilSiContieneFamilia(familia.Nombre456VG);
                    button5_Click(null, null);
                }
                else
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorEliminarFamiliaRaiz"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void BTNAplicar456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nombreFamilia = ObtenerClaveOriginal(dictFamiliasTraducidas, CBFamilias456VG.SelectedItem);
            var familiaAInsertar = bllp.ObtenerFamiliaCompleta456VG(nombreFamilia);
            if (string.IsNullOrEmpty(nombreFamilia) || familiaAInsertar == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarFamiliaAgregar"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (treeView1.Nodes.Count == 0)
            {
                var nodoRaiz = CrearNodoPermisoRecursivo456VG(familiaAInsertar, true);
                treeView1.Nodes.Add(nodoRaiz);
                treeView1.ExpandAll();
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.FamiliaAgregadaOk"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var nodoSel = treeView1.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is FamiliaPermiso_456VG familiaDestino))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarFamiliaDestino"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nodoSel.Parent != null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloAgregarAFamiliaRaiz"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (familiaDestino.CodPermiso456VG == familiaAInsertar.CodPermiso456VG)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NoAgregarFamiliaASiMisma"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            familiaDestino = bllp.ObtenerFamiliaCompleta456VG(familiaDestino.Nombre456VG);
            var permisosDestino = familiaDestino.ObtenerTodosLosPermisos_456VG();
            var permisosInsertar = familiaAInsertar.ObtenerTodosLosPermisos_456VG();
            if (permisosInsertar.Any(p => permisosDestino.Any(pd => pd.CodPermiso456VG == p.CodPermiso456VG)))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.FamiliaYaContenida"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bllp.AgregarPermisoAFamilia456VG(familiaDestino.Nombre456VG, familiaAInsertar))
            {
                var nodoHijo = CrearNodoPermisoRecursivo456VG(familiaAInsertar, true);
                nodoSel.Nodes.Add(nodoHijo);
                nodoSel.Expand();
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.FamiliaAgregadaOk"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCombos456VG();
                RefrescarPerfilSiContieneFamilia(familiaDestino.Nombre456VG);
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorAgregarFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BTNAgregarPermiso456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is FamiliaPermiso_456VG familia))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarFamiliaPermiso"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nodoSel.Parent != null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloPermisoAFamiliaRaiz"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var nombrePermiso = ObtenerClaveOriginal(dictPermisosTraducidos, CBPermisos456VG.SelectedItem);
            if (string.IsNullOrEmpty(nombrePermiso))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarPermisoAgregar"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var codPermiso = bllp.ObtenerCodPermisoPorNombre456VG(nombrePermiso);
            if (codPermiso <= 0)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorPermisoInvalido"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var permiso = new Permiso_456VG
            {
                CodPermiso456VG = codPermiso,
                Nombre456VG = nombrePermiso
            };
            var permisosActuales = familia.ObtenerTodosLosPermisos_456VG();
            if (permisosActuales.Any(p => p.CodPermiso456VG == permiso.CodPermiso456VG))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoYaAsignadoFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bllp.AgregarPermisoAFamilia456VG(familia.Nombre456VG, permiso))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoAgregadoFamiliaOK"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                nodoSel.Nodes.Clear();
                var familiaActualizada = bllp.ObtenerFamiliaCompleta456VG(familia.Nombre456VG);
                if (familiaActualizada != null)
                {
                    foreach (var hijo in familiaActualizada.Permisos456VG)
                    {
                        nodoSel.Nodes.Add(CrearNodoPermisoRecursivo456VG(hijo, true));
                    }
                    nodoSel.Expand();
                }
                CargarCombos456VG();
                RefrescarPerfilSiContieneFamilia(familia.Nombre456VG);
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorAgregarPermisoFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1456VG.SelectedNode;
            if (nodoSel == null || nodoSel.Parent == null || !(nodoSel.Parent.Tag is BEPerfil_456VG perfil))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloQuitarDirectoDePerfil"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!(nodoSel.Tag is IPerfil_456VG permiso))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionNoValida"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirmar = MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ConfirmarQuitarPermiso"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                if (bllp.QuitarPermisoOFamiliaDePerfil456VG(perfil.Nombre456VG, permiso.CodPermiso456VG))
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoQuitado"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button4456VG_Click(null, null);
                }
                else
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorQuitarPermiso"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1456VG.SelectedNode;
            if (nodoSel == null || nodoSel.Parent == null || !(nodoSel.Parent.Tag is BEPerfil_456VG perfil))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloQuitarFamiliaDirecta"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!(nodoSel.Tag is FamiliaPermiso_456VG familia))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionNoEsFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirmar = MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ConfirmarQuitarFamilia"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                if (bllp.QuitarPermisoOFamiliaDePerfil456VG(perfil.Nombre456VG, familia.CodPermiso456VG))
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.FamiliaQuitada"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarPerfilSiContieneFamilia(familia.Nombre456VG);
                    button4456VG_Click(null, null);
                }
                else
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorQuitarFamilia"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is FamiliaPermiso_456VG familiaAEliminar))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarSubfamiliaEliminar"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nodoSel.Parent == null || !(nodoSel.Parent.Tag is FamiliaPermiso_456VG familiaPadre))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloSubfamiliaDirecta"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirmar = MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ConfirmarQuitarSubfamilia"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                if (bllp.QuitarPermisoDeFamilia456VG(familiaPadre.Nombre456VG, familiaAEliminar.CodPermiso456VG))
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SubfamiliaQuitadaOk"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TreeNode nodoPadre = nodoSel.Parent;
                    nodoPadre.Nodes.Clear();
                    var familiaActualizada = bllp.ObtenerFamiliaCompleta456VG(familiaPadre.Nombre456VG);
                    if (familiaActualizada != null)
                    {
                        foreach (var hijo in familiaActualizada.Permisos456VG)
                        {
                            nodoPadre.Nodes.Add(CrearNodoPermisoRecursivo456VG(hijo, true));
                        }
                        nodoPadre.Expand();
                        CargarCombos456VG();
                        RefrescarPerfilSiContieneFamilia(familiaPadre.Nombre456VG);
                    }
                }
                else
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorQuitarSubfamilia"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is Permiso_456VG permiso))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarPermisoEnFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nodoSel.Parent == null || !(nodoSel.Parent.Tag is FamiliaPermiso_456VG familiaPadre))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloPermisoDirectoEnFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nodoSel.Parent.Parent != null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloPermisoDirectoEnFamiliaRaiz"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirmar = MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ConfirmarQuitarPermisoFamilia"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                if (bllp.QuitarPermisoDeFamilia456VG(familiaPadre.Nombre456VG, permiso.CodPermiso456VG))
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoQuitadoOkFamilia"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TreeNode nodoPadre = nodoSel.Parent;
                    nodoPadre.Nodes.Clear();
                    var familiaActualizada = bllp.ObtenerFamiliaCompleta456VG(familiaPadre.Nombre456VG);
                    if (familiaActualizada != null)
                    {
                        foreach (var hijo in familiaActualizada.Permisos456VG)
                        {
                            nodoPadre.Nodes.Add(CrearNodoPermisoRecursivo456VG(hijo, true));
                        }
                        nodoPadre.Expand();
                        RefrescarPerfilSiContieneFamilia(familiaPadre.Nombre456VG);
                    }
                    CargarCombos456VG();
                }
                else
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorQuitarPermisoFamilia"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void CargarCombos456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            comboBox1456VG.Items.Clear();
            cmboxflia456VG.Items.Clear();
            CBFamilias456VG.Items.Clear();
            CBPermisos456VG.Items.Clear();
            comboBox3456VG.Items.Clear();
            dictPermisosTraducidos.Clear();
            dictFamiliasTraducidas.Clear();
            dictPerfilesTraducidos.Clear();
            var permisosSimples = bllp.ListarPermisosSimples456VG();
            foreach (var permiso in permisosSimples)
            {
                string clave = permiso.Nombre456VG;
                string traducido = lng.ObtenerTexto_456VG("Perfiles_456VG.Item.comboBox1456VG." + clave);
                dictPermisosTraducidos[traducido] = clave;
                comboBox1456VG.Items.Add(traducido);
                CBPermisos456VG.Items.Add(traducido);
            }
            var familiasNombres = bllp.ObtenerTodasLasFamilias456VG();
            foreach (var nombre in familiasNombres)
            {
                string traducido = lng.ObtenerTexto_456VG("Perfiles_456VG.Item.cmboxflia456VG." + nombre);
                dictFamiliasTraducidas[traducido] = nombre;
                cmboxflia456VG.Items.Add(traducido);
                CBFamilias456VG.Items.Add(traducido);
            }
            var perfiles = bllp.CargarCBPerfil456VG();
            foreach (var perfil in perfiles)
            {
                string clave = perfil.Nombre456VG;
                string traducido = lng.ObtenerTexto_456VG("Perfiles_456VG.Item.comboBox3456VG." + clave);
                dictPerfilesTraducidos[traducido] = clave;
                comboBox3456VG.Items.Add(traducido);
            }
            comboBox3456VG.SelectedIndex = -1;
            comboBox1456VG.SelectedIndex = -1;
            cmboxflia456VG.SelectedIndex = -1;
            CBFamilias456VG.SelectedIndex = -1;
            CBPermisos456VG.SelectedIndex = -1;
        }
        private string ObtenerClaveOriginal(Dictionary<string, string> diccionario, object selectedItem)
        {
            if (selectedItem == null) return null;
            string valorTraducido = selectedItem.ToString();
            if (diccionario.TryGetValue(valorTraducido, out var claveOriginal))
            {
                return claveOriginal;
            }
            return valorTraducido;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1456VG.Clear();
            treeView1456VG.Nodes.Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            TXTFamilia456VG.Clear();
            treeView1.Nodes.Clear();
        }
        private void RefrescarPerfilSiContieneFamilia(string nombreFamilia)
        {
            if (treeView1456VG.Nodes.Count == 0) return;

            var nodoPerfil = treeView1456VG.Nodes[0];
            if (!(nodoPerfil.Tag is BEPerfil_456VG perfil)) return;

            bool contiene = perfil.Permisos456VG.Any(p => ContieneFamilia(p, nombreFamilia));

            if (contiene)
                CargarPerfilPorNombre456VG(perfil.Nombre456VG);
        }
        private bool ContieneFamilia(IPerfil_456VG permiso, string nombreBuscado)
        {
            if (permiso is FamiliaPermiso_456VG familia)
            {
                if (familia.Nombre456VG == nombreBuscado)
                    return true;

                foreach (var hijo in familia.Permisos456VG)
                {
                    if (ContieneFamilia(hijo, nombreBuscado))
                        return true;
                }
            }
            return false;
        }
        private void CargarPerfilPorNombre456VG(string nombrePerfil)
        {
            var perfil = bllp.ObtenerPerfilCompleto456VG(nombrePerfil);
            if (perfil == null) return;
            treeView1456VG.Nodes.Clear();
            TreeNode nodoPerfil = new TreeNode(perfil.Nombre456VG) { Tag = perfil };
            foreach (var permiso in perfil.Permisos456VG)
            {
                TreeNode nodoPermiso = CrearNodoPermisoRecursivo456VG(permiso, true);
                nodoPerfil.Nodes.Add(nodoPermiso);
            }
            treeView1456VG.Nodes.Add(nodoPerfil);
            treeView1456VG.ExpandAll();
        }
    }
}
