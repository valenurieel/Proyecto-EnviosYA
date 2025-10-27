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
                    bool historialYaActivo = false;
                    using (var cmd = new SqlCommand(@"
                USE EnviosYA_456VG;
                SELECT TOP(1) activo_456VG
                FROM dbo.Choferes_C_456VG WITH (UPDLOCK, HOLDLOCK)
                WHERE dni_chofer_456VG = @dni
                  AND ABS(DATEDIFF(SECOND, fecha_456VG, @fecha)) <= 1;", db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.Parameters.AddWithValue("@fecha", fechaSeleccionada);
                        var o = cmd.ExecuteScalar();
                        if (o == null)
                        {
                            r.resultado = false;
                            r.mensaje = "No existe la versión histórica seleccionada.";
                            tx.Rollback();
                            return r;
                        }
                        historialYaActivo = Convert.ToBoolean(o);
                    }
                    if (historialYaActivo)
                    {
                        r.resultado = false;
                        r.mensaje = "YA_ACTIVO";
                        tx.Rollback();
                        return r;
                    }
                    string fechaIso = fechaSeleccionada.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                    using (var ctx1 = new SqlCommand(
                        "EXEC sys.sp_set_session_context @key=N'BitacoraAccion', @value=N'ActivarDesdeBitacora';",
                        db.Connection, tx)) ctx1.ExecuteNonQuery();
                    using (var ctx2 = new SqlCommand(
                        "EXEC sys.sp_set_session_context @key=N'BitacoraFechaSel', @value=@v;",
                        db.Connection, tx))
                    {
                        ctx2.Parameters.AddWithValue("@v", fechaIso);
                        ctx2.ExecuteNonQuery();
                    }
                    int rows;
                    using (var cmdUpd = new SqlCommand(@"
                USE EnviosYA_456VG;
                UPDATE c SET
                    c.nombre_456VG               = h.nombre_456VG,
                    c.apellido_456VG             = h.apellido_456VG,
                    c.telefono_456VG             = h.telefono_456VG,
                    c.registro_456VG             = h.registro_456VG,
                    c.vencimiento_registro_456VG = h.vencimiento_registro_456VG,
                    c.fechanacimiento_456VG      = h.fechanacimiento_456VG,
                    c.disponible_456VG           = h.disponible_456VG,
                    c.activo_456VG               = 1
                FROM dbo.Choferes_456VG c
                INNER JOIN dbo.Choferes_C_456VG h
                    ON h.dni_chofer_456VG = c.dni_chofer_456VG
                WHERE c.dni_chofer_456VG = @dni
                  AND ABS(DATEDIFF(SECOND, h.fecha_456VG, @fecha)) <= 1;", db.Connection, tx))
                    {
                        cmdUpd.Parameters.AddWithValue("@dni", dni);
                        cmdUpd.Parameters.AddWithValue("@fecha", fechaSeleccionada);
                        rows = cmdUpd.ExecuteNonQuery();
                    }
                    using (var clr1 = new SqlCommand("EXEC sys.sp_set_session_context @key=N'BitacoraAccion', @value=NULL;", db.Connection, tx)) clr1.ExecuteNonQuery();
                    using (var clr2 = new SqlCommand("EXEC sys.sp_set_session_context @key=N'BitacoraFechaSel', @value=NULL;", db.Connection, tx)) clr2.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        r.resultado = false;
                        r.mensaje = "No se pudo activar la versión: no coincidió la fecha.";
                        tx.Rollback();
                        return r;
                    }
                    tx.Commit();
                    r.resultado = true;
                    r.mensaje = "Versión activada sin crear un nuevo registro.";
                }
            }
            catch (Exception ex)
            {
                r.resultado = false;
                r.mensaje = "Error al restaurar versión: " + ex.Message;
            }
            finally
            {
                db.Desconectar456VG();
            }
            return r;
        }

    }
}
