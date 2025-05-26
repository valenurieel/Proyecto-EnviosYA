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
            throw new Exception();
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
                        "  (dni_456VG, nombre_456VG, apellido_456VG, telefono_456VG, domicilio_456VG, fechanacimiento_456VG) " +
                        "VALUES " +
                        "  (@Dni, @Nombre, @Apellido, @Telefono, @Domicilio, @FechaNacimiento);";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@Dni", obj.DNI456VG);
                        cmd.Parameters.AddWithValue("@Nombre", obj.Nombre456VG);
                        cmd.Parameters.AddWithValue("@Apellido", obj.Apellido456VG);
                        cmd.Parameters.AddWithValue("@Telefono", obj.Teléfono456VG);
                        cmd.Parameters.AddWithValue("@Domicilio", obj.Domicilio456VG);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", obj.FechaNacimiento456VG);
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
                resultado.mensaje = ex.Message;
                resultado.entidad = null;
            }
            finally
            {
                db.Connection.Close();
            }
            return resultado;
        }

        public Resultado_456VG<BECliente_456VG> eliminarEntidad456VG(BECliente_456VG obj)
        {
            throw new Exception();
        }
        public List<BECliente_456VG> leerEntidades456VG()
        {
            throw new Exception();
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
