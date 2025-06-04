using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _456VG_DAL
{
    public class DALDatosPago_456VG : ICrud_456VG<BEDatosPago_456VG>
    {
        BasedeDatos_456VG db { get; }
        HashSHA256_456VG hasher { get; set; }

        public DALDatosPago_456VG()
        {
            db = new BasedeDatos_456VG();
            hasher = new HashSHA256_456VG();
        }

        public Resultado_456VG<BEDatosPago_456VG> crearEntidad456VG(BEDatosPago_456VG obj)
        {
            var resultado = new Resultado_456VG<BEDatosPago_456VG>();
            try
            {
                if (obj == null || obj.Cliente_456VG == null || string.IsNullOrWhiteSpace(obj.Cliente_456VG.DNI456VG))
                    throw new ArgumentException("Los datos de pago y el cliente con DNI no pueden ser nulos.");

                string dni = obj.Cliente_456VG.DNI456VG.Trim();
                if (string.IsNullOrWhiteSpace(obj.MedioPago456VG) ||
                    string.IsNullOrWhiteSpace(obj.NumTarjeta) ||
                    string.IsNullOrWhiteSpace(obj.Titular) ||
                    obj.FechaVencimiento == default ||
                    string.IsNullOrWhiteSpace(obj.CVC))
                {
                    throw new ArgumentException("Todos los campos de DatosPago deben estar completos.");
                }

                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string checkSql =
                        "USE EnviosYA_456VG; " +
                        "SELECT COUNT(*) FROM DatosPago_456VG WHERE dni_cliente_456VG = @DniCli;";
                    using (var cmdCheck = new SqlCommand(checkSql, db.Connection, tx))
                    {
                        cmdCheck.Parameters.AddWithValue("@DniCli", dni);
                        int existe = (int)cmdCheck.ExecuteScalar();
                        if (existe > 0)
                            throw new Exception($"Ya existen datos de pago para el cliente {dni}.");
                    }

                    const string insertSql =
                        "USE EnviosYA_456VG; " +
                        "INSERT INTO DatosPago_456VG " +
                        "  (dni_cliente_456VG, medio_pago_456VG, numtarjeta_456VG, titular_456VG, fechavencimiento_456VG, cvc_456VG) " +
                        "VALUES (@DniCli, @Medio, @NumTarj, @Tit, @FecVenc, @CVC);";
                    using (var cmd = new SqlCommand(insertSql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@DniCli", dni);
                        cmd.Parameters.AddWithValue("@Medio", obj.MedioPago456VG.Trim());
                        cmd.Parameters.AddWithValue("@NumTarj", obj.NumTarjeta.Trim());
                        cmd.Parameters.AddWithValue("@Tit", obj.Titular.Trim());
                        cmd.Parameters.AddWithValue("@FecVenc", obj.FechaVencimiento.Date);
                        cmd.Parameters.AddWithValue("@CVC", obj.CVC.Trim());

                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                }

                resultado.resultado = true;
                resultado.entidad = obj;
                resultado.mensaje = "Datos de pago guardados correctamente.";
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
            }
            finally
            {
                db.Connection.Close();
            }

            return resultado;
        }

        public Resultado_456VG<BEDatosPago_456VG> actualizarEntidad456VG(BEDatosPago_456VG obj)
        {
            var resultado = new Resultado_456VG<BEDatosPago_456VG>();
            try
            {
                if (obj == null || obj.Cliente_456VG == null || string.IsNullOrWhiteSpace(obj.Cliente_456VG.DNI456VG))
                    throw new ArgumentException("Los datos de pago y el cliente con DNI no pueden ser nulos.");

                string dni = obj.Cliente_456VG.DNI456VG.Trim();
                if (string.IsNullOrWhiteSpace(obj.MedioPago456VG) ||
                    string.IsNullOrWhiteSpace(obj.NumTarjeta) ||
                    string.IsNullOrWhiteSpace(obj.Titular) ||
                    obj.FechaVencimiento == default ||
                    string.IsNullOrWhiteSpace(obj.CVC))
                {
                    throw new ArgumentException("Todos los campos de DatosPago deben estar completos.");
                }

                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string updateSql =
                        "USE EnviosYA_456VG; " +
                        "UPDATE DatosPago_456VG " +
                        "SET medio_pago_456VG = @Medio, " +
                        "    numtarjeta_456VG = @NumTarj, " +
                        "    titular_456VG = @Tit, " +
                        "    fechavencimiento_456VG = @FecVenc, " +
                        "    cvc_456VG = @CVC " +
                        "WHERE dni_cliente_456VG = @DniCli;";
                    using (var cmd = new SqlCommand(updateSql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@DniCli", dni);
                        cmd.Parameters.AddWithValue("@Medio", obj.MedioPago456VG.Trim());
                        cmd.Parameters.AddWithValue("@NumTarj", obj.NumTarjeta.Trim());
                        cmd.Parameters.AddWithValue("@Tit", obj.Titular.Trim());
                        cmd.Parameters.AddWithValue("@FecVenc", obj.FechaVencimiento.Date);
                        cmd.Parameters.AddWithValue("@CVC", obj.CVC.Trim());

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas == 0)
                            throw new Exception($"No se encontró ningún registro de datos de pago para el cliente {dni}.");
                    }

                    tx.Commit();
                }

                resultado.resultado = true;
                resultado.entidad = obj;
                resultado.mensaje = "Datos de pago actualizados correctamente.";
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
            }
            finally
            {
                db.Connection.Close();
            }

            return resultado;
        }

        public Resultado_456VG<BEDatosPago_456VG> eliminarEntidad456VG(BEDatosPago_456VG obj)
        {
            throw new NotImplementedException();
        }

        public List<BEDatosPago_456VG> leerEntidades456VG()
        {
            var lista = new List<BEDatosPago_456VG>();

            const string sql =
                "USE EnviosYA_456VG; " +
                "SELECT dni_cliente_456VG, medio_pago_456VG, numtarjeta_456VG, titular_456VG, fechavencimiento_456VG, cvc_456VG " +
                "FROM DatosPago_456VG;";

            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string dniCli = reader.GetString(reader.GetOrdinal("dni_cliente_456VG"));
                        string medioPago = reader.GetString(reader.GetOrdinal("medio_pago_456VG"));
                        string numTarjeta = reader.GetString(reader.GetOrdinal("numtarjeta_456VG"));
                        string titular = reader.GetString(reader.GetOrdinal("titular_456VG"));
                        DateTime fechaVenc = reader.GetDateTime(reader.GetOrdinal("fechavencimiento_456VG"));
                        string cvc = reader.GetString(reader.GetOrdinal("cvc_456VG"));

                        var datosPago = new BEDatosPago_456VG(
                            cliente: null,       // Si necesitas el BECliente, asínalo después en la BLL
                            mediopago: medioPago,
                            numtarj: numTarjeta,
                            titu: titular,
                            fvenc: fechaVenc,
                            cvc: cvc
                        );
                        lista.Add(datosPago);
                    }
                }
            }
            catch
            {
                // En caso de error, devolvemos lista vacía
            }
            finally
            {
                db.Desconectar456VG();
            }

            return lista;
        }

        public BEDatosPago_456VG LeerPorDni(string dni)
        {
            BEDatosPago_456VG resultado = null;

            const string sql =
                "USE EnviosYA_456VG; " +
                "SELECT medio_pago_456VG, numtarjeta_456VG, titular_456VG, fechavencimiento_456VG, cvc_456VG " +
                "FROM DatosPago_456VG " +
                "WHERE dni_cliente_456VG = @Dni;";

            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Dni", dni);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string medioPago = reader.GetString(reader.GetOrdinal("medio_pago_456VG"));
                            string numTarjeta = reader.GetString(reader.GetOrdinal("numtarjeta_456VG"));
                            string titular = reader.GetString(reader.GetOrdinal("titular_456VG"));
                            DateTime fechaVenc = reader.GetDateTime(reader.GetOrdinal("fechavencimiento_456VG"));
                            string cvc = reader.GetString(reader.GetOrdinal("cvc_456VG"));

                            resultado = new BEDatosPago_456VG(
                                cliente: null,      // La BLL asignará BECliente_456VG si lo desea
                                mediopago: medioPago,
                                numtarj: numTarjeta,
                                titu: titular,
                                fvenc: fechaVenc,
                                cvc: cvc
                            );
                        }
                    }
                }
            }
            catch
            {
                resultado = null;
            }
            finally
            {
                db.Desconectar456VG();
            }

            return resultado;
        }
    }
}
