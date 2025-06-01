using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;

namespace Proyecto_EnviosYA
{
    public partial class CambiarIdioma_456VG : Form
    {
        public CambiarIdioma_456VG()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                MessageBox.Show("Complete el campo de idioma.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string nuevoIdioma = box.Text == "Español" ? "ES" : "EN";
            Lenguaje_456VG.ObtenerInstancia_456VG().IdiomaActual_456VG = nuevoIdioma;
            if (SessionManager_456VG.verificarInicioSesion456VG())
            {
                BEUsuario_456VG usuarioActual = SessionManager_456VG.Obtenerdatosuser456VG();
                BLLUsuario_456VG bllUsuario = new BLLUsuario_456VG();
                bllUsuario.modificarIdioma456VG(usuarioActual, nuevoIdioma);
            }

            MessageBox.Show($"Se cambió el idioma correctamente a: {box.Text}",
                            "Idioma Actualizado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
    }
}
