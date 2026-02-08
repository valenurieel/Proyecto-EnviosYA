using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public class BasedeDatos_456VG
{
    private static readonly string dataSource = ConfigurationManager.AppSettings["ServidorSQL"] ?? ".";
    private static readonly string dbName = ConfigurationManager.AppSettings["NombreBD"] ?? "EnviosYA_456VG";
    public static string conexionDB =
        $"Data Source={dataSource};Initial Catalog={dbName};Integrated Security=True;MultipleActiveResultSets=True;";
    private string conexionMaster = $"Data Source={dataSource};Initial Catalog=master;Integrated Security=True;";
    public SqlConnection Connection { get; }
    public BasedeDatos_456VG(bool apuntarAMaster = false)
    {
        string cs = apuntarAMaster ? conexionMaster : conexionDB;
        Connection = new SqlConnection(cs);
    }
    public bool Conectar456VG()
    {
        if (Connection.State == ConnectionState.Closed)
        {
            Connection.Open();
            return true;
        }
        return false;
    }
    public bool Desconectar456VG()
    {
        if (Connection.State == ConnectionState.Open)
        {
            Connection.Close();
            return true;
        }
        return false;
    }
    public void ConfigurarEntornoInicial456VG()
    {
        try
        {
            if (!ExisteBaseDeDatos456VG())
            {
                EjecutarScriptInstalacion456VG();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error crítico de base de datos: " + ex.Message, "Instalador EnviosYA", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private bool ExisteBaseDeDatos456VG()
    {
        using (SqlConnection conn = new SqlConnection(conexionMaster))
        {
            string query = $"SELECT database_id FROM sys.databases WHERE name = '{dbName}'";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            return cmd.ExecuteScalar() != null;
        }
    }
    private void EjecutarScriptInstalacion456VG()
    {
        string pathScript = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "instalar_db.sql");

        if (!File.Exists(pathScript))
        {
            throw new FileNotFoundException("El archivo 'instalar_db.sql' no se encuentra en la carpeta de la aplicación.");
        }
        string scriptCompleto = File.ReadAllText(pathScript);
        string[] comandos = Regex.Split(scriptCompleto, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        using (SqlConnection conn = new SqlConnection(conexionMaster))
        {
            conn.Open();
            foreach (string sql in comandos)
            {
                if (!string.IsNullOrWhiteSpace(sql.Trim()))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}