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
            // 1) etiquetas estáticas
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

            // --- Perfiles ---
            var perfiles = bllp.CargarCBPerfil456VG()
                               .Where(p => p.is_perfil456VG)
                               .ToList();
            foreach (var be in perfiles)
            {
                string clave = $"{Name}.Item.{comboBox3456VG.Name}.{be.nombre456VG}";
                string texto = lng.ObtenerTexto_456VG(clave);
                comboBox3456VG.Items.Add(new KeyValuePair<BEPerfil_456VG, string>(be, texto));
            }
            comboBox3456VG.DisplayMember = "Value";
            comboBox3456VG.ValueMember = "Key";

            // --- Permisos ---
            var permisos = bllp.CargarCBPermisos456VG()
                              .Where(p => !p.is_perfil456VG)
                              .ToList();
            foreach (var be in permisos)
            {
                string clave = $"{Name}.Item.{comboBox1456VG.Name}.{be.nombre456VG}";
                string texto = lng.ObtenerTexto_456VG(clave);
                comboBox1456VG.Items.Add(new KeyValuePair<BEPerfil_456VG, string>(be, texto));
            }
            comboBox1456VG.DisplayMember = "Value";
            comboBox1456VG.ValueMember = "Key";
        }

        private void CargarTreeView()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            treeView1456VG.Nodes.Clear();

            // nodo raíz
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

        // Agregar nuevo perfil
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

        // Agregar perfil al árbol
        private void button4456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var sel = comboBox3456VG.SelectedItem as KeyValuePair<BEPerfil_456VG, string>?;
            if (!sel.HasValue)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaPerfilCombo"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var perfilBE = sel.Value.Key;
            var perfilObj = new Perfil_456VG(perfilBE.nombre456VG);
            var root = treeView1456VG.Nodes[0];
            if (root.Nodes.Cast<TreeNode>().Any(n =>
                    (n.Tag is Perfil_456VG p) && p.Nombre456VG == perfilObj.Nombre456VG))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PerfilYaExisteTree"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }
            string clavePerfil = $"{nameof(Perfiles_456VG)}.Item.{comboBox3456VG.Name}.{perfilBE.nombre456VG}";
            var nodoPerfil = new TreeNode(lng.ObtenerTexto_456VG(clavePerfil))
            {
                Name = clavePerfil,
                Tag = perfilObj
            };
            root.Nodes.Add(nodoPerfil);
            root.Expand();
            var listaRel = bllper.ListaPermisos456VG()
                                 .Where(r => r.id_permisopadre456VG == perfilBE.id_permiso456VG)
                                 .ToList();
            var todosPermisosBE = bllp.CargarCBPermisos456VG();
            foreach (var rel in listaRel)
            {
                var permisoBE = todosPermisosBE
                    .FirstOrDefault(p => p.id_permiso456VG == rel.id_permisohijo456VG);
                if (permisoBE == null) continue;
                if (nodoPerfil.Nodes.Cast<TreeNode>().Any(n => n.Text == permisoBE.nombre456VG))
                    continue;
                string clavePerm = $"{nameof(Perfiles_456VG)}.Item.{comboBox1456VG.Name}.{permisoBE.nombre456VG}";
                var nodoPermiso = new TreeNode(lng.ObtenerTexto_456VG(clavePerm))
                {
                    Name = clavePerm,
                    Tag = new Permisos_456VG(permisoBE.nombre456VG)
                };
                nodoPerfil.Nodes.Add(nodoPermiso);
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
            var permisoObj = new Permisos_456VG(permisoBE.nombre456VG);

            padre.AgregarHijo456VG(permisoObj);
            string clave = $"{Name}.Item.{comboBox1456VG.Name}.{permisoBE.nombre456VG}";
            string texto = lng.ObtenerTexto_456VG(clave);
            var nodo = new TreeNode(texto) { Name = clave, Tag = permisoObj };
            nodoSel.Nodes.Add(nodo);
            nodoSel.Expand();

            // grabo relación en BD
            int idPadre = ObtenerIdPorNombre456VG(padre.Nombre456VG);
            int idHijo = permisoBE.id_permiso456VG;
            bllper.aggPermisos456VG(new BEPermisoComp_456VG(idPadre, idHijo));

            MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoAgregadoOK"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // Eliminar nodo
        private void button2456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodo = treeView1456VG.SelectedNode;
            if (nodo == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaNodoEliminar"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var confirm = MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ConfirmarElim"),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirm == DialogResult.Yes)
                nodo.Remove();
        }

        // Cerrar form
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
    }
}
