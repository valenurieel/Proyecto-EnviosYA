using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_DAL
{
    public class DALChoferes_C_456VG
    {
        BasedeDatos_456VG db { get; }
        public DALChoferes_C_456VG()
        {
            db = new BasedeDatos_456VG();
        }
        public List<BEChoferes_C_456VG> LeerCambios456VG(DateTime fechaInicio, DateTime fechaFin, string dni = null, string nombre = null)
        {
            var lista = new List<BEChoferes_C_456VG>();
            string sql =
                "USE EnviosYA_456VG; " +
                "SELECT dni_chofer_456VG, nombre_456VG, apellido_456VG, telefono_456VG, registro_456VG, " +
                "vencimiento_registro_456VG, fechanacimiento_456VG, disponible_456VG, activo_456VG, fecha_456VG " +
                "FROM Choferes_C_456VG WHERE fecha_456VG BETWEEN @ini AND @fin ";
            if (!string.IsNullOrEmpty(dni))
                sql += "AND dni_chofer_456VG = @dni ";
            if (!string.IsNullOrEmpty(nombre))
                sql += "AND nombre_456VG LIKE @nom ";
            sql += "ORDER BY fecha_456VG DESC;";
            try
            {
                if (!db.Conectar456VG()) throw new Exception("No se pudo conectar.");
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.Add("@ini", SqlDbType.DateTime).Value = fechaInicio;
                    cmd.Parameters.AddWithValue("@fin", fechaFin);
                    if (!string.IsNullOrEmpty(dni))
                        cmd.Parameters.AddWithValue("@dni", dni);
                    if (!string.IsNullOrEmpty(nombre))
                        cmd.Parameters.AddWithValue("@nom", "%" + nombre + "%");
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var c = new BEChoferes_C_456VG(
                                rd.GetString(0),
                                rd.GetString(1),
                                rd.GetString(2),
                                rd.GetString(3),
                                rd.GetBoolean(4),
                                rd.GetDateTime(5),
                                rd.GetDateTime(6),
                                rd.GetBoolean(7),
                                rd.GetBoolean(8),
                                rd.GetDateTime(9)
                            );
                            lista.Add(c);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer cambios: " + ex.Message);
                lista = new List<BEChoferes_C_456VG>();
            }
            finally { db.Desconectar456VG(); }

            return lista;
        }
        public Resultado_456VG<BEChoferes_C_456VG> CambiarEstadoChofer456VG(string dni, DateTime fechaSeleccionada)
        {
            var r = new Resultado_456VG<BEChoferes_C_456VG>();
            try
            {
                if (!db.Conectar456VG()) throw new Exception("No se pudo conectar.");
                using (var tx = db.Connection.BeginTransaction())
                {
                    string sqlInactivar = @"
                UPDATE Choferes_C_456VG 
                SET activo_456VG = 0 
                WHERE dni_chofer_456VG = @dni;";
                    using (var cmd = new SqlCommand(sqlInactivar, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.ExecuteNonQuery();
                    }
                    string sqlActivarExacta = @"
                UPDATE Choferes_C_456VG
                SET activo_456VG = 1
                WHERE dni_chofer_456VG = @dni
                  AND ABS(DATEDIFF(MILLISECOND, fecha_456VG, @fecha)) < 10;";
                    int filasAfectadas;
                    using (var cmd = new SqlCommand(sqlActivarExacta, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.Parameters.AddWithValue("@fecha", fechaSeleccionada);
                        filasAfectadas = cmd.ExecuteNonQuery();
                    }
                    if (filasAfectadas == 0)
                    {
                        string sqlActivarCercana = @"
                    UPDATE Choferes_C_456VG
                    SET activo_456VG = 1
                    WHERE dni_chofer_456VG = @dni
                      AND DATEDIFF(SECOND, fecha_456VG, @fecha) BETWEEN -1 AND 1;";
                        using (var cmd = new SqlCommand(sqlActivarCercana, db.Connection, tx))
                        {
                            cmd.Parameters.AddWithValue("@dni", dni);
                            cmd.Parameters.AddWithValue("@fecha", fechaSeleccionada);
                            filasAfectadas = cmd.ExecuteNonQuery();
                        }
                    }
                    if (filasAfectadas == 0)
                    {
                        string sqlActivarUltima = @"
                    UPDATE Choferes_C_456VG 
                    SET activo_456VG = 1
                    WHERE dni_chofer_456VG = @dni
                      AND fecha_456VG = (
                          SELECT MAX(fecha_456VG)
                          FROM Choferes_C_456VG
                          WHERE dni_chofer_456VG = @dni
                      );";
                        using (var cmd = new SqlCommand(sqlActivarUltima, db.Connection, tx))
                        {
                            cmd.Parameters.AddWithValue("@dni", dni);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    string sqlActualizarChofer = @"
                UPDATE c
                SET 
                    c.nombre_456VG = h.nombre_456VG,
                    c.apellido_456VG = h.apellido_456VG,
                    c.telefono_456VG = h.telefono_456VG,
                    c.registro_456VG = h.registro_456VG,
                    c.vencimiento_registro_456VG = h.vencimiento_registro_456VG,
                    c.fechanacimiento_456VG = h.fechanacimiento_456VG,
                    c.disponible_456VG = h.disponible_456VG,
                    c.activo_456VG = 1
                FROM Choferes_456VG c
                INNER JOIN Choferes_C_456VG h 
                    ON c.dni_chofer_456VG = h.dni_chofer_456VG
                WHERE h.dni_chofer_456VG = @dni AND h.activo_456VG = 1;";
                    using (var cmd = new SqlCommand(sqlActualizarChofer, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }

                r.resultado = true;
                r.mensaje = "Versión activada correctamente.";
            }
            catch (Exception ex)
            {
                r.resultado = false;
                r.mensaje = "Error al activar versión: " + ex.Message;
            }
            finally
            {
                db.Desconectar456VG();
            }
            return r;
        }
    }
}
