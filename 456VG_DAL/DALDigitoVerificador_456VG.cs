using _456VG_BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_DAL
{
    public class DALDigitoVerificador_456VG
    {
        BasedeDatos_456VG db { get; }
        public DALDigitoVerificador_456VG()
        {
            db = new BasedeDatos_456VG();
        }
        public List<(string Tabla, long DVH, long DVV)> CalcularDVGeneral456VG()
        {
            string[] tablasNegocio = {
        "Clientes_456VG", "Paquetes_456VG", "Envios_456VG", "EnviosPaquetes_456VG",
        "Facturas_456VG", "DatosPago_456VG", "Transportes_456VG", "Choferes_456VG",
        "ListaCarga_456VG", "DetalleListaCarga_456VG", "Entregado_456VG"
    };
            var resultados = new List<(string Tabla, long DVH, long DVV)>();
            db.Conectar456VG();
            foreach (var tabla in tablasNegocio)
            {
                DataTable dt = ObtenerTabla(tabla);
                long dvh = CalcularDVH(dt);
                long dvv = CalcularDVV(dt);
                resultados.Add((tabla, dvh, dvv));
            }
            db.Desconectar456VG();
            return resultados;
        }
        public List<(string Tabla, long DVH, long DVV)> LeerDVsGuardados()
        {
            List<(string Tabla, long DVH, long DVV)> registros = new List<(string, long, long)>();
            db.Conectar456VG();
            string query = "SELECT NombreTabla_456VG, DVH, DVV FROM DigitoVerificador_456VG";
            SqlCommand cmd = new SqlCommand(query, db.Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                registros.Add((
                    reader["NombreTabla_456VG"].ToString(),
                    Convert.ToInt64(reader["DVH"]),
                    Convert.ToInt64(reader["DVV"])
                ));
            }
            db.Desconectar456VG();
            return registros;
        }
        private DataTable ObtenerTabla(string nombreTabla)
        {
            var query = $"SELECT * FROM {nombreTabla}";
            SqlDataAdapter da = new SqlDataAdapter(query, db.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        //Método Extra - Hace suma Hexadecimal
        private static long SumarHexDeString(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return 0;
            valor = valor.Trim();
            byte[] bytes = Encoding.UTF8.GetBytes(valor);
            long suma = 0;
            foreach (byte b in bytes)
            {
                string hex = b.ToString("X2");
                suma += Convert.ToInt32(hex, 16);
            }
            return suma;
        }
        //Método Extra - Convierte distintas variables a un fin "Normal"
        private static string AStringNormalizado(object valor)
        {
            if (valor == null || valor == DBNull.Value) return string.Empty;
            switch (valor)
            {
                case DateTime dt:
                    return dt.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                case decimal d:
                    return d.ToString(CultureInfo.InvariantCulture);
                case double db:
                    return db.ToString(CultureInfo.InvariantCulture);
                case float f:
                    return f.ToString(CultureInfo.InvariantCulture);
                case IFormattable formattable:
                    return formattable.ToString(null, CultureInfo.InvariantCulture);
                default:
                    return valor.ToString();
            }
        }
        private long CalcularDVH(DataTable table)
        {
            long total = 0;
            foreach (DataRow row in table.Rows)
            {
                long sumaFila = 0;
                foreach (DataColumn col in table.Columns)
                {
                    string valorStr = AStringNormalizado(row[col]);
                    sumaFila += SumarHexDeString(valorStr);
                }
                total += sumaFila;
            }
            return total;
        }
        private long CalcularDVV(DataTable table)
        {
            long total = 0;
            foreach (DataColumn col in table.Columns)
            {
                long sumaCol = 0;
                foreach (DataRow row in table.Rows)
                {
                    string valorStr = AStringNormalizado(row[col]);
                    sumaCol += SumarHexDeString(valorStr);
                }
                total += sumaCol;
            }
            return total;
        }
        public void ActualizarDV456VG()
        {
            string[] tablasNegocio = {
                "Clientes_456VG",
                "Paquetes_456VG",
                "Envios_456VG",
                "EnviosPaquetes_456VG",
                "Facturas_456VG",
                "DatosPago_456VG",
                "Transportes_456VG",
                "Choferes_456VG",
                "ListaCarga_456VG",
                "DetalleListaCarga_456VG",
                "Entregado_456VG"
            };
            db.Conectar456VG();
            string limpiar = "DELETE FROM DigitoVerificador_456VG";
            SqlCommand cmdLimpiar = new SqlCommand(limpiar, db.Connection);
            cmdLimpiar.ExecuteNonQuery();
            foreach (var tabla in tablasNegocio)
            {
                DataTable dt = ObtenerTabla(tabla);
                long dvh = CalcularDVH(dt);
                long dvv = CalcularDVV(dt);
                string insertar = @"
            INSERT INTO DigitoVerificador_456VG (NombreTabla_456VG, DVH, DVV)
            VALUES (@tabla, @dvh, @dvv)";
                SqlCommand cmdInsertar = new SqlCommand(insertar, db.Connection);
                cmdInsertar.Parameters.AddWithValue("@tabla", tabla);
                cmdInsertar.Parameters.AddWithValue("@dvh", dvh.ToString());
                cmdInsertar.Parameters.AddWithValue("@dvv", dvv.ToString());
                cmdInsertar.ExecuteNonQuery();
            }
            db.Desconectar456VG();
        }
        public BEDigitoVerificador_456VG LeerDV456VG()
        {
            db.Conectar456VG();
            string query = "SELECT TOP 1 * FROM DigitoVerificador_456VG";
            SqlCommand cmd = new SqlCommand(query, db.Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            BEDigitoVerificador_456VG dv = null;
            if (reader.Read())
            {
                dv = new BEDigitoVerificador_456VG(
                    Convert.ToInt32(reader["IdDigitoVerificador"]),
                    reader["NombreTabla_456VG"].ToString(),
                    reader["DVV"].ToString(),
                    reader["DVH"].ToString()
                );
            }
            db.Desconectar456VG();
            return dv;
        }
    }
}