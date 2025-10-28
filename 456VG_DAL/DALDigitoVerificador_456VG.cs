using _456VG_BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public (string DVH, string DVV) CalcularDVGeneral456VG()
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
            long totalDVH = 0;
            long totalDVV = 0;
            db.Conectar456VG();
            foreach (var tabla in tablasNegocio)
            {
                DataTable dt = ObtenerTabla(tabla);
                totalDVH += CalcularDVH(dt);
                totalDVV += CalcularDVV(dt);
            }
            db.Desconectar456VG();
            return (totalDVH.ToString(), totalDVV.ToString());
        }
        private DataTable ObtenerTabla(string nombreTabla)
        {
            var query = $"SELECT * FROM {nombreTabla}";
            SqlDataAdapter da = new SqlDataAdapter(query, db.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private long CalcularDVH(DataTable table)
        {
            long total = 0;
            foreach (DataRow row in table.Rows)
            {
                long sumaFila = 0;
                foreach (DataColumn col in table.Columns)
                {
                    string valor = row[col] == DBNull.Value ? "" : row[col].ToString();
                    foreach (char c in valor)
                        sumaFila += c;
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
                    string valor = row[col] == DBNull.Value ? "" : row[col].ToString();
                    foreach (char c in valor)
                        sumaCol += c;
                }
                total += sumaCol;
            }
            return total;
        }
        public void ActualizarDV456VG()
        {
            var dv = CalcularDVGeneral456VG();
            db.Conectar456VG();
            string query = "UPDATE DigitoVerificador_456VG SET DVH = @dvh, DVV = @dvv WHERE IdDigitoVerificador = 1";
            SqlCommand cmd = new SqlCommand(query, db.Connection);
            cmd.Parameters.AddWithValue("@dvh", dv.DVH);
            cmd.Parameters.AddWithValue("@dvv", dv.DVV);
            cmd.ExecuteNonQuery();
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
                    reader["DVV"].ToString(),
                    reader["DVH"].ToString()
                );
            }
            db.Desconectar456VG();
            return dv;
        }
    }
}