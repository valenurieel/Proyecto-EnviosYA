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
        private Perfil_456VG INICIO;
        private readonly BLLPerfil_456VG bllp = new BLLPerfil_456VG();
        private readonly BLLPermisoComp_456VG bllper = new BLLPermisoComp_456VG();
        private readonly BLLFamilia_456VG bllf = new BLLFamilia_456VG();
        public Perfiles_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
            cargarcb();
            INICIO = new Perfil_456VG("BASE");
            CargarTreeView();
        }
        public void ActualizarIdioma_456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            lng.CambiarIdiomaControles_456VG(this);
            cargarcb();
            ActualizarTextosTree(treeView1456VG.Nodes);
        }
        private void cargarcb()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            textBox1456VG.Clear();
            comboBox3456VG.Items.Clear();
            comboBox1456VG.Items.Clear();
            cmboxflia456VG.Items.Clear();
            var perfiles = bllp.CargarCBPerfil456VG()
                               .Where(p => p.is_perfil456VG)
                               .ToList();
            foreach (var be in perfiles)
            {
                string clave = $"{Name}.Item.{comboBox3456VG.Name}.{be.nombre456VG}";
                string texto = lng.ObtenerTexto_456VG(clave);
                if (texto == clave)
                    texto = be.nombre456VG;
                comboBox3456VG.Items.Add(
                    new KeyValuePair<BEPerfil_456VG, string>(be, texto)
                );
            }
            comboBox3456VG.DisplayMember = "Value";
            comboBox3456VG.ValueMember = "Key";
            var permisos = bllp.CargarCBPermisos456VG()
                              .Where(p => !p.is_perfil456VG)
                              .ToList();
            foreach (var be in permisos)
            {
                string clave = $"{Name}.Item.{comboBox1456VG.Name}.{be.nombre456VG}";
                string texto = lng.ObtenerTexto_456VG(clave);
                if (texto == clave)
                    texto = be.nombre456VG;
                comboBox1456VG.Items.Add(
                    new KeyValuePair<BEPerfil_456VG, string>(be, texto)
                );
            }
            comboBox1456VG.DisplayMember = "Value";
            comboBox1456VG.ValueMember = "Key";
            var familias = bllf.leerEntidades456VG();
            foreach (var be in familias)
            {
                string clave = $"{Name}.Item.{cmboxflia456VG.Name}.{be.nombre456VG}";
                string texto = lng.ObtenerTexto_456VG(clave);
                if (texto == clave) texto = be.nombre456VG;
                cmboxflia456VG.Items.Add(new KeyValuePair<BEFamilia_456VG, string>(be, texto));
            }
            cmboxflia456VG.DisplayMember = "Value";
            cmboxflia456VG.ValueMember = "Key";
        }
        private void CargarTreeView()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            treeView1456VG.Nodes.Clear();
            string claveRoot = $"{Name}.Item.{comboBox3456VG.Name}.BASE";
            string txtRoot = lng.ObtenerTexto_456VG(claveRoot);
            INICIO = new Perfil_456VG("BASE");
            var root = new TreeNode(txtRoot) { Name = claveRoot, Tag = INICIO };
            treeView1456VG.Nodes.Add(root);
            AgregarHijos(root);
        }
        private void AgregarHijos(TreeNode padre)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var comp = padre.Tag as Componente_456VG;
            if (comp == null) return;
            foreach (var hijo in comp.ObtenerHijos456VG() ?? Enumerable.Empty<Componente_456VG>())
            {
                bool esPerfil = hijo is Perfil_456VG;
                string comboName = esPerfil ? comboBox3456VG.Name : comboBox1456VG.Name;
                string nombre = esPerfil
                                   ? ((Perfil_456VG)hijo).Nombre456VG
                                   : hijo.ToString();
                string clave = $"{Name}.Item.{comboName}.{nombre}";
                string texto = lng.ObtenerTexto_456VG(clave);
                var nodo = new TreeNode(texto) { Name = clave, Tag = hijo };
                padre.Nodes.Add(nodo);
                AgregarHijos(nodo);
            }
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
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.CompleteNombre"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var nuevo = new BEPerfil_456VG(nombre, "usuarioToolStripMenuItem456VG", true);
            var res = bllp.aggPerfil456VG(nuevo);
            if (res.resultado)
            {
                cargarcb();
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PerfilAgregadoOK"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorAgregarPerfil").Replace("{0}", res.mensaje),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void button4456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var sel = comboBox3456VG.SelectedItem as KeyValuePair<BEPerfil_456VG, string>?;
            if (!sel.HasValue)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaPerfilCombo"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return;
            }
            var bePerfil = sel.Value.Key;
            var perfilComp = new Perfil_456VG(bePerfil.nombre456VG);
            var todasRel = bllper.ListaPermisos456VG()
                                 .Where(r => r.id_permisopadre456VG == bePerfil.id_permiso456VG)
                                 .ToList();
            var todasFamiliasBE = bllf.leerEntidades456VG();
            var todosPermisosBE = bllp.CargarCBPermisos456VG();
            foreach (var rel in todasRel)
            {
                var famBE = todasFamiliasBE
                    .FirstOrDefault(f => f.id_permiso456VG == rel.id_permisohijo456VG);
                if (famBE != null)
                {
                    var famObj = new Familia_456VG(famBE.nombre456VG);
                    var hijosRel = bllf.ObtenerRelacionesDeFamilia456VG(famBE.id_permiso456VG);
                    foreach (var r2 in hijosRel)
                    {
                        var permBE = todosPermisosBE
                            .First(p => p.id_permiso456VG == r2.id_permisohijo456VG);
                        famObj.AgregarHijo456VG(new Permisos_456VG(permBE.nombre456VG));
                    }
                    perfilComp.AgregarHijo456VG(famObj);
                }
                else
                {
                    var permBE = todosPermisosBE
                        .First(p => p.id_permiso456VG == rel.id_permisohijo456VG);
                    perfilComp.AgregarHijo456VG(new Permisos_456VG(permBE.nombre456VG));
                }
            }
            var root = treeView1456VG.Nodes[0];
            root.Nodes.Clear();
            string clave = $"{Name}.Item.{comboBox3456VG.Name}.{bePerfil.nombre456VG}";
            string textoTraducido = lng.ObtenerTexto_456VG(clave);
            if (textoTraducido == clave)
                textoTraducido = bePerfil.nombre456VG;
            var nodoPerfil = new TreeNode(textoTraducido)
            {
                Name = clave,
                Tag = perfilComp
            };
            root.Nodes.Add(nodoPerfil);
            PintaChildren(nodoPerfil, perfilComp);
            root.Expand();
        }
        private void PintaChildren(TreeNode padre, Componente_456VG comp)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();

            foreach (var hijo in comp.ObtenerHijos456VG() ?? Enumerable.Empty<Componente_456VG>())
            {
                string texto;
                string clave;
                if (hijo is Perfil_456VG p)
                {
                    clave = $"{Name}.Item.{comboBox3456VG.Name}.{p.Nombre456VG}";
                    texto = lng.ObtenerTexto_456VG(clave);
                    if (texto == clave) texto = p.Nombre456VG;
                }
                else if (hijo is Familia_456VG f)
                {
                    clave = $"{Name}.Item.{cmboxflia456VG.Name}.{f.Nombre456VG}";
                    texto = lng.ObtenerTexto_456VG(clave);
                    if (texto == clave) texto = f.Nombre456VG;
                }
                else if (hijo is Permisos_456VG perm)
                {
                    clave = $"{Name}.Item.{comboBox1456VG.Name}.{perm.Nombre456VG}";
                    texto = lng.ObtenerTexto_456VG(clave);
                    if (texto == clave) texto = perm.Nombre456VG;
                }
                else
                {
                    texto = "???";
                    clave = "???";
                }
                var nodo = new TreeNode(texto) { Name = clave, Tag = hijo };
                padre.Nodes.Add(nodo);
                PintaChildren(nodo, hijo);
            }
        }
        private void button3456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1456VG.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is Perfil_456VG padre))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaPerfilArbol"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (padre.Nombre456VG.Equals("BASE", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorAgregarPermisoBase"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            var sel = comboBox1456VG.SelectedItem as KeyValuePair<BEPerfil_456VG, string>?;
            if (!sel.HasValue)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaPermisoCombo"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var permisoBE = sel.Value.Key;
            if (padre.IncluyePermiso(permisoBE.nombre456VG))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoYaExiste"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var permisoObj = new Permisos_456VG(permisoBE.nombre456VG);
            try
            {
                padre.AgregarHijo456VG(permisoObj);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            string clave = $"{Name}.Item.{comboBox1456VG.Name}.{permisoBE.nombre456VG}";
            string texto = lng.ObtenerTexto_456VG(clave);
            if (texto == clave) texto = permisoBE.nombre456VG;
            var nodoPerm = new TreeNode(texto) { Name = clave, Tag = permisoObj };
            nodoSel.Nodes.Add(nodoPerm);
            nodoSel.Expand();
            int idPadre = ObtenerIdPorNombre456VG(padre.Nombre456VG);
            var resBD = bllper.aggPermisos456VG(
                new BEPermisoComp_456VG(idPadre, permisoBE.id_permiso456VG)
            );
            if (!resBD.resultado)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorAgregarPermiso")
                       .Replace("{0}", resBD.mensaje),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoAgregadoOK"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
        private void button2456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodo = treeView1456VG.SelectedNode;
            if (nodo == null || !(nodo.Tag is Perfil_456VG perfilComp))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaNodoEliminar"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (perfilComp.Nombre456VG.Equals("BASE", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NoEliminarBase"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var confirm = MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ConfirmarElim"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            int idPerfil = ObtenerIdPorNombre456VG(perfilComp.Nombre456VG);
            if (idPerfil < 0)
            {
                MessageBox.Show(
                    "No pude encontrar el ID del perfil en la base.",
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var res = bllp.EliminarPerfil456VG(idPerfil);
            if (!res.resultado)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorEliminarPerfil")
                      .Replace("{0}", res.mensaje),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            nodo.Remove();
            cargarcb();
            MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PerfilEliminadoOK"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button8456VG_Click(object sender, EventArgs e)
        {
            Close();
        }
        private int ObtenerIdPorNombre456VG(string nombrePerfil)
        {
            var lista = bllp.CargarCBPerfil456VG();
            var p = lista.FirstOrDefault(x =>
                x.nombre456VG?.Trim().Equals(nombrePerfil.Trim(), StringComparison.OrdinalIgnoreCase) ?? false);
            return p?.id_permiso456VG ?? -1;
        }
        private void Perfiles_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
        }
        private void comboBox3456VG_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void button6456VG_Click(object sender, EventArgs e)
        {
            using (var fr = new Familias_456VG())
            {
                fr.ShowDialog();
                cargarcb();
            }
        }
        private void cmboxflia456VG_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void aggflia456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1456VG.SelectedNode;
            if (nodoSel == null)
            {
                MessageBox.Show(
                    "Please select a node from the tree to add the family.",
                    "Profiles",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (nodoSel.Tag is Permisos_456VG)
            {
                MessageBox.Show(
                    "You cannot assign a family to a permission. Please select a profile.",
                    "Profiles",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var padrePerfil = nodoSel.Tag as Perfil_456VG;
            if (padrePerfil == null)
            {
                MessageBox.Show(
                    "You can only assign families to profiles. Please select a profile node.",
                    "Profiles",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (padrePerfil.Nombre456VG.Equals("BASE", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "You cannot assign a family to the BASE node. Please create or select another profile.",
                    "Profiles",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var sel = cmboxflia456VG.SelectedItem as KeyValuePair<BEFamilia_456VG, string>?;
            if (!sel.HasValue)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaFamiliaCombo"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var beFamilia = sel.Value.Key;
            var familiaObj = new Familia_456VG(beFamilia.nombre456VG);
            var hijosRel = bllf.ObtenerRelacionesDeFamilia456VG(beFamilia.id_permiso456VG);
            var todosPermisosBE = bllp.CargarCBPermisos456VG();
            foreach (var rel in hijosRel)
            {
                var permisoBE = todosPermisosBE
                    .FirstOrDefault(p => p.id_permiso456VG == rel.id_permisohijo456VG);
                if (permisoBE == null) continue;
                familiaObj.AgregarHijo456VG(new Permisos_456VG(permisoBE.nombre456VG));
            }
            try
            {
                padrePerfil.AgregarHijo456VG(familiaObj);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            string claveFam = $"{Name}.Item.{cmboxflia456VG.Name}.{beFamilia.nombre456VG}";
            string textoFam = lng.ObtenerTexto_456VG(claveFam);
            if (textoFam == claveFam) textoFam = beFamilia.nombre456VG;
            var nodoFam = new TreeNode(textoFam)
            {
                Name = claveFam,
                Tag = familiaObj
            };
            nodoSel.Nodes.Add(nodoFam);
            foreach (var rel in hijosRel)
            {
                var permisoBE = todosPermisosBE
                    .FirstOrDefault(p => p.id_permiso456VG == rel.id_permisohijo456VG);
                if (permisoBE == null) continue;
                string clavePerm = $"{Name}.Item.{comboBox1456VG.Name}.{permisoBE.nombre456VG}";
                string textoPerm = lng.ObtenerTexto_456VG(clavePerm);
                if (textoPerm == clavePerm) textoPerm = permisoBE.nombre456VG;
                var nodoPerm = new TreeNode(textoPerm)
                {
                    Name = clavePerm,
                    Tag = new Permisos_456VG(permisoBE.nombre456VG)
                };
                nodoFam.Nodes.Add(nodoPerm);
            }
            nodoSel.Expand();
            int idPerfil = ObtenerIdPorNombre456VG(padrePerfil.Nombre456VG);
            int idFamilia = beFamilia.id_permiso456VG;
            var resBD = bllper.aggPermisos456VG(
                new BEPermisoComp_456VG(idPerfil, idFamilia)
            );
            if (!resBD.resultado)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorAgregarFamilia")
                       .Replace("{0}", resBD.mensaje),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.FamiliaAgregadaOK"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}
