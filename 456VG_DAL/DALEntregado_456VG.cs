using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_DAL
{
    public class DALEntregado_456VG
    {
        BasedeDatos_456VG db { get; }
        public DALEntregado_456VG()
        {
            db = new BasedeDatos_456VG();
        }
        public int ContarIntentosPorEnvio456VG(string codEnvio)
        {
            try
            {
                db.Conectar456VG();

                string query = @"
            SELECT COUNT(*) 
            FROM Entregado_456VG 
            WHERE codenvio_456VG = @codEnvio;";
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@codEnvio", codEnvio);
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch
            {
                return 0;
            }
            finally
            {
                db.Desconectar456VG();
            }
        }
        public bool InsertarEntrega456VG(BEEntregado_456VG entrega)
        {
            try
            {
                db.Conectar456VG();
                string query = @"INSERT INTO Entregado_456VG
                                (codentrega_456VG, codenvio_456VG, fechaintento_456VG, cantidadintento_456VG, entregado_456VG, motivo_456VG, nombreencargado_456VG)
                                VALUES (@cod, @envio, @fecha, @cant, @entregado, @motivo, @nombre)";
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@cod", entrega.CodEntrega456VG);
                    cmd.Parameters.AddWithValue("@envio", entrega.Envio.CodEnvio456VG);
                    cmd.Parameters.AddWithValue("@fecha", entrega.FechaIntentoEntrega456VG);
                    cmd.Parameters.AddWithValue("@cant", entrega.CantIntento456VG);
                    cmd.Parameters.AddWithValue("@entregado", entrega.Entregado456VG);
                    cmd.Parameters.AddWithValue("@motivo", entrega.Motivo456VG);
                    cmd.Parameters.AddWithValue("@nombre", entrega.NombreEncargado456VG);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                db.Desconectar456VG();
            }
        }
    }
}
