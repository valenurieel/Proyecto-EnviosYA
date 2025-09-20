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
    public partial class EntregaEnvíos_456VG : Form
    {
        public EntregaEnvíos_456VG()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReasignarEntrega_456VG fr = new ReasignarEntrega_456VG();
            fr.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
