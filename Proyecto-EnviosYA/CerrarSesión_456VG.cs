using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _456VG_Servicios;

namespace Proyecto_EnviosYA
{
    public partial class CerrarSesión_456VG : Form
    {
        public CerrarSesión_456VG()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (SessionManager_456VG.ObtenerInstancia().Usuario != null)
            {
                SessionManager_456VG.ObtenerInstancia().CerrarSesion();
                MessageBox.Show("Se ha cerrado la Sesión Correctamente");
            }
            else
            {
                MessageBox.Show("No se ha iniciado sesión anteriormente");
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
