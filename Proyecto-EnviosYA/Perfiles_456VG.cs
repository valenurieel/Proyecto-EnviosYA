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
        private Familia_456VG familiaRaiz456VG;
        public Perfiles_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
            CargarTreeViewFamilias456VG();
            cargarcb();
            CargarTreeView();
        }
        public void ActualizarIdioma_456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            lng.CambiarIdiomaControles_456VG(this);
            cargarcb();
            ActualizarTextosTree(treeView1456VG.Nodes);
        }
        private void CargarTreeViewFamilias456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            treeView1.Nodes.Clear();
            familiaRaiz456VG = new Familia_456VG("BASE");
            string claveRoot = $"{Name}.Item.{CBFamilias456VG.Name}.BASE";
            string textoTraducido = lng.ObtenerTexto_456VG(claveRoot);
            if (textoTraducido == claveRoot) textoTraducido = "Base";
            var root = new TreeNode(textoTraducido)
            {
                Name = claveRoot,
                Tag = familiaRaiz456VG
            };
            treeView1.Nodes.Add(root);
            PintarNodosRecursivo456VG(familiaRaiz456VG, root);
            treeView1.ExpandAll();
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
        private void cargarcb()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            textBox1456VG.Clear();
            TXTFamilia456VG.Clear();
            comboBox3456VG.Items.Clear();
            comboBox1456VG.Items.Clear();
            cmboxflia456VG.Items.Clear();
            CBFamilias456VG.Items.Clear();
            CBPermisos456VG.Items.Clear();
            var perfiles = bllp.CargarCBPerfil456VG()
                               .Where(p => p.IsPerfil456VG)
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
            CBPermisos456VG.DisplayMember = "Value";
            CBPermisos456VG.ValueMember = "Key";
            var permisos = bllp.CargarCBPermisos456VG()
                              .Where(p => !p.IsPerfil456VG)
                              .ToList();
            foreach (var be in permisos)
            {
                string clave1456 = $"{Name}.Item.{comboBox1456VG.Name}.{be.nombre456VG}";
                string claveFamilia = $"{Name}.Item.{CBPermisos456VG.Name}.{be.nombre456VG}";

                string texto1456 = lng.ObtenerTexto_456VG(clave1456);
                if (texto1456 == clave1456)
                    texto1456 = be.nombre456VG;
                string textoFamilia = lng.ObtenerTexto_456VG(claveFamilia);
                if (textoFamilia == claveFamilia)
                    textoFamilia = be.nombre456VG;
                comboBox1456VG.Items.Add(new KeyValuePair<BEPerfil_456VG, string>(be, texto1456));
                CBPermisos456VG.Items.Add(new KeyValuePair<BEPerfil_456VG, string>(be, textoFamilia));
            }
            comboBox1456VG.DisplayMember = "Value";
            comboBox1456VG.ValueMember = "Key";
            CBPermisos456VG.DisplayMember = "Value";
            CBPermisos456VG.ValueMember = "Key";
            var familias = bllf.leerEntidades456VG();
            foreach (var be in familias)
            {
                string clave1 = $"{Name}.Item.{cmboxflia456VG.Name}.{be.nombre456VG}";
                string clave2 = $"{Name}.Item.{CBFamilias456VG.Name}.{be.nombre456VG}";
                string texto1 = lng.ObtenerTexto_456VG(clave1);
                if (texto1 == clave1) texto1 = be.nombre456VG;
                string texto2 = lng.ObtenerTexto_456VG(clave2);
                if (texto2 == clave2) texto2 = be.nombre456VG;
                cmboxflia456VG.Items.Add(new KeyValuePair<BEFamilia_456VG, string>(be, texto1));
                CBFamilias456VG.Items.Add(new KeyValuePair<BEFamilia_456VG, string>(be, texto2));
            }
            cmboxflia456VG.DisplayMember = "Value";
            cmboxflia456VG.ValueMember = "Key";
            CBFamilias456VG.DisplayMember = "Value";
            CBFamilias456VG.ValueMember = "Key";
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
                                 .Where(r => r.CodPermisoPadre456VG == bePerfil.CodPermiso456VG)
                                 .ToList();
            var todasFamiliasBE = bllf.leerEntidades456VG();
            var todosPermisosBE = bllp.CargarCBPermisos456VG();
            foreach (var rel in todasRel)
            {
                var famBE = todasFamiliasBE
                    .FirstOrDefault(f => f.CodPermiso456VG == rel.CodPermisoHijo456VG);

                if (famBE != null)
                {
                    var famObj = ReconstruirFamiliaRecursiva456VG(famBE.CodPermiso456VG);
                    if (famObj != null)
                        perfilComp.AgregarHijo456VG(famObj);
                }
                else
                {
                    var permBE = todosPermisosBE
                        .FirstOrDefault(p => p.CodPermiso456VG == rel.CodPermisoHijo456VG);

                    if (permBE != null)
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
            if (nodoSel.Parent != treeView1456VG.Nodes[0])
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloPermisoEnPerfilDirecto"),
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
                new BEPermisoComp_456VG(idPadre, permisoBE.CodPermiso456VG)
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
            return p?.CodPermiso456VG ?? -1;
        }
        private void Perfiles_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
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
            if (nodoSel == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionarNodoAgregarFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (nodoSel.Tag is Permisos_456VG)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NoAsignarFamiliaPermiso"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (nodoSel.Tag is Familia_456VG)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NoAsignarFamiliaAFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (nodoSel.Parent != treeView1456VG.Nodes[0])
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloFamiliaEnPerfilDirecto"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var padrePerfil = nodoSel.Tag as Perfil_456VG;
            if (padrePerfil == null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloPerfilParaFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (padrePerfil.Nombre456VG.Equals("BASE", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NoAsignarBase"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
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
            var familiaObj = ReconstruirFamiliaRecursiva456VG(beFamilia.CodPermiso456VG);
            if (familiaObj == null)
            {
                MessageBox.Show(
                    "No se pudo reconstruir la familia seleccionada.",
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return;
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
            string claveFam = $"{Name}.Item.{cmboxflia456VG.Name}.{familiaObj.Nombre456VG}";
            string textoFam = lng.ObtenerTexto_456VG(claveFam);
            if (textoFam == claveFam) textoFam = familiaObj.Nombre456VG;
            var nodoFam = new TreeNode(textoFam)
            {
                Name = claveFam,
                Tag = familiaObj
            };
            nodoSel.Nodes.Add(nodoFam);
            PintaChildren(nodoFam, familiaObj);
            nodoSel.Expand();
            int idPerfil = ObtenerIdPorNombre456VG(padrePerfil.Nombre456VG);
            int idFamilia = beFamilia.CodPermiso456VG;
            var resBD = bllper.aggPermisos456VG(new BEPermisoComp_456VG(idPerfil, idFamilia));
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
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void BTNCrearFamilia456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            string nombre = TXTFamilia456VG.Text.Trim(); // textbox del lado derecho

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.CompletarNombre"),
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var beNuevaFamilia = new BEFamilia_456VG() { nombre456VG = nombre };
            var res = bllf.crearEntidad456VG(beNuevaFamilia);
            if (!res.resultado)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorCrear")
                                       .Replace("{0}", res.mensaje),
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.FamiliaCreadaOK"),
                            lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            cargarcb();
            TXTFamilia456VG.Clear();
        }
        private void BTNEliminarFamilia456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var sel = CBFamilias456VG.SelectedItem as KeyValuePair<BEFamilia_456VG, string>?;
            if (!sel.HasValue)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaFamiliaCombo"),
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var beFamilia = sel.Value.Key;
            var confirm = MessageBox.Show(
                lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ConfirmarElim")
                   .Replace("{0}", beFamilia.nombre456VG),
                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            var res = bllf.eliminarEntidad456VG(beFamilia);
            if (!res.resultado)
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorEliminar")
                                   .Replace("{0}", res.mensaje),
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.EliminadaOK"),
                            lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            cargarcb();
        }
        private void PintarNodosRecursivo456VG(Componente_456VG comp, TreeNode nodo)
        {
            nodo.Nodes.Clear();
            foreach (var hijo in comp.ObtenerHijos456VG())
            {
                bool esPermiso = hijo is Permisos_456VG;
                string controlName = esPermiso ? CBPermisos456VG.Name : CBFamilias456VG.Name;
                string nombre = hijo is Permisos_456VG p ? p.Nombre456VG :
                                hijo is Familia_456VG f ? f.Nombre456VG : "Componente";

                string clave = $"{Name}.Item.{controlName}.{nombre}";
                string texto = Lenguaje_456VG.ObtenerInstancia_456VG().ObtenerTexto_456VG(clave);
                if (texto == clave) texto = nombre;

                var subNodo = new TreeNode(texto)
                {
                    Name = clave,
                    Tag = hijo
                };
                nodo.Nodes.Add(subNodo);
                PintarNodosRecursivo456VG(hijo, subNodo);
            }
        }
        private void BTNAplicar456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var selFamilia = CBFamilias456VG.SelectedItem as KeyValuePair<BEFamilia_456VG, string>?;
            var nodoSel = treeView1.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is Familia_456VG familiaPadre))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccioneFamiliaNodo"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var famBE = selFamilia.Value.Key;
            if (familiaPadre.Nombre456VG == famBE.nombre456VG)
            {
                MessageBox.Show(
                    "No se puede agregar una familia dentro de sí misma.",
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var subFamilia = ReconstruirFamiliaRecursiva456VG(famBE.CodPermiso456VG);
            try
            {
                familiaPadre.AgregarHijo456VG(subFamilia);
                PintarNodosRecursivo456VG(familiaPadre, nodoSel);
                nodoSel.Expand();
                LimpiarTreeViewPerfiles456VG();
                var padreBE = bllf.leerEntidades456VG()
                                  .FirstOrDefault(f => f.nombre456VG == familiaPadre.Nombre456VG);
                if (padreBE != null)
                {
                    var res = bllf.AgregarHijo456VG(padreBE.CodPermiso456VG, famBE.CodPermiso456VG);
                    if (!res.resultado)
                    {
                        MessageBox.Show(
                            $"Error en BD: {res.mensaje}",
                            lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
        private Familia_456VG ReconstruirFamiliaRecursiva456VG(int idFamilia)
        {
            var todasFamilias = bllf.leerEntidades456VG();
            var todosPermisos = bllp.CargarCBPermisos456VG();
            var familiaBE = todasFamilias.FirstOrDefault(f => f.CodPermiso456VG == idFamilia);
            if (familiaBE == null) return null;
            var familia = new Familia_456VG(familiaBE.nombre456VG);
            var relaciones = bllf.ObtenerRelacionesDeFamilia456VG(idFamilia);
            foreach (var rel in relaciones)
            {
                var permiso = todosPermisos.FirstOrDefault(p => p.CodPermiso456VG == rel.CodPermisoHijo456VG);
                if (permiso != null)
                {
                    familia.AgregarHijo456VG(new Permisos_456VG(permiso.nombre456VG));
                }
                else
                {
                    var subfamilia = ReconstruirFamiliaRecursiva456VG(rel.CodPermisoHijo456VG);
                    if (subfamilia != null)
                        familia.AgregarHijo456VG(subfamilia);
                }
            }
            return familia;
        }
        private void BTNAgregarPermiso456VG_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var selPermiso = CBPermisos456VG.SelectedItem as KeyValuePair<BEPerfil_456VG, string>?;
            var nodoSel = treeView1.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is Familia_456VG familiaSel))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccioneFamiliaNodo"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (nodoSel == null || nodoSel.Parent == null || nodoSel.Parent.Parent != null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloPermisoEnFamiliaPadre"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var permisoBE = selPermiso.Value.Key;
            var permisoObj = new Permisos_456VG(permisoBE.nombre456VG);
            if (familiaSel.IncluyePermiso(permisoObj.Nombre456VG))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoYaExisteFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            try
            {
                familiaSel.AgregarHijo456VG(permisoObj);
                PintarNodosRecursivo456VG(familiaSel, nodoSel);
                nodoSel.Expand();
                LimpiarTreeViewPerfiles456VG();
                var familiaBE = bllf.leerEntidades456VG()
                                    .FirstOrDefault(f => f.nombre456VG == familiaSel.Nombre456VG);
                if (familiaBE != null)
                {
                    var res = bllf.AgregarHijo456VG(familiaBE.CodPermiso456VG, permisoBE.CodPermiso456VG);
                    if (!res.resultado)
                    {
                        MessageBox.Show(
                            $"Error en BD: {res.mensaje}",
                            lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1456VG.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is Permisos_456VG permiso))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaPermisoNodo"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nodoSel.Parent == null || !(nodoSel.Parent.Tag is Perfil_456VG perfil))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloPermisosDesdePerfil"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (perfil.Nombre456VG.Equals("BASE", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NoEliminarBase"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                perfil.QuitarHijo456VG(permiso);
                nodoSel.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idPerfil = ObtenerIdPorNombre456VG(perfil.Nombre456VG);
            int idPermiso = ObtenerIdPermisoPorNombre456VG(permiso.Nombre456VG);
            var res = bllper.eliminarRelacion456VG(idPerfil, idPermiso);
            if (!res.resultado)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorEliminarRelacion")
                       .Replace("{0}", res.mensaje),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoEliminadoOK"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private int ObtenerIdPermisoPorNombre456VG(string nombrePermiso)
        {
            var listaPermisos = bllp.CargarCBPermisos456VG();
            var p = listaPermisos.FirstOrDefault(x =>
                x.nombre456VG?.Trim().Equals(nombrePermiso.Trim(), StringComparison.OrdinalIgnoreCase) ?? false);
            return p?.CodPermiso456VG ?? -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1456VG.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is Familia_456VG familia))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaFamiliaNodo"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nodoSel.Parent == null || !(nodoSel.Parent.Tag is Perfil_456VG perfil))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloFamiliasDesdePerfil"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (perfil.Nombre456VG.Equals("BASE", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NoEliminarBase"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                perfil.QuitarHijo456VG(familia); 
                nodoSel.Remove(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idPerfil = ObtenerIdPorNombre456VG(perfil.Nombre456VG);
            int idFamilia = ObtenerIdFamiliaPorNombre456VG(familia.Nombre456VG);
            if (idFamilia < 0)
            {
                MessageBox.Show("No se pudo obtener el ID de la familia.",
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var res = bllper.eliminarRelacion456VG(idPerfil, idFamilia);
            if (!res.resultado)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.ErrorEliminarRelacion")
                       .Replace("{0}", res.mensaje),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.FamiliaEliminadaOK"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private int ObtenerIdFamiliaPorNombre456VG(string nombreFamilia)
        {
            var lista = bllf.leerEntidades456VG();
            var f = lista.FirstOrDefault(x =>
                x.nombre456VG?.Trim().Equals(nombreFamilia.Trim(), StringComparison.OrdinalIgnoreCase) ?? false);
            return f?.CodPermiso456VG ?? -1;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is Familia_456VG familiaHija))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaSubfamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nodoSel.Parent != null || nodoSel.Parent.Tag != null)
            {
                if (nodoSel.Parent?.Text == "Base")
                {
                    MessageBox.Show(
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NoEliminarFamiliaDeBase"),
                        lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!(nodoSel.Parent.Tag is Familia_456VG familiaPadre))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NoEsSubfamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                familiaPadre.QuitarHijo456VG(familiaHija);
                nodoSel.Remove();
                LimpiarTreeViewPerfiles456VG();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var todasFamilias = bllf.leerEntidades456VG();
            int idPadre = todasFamilias.FirstOrDefault(f => f.nombre456VG == familiaPadre.Nombre456VG)?.CodPermiso456VG ?? -1;
            int idHija = todasFamilias.FirstOrDefault(f => f.nombre456VG == familiaHija.Nombre456VG)?.CodPermiso456VG ?? -1;
            if (idPadre < 0 || idHija < 0)
            {
                MessageBox.Show("No se encontraron los IDs de las familias.",
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var res = bllf.EliminarRelacion456VG(idPadre, idHija);
            if (!res.resultado)
            {
                MessageBox.Show($"Error en base de datos: {res.mensaje}",
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SubfamiliaQuitadaOK"),
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            var nodoSel = treeView1.SelectedNode;
            if (nodoSel == null || !(nodoSel.Tag is Permisos_456VG permiso))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SeleccionaPermisoEnFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TreeNode nodoFamilia = nodoSel.Parent;
            if (nodoFamilia == null || !(nodoFamilia.Tag is Familia_456VG familiaPadre))
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.SoloPermisoDirectoEnFamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nodoFamilia.Parent != null && nodoFamilia.Parent.Parent != null)
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.NoPermisoDesdeSubfamilia"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                familiaPadre.QuitarHijo456VG(permiso);
                nodoSel.Remove();
                LimpiarTreeViewPerfiles456VG();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idFamilia = bllf.leerEntidades456VG()
                                .FirstOrDefault(f => f.nombre456VG == familiaPadre.Nombre456VG)?.CodPermiso456VG ?? -1;
            int idPermiso = bllp.CargarCBPermisos456VG()
                                .FirstOrDefault(p => p.nombre456VG == permiso.Nombre456VG)?.CodPermiso456VG ?? -1;
            if (idFamilia < 0 || idPermiso < 0)
            {
                MessageBox.Show("No se pudieron obtener los IDs para eliminar.",
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var res = bllf.EliminarRelacion456VG(idFamilia, idPermiso);
            if (!res.resultado)
            {
                MessageBox.Show($"Error al eliminar relación: {res.mensaje}",
                                lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Msg.PermisoQuitadoOK"),
                    lng.ObtenerTexto_456VG("Perfiles_456VG.Text"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LimpiarTreeViewPerfiles456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            treeView1456VG.Nodes.Clear();
            INICIO = new Perfil_456VG("BASE");
            string claveRoot = $"{Name}.Item.{comboBox3456VG.Name}.BASE";
            string textoTraducido = lng.ObtenerTexto_456VG(claveRoot);
            if (textoTraducido == claveRoot) textoTraducido = "Base";
            var root = new TreeNode(textoTraducido) { Name = claveRoot, Tag = INICIO };
            treeView1456VG.Nodes.Add(root);
        }
        private void LimpiarTreeViewFamilias456VG()
        {
            var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
            treeView1.Nodes.Clear();
            familiaRaiz456VG = new Familia_456VG("BASE");
            string claveRoot = $"{Name}.Item.{CBFamilias456VG.Name}.BASE";
            string textoTraducido = lng.ObtenerTexto_456VG(claveRoot);
            if (textoTraducido == claveRoot) textoTraducido = "Base";
            var root = new TreeNode(textoTraducido)
            {
                Name = claveRoot,
                Tag = familiaRaiz456VG
            };
            treeView1.Nodes.Add(root);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            LimpiarTreeViewPerfiles456VG();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LimpiarTreeViewFamilias456VG();
        }
    }
}
