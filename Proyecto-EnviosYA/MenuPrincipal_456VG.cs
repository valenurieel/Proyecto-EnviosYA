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
        public MenuPrincipal_456VG()
        {
            InitializeComponent();
            BasedeDatos_456VG bd = new BasedeDatos_456VG();
            bd.scriptInicio();
        }

        private void MenuPrincipal_456VG_Load(object sender, EventArgs e)
        {

        }
    }
}
