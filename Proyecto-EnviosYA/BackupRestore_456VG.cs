using _456VG_BE;
using _456VG_BLL;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_EnviosYA
{
    public partial class BackupRestore_456VG : Form, IObserver_456VG
    {
        private const string BACKUP_DIR_NAME = "BackupsSQL";
        BLLBackupRestore_456VG bll = new BLLBackupRestore_456VG();
        private string T(string key) => Lenguaje_456VG.ObtenerInstancia_456VG().ObtenerTexto_456VG(key);
        public BackupRestore_456VG()
        {
            InitializeComponent();
            Lenguaje_456VG.ObtenerInstancia_456VG().Agregar_456VG(this);
        }
        private void button8456VG_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string BackupDirectorio456VG()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            return bll.DirectorioBackup456VG(baseDir, BACKUP_DIR_NAME);
        }
        private void iconBack_Click(object sender, EventArgs e)
        {
            string defaultDir = BackupDirectorio456VG();
            string startDir = Directory.Exists(txtBack.Text) ? txtBack.Text : defaultDir;
            using (var fbd = new FolderBrowserDialog
            {
                SelectedPath = startDir,
                ShowNewFolderButton = true,
            })
            {
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtBack.Text = fbd.SelectedPath;
                }
            }
        }
        private void iconRest_Click(object sender, EventArgs e)
        {
            string startDir = BackupDirectorio456VG();
            using (var ofd = new OpenFileDialog
            {
                Filter = "SQL Backup (*.bak)|*.bak",
                InitialDirectory = startDir
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtRest.Text = ofd.FileName;
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBack.Text) || !Directory.Exists(txtBack.Text))
            {
                MessageBox.Show(
                    T("BackupRestore_456VG.Msg.SeleccioneCarpetaValida"),
                    T("BackupRestore_456VG.Msg.TituloValidacion"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            try
            {
                string fullPath = bll.Backup456VG(targetDir: txtBack.Text, onInfo: msg => Console.WriteLine(msg));
                MessageBox.Show(
                    string.Format(T("BackupRestore_456VG.Msg.BackupOk"), fullPath),
                    T("BackupRestore_456VG.Msg.TituloInfo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                var blleven = new BLLEventoBitacora_456VG();
                string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                blleven.AddBitacora456VG(dni: dniLog, modulo: "Administrador", accion: "Backup", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Crítico);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format(T("BackupRestore_456VG.Msg.ErrorBackup"), ex.Message),
                    T("BackupRestore_456VG.Msg.TituloError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtRest.Text))
            {
                MessageBox.Show(
                    T("BackupRestore_456VG.Msg.SeleccioneBakValido"),
                    T("BackupRestore_456VG.Msg.TituloValidacion"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (MessageBox.Show(
                    string.Format(T("BackupRestore_456VG.Msg.ConfirmarRestore"), txtRest.Text),
                    T("BackupRestore_456VG.Msg.TituloConfirm"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                bll.Restore456VG(bakPath: txtRest.Text, onInfo: msg => Console.WriteLine(msg));
                MessageBox.Show(
                    T("BackupRestore_456VG.Msg.RestoreOk"),
                    T("BackupRestore_456VG.Msg.TituloInfo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                var blleven = new BLLEventoBitacora_456VG();
                string dniLog = SessionManager_456VG.ObtenerInstancia456VG().Usuario.DNI456VG;
                blleven.AddBitacora456VG(dni: dniLog, modulo: "Administrador", accion: "Restaurar", crit: BEEventoBitacora_456VG.NVCriticidad456VG.Crítico);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format(T("BackupRestore_456VG.Msg.ErrorRestore"), ex.Message),
                    T("BackupRestore_456VG.Msg.TituloError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        public void ActualizarIdioma_456VG()
        {
            Lenguaje_456VG.ObtenerInstancia_456VG().CambiarIdiomaControles_456VG(this);
        }
        private void BackupRestore_456VG_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_456VG();
            txtBack.Text = BackupDirectorio456VG();
            txtBack.ReadOnly = true;
        }
    }
}
