using _456VG_BE;
using _456VG_Servicios;
using iTextSharp.text.pdf.codec.wmf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace _456VG_DAL
{
    public class DALEnvio_456VG : ICrud_456VG<BEEnvío_456VG>
    {
        BasedeDatos_456VG db { get; }
        HashSHA256_456VG hasher { get; set; }
        public DALEnvio_456VG()
        {
            db = new BasedeDatos_456VG();
            hasher = new HashSHA256_456VG();
        }
        public Resultado_456VG<BEEnvío_456VG> actualizarEntidad456VG(BEEnvío_456VG obj)
        {
            var resultado = new Resultado_456VG<BEEnvío_456VG>();
            try
            {
                if (obj == null || string.IsNullOrWhiteSpace(obj.CodEnvio456VG))
                    throw new ArgumentException("El envío o su código no puede ser nulo.");
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "UPDATE Envios_456VG " +
                        "SET pagado_456VG = @Pagado " +
                        "WHERE codenvio_456VG = @CodEnvio;";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@Pagado", obj.Pagado456VG);
                        cmd.Parameters.AddWithValue("@CodEnvio", obj.CodEnvio456VG);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas == 0)
                            throw new Exception($"No se encontró ningún envío con código '{obj.CodEnvio456VG}'.");
                    }
                    tx.Commit();
                }
                resultado.resultado = true;
                resultado.entidad = obj;
                resultado.mensaje = "Envío marcado como pagado.";
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
        public Resultado_456VG<BEEnvío_456VG> crearEntidad456VG(BEEnvío_456VG obj)
        {
            var resultado = new Resultado_456VG<BEEnvío_456VG>();
            try
            {
                if (obj == null)
                    throw new ArgumentException("El envío no puede ser nulo.");
                if (obj.Cliente == null || string.IsNullOrWhiteSpace(obj.Cliente.DNI456VG))
                    throw new ArgumentException("El envío debe tener un cliente válido con DNI.");
                if (obj.Paquetes == null || !obj.Paquetes.Any())
                    throw new ArgumentException("El envío debe contener al menos un paquete.");
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sqlEnvio =
                    "USE EnviosYA_456VG; " +
                    "INSERT INTO Envios_456VG " +
                    " (codenvio_456VG, dni_cli_456VG, dni_dest_456VG, nombre_dest_456VG, apellido_dest_456VG, telefono_dest_456VG, provincia_456VG, localidad_456VG, domicilio_456VG, codpostal_456VG, tipoenvio_456VG, importe_456VG, pagado_456VG, estadoenvio_456VG, fechaentrega_456VG) " +
                    "VALUES (@CodEnvio, @DniCli, @DniDest, @NomDest, @ApeDest, @TelDest, @Prov, @Loc, @Dom, @CP, @Tipo, @Imp, @Pag, @Estado, @FechaEntrega);";
                    using (var cmdEnvio = new SqlCommand(sqlEnvio, db.Connection, tx))
                    {
                        cmdEnvio.Parameters.AddWithValue("@CodEnvio", obj.CodEnvio456VG);
                        cmdEnvio.Parameters.AddWithValue("@DniCli", obj.Cliente.DNI456VG);
                        cmdEnvio.Parameters.AddWithValue("@DniDest", obj.DNIDest456VG);
                        cmdEnvio.Parameters.AddWithValue("@NomDest", obj.NombreDest456VG);
                        cmdEnvio.Parameters.AddWithValue("@ApeDest", obj.ApellidoDest456VG);
                        cmdEnvio.Parameters.AddWithValue("@TelDest", obj.TeléfonoDest456VG);
                        cmdEnvio.Parameters.AddWithValue("@Prov", obj.Provincia456VG);
                        cmdEnvio.Parameters.AddWithValue("@Loc", obj.Localidad456VG);
                        cmdEnvio.Parameters.AddWithValue("@Dom", obj.Domicilio456VG);
                        cmdEnvio.Parameters.AddWithValue("@CP", obj.CodPostal456VG);
                        cmdEnvio.Parameters.AddWithValue("@Tipo", obj.tipoenvio456VG);
                        cmdEnvio.Parameters.AddWithValue("@Imp", obj.Importe456VG);
                        cmdEnvio.Parameters.AddWithValue("@Pag", obj.Pagado456VG);
                        cmdEnvio.Parameters.AddWithValue("@Estado", obj.EstadoEnvio456VG);
                        cmdEnvio.Parameters.AddWithValue("@FechaEntrega",
    obj.FechaEntregaProgramada456VG == default(DateTime) ? DBNull.Value : (object)obj.FechaEntregaProgramada456VG);
                        cmdEnvio.ExecuteNonQuery();
                    }
                    const string sqlEnvioPaq =
                    "USE EnviosYA_456VG; " +
                    "INSERT INTO EnviosPaquetes_456VG (codenvio_456VG, codpaq_456VG) " +
                    "VALUES (@CodEnvio, @CodPaq);";
                    foreach (var paquete in obj.Paquetes)
                    {
                        if (string.IsNullOrWhiteSpace(paquete.CodPaq456VG))
                            throw new ArgumentException("Cada paquete debe tener un CodPaq válido.");
                        using (var cmdEP = new SqlCommand(sqlEnvioPaq, db.Connection, tx))
                        {
                            cmdEP.Parameters.AddWithValue("@CodEnvio", obj.CodEnvio456VG);
                            cmdEP.Parameters.AddWithValue("@CodPaq", paquete.CodPaq456VG);
                            cmdEP.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                resultado.resultado = true;
                resultado.entidad = obj;
                resultado.mensaje = "Envío creado correctamente.";
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
        public Resultado_456VG<BEEnvío_456VG> eliminarEntidad456VG(BEEnvío_456VG obj)
        {
            throw new NotImplementedException();
        }
        public List<BEEnvío_456VG> leerEntidades456VG()
        {
            var listaEnvios = new List<BEEnvío_456VG>();
            string sqlEnvios =
            "USE EnviosYA_456VG; " +
            "SELECT " +
            " e.codenvio_456VG, e.dni_cli_456VG, e.dni_dest_456VG, " +
            " e.nombre_dest_456VG, e.apellido_dest_456VG, e.telefono_dest_456VG, " +
            " e.provincia_456VG, e.localidad_456VG, e.domicilio_456VG, " +
            " e.codpostal_456VG, e.tipoenvio_456VG, e.importe_456VG, e.pagado_456VG, " +
            " e.estadoenvio_456VG, e.fechaentrega_456VG, " +
            " c.nombre_456VG AS cliNombre, c.apellido_456VG AS cliApellido, " +
            " c.telefono_456VG AS cliTelefono, c.domicilio_456VG AS cliDomicilio, " +
            " c.fechanacimiento_456VG AS cliFN, c.activo_456VG AS cliActivo " +
            "FROM Envios_456VG e " +
            "JOIN Clientes_456VG c ON e.dni_cli_456VG = c.dni_456VG;";
            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sqlEnvios, db.Connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string codEnvio = reader.GetString(reader.GetOrdinal("codenvio_456VG"));
                        string dniCli = reader.GetString(reader.GetOrdinal("dni_cli_456VG"));
                        string dniDest = reader.GetString(reader.GetOrdinal("dni_dest_456VG"));
                        string nomDest = reader.GetString(reader.GetOrdinal("nombre_dest_456VG"));
                        string apeDest = reader.GetString(reader.GetOrdinal("apellido_dest_456VG"));
                        string telDest = reader.GetString(reader.GetOrdinal("telefono_dest_456VG"));
                        string prov = reader.GetString(reader.GetOrdinal("provincia_456VG"));
                        string loc = reader.GetString(reader.GetOrdinal("localidad_456VG"));
                        string domEnv = reader.GetString(reader.GetOrdinal("domicilio_456VG"));
                        float cp = (float)reader.GetDouble(reader.GetOrdinal("codpostal_456VG"));
                        string tipoEnv = reader.GetString(reader.GetOrdinal("tipoenvio_456VG"));
                        decimal importe = reader.GetDecimal(reader.GetOrdinal("importe_456VG"));
                        bool pagado = reader.GetBoolean(reader.GetOrdinal("pagado_456VG"));
                        string estado = reader.GetString(reader.GetOrdinal("estadoenvio_456VG"));
                        DateTime fechaEntrega = reader.IsDBNull(reader.GetOrdinal("fechaentrega_456VG"))
                            ? DateTime.MinValue
                            : reader.GetDateTime(reader.GetOrdinal("fechaentrega_456VG"));
                        string cliNom = reader.GetString(reader.GetOrdinal("cliNombre"));
                        string cliApe = reader.GetString(reader.GetOrdinal("cliApellido"));
                        string cliTel = reader.GetString(reader.GetOrdinal("cliTelefono"));
                        string cliDom = reader.GetString(reader.GetOrdinal("cliDomicilio"));
                        DateTime cliFN = reader.GetDateTime(reader.GetOrdinal("cliFN"));
                        bool cliActivo = reader.GetBoolean(reader.GetOrdinal("cliActivo"));
                        var clienteEnvio = new BECliente_456VG(
                            dniCli, cliNom, cliApe, cliTel, cliDom, cliFN, cliActivo
                        );
                        var paquetesEnvio = new List<BEPaquete_456VG>();
                        string sqlPaquetes =
                            "SELECT p.codpaq_456VG, p.peso_456VG, p.ancho_456VG, " +
                            "  p.alto_456VG, p.largo_456VG, p.enviado_456VG " +
                            "FROM Paquetes_456VG p " +
                            "JOIN EnviosPaquetes_456VG ep ON p.codpaq_456VG = ep.codpaq_456VG " +
                            "WHERE ep.codenvio_456VG = @CodEnvio;";
                        using (var cmdPaq = new SqlCommand(sqlPaquetes, db.Connection))
                        {
                            cmdPaq.Parameters.AddWithValue("@CodEnvio", codEnvio);
                            using (var rP = cmdPaq.ExecuteReader())
                            {
                                while (rP.Read())
                                {
                                    string codPaq = rP.GetString(rP.GetOrdinal("codpaq_456VG"));
                                    float peso = (float)rP.GetDouble(rP.GetOrdinal("peso_456VG"));
                                    float ancho = (float)rP.GetDouble(rP.GetOrdinal("ancho_456VG"));
                                    float alto = (float)rP.GetDouble(rP.GetOrdinal("alto_456VG"));
                                    float largo = (float)rP.GetDouble(rP.GetOrdinal("largo_456VG"));
                                    bool enviadoPaq = rP.GetBoolean(rP.GetOrdinal("enviado_456VG"));
                                    var paquete = new BEPaquete_456VG(
                                        codPaq, clienteEnvio, peso, ancho, largo, alto, enviadoPaq
                                    );
                                    paquetesEnvio.Add(paquete);
                                }
                            }
                        }
                        var envio = new BEEnvío_456VG(codEnvio, clienteEnvio, paquetesEnvio, dniDest, nomDest, apeDest, telDest, cp, domEnv, loc, prov, tipoEnv, pagado, importe, estado, fechaEntrega); listaEnvios.Add(envio);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                db.Desconectar456VG();
            }
            return listaEnvios;
        }
        public Resultado_456VG<BEEnvío_456VG> actualizarEstadoEnvio_456VG(string codEnvio, string nuevoEstado)
        {
            var resultado = new Resultado_456VG<BEEnvío_456VG>();
            try
            {
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                    "USE EnviosYA_456VG; " +
                    "UPDATE Envios_456VG SET estadoenvio_456VG = @Estado " +
                    "WHERE codenvio_456VG = @CodEnvio;";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@CodEnvio", codEnvio);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                            throw new Exception("No se encontró el envío.");
                    }
                    tx.Commit();
                }
                resultado.resultado = true;
                resultado.mensaje = "Estado actualizado correctamente.";
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
    }
}
