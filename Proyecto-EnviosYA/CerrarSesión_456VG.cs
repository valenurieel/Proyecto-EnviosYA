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
        private void label()
        {
            MenuPrincipal_456VG menu = Application.OpenForms.OfType<MenuPrincipal_456VG>().FirstOrDefault();
            if (menu != null)
            {
                menu.chau();
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (SessionManager_456VG.ObtenerInstancia().Usuario != null)
            {
                SessionManager_456VG.ObtenerInstancia().CerrarSesion();
                MessageBox.Show("Se ha Cerrado la Sesión Correctamente");
            }
            else
            {
                MessageBox.Show("No se ha Iniciado Sesión");
            }
            label();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
