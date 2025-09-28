using _456VG_DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BLL
{
    public class BLLBackupRestore_456VG
    {
        DALBackupRestore_456VG dal;
        public BLLBackupRestore_456VG()
        {
            dal = new DALBackupRestore_456VG();
        }
        public string DirectorioBackup456VG(string baseDirectory, string folderName)
        {
            string dir = Path.Combine(baseDirectory, folderName);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            return dir;
        }
        public string NombreBackup456VG(string prefix = "Backup_EnviosYA")
        {
            string stamp = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            return $"{prefix}-{stamp}.bak";
        }
        public string Backup456VG(string targetDir, Action<string> onInfo = null)
        {
            string fileName = NombreBackup456VG();
            string fullPath = Path.Combine(targetDir, fileName);
            dal.BackupBD456VG(fullPath, onInfo);
            return fullPath;
        }
        public void Restore456VG(string bakPath, Action<string> onInfo = null)
        {
            dal.RestoreBD456VG(bakPath, onInfo);
        }
    }
}
