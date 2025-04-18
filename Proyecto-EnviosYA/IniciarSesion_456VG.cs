using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _456VG_BLL;
using _456VG_Servicios;
using _456VG_BE;

namespace Proyecto_EnviosYA
{
    public partial class IniciarSesion_456VG : Form
    {
        public IniciarSesion_456VG()
        {
            InitializeComponent();
        }
        BLLUsuario_456VG BLLUsuario = new BLLUsuario_456VG();
        private void IniciarSesion_456VG_Load(object sender, EventArgs e)
        {

        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            if (txtdni.Text == string.Empty || txtcontraseña.Text == string.Empty)
            {
                MessageBox.Show("Complete los campos");
                return;
            }
            if (SessionManager_456VG.ObtenerInstancia().Usuario != null)
            {
                MessageBox.Show("Ya hay una sesión activa.");
                return;
            }
            Resultado_456VG<BEUsuario_456VG> result = BLLUsuario.recuperarUsuario(txtdni.Text.Trim(), txtcontraseña.Text.Trim());
            if (!result.resultado)
            {
                MessageBox.Show(result.mensaje);
                return;
            }
            SessionManager_456VG.ObtenerInstancia().IniciarSesion(result.entidad);
            MessageBox.Show("Sesion Iniciada Correctamente");
        }
    }
}
