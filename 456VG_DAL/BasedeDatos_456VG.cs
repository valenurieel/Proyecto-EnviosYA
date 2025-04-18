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


                //scriptDatos();
            }
        }
    }
}
