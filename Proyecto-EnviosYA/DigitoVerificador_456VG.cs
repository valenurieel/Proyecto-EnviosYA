using _456VG_BLL;
using _456VG_Servicios;
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
    public partial class DigitoVerificador_456VG : Form, IObserver_456VG
    {
        BLLDigitoVerificador_456VG bllDV = new BLLDigitoVerificador_456VG();
        private readonly bool desdeInconsistencia;
        private bool cierreSeguro = false;
        public DigitoVerificador_456VG(bool desdeInconsistencia = false)
        {
            InitializeComponent();
            this.desdeInconsistencia = desdeInconsistencia;
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        private void DigitoVerificador_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
            VerificarInconsistencias();
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
        }
        private void VerificarInconsistencias()
        {
            var inconsistencias = bllDV.DetectarInconsistencias456VG();
            if (inconsistencias.Count == 0)
            {
                labelTablas.Text = "";
                btnrecalcular.Enabled = false;
                btnrestaurar.Enabled = false;
            }
            else
            {
                labelTablas.Text = string.Join(", ", inconsistencias);
                btnrecalcular.Enabled = true;
                btnrestaurar.Enabled = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            cierreSeguro = true;
            SessionManager_456VG.ObtenerInstancia456VG().CerrarSesion456VG();
            Application.Exit();
        }
        private void btnrecalcular_Click(object sender, EventArgs e)
        {
            bllDV.ActualizarDV456VG();
            MessageBox.Show(
                Lenguaje_456VG.ObtenerInstancia_456VG().ObtenerTexto_456VG("DigitoVerificador_456VG.Msg.Recalculado"),
                Lenguaje_456VG.ObtenerInstancia_456VG().ObtenerTexto_456VG("General.TítuloInfo"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            SessionManager_456VG.ObtenerInstancia456VG().CerrarSesion456VG();
            cierreSeguro = true;
            Application.Restart();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (cierreSeguro)
            {
                base.OnFormClosing(e);
                return;
            }
            if (desdeInconsistencia)
            {
                var msg = Lenguaje_456VG.ObtenerInstancia_456VG()
                    .ObtenerTexto_456VG("BackupRestore_456VG.Msg.SalirPorInconsistencia");
                var titulo = Lenguaje_456VG.ObtenerInstancia_456VG()
                    .ObtenerTexto_456VG("BackupRestore_456VG.Msg.TituloSalirPorInconsistencia");
                DialogResult result = MessageBox.Show(msg, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                Application.Exit();
                return;
            }
            base.OnFormClosing(e);
        }
        private void btnrestaurar_Click(object sender, EventArgs e)
        {
            cierreSeguro = true;
            var frm = new BackupRestore_456VG(true, true);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            this.Close();
        }
        private void labelTablas_Click(object sender, EventArgs e)
        {
        }
    }
}
