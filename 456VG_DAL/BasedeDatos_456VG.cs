using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_DAL
{
    public class BasedeDatos_456VG
    {
        public static string dataSource = "DESKTOP-Q714KGU\\SQLEXPRESS";
        public static string dbName = "EnviosYA";
        public static string conexionMaster = $"Data source={dataSource};Initial Catalog=master;Integrated Security=True;";
        public SqlConnection Connection = new SqlConnection(conexionMaster);
        public SqlCommand Command = new SqlCommand();
        public bool Conectar()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
                return true;
            }
            return false;
        }
        public bool Desconectar()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                return true;
            }
            return false;
        }
        public bool ejecutarQuery(string query)
        {
            try
            {
                Conectar();
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
                Desconectar();
            }
        }
        public void scriptInicio()
        {
            bool bdCreada = ejecutarQuery("CREATE DATABASE EnviosYA;");
            // Crear las tablas dentro de la base de datos EnviosYA
            if (bdCreada)
            {
                //Tabla Usuario
                ejecutarQuery("USE EnviosYA; CREATE TABLE Usuario (" +
                    "dni VARCHAR(20) PRIMARY KEY," +
                    "nombre VARCHAR(50) NOT NULL," +
                    "apellido VARCHAR(50) NOT NULL," +
                    "email VARCHAR(50) NOT NULL," +
                    "telefono VARCHAR(20) NOT NULL," +
                    "nombreusuario VARCHAR(50) NOT NULL," +
                    "contraseña VARCHAR(100) NOT NULL," + 
                    "salt VARCHAR(24) NOT NULL," +
                    "domicilio VARCHAR(100) NOT NULL," +
                    "rol VARCHAR(100) NOT NULL," +
                    "bloqueado BIT NOT NULL DEFAULT 0," +
                    "activo BIT NOT NULL DEFAULT 1" +
                ");");




                //scriptDatos();
            }
        }
    }
}
