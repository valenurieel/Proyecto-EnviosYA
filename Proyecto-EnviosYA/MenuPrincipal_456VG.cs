using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _456VG_DAL;

namespace Proyecto_EnviosYA
{
    public partial class MenuPrincipal_456VG : Form
    {
        //private static Form formactivo = null;
        public MenuPrincipal_456VG()
        {
            InitializeComponent();
            BasedeDatos_456VG bd = new BasedeDatos_456VG();
            bd.scriptInicio();
        }
        //private void AbrirForm(Form formu)
        //{
        //    if (formactivo != null)
        //    {
        //        formactivo.Close();
        //    }
        //    formactivo = formu;
        //    formu.TopLevel = false;
        //    formu.FormBorderStyle = FormBorderStyle.None;
        //    formu.Dock = DockStyle.Fill;

        //    this.Controls.Add(formu);
        //    formu.Show();
        //}
        private void MenuPrincipal_456VG_Load(object sender, EventArgs e)
        {

        }

        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AbrirForm(new IniciarSesion_456VG());
            IniciarSesion_456VG fRM = new IniciarSesion_456VG();
            fRM.ShowDialog();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AbrirForm(new RegistrarUsuario_456VG());
            RegistrarUsuario_456VG fRM = new RegistrarUsuario_456VG();
            fRM.ShowDialog();
        }
    }
}
