using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;

namespace _456VG_DAL
{
    public class BasedeDatos_456VG
    {
        public static string dataSource = "DESKTOP-Q714KGU\\SQLEXPRESS";
        public static string dbName = "EnviosYA_456VG";
        public static string conexionMaster = $"Data source={dataSource};Initial Catalog=master;Integrated Security=True;";
        public SqlConnection Connection = new SqlConnection(conexionMaster);
        public SqlCommand Command = new SqlCommand();
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
        public bool ejecutarQuery456VG(string query)
        {
            try
            {
                Conectar456VG();
                Command = new SqlCommand(query, Connection);
                Command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Desconectar456VG();
            }
        }
        public void scriptInicio456VG()
        {
            bool bdCreada = ejecutarQuery456VG("CREATE DATABASE EnviosYA_456VG;");
            if (bdCreada)
            {
                ejecutarQuery456VG("USE EnviosYA_456VG; CREATE TABLE Usuario_456VG (" +
                    "dni_456VG VARCHAR(20) PRIMARY KEY," +
                    "nombre_456VG VARCHAR(50) NOT NULL," +
                    "apellido_456VG VARCHAR(50) NOT NULL," +
                    "email_456VG VARCHAR(50) NOT NULL," +
                    "telefono_456VG VARCHAR(20) NOT NULL," +
                    "nombreusuario_456VG VARCHAR(50) NOT NULL," +
                    "contraseña_456VG VARCHAR(100) NOT NULL," +
                    "salt_456VG VARCHAR(24) NOT NULL," +
                    "domicilio_456VG VARCHAR(100) NOT NULL," +
                    "rol_456VG VARCHAR(100) NOT NULL," +
                    "bloqueado_456VG BIT NOT NULL DEFAULT 0," +
                    "activo_456VG BIT NOT NULL DEFAULT 1" +
                ");");

                ejecutarQuery456VG("USE EnviosYA_456VG; CREATE TABLE HistorialContraseñas_456VG (" +
                    "dni_456VG VARCHAR(20) NOT NULL, " +
                    "contraseñahash_456VG VARCHAR(100) NOT NULL, " +
                    "salt_456VG VARCHAR(24) NOT NULL, " +
                    "fechacambio_456VG DATETIME NOT NULL DEFAULT GETDATE(), " +
                    "hashsimple_456VG VARCHAR(100) NOT NULL, " +
                    "PRIMARY KEY (dni_456VG, fechacambio_456VG) " + 
                ");");

                ejecutarQuery456VG("USE EnviosYA_456VG; " +
                    "ALTER TABLE HistorialContraseñas_456VG " +
                    "ADD CONSTRAINT FK_HistorialContraseñas_Usuario_456VG " +
                    "FOREIGN KEY (dni_456VG) REFERENCES Usuario_456VG(dni_456VG)");
            }
        }
    }
}
