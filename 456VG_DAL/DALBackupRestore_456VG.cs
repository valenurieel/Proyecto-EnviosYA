using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_DAL
{
    public class DALBackupRestore_456VG
    {
        BasedeDatos_456VG db { get; }
        public DALBackupRestore_456VG()
        {
            db = new BasedeDatos_456VG();
        }
        public void BackupBD456VG(string fullPath, System.Action<string> onInfo = null)
        {
            var db = new BasedeDatos_456VG(apuntarAMaster: true);
            try
            {
                db.Conectar456VG();
                if (onInfo != null)
                    db.Connection.InfoMessage += (s, e) => onInfo(e.Message);
                var sql = @"
                DECLARE @p nvarchar(4000) = @path;
                DECLARE @supportsCompression bit = CASE WHEN CAST(SERVERPROPERTY('Edition') AS nvarchar(128)) LIKE '%Express%' THEN 0 ELSE 1 END;
                DECLARE @stmt nvarchar(max) =
                N'BACKUP DATABASE [' + CAST(DB_NAME() AS nvarchar(128)) + N'] TO DISK = @p WITH INIT, STATS=10, CHECKSUM'
                + CASE WHEN @supportsCompression = 1 THEN N', COMPRESSION' ELSE N'' END
                + N';';
                SET @stmt = REPLACE(@stmt, N'[' + DB_NAME() + N']', N'[EnviosYA_456VG]');
                EXEC sp_executesql @stmt, N'@p nvarchar(4000)', @p=@p;
                RESTORE VERIFYONLY FROM DISK = @p WITH CHECKSUM;";
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@path", fullPath);
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                db.Desconectar456VG();
            }
        }
        public void RestoreBD456VG(string bakPath, System.Action<string> onInfo = null)
        {
            var db = new BasedeDatos_456VG(apuntarAMaster: true);
            try
            {
                db.Conectar456VG();
                if (onInfo != null)
                    db.Connection.InfoMessage += (s, e) => onInfo(e.Message);
                var sql = @"
                DECLARE @p nvarchar(4000) = @path;
                ALTER DATABASE [EnviosYA_456VG] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                BEGIN TRY
                    RESTORE DATABASE [EnviosYA_456VG] FROM DISK = @p WITH REPLACE, STATS=10;
                END TRY
                BEGIN CATCH
                    ALTER DATABASE [EnviosYA_456VG] SET MULTI_USER;
                    THROW;
                END CATCH;
                ALTER DATABASE [EnviosYA_456VG] SET MULTI_USER;";
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@path", bakPath);
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                db.Desconectar456VG();
            }
        }
    }
}
