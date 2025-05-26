using _456VG_BE;
using _456VG_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class RegistrarCliente_456VG : Form
    {
        BLLCliente_456VG BLLCliente = new BLLCliente_456VG();
        public RegistrarCliente_456VG()
        {
            InitializeComponent();
        }

        private void btnRegCli456VG_Click(object sender, EventArgs e)
        {
            string dniCli = txtDNICli456VG.Text.Trim();
            string nomCli = txtNomCli456VG.Text.Trim();
            string apeCli = txtApeCli456VG.Text.Trim();
            string telCli = txtTelCli456VG.Text.Trim();
            string domCli = txtDomiCli456VG.Text.Trim();
            DateTime fechaNacCli = dateTimePicker1456VG.Value;
            BECliente_456VG cliente = new BECliente_456VG(dniCli, nomCli, apeCli, telCli, domCli, fechaNacCli);
            var resp = BLLCliente.crearEntidad456VG(cliente);
            if (resp.resultado)
            {
                MessageBox.Show("Cliente registrado exitosamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show($"Error al registrar cliente: {resp.mensaje}", "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegistrarCliente_456VG_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
