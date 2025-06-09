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
            // Nos suscribimos al cambio de idioma
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);

            // Cargamos combobox y árbol
            cargarcb();
            INICIO = new Perfil_456VG("BASE");
            CargarTreeView();

            // Asegúrate de que este evento esté enlazado en el diseñador
            comboBox3456VG.SelectedIndexChanged += comboBox3456VG_SelectedIndexChanged;
        }

        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
            cargarcb();
        }

        private void CargarTreeView()
        {
            treeView1456VG.Nodes.Clear();
            var root = CrearNodo(INICIO, "BASE");
            treeView1456VG.Nodes.Add(root);
        }

        private TreeNode CrearNodo(Componente_456VG componente, string texto)
        {
            var node = new TreeNode(texto) { Tag = componente };
            foreach (var hijo in componente.ObtenerHijos456VG() ?? Enumerable.Empty<Componente_456VG>())
            {
                string label = hijo is Perfil_456VG p
                    ? p.Nombre456VG
                    : hijo.ToString();
                node.Nodes.Add(CrearNodo(hijo, label));
            }
            return node;
        }

        private void cargarcb()
        {
            // PERFIL
            comboBox3456VG.DataSource = null;
            comboBox3456VG.DataSource = bllp.CargarCBPerfil456VG();
            comboBox3456VG.DisplayMember = nameof(BEPerfil_456VG.nombre456VG);
            comboBox3456VG.ValueMember = nameof(BEPerfil_456VG.id_permiso456VG);
            comboBox3456VG.DropDownStyle = ComboBoxStyle.DropDownList;

            // PERMISOS — siempre TODOS los permisos del sistema
            comboBox1456VG.DataSource = null;
            comboBox1456VG.DataSource = bllper.CargarCBPermisos456VG();
            comboBox1456VG.DisplayMember = nameof(BEPerfil_456VG.nombre456VG);
            comboBox1456VG.ValueMember = nameof(BEPerfil_456VG.id_permiso456VG);
            comboBox1456VG.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void comboBox3456VG_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Intencionalmente vacío:
            // No filtramos permisos según el perfil seleccionado,
            // porque siempre queremos mostrar TODOS.
        }

        private void button1456VG_Click(object sender, EventArgs e)
        {
            // Agregar un nuevo perfil
            var nombre = textBox1456VG.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Debe ingresar un nombre de perfil.");
                return;
            }

            var nuevo = new BEPerfil_456VG(nombre, "usuarioToolStripMenuItem456VG", true);
            var res = bllp.aggPerfil456VG(nuevo);
            if (res.resultado)
            {
                cargarcb();
                MessageBox.Show("Se ha agregado el perfil correctamente.");
            }
            else
            {
                MessageBox.Show($"Error al agregar perfil: {res.mensaje}");
            }
        }

        private void button4456VG_Click(object sender, EventArgs e)
        {
            // Agregar sub-perfil al nodo seleccionado
            if (!(comboBox3456VG.SelectedItem is BEPerfil_456VG selPerfil))
            {
                MessageBox.Show("Seleccione un perfil válido.");
                return;
            }
            if (!(treeView1456VG.SelectedNode?.Tag is Perfil_456VG nodoPerfil))
            {
                MessageBox.Show("Seleccione un nodo de perfil en el árbol.");
                return;
            }

            var hijo = new Perfil_456VG(selPerfil.nombre456VG);
            nodoPerfil.AgregarHijo456VG(hijo);
            treeView1456VG.SelectedNode.Nodes.Add(CrearNodo(hijo, hijo.Nombre456VG));
            treeView1456VG.SelectedNode.Expand();

            // También añado al árbol los permisos que ya tuviera ese perfil en BBDD
            var rels = ObtenerPermisosHijosPorNombrePadre456VG(selPerfil.nombre456VG);
            var todosPermisos = bllper.CargarCBPermisos456VG();
            foreach (var r in rels)
            {
                var texto = todosPermisos
                              .FirstOrDefault(x => x.id_permiso456VG == r.id_permisohijo456VG)
                              ?.nombre456VG;
                if (!string.IsNullOrEmpty(texto))
                    treeView1456VG.SelectedNode.Nodes.Add(new TreeNode(texto));
            }

            MessageBox.Show($"Perfil '{selPerfil.nombre456VG}' agregado bajo '{nodoPerfil.Nombre456VG}'.");
        }

        private List<BEPermisoComp_456VG> ObtenerPermisosHijosPorNombrePadre456VG(string nombre)
        {
            var padre = bllp.CargarCBPerfil456VG()
                           .FirstOrDefault(p => p.nombre456VG
                               .Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (padre == null) return new List<BEPermisoComp_456VG>();

            return bllper.ListaPermisos456VG()
                         .Where(x => x.id_permisopadre456VG == padre.id_permiso456VG)
                         .ToList();
        }

        private void button3456VG_Click(object sender, EventArgs e)
        {
            // Agregar permiso al perfil seleccionado en el árbol
            if (!(treeView1456VG.SelectedNode?.Tag is Perfil_456VG perfilNodo))
            {
                MessageBox.Show("Seleccione un perfil en el árbol.");
                return;
            }
            if (!(comboBox1456VG.SelectedItem is BEPerfil_456VG selPermiso))
            {
                MessageBox.Show("Seleccione un permiso válido.");
                return;
            }

            perfilNodo.AgregarHijo456VG(new Permisos_456VG(selPermiso.nombre456VG));
            treeView1456VG.SelectedNode.Nodes.Add(new TreeNode(selPermiso.nombre456VG));
            treeView1456VG.SelectedNode.Expand();

            var link = new BEPermisoComp_456VG(
                ((BEPerfil_456VG)comboBox3456VG.SelectedItem).id_permiso456VG,
                selPermiso.id_permiso456VG
            );
            var res = bllper.aggPermisos456VG(link);
            MessageBox.Show(res.resultado
                ? $"Permiso '{selPermiso.nombre456VG}' agregado."
                : $"Error al agregar permiso: {res.mensaje}"
            );
        }

        private void button2456VG_Click(object sender, EventArgs e)
        {
            // Eliminar nodo del árbol
            var node = treeView1456VG.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("Seleccione un nodo para eliminar.");
                return;
            }
            if (MessageBox.Show("¿Eliminar este nodo?", "Confirmar", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                node.Remove();
            }
        }

        private void button8456VG_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario
            Close();
        }

        private void Perfiles_456VG_Load(object sender, EventArgs e)
        {
            // No hace nada
        }
    }
}
