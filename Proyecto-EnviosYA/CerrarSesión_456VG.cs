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
        private void label456VG()
        {
            MenuPrincipal_456VG menu = Application.OpenForms.OfType<MenuPrincipal_456VG>().FirstOrDefault();
            if (menu != null)
            {
                menu.chau();
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (SessionManager_456VG.ObtenerInstancia456VG().Usuario != null)
            {
                SessionManager_456VG.ObtenerInstancia456VG().CerrarSesion456VG();
                MessageBox.Show("Se ha Cerrado la Sesión exitosamente");
                MenuPrincipal_456VG menu = Application.OpenForms.OfType<MenuPrincipal_456VG>().FirstOrDefault();
                if (menu != null)
                {
                    menu.deshabilitados();
                }
            }
            else
            {
                MessageBox.Show("No se ha Iniciado Sesión aún");
            }
            label456VG();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CerrarSesión_456VG_Load(object sender, EventArgs e)
        {

        }
    }
}
