using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _456VG_DAL
{
    public class DALCliente_456VG:ICrud_456VG<BECliente_456VG>
    {
        BasedeDatos_456VG db { get; }
        HashSHA256_456VG hasher { get; set; }
        public DALCliente_456VG()
        {
            db = new BasedeDatos_456VG();
            hasher = new HashSHA256_456VG();
        }
        public Resultado_456VG<BECliente_456VG> actualizarEntidad456VG(BECliente_456VG obj)
        {
            var resultado = new Resultado_456VG<BECliente_456VG>();
            string queryUpdateClient = @"
                                        USE EnviosYA_456VG;
                                        UPDATE Clientes_456VG
                                        SET 
                                            nombre_456VG = @Nombre,
                                            apellido_456VG = @Apellido,
                                            telefono_456VG = @Telefono,
                                            domicilio_456VG = @Domicilio,
                                            fechanacimiento_456VG = @FechaNacimiento
                                        WHERE dni_456VG = @DNI";
            try
            {
                bool conectado = db.Conectar456VG();
                if (!conectado) throw new Exception("Error al conectarse a la base de datos");
                using (SqlCommand cmd = new SqlCommand(queryUpdateClient, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre456VG);
                    cmd.Parameters.AddWithValue("@Apellido", obj.Apellido456VG);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Teléfono456VG);
                    cmd.Parameters.AddWithValue("@Domicilio", obj.Domicilio456VG);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", obj.FechaNacimiento456VG);
                    cmd.Parameters.AddWithValue("@DNI", obj.DNI456VG);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        resultado.resultado = true;
                        resultado.mensaje = "Cliente actualizado correctamente.";
                        resultado.entidad = obj;
                    }
                    else
                    {
                        resultado.resultado = false;
                        resultado.mensaje = "No se encontró el cliente con el DNI proporcionado.";
                        resultado.entidad = null;
                    }
                }
                bool desconectado = db.Desconectar456VG();
                if (!desconectado) throw new Exception("Error al desconectarse de la base de datos");
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = "Error al actualizar cliente: " + ex.Message;
                resultado.entidad = null;
            }
            return resultado;
        }
        public Resultado_456VG<BECliente_456VG> crearEntidad456VG(BECliente_456VG obj)
        {
            var resultado = new Resultado_456VG<BECliente_456VG>();
            try
            {
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "INSERT INTO Clientes_456VG " +
                        "  (dni_456VG, nombre_456VG, apellido_456VG, telefono_456VG, domicilio_456VG, fechanacimiento_456VG, activo_456VG) " +
                        "VALUES " +
                        "  (@Dni, @Nombre, @Apellido, @Telefono, @Domicilio, @FechaNacimiento, @Activo);";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@Dni", obj.DNI456VG);
                        cmd.Parameters.AddWithValue("@Nombre", obj.Nombre456VG);
                        cmd.Parameters.AddWithValue("@Apellido", obj.Apellido456VG);
                        cmd.Parameters.AddWithValue("@Telefono", obj.Teléfono456VG);
                        cmd.Parameters.AddWithValue("@Domicilio", obj.Domicilio456VG);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", obj.FechaNacimiento456VG);
                        cmd.Parameters.AddWithValue("@Activo", obj.Activo456VG);      // <-- Nuevo parámetro
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                resultado.resultado = true;
                resultado.entidad = obj;
                resultado.mensaje = "Cliente creado correctamente.";
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.entidad = null;
                resultado.mensaje = ex.Message;
            }
            finally
            {
                db.Connection.Close();
            }
            return resultado;
        }

        public Resultado_456VG<BECliente_456VG> ActDesacCli456(string dni, bool nuevoEstadoActivo)
        {
            Resultado_456VG<BECliente_456VG> resultado = new Resultado_456VG<BECliente_456VG>();
            string query = "USE EnviosYA_456VG; UPDATE Clientes_456VG SET Activo_456VG = @Activo WHERE DNI_456VG = @DNI";
            try
            {
                bool conectado = db.Conectar456VG();
                if (!conectado) throw new Exception("Error al conectarse a la base de datos");
                var trans = db.Connection.BeginTransaction();
                using (SqlCommand comando = new SqlCommand(query, db.Connection, trans))
                {
                    comando.Parameters.AddWithValue("@Activo", nuevoEstadoActivo);
                    comando.Parameters.AddWithValue("@DNI", dni);
                    int filasAfectadas = comando.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        trans.Commit();
                        resultado.resultado = true;
                        resultado.mensaje = "Cliente actualizado correctamente.";
                        resultado.entidad = null;
                    }
                    else
                    {
                        trans.Rollback();
                        resultado.resultado = false;
                        resultado.mensaje = "No se encontró el cliente con ese DNI.";
                    }
                }
                db.Desconectar456VG();
            }
            catch (Exception ex)
            {
                try { db.Connection?.BeginTransaction()?.Rollback(); } catch { }
                db.Desconectar456VG();
                resultado.resultado = false;
                resultado.mensaje = "Error al actualizar el estado del cliente: " + ex.Message;
            }
            return resultado;
        }
        public Resultado_456VG<BECliente_456VG> eliminarEntidad456VG(BECliente_456VG obj)
        {
            throw new Exception();
        }
        public List<BECliente_456VG> leerEntidades456VG()
        {
            List<BECliente_456VG> lista = new List<BECliente_456VG>();
            string sqlQuery = "USE EnviosYA_456VG; SELECT dni_456VG, nombre_456VG, apellido_456VG, " +
                              "telefono_456VG, domicilio_456VG, fechanacimiento_456VG, activo_456VG " +
                              "FROM Clientes_456VG;";
            try
            {
                bool conectado = db.Conectar456VG();
                if (!conectado)
                    throw new Exception("Error al conectarse a la base de datos");
                using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
                {
                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            string dni = !lector.IsDBNull(lector.GetOrdinal("dni_456VG"))
                                         ? lector.GetString(lector.GetOrdinal("dni_456VG"))
                                         : string.Empty;
                            string nombre = !lector.IsDBNull(lector.GetOrdinal("nombre_456VG"))
                                            ? lector.GetString(lector.GetOrdinal("nombre_456VG"))
                                            : string.Empty;
                            string apellido = !lector.IsDBNull(lector.GetOrdinal("apellido_456VG"))
                                              ? lector.GetString(lector.GetOrdinal("apellido_456VG"))
                                              : string.Empty;
                            string telefono = !lector.IsDBNull(lector.GetOrdinal("telefono_456VG"))
                                              ? lector.GetString(lector.GetOrdinal("telefono_456VG"))
                                              : string.Empty;
                            string domicilio = !lector.IsDBNull(lector.GetOrdinal("domicilio_456VG"))
                                               ? lector.GetString(lector.GetOrdinal("domicilio_456VG"))
                                               : string.Empty;
                            DateTime fechaNacimiento = !lector.IsDBNull(lector.GetOrdinal("fechanacimiento_456VG"))
                                                       ? lector.GetDateTime(lector.GetOrdinal("fechanacimiento_456VG"))
                                                       : DateTime.MinValue;
                            bool activo = !lector.IsDBNull(lector.GetOrdinal("activo_456VG"))
                                          && lector.GetBoolean(lector.GetOrdinal("activo_456VG"));
                            BECliente_456VG cliente = new BECliente_456VG(
                                dni,
                                nombre,
                                apellido,
                                telefono,
                                domicilio,
                                fechaNacimiento,
                                activo
                            );
                            lista.Add(cliente);
                        }
                    }
                }
                bool desconectado = db.Desconectar456VG();
                if (!desconectado)
                    throw new Exception("Error al desconectarse de la base de datos");
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar456VG();
                return null;
            }
        }
        public Resultado_456VG<BECliente_456VG> recuperarClientePorDNI456VG(string DNI)
        {
            Resultado_456VG<BECliente_456VG> resultado = new Resultado_456VG<BECliente_456VG>();
            List<BECliente_456VG> list = new List<BECliente_456VG>();
            string sqlQuery = "USE EnviosYA_456VG; SELECT * FROM Clientes_456VG WHERE DNI_456VG = @DNI";
            try
            {
                bool result = db.Conectar456VG();
                if (!result) throw new Exception("Error al conectarse a la base de datos");
                using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
                {
                    command.Parameters.AddWithValue("@DNI", DNI);
                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            string dni = !lector.IsDBNull(lector.GetOrdinal("dni_456VG")) ? lector.GetString(lector.GetOrdinal("dni_456VG")) : string.Empty;
                            string nombre = !lector.IsDBNull(lector.GetOrdinal("nombre_456VG")) ? lector.GetString(lector.GetOrdinal("nombre_456VG")) : string.Empty;
                            string apellido = !lector.IsDBNull(lector.GetOrdinal("apellido_456VG")) ? lector.GetString(lector.GetOrdinal("apellido_456VG")) : string.Empty;
                            string telefono = !lector.IsDBNull(lector.GetOrdinal("telefono_456VG")) ? lector.GetString(lector.GetOrdinal("telefono_456VG")) : string.Empty;
                            string domicilio = !lector.IsDBNull(lector.GetOrdinal("domicilio_456VG")) ? lector.GetString(lector.GetOrdinal("domicilio_456VG")) : string.Empty;
                            DateTime fechanacimiento = !lector.IsDBNull(lector.GetOrdinal("fechanacimiento_456VG")) ? lector.GetDateTime(lector.GetOrdinal("fechanacimiento_456VG")) : DateTime.MinValue;
                            bool activo = !lector.IsDBNull(lector.GetOrdinal("activo_456VG")) && lector.GetBoolean(lector.GetOrdinal("activo_456VG"));
                            BECliente_456VG usuario = new BECliente_456VG(dni, nombre, apellido, telefono, domicilio, fechanacimiento, activo);
                            list.Add(usuario);
                        }
                    }
                }
                db.Desconectar456VG();
                if (list.Count > 0)
                {
                    resultado.resultado = true;
                    resultado.entidad = list[0];
                    resultado.mensaje = "Cliente encontrado correctamente";
                }
                else
                {
                    resultado.resultado = false;
                    resultado.mensaje = "No se encontró un cliente con ese DNI";
                    resultado.entidad = null;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                resultado.entidad = null;
                db.Desconectar456VG();
                return resultado;
            }
        }
        public Resultado_456VG<BECliente_456VG> ObtenerClientePorDNI456VG(string DNI)
            {
                var resultado = new Resultado_456VG<BECliente_456VG>();
                var list = new List<BECliente_456VG>();
                string sqlQuery = "USE EnviosYA_456VG; " +
                                  "SELECT dni_456VG, nombre_456VG, apellido_456VG, telefono_456VG, domicilio_456VG, fechanacimiento_456VG " +
                                  "FROM Clientes_456VG WHERE dni_456VG = @DNI;";
                try
                {
                    if (!db.Conectar456VG())
                        throw new Exception("Error al conectarse a la base de datos");
                    using (var cmd = new SqlCommand(sqlQuery, db.Connection))
                    {
                        cmd.Parameters.AddWithValue("@DNI", DNI);
                        using (var lector = cmd.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                string dni = !lector.IsDBNull(lector.GetOrdinal("dni_456VG"))
                                                      ? lector.GetString(lector.GetOrdinal("dni_456VG"))
                                                      : string.Empty;
                                string nombre = !lector.IsDBNull(lector.GetOrdinal("nombre_456VG"))
                                                      ? lector.GetString(lector.GetOrdinal("nombre_456VG"))
                                                      : string.Empty;
                                string apellido = !lector.IsDBNull(lector.GetOrdinal("apellido_456VG"))
                                                      ? lector.GetString(lector.GetOrdinal("apellido_456VG"))
                                                      : string.Empty;
                                string telefono = !lector.IsDBNull(lector.GetOrdinal("telefono_456VG"))
                                                      ? lector.GetString(lector.GetOrdinal("telefono_456VG"))
                                                      : string.Empty;
                                string domicilio = !lector.IsDBNull(lector.GetOrdinal("domicilio_456VG"))
                                                      ? lector.GetString(lector.GetOrdinal("domicilio_456VG"))
                                                      : string.Empty;
                                DateTime fnac = !lector.IsDBNull(lector.GetOrdinal("fechanacimiento_456VG"))
                                                      ? lector.GetDateTime(lector.GetOrdinal("fechanacimiento_456VG"))
                                                      : DateTime.MinValue;
                                var cliente = new BECliente_456VG(dni, nombre, apellido, telefono, domicilio, fnac);
                                list.Add(cliente);
                            }
                        }
                    }
                    db.Desconectar456VG();
                    if (list.Count > 0)
                    {
                        resultado.resultado = true;
                        resultado.entidad = list[0];
                        resultado.mensaje = "Cliente encontrado correctamente.";
                    }
                    else
                    {
                        resultado.resultado = false;
                        resultado.entidad = null;
                        resultado.mensaje = "No se encontró un cliente con ese DNI.";
                    }
                }
                catch (Exception ex)
                {
                    resultado.resultado = false;
                    resultado.entidad = null;
                    resultado.mensaje = $"Error al buscar cliente: {ex.Message}";
                }
                finally
                {
                    db.Desconectar456VG();
                }
                return resultado;
        }
    }
}
