using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class Familias_456VG : Form, IObserver_456VG
    {
        private Familia_456VG nuevaFamilia456VG;
        private readonly BLLFamilia_456VG bllFamilia = new BLLFamilia_456VG();
        private readonly BLLPermisoComp_456VG bllPermiso = new BLLPermisoComp_456VG();
        private readonly BLLPerfil_456VG bllPerfil = new BLLPerfil_456VG();
        private List<BEPerfil_456VG> permisosDisponibles456VG;
        private List<BEFamilia_456VG> familiasDisponibles456VG;

        public Familias_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
            nuevaFamilia456VG = new Familia_456VG("BASE");
            CargarCombos456VG();
            CargarTreeView456VG();
        }

        public void ActualizarIdioma_456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            lng.CambiarIdiomaControles_456VG(this);
            CargarCombos456VG();
            ActualizarTextosTree456VG(treeView1456VG.Nodes);
        }

        private void Familias_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
        }

        // --------------------------- CARGA COMBOS Y NODOS ---------------------------

        private void CargarCombos456VG()
        {
            permisosDisponibles456VG = bllPerfil.CargarCBPermisos456VG()
                                           .Where(p => !p.is_perfil456VG)
                                           .ToList();
            familiasDisponibles456VG = bllFamilia.leerEntidades456VG();

            CargarCombo456VG(CBPermisos456VG, permisosDisponibles456VG, p => p.nombre456VG);
            CargarCombo456VG(CBFamilias456VG, familiasDisponibles456VG, f => f.nombre456VG);
        }

        private void CargarCombo456VG<T>(ComboBox cb, IEnumerable<T> lista, Func<T, string> getNombre)
        {
            cb.Items.Clear();
            foreach (var item in lista)
            {
                string nombre = getNombre(item);
                string textoTraducido = TraducirItem456VG(cb.Name, nombre);
                cb.Items.Add(new KeyValuePair<T, string>(item, textoTraducido));
            }
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }

        private void CargarTreeView456VG()
        {
            treeView1456VG.Nodes.Clear();
            string claveRoot = $"{Name}.Item.{CBPermisos456VG.Name}.BASE";
            string txtRoot = TraducirItem456VG(CBPermisos456VG.Name, "BASE");

            nuevaFamilia456VG = new Familia_456VG("BASE");
            var root = new TreeNode(txtRoot) { Name = claveRoot, Tag = nuevaFamilia456VG };
            treeView1456VG.Nodes.Add(root);
            AgregarHijos456VG(root);
        }

        private void AgregarHijos456VG(TreeNode padre)
        {
            var comp = padre.Tag as Componente_456VG;
            if (comp == null) return;

            foreach (var hijo in comp.ObtenerHijos456VG() ?? Enumerable.Empty<Componente_456VG>())
            {
                bool esPermiso = hijo is Permisos_456VG;
                string controlName = esPermiso ? CBPermisos456VG.Name : CBFamilias456VG.Name;
                string nombre = hijo is Permisos_456VG p ? p.Nombre456VG :
                                hijo is Familia_456VG f ? f.Nombre456VG : "Componente";

                var nodo = CrearNodoTraducido456VG(controlName, nombre, hijo);
                padre.Nodes.Add(nodo);
                AgregarHijos456VG(nodo);
            }
        }

        private void ActualizarTextosTree456VG(TreeNodeCollection nodes)
        {
            foreach (TreeNode n in nodes)
            {
                if (n.Tag is Componente_456VG comp)
                {
                    string nombre = comp is Permisos_456VG p ? p.Nombre456VG :
                                     comp is Familia_456VG f ? f.Nombre456VG : "Componente";
                    string controlName = comp is Permisos_456VG ? CBPermisos456VG.Name : CBFamilias456VG.Name;
                    string clave = $"{Name}.Item.{controlName}.{nombre}";
                    string traduccion = Lenguaje_456VG.ObtenerInstancia_456VG().ObtenerTexto_456VG(clave);
                    n.Text = traduccion == clave ? nombre : traduccion;
                }
                if (n.Nodes.Count > 0)
                    ActualizarTextosTree456VG(n.Nodes);
            }
        }

        private void ActualizarTree456VG()
        {
            treeView1456VG.Nodes.Clear();
            string claveRoot = $"{Name}.Item.{CBPermisos456VG.Name}.BASE";
            string txtRoot = TraducirItem456VG(CBPermisos456VG.Name, "BASE");
            var root = new TreeNode(txtRoot) { Name = claveRoot, Tag = nuevaFamilia456VG };
            treeView1456VG.Nodes.Add(root);
            PintarNodosRecursivo456VG(nuevaFamilia456VG, root);
            treeView1456VG.ExpandAll();
        }

        private void PintarNodosRecursivo456VG(Componente_456VG comp, TreeNode nodo)
        {
            foreach (var hijo in comp.ObtenerHijos456VG())
            {
                bool esPermiso = hijo is Permisos_456VG;
                string controlName = esPermiso ? CBPermisos456VG.Name : CBFamilias456VG.Name;
                string nombre = hijo is Permisos_456VG p ? p.Nombre456VG :
                                hijo is Familia_456VG f ? f.Nombre456VG : "Componente";

                var sub = CrearNodoTraducido456VG(controlName, nombre, hijo);
                nodo.Nodes.Add(sub);
                PintarNodosRecursivo456VG(hijo, sub);
            }
        }


        // --------------------------- ACCIONES ---------------------------

        private void BTNAgregarPermiso456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var sel = CBPermisos456VG.SelectedItem as KeyValuePair<BEPerfil_456VG, string>?;
            if (!sel.HasValue) return;

            var permisoBE = sel.Value.Key;
            var permisoObj = new Permisos_456VG(permisoBE.nombre456VG);

            TreeNode nodoSel = treeView1456VG.SelectedNode;

            if (nodoSel == null || !(nodoSel.Tag is Familia_456VG familiaSel))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Familias_456VG.Msg.SeleccioneFamiliaNodo"),
                    lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                familiaSel.AgregarHijo456VG(permisoObj);
                ActualizarTree456VG();

                // Guardar en BD si familia seleccionada ya está creada
                var familiaExistente = familiasDisponibles456VG.FirstOrDefault(f =>
                    f.nombre456VG.Equals(familiaSel.Nombre456VG, StringComparison.OrdinalIgnoreCase));

                if (familiaExistente != null)
                {
                    int idPadre = familiaExistente.id_permiso456VG;
                    int idHijo = permisoBE.id_permiso456VG;

                    var res = bllFamilia.AgregarHijo456VG(idPadre, idHijo);
                    if (!res.resultado)
                    {
                        MessageBox.Show(
                            $"Error al guardar la relación en base: {res.mensaje}",
                            lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                else
                {
                    Console.WriteLine("Permiso agregado a una familia no persistida. Se guardará al crear la familia.");
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BTNAplicar456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var sel = CBFamilias456VG.SelectedItem as KeyValuePair<BEFamilia_456VG, string>?;
            if (!sel.HasValue) return;

            var famBE = sel.Value.Key;
            var subFamilia = new Familia_456VG(famBE.nombre456VG);
            var hijos = bllFamilia.ObtenerRelacionesDeFamilia456VG(famBE.id_permiso456VG);

            foreach (var rel in hijos)
            {
                var permisoBE = permisosDisponibles456VG.FirstOrDefault(p => p.id_permiso456VG == rel.id_permisohijo456VG);
                if (permisoBE != null)
                    subFamilia.AgregarHijo456VG(new Permisos_456VG(permisoBE.nombre456VG));
            }

            try
            {
                nuevaFamilia456VG.AgregarHijo456VG(subFamilia);
                ActualizarTree456VG();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message,
                                lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void BTNCrearFamilia456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            if (!ValidarNombreFamilia456VG(out string nombre)) return;

            var beNuevaFamilia = new BEFamilia_456VG() { nombre456VG = nombre };
            var res = bllFamilia.crearEntidad456VG(beNuevaFamilia);
            if (!res.resultado)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("Familias_456VG.Msg.ErrorCrear").Replace("{0}", res.mensaje),
                                lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idNueva = res.entidad.id_permiso456VG;
            foreach (var hijo in nuevaFamilia456VG.ObtenerHijos456VG())
            {
                int idHijo = BuscarID(hijo);
                if (idHijo > 0)
                    bllFamilia.AgregarHijo456VG(idNueva, idHijo);
            }

            MessageBox.Show(lng.ObtenerTexto_456VG("Familias_456VG.Msg.FamiliaCreadaOK"),
                            lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarCombos456VG();
            CargarTreeView456VG();
            TXTFamilia456VG.Clear();
        }

        private void BTNEliminarFamilia456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1456VG.SelectedNode;

            if (nodoSel == null || !(nodoSel.Tag is Familia_456VG familiaSel))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Familias_456VG.Msg.SeleccioneFamiliaNodo"),
                    lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (familiaSel.Nombre456VG.Equals("BASE", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Familias_456VG.Msg.NoEliminarBase"),
                    lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show(
                lng.ObtenerTexto_456VG("Familias_456VG.Msg.ConfirmarElim")
                   .Replace("{0}", familiaSel.Nombre456VG),
                lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            // Buscar ID de la familia a eliminar
            var familiaBE = familiasDisponibles456VG
                .FirstOrDefault(f => f.nombre456VG.Equals(familiaSel.Nombre456VG, StringComparison.OrdinalIgnoreCase));

            if (familiaBE == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Familias_456VG.Msg.NoEncontrada"),
                    lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var resultado = bllFamilia.eliminarEntidad456VG(familiaBE);
            if (!resultado.resultado)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Familias_456VG.Msg.ErrorEliminar")
                       .Replace("{0}", resultado.mensaje),
                    lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Quitar nodo del TreeView
            nodoSel.Remove();

            // Refrescar combos
            CargarCombos456VG();

            MessageBox.Show(
                lng.ObtenerTexto_456VG("Familias_456VG.Msg.EliminadaOK"),
                lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        private void button8456VG_Click(object sender, EventArgs e)
        {
            Close();
        }

        // --------------------------- HELPERS ---------------------------

        private int BuscarID(Componente_456VG comp)
        {
            if (comp is Permisos_456VG p)
                return permisosDisponibles456VG.FirstOrDefault(x => x.nombre456VG == p.Nombre456VG)?.id_permiso456VG ?? -1;
            if (comp is Familia_456VG f)
                return familiasDisponibles456VG.FirstOrDefault(x => x.nombre456VG == f.Nombre456VG)?.id_permiso456VG ?? -1;
            return -1;
        }

        private bool ValidarNombreFamilia456VG(out string nombre)
        {
            nombre = TXTFamilia456VG.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
                MessageBox.Show(lng.ObtenerTexto_456VG("Familias_456VG.Msg.CompletarNombre"),
                                lng.ObtenerTexto_456VG("Familias_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private string TraducirItem456VG(string controlName, string nombre)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string clave = $"{Name}.Item.{controlName}.{nombre}";
            string texto = lng.ObtenerTexto_456VG(clave);
            return texto == clave ? nombre : texto;
        }

        private TreeNode CrearNodoTraducido456VG(string controlName, string nombre, object tag)
        {
            string clave = $"{Name}.Item.{controlName}.{nombre}";
            string texto = TraducirItem456VG(controlName, nombre);
            return new TreeNode(texto) { Name = clave, Tag = tag };
        }
    }
}
