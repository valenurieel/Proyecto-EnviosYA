using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _456VG_DAL
{
    public class DALChofer_456VG : ICrud_456VG<BEChofer_456VG>
    {
        BasedeDatos_456VG db { get; }
        public DALChofer_456VG()
        {
            db = new BasedeDatos_456VG();
        }
        public Resultado_456VG<BEChofer_456VG> crearEntidad456VG(BEChofer_456VG obj)
        {
            var r = new Resultado_456VG<BEChofer_456VG>();
            try
            {
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "INSERT INTO Choferes_456VG " +
                        "(dni_chofer_456VG, nombre_456VG, apellido_456VG, telefono_456VG, registro_456VG, vencimiento_registro_456VG, fechanacimiento_456VG, disponible_456VG, activo_456VG) " +
                        "VALUES (@dni, @nom, @ape, @tel, @reg, @venc, @fnac, @disp, @act);";

                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@dni", obj.DNIChofer456VG);
                        cmd.Parameters.AddWithValue("@nom", obj.Nombre456VG);
                        cmd.Parameters.AddWithValue("@ape", obj.Apellido456VG);
                        cmd.Parameters.AddWithValue("@tel", obj.Teléfono456VG);
                        cmd.Parameters.AddWithValue("@reg", obj.Registro456VG);
                        cmd.Parameters.AddWithValue("@venc", obj.VencimientoRegistro456VG);
                        cmd.Parameters.AddWithValue("@fnac", obj.FechaNacimiento456VG);
                        cmd.Parameters.AddWithValue("@disp", obj.Disponible456VG);
                        cmd.Parameters.AddWithValue("@act", obj.Activo456VG);
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                r.resultado = true;
                r.entidad = obj;
                r.mensaje = "Chofer creado correctamente.";
            }
            catch (Exception ex)
            {
                r.resultado = false;
                r.mensaje = ex.Message;
            }
            finally { db.Connection.Close(); }
            return r;
        }
        public Resultado_456VG<BEChofer_456VG> actualizarEntidad456VG(BEChofer_456VG obj)
        {
            var r = new Resultado_456VG<BEChofer_456VG>();
            try
            {
                if (!db.Conectar456VG()) throw new Exception("No se pudo conectar.");
                const string sql =
                    "USE EnviosYA_456VG; " +
                    "UPDATE dbo.Choferes_456VG SET " +
                    "    nombre_456VG = @nom, " +
                    "    apellido_456VG = @ape, " +
                    "    telefono_456VG = @tel, " +
                    "    registro_456VG = @reg, " +
                    "    vencimiento_registro_456VG = @venc, " +
                    "    fechanacimiento_456VG = @fnac " +
                    "WHERE dni_chofer_456VG = @dni;";
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@nom", obj.Nombre456VG);
                    cmd.Parameters.AddWithValue("@ape", obj.Apellido456VG);
                    cmd.Parameters.AddWithValue("@tel", obj.Teléfono456VG);
                    cmd.Parameters.AddWithValue("@reg", obj.Registro456VG);
                    cmd.Parameters.AddWithValue("@venc", obj.VencimientoRegistro456VG);
                    cmd.Parameters.AddWithValue("@fnac", obj.FechaNacimiento456VG);
                    cmd.Parameters.AddWithValue("@dni", obj.DNIChofer456VG);
                    int rows = cmd.ExecuteNonQuery();
                    r.resultado = rows > 0;
                    r.mensaje = rows > 0 ? "Chofer actualizado." : "No se encontró el chofer.";
                    r.entidad = rows > 0 ? obj : null;
                }
            }
            catch (Exception ex)
            {
                r.resultado = false;
                r.mensaje = ex.Message;
            }
            finally
            {
                db.Desconectar456VG();
            }
            return r;
        }
        public Resultado_456VG<BEChofer_456VG> eliminarEntidad456VG(BEChofer_456VG obj)
        {
            throw new NotImplementedException();
        }
        public List<BEChofer_456VG> leerEntidades456VG()
        {
            var lista = new List<BEChofer_456VG>();
            const string sql =
                "USE EnviosYA_456VG; " +
                "SELECT dni_chofer_456VG, nombre_456VG, apellido_456VG, telefono_456VG, registro_456VG, vencimiento_registro_456VG, fechanacimiento_456VG, disponible_456VG, activo_456VG " +
                "FROM Choferes_456VG;";
            try
            {
                if (!db.Conectar456VG()) throw new Exception("No se pudo conectar.");
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var c = new BEChofer_456VG(
                            rd.GetString(rd.GetOrdinal("dni_chofer_456VG")),
                            rd.GetString(rd.GetOrdinal("nombre_456VG")),
                            rd.GetString(rd.GetOrdinal("apellido_456VG")),
                            rd.GetString(rd.GetOrdinal("telefono_456VG")),
                            rd.GetBoolean(rd.GetOrdinal("registro_456VG")),
                            rd.GetDateTime(rd.GetOrdinal("vencimiento_registro_456VG")),
                            rd.GetDateTime(rd.GetOrdinal("fechanacimiento_456VG")),
                            rd.GetBoolean(rd.GetOrdinal("disponible_456VG")),
                            rd.GetBoolean(rd.GetOrdinal("activo_456VG"))
                        );
                        lista.Add(c);
                    }
                }
            }
            catch
            {
                lista = new List<BEChofer_456VG>();
            }
            finally { db.Desconectar456VG(); }
            return lista;
        }
        public Resultado_456VG<BEChofer_456VG> ActDesacChof456(string dni, bool nuevoEstadoActivo)
        {
            var r = new Resultado_456VG<BEChofer_456VG>();
            try
            {
                if (!db.Conectar456VG()) throw new Exception("No se pudo conectar.");
                const string sql = "USE EnviosYA_456VG; UPDATE Choferes_456VG SET activo_456VG=@act WHERE dni_chofer_456VG=@dni;";
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@act", nuevoEstadoActivo);
                    cmd.Parameters.AddWithValue("@dni", dni);
                    int rows = cmd.ExecuteNonQuery();
                    r.resultado = rows > 0;
                    r.mensaje = rows > 0 ? "Chofer actualizado." : "No se encontró el chofer.";
                }
            }
            catch (Exception ex)
            {
                r.resultado = false;
                r.mensaje = ex.Message;
            }
            finally { db.Desconectar456VG(); }
            return r;
        }
        public Resultado_456VG<BEChofer_456VG> ObtenerChoferPorDNI456VG(string dni)
        {
            var r = new Resultado_456VG<BEChofer_456VG>();
            const string sql =
                "USE EnviosYA_456VG; " +
                "SELECT dni_chofer_456VG, nombre_456VG, apellido_456VG, telefono_456VG, registro_456VG, vencimiento_registro_456VG, fechanacimiento_456VG, disponible_456VG, activo_456VG " +
                "FROM Choferes_456VG WHERE dni_chofer_456VG = @dni;";
            try
            {
                if (!db.Conectar456VG()) throw new Exception("No se pudo conectar.");
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            var c = new BEChofer_456VG(
                                rd.GetString(0),
                                rd.GetString(1),
                                rd.GetString(2),
                                rd.GetString(3),
                                rd.GetBoolean(4),
                                rd.GetDateTime(5),
                                rd.GetDateTime(6),
                                rd.GetBoolean(7),
                                rd.GetBoolean(8)
                            );
                            r.resultado = true;
                            r.entidad = c;
                            r.mensaje = "Chofer encontrado.";
                        }
                        else
                        {
                            r.resultado = false;
                            r.mensaje = "No existe chofer con ese DNI.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                r.resultado = false;
                r.mensaje = ex.Message;
            }
            finally { db.Desconectar456VG(); }
            return r;
        }
    }
}
