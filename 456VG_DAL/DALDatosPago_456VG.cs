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
            throw new NotImplementedException();
        }
        public BEDatosPago_456VG LeerPorDni(string dni)
        {
            BEDatosPago_456VG resultado = null;
            const string sql =
                "USE EnviosYA_456VG; " +
                "SELECT " +
                "  c.dni_456VG              AS ClienteDNI, " +
                "  c.nombre_456VG           AS ClienteNombre, " +
                "  c.apellido_456VG         AS ClienteApellido, " +
                "  c.telefono_456VG         AS ClienteTelefono, " +
                "  c.domicilio_456VG        AS ClienteDomicilio, " +
                "  c.fechanacimiento_456VG  AS ClienteFechaNacimiento, " +
                "  c.activo_456VG           AS ClienteActivo, " +
                "  dp.medio_pago_456VG      AS MedioPago, " +
                "  dp.numtarjeta_456VG      AS NumTarjeta, " +
                "  dp.titular_456VG         AS Titular, " +
                "  dp.fechavencimiento_456VG AS FechaVencimiento, " +
                "  dp.cvc_456VG             AS CVC " +
                "FROM DatosPago_456VG dp " +
                "JOIN Clientes_456VG c " +
                "  ON c.dni_456VG = dp.dni_cliente_456VG " +
                "WHERE dp.dni_cliente_456VG = @Dni;";
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
                            string dniCliente = reader.GetString(reader.GetOrdinal("ClienteDNI"));
                            string nombreCliente = reader.GetString(reader.GetOrdinal("ClienteNombre"));
                            string apellidoCliente = reader.GetString(reader.GetOrdinal("ClienteApellido"));
                            string telefonoCliente = reader.GetString(reader.GetOrdinal("ClienteTelefono"));
                            string domicilioCliente = reader.GetString(reader.GetOrdinal("ClienteDomicilio"));
                            DateTime fnacCliente = reader.GetDateTime(reader.GetOrdinal("ClienteFechaNacimiento"));
                            bool activoCliente = reader.GetBoolean(reader.GetOrdinal("ClienteActivo"));
                            var cliente = new BECliente_456VG(
                                dni: dniCliente,
                                name: nombreCliente,
                                ape: apellidoCliente,
                                tel: telefonoCliente,
                                dom: domicilioCliente,
                                fechanac: fnacCliente,
                                act: activoCliente
                            );
                            string medioPago = reader.GetString(reader.GetOrdinal("MedioPago"));
                            string numTarj = reader.GetString(reader.GetOrdinal("NumTarjeta"));
                            string titular = reader.GetString(reader.GetOrdinal("Titular"));
                            DateTime fvenc = reader.GetDateTime(reader.GetOrdinal("FechaVencimiento"));
                            string cvc = reader.GetString(reader.GetOrdinal("CVC"));
                            resultado = new BEDatosPago_456VG(
                                cliente: cliente,
                                mediopago: medioPago,
                                numtarj: numTarj,
                                titu: titular,
                                fvenc: fvenc,
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
