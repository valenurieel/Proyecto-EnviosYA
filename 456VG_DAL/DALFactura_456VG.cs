using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace _456VG_DAL
{
    public class DALFactura_456VG : ICrud_456VG<BEFactura_456VG>
    {
        BasedeDatos_456VG db { get; }
        HashSHA256_456VG hasher { get; set; }
        public DALFactura_456VG()
        {
            db = new BasedeDatos_456VG();
            hasher = new HashSHA256_456VG();
        }
        public Resultado_456VG<BEFactura_456VG> actualizarEntidad456VG(BEFactura_456VG obj)
        {
            throw new NotImplementedException();
        }
        public Resultado_456VG<BEFactura_456VG> crearEntidad456VG(BEFactura_456VG obj)
        {
            var resultado = new Resultado_456VG<BEFactura_456VG>();
            try
            {
                if (obj == null)
                    throw new ArgumentException("La factura no puede ser nula.");
                if (obj.Envio == null || string.IsNullOrWhiteSpace(obj.Envio.CodEnvio456VG))
                    throw new ArgumentException("La factura debe tener un envío válido con código.");
                if (string.IsNullOrWhiteSpace(obj.CodFactura456VG))
                    throw new ArgumentException("El código de factura no puede ser nulo o vacío.");
                if (obj.Envio.Cliente == null || string.IsNullOrWhiteSpace(obj.Envio.Cliente.DNI456VG))
                    throw new ArgumentException("La factura debe tener un cliente válido con DNI.");
                DateTime fecha = obj.FechaEmision456VG;
                TimeSpan hora = obj.HoraEmision456VG;
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "INSERT INTO Facturas_456VG " +
                        "  (codfactura_456VG, codenvio_456VG, dni_cli_456VG, fechaemision_456VG, horaemision_456VG) " +
                        "VALUES (@CodFactura, @CodEnvio, @DniCli, @Fecha, @Hora);";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@CodFactura", obj.CodFactura456VG);
                        cmd.Parameters.AddWithValue("@CodEnvio", obj.Envio.CodEnvio456VG);
                        cmd.Parameters.AddWithValue("@DniCli", obj.Envio.Cliente.DNI456VG);
                        cmd.Parameters.AddWithValue("@Fecha", fecha);
                        cmd.Parameters.AddWithValue("@Hora", hora);
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                resultado.resultado = true;
                resultado.entidad = obj;
                resultado.mensaje = "Factura creada correctamente.";
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
        public Resultado_456VG<BEFactura_456VG> eliminarEntidad456VG(BEFactura_456VG obj)
        {
            throw new NotImplementedException();
        }
        public List<BEFactura_456VG> leerEntidades456VG()
        {
            var listaFacturas = new List<BEFactura_456VG>();
            string sqlFacturas =
                "USE EnviosYA_456VG; " +
                "SELECT " +
                "  f.codfactura_456VG, " +
                "  f.codenvio_456VG, " +
                "  f.dni_cli_456VG AS dniCliEnv, " +
                "  f.fechaemision_456VG, " +
                "  f.horaemision_456VG, " +
                "  e.dni_dest_456VG, " +
                "  e.nombre_dest_456VG, " +
                "  e.apellido_dest_456VG, " +
                "  e.telefono_dest_456VG, " +
                "  e.provincia_456VG, " +
                "  e.localidad_456VG, " +
                "  e.domicilio_456VG, " +
                "  e.codpostal_456VG, " +
                "  e.tipoenvio_456VG, " +
                "  e.importe_456VG, " +
                "  e.pagado_456VG, " +
                "  cEnv.nombre_456VG AS cliNombreEnv, " +
                "  cEnv.apellido_456VG AS cliApellidoEnv, " +
                "  cEnv.telefono_456VG AS cliTelefonoEnv, " +
                "  cEnv.domicilio_456VG AS cliDomicilioEnv, " +
                "  cEnv.fechanacimiento_456VG AS cliFNEnv, " +
                "  cEnv.activo_456VG AS cliActivoEnv " +
                "FROM Facturas_456VG f " +
                "JOIN Envios_456VG e ON f.codenvio_456VG = e.codenvio_456VG " +
                "JOIN Clientes_456VG cEnv ON e.dni_cli_456VG = cEnv.dni_456VG;";
            try
            {
                if (!db.Conectar456VG())
                    throw new Exception("Error al conectar a la base de datos.");
                using (var cmdF = new SqlCommand(sqlFacturas, db.Connection))
                using (var readerF = cmdF.ExecuteReader())
                {
                    while (readerF.Read())
                    {
                        string codFactura = readerF.GetString(readerF.GetOrdinal("codfactura_456VG"));
                        string codEnvio = readerF.GetString(readerF.GetOrdinal("codenvio_456VG"));
                        string dniCliEnv = readerF.GetString(readerF.GetOrdinal("dniCliEnv"));
                        DateTime fechaF = readerF.GetDateTime(readerF.GetOrdinal("fechaemision_456VG"));
                        TimeSpan horaF = readerF.GetTimeSpan(readerF.GetOrdinal("horaemision_456VG"));
                        string dniDest = readerF.GetString(readerF.GetOrdinal("dni_dest_456VG"));
                        string nomDest = readerF.GetString(readerF.GetOrdinal("nombre_dest_456VG"));
                        string apeDest = readerF.GetString(readerF.GetOrdinal("apellido_dest_456VG"));
                        string telDest = readerF.GetString(readerF.GetOrdinal("telefono_dest_456VG"));
                        string prov = readerF.GetString(readerF.GetOrdinal("provincia_456VG"));
                        string loc = readerF.GetString(readerF.GetOrdinal("localidad_456VG"));
                        string domEnv = readerF.GetString(readerF.GetOrdinal("domicilio_456VG"));
                        float codPostal = (float)readerF.GetDouble(readerF.GetOrdinal("codpostal_456VG"));
                        string tipoEnv = readerF.GetString(readerF.GetOrdinal("tipoenvio_456VG"));
                        bool pagado = readerF.GetBoolean(readerF.GetOrdinal("pagado_456VG"));
                        string cliNomEnv = readerF.GetString(readerF.GetOrdinal("cliNombreEnv"));
                        string cliApeEnv = readerF.GetString(readerF.GetOrdinal("cliApellidoEnv"));
                        string cliTelEnv = readerF.GetString(readerF.GetOrdinal("cliTelefonoEnv"));
                        string cliDomEnv = readerF.GetString(readerF.GetOrdinal("cliDomicilioEnv"));
                        DateTime cliFNEnv = readerF.GetDateTime(readerF.GetOrdinal("cliFNEnv"));
                        bool cliActivoEnv = readerF.GetBoolean(readerF.GetOrdinal("cliActivoEnv"));
                        var clienteEnvio = new BECliente_456VG(
                            dniCliEnv,
                            cliNomEnv,
                            cliApeEnv,
                            cliTelEnv,
                            cliDomEnv,
                            cliFNEnv,
                            cliActivoEnv
                        );
                        var paquetesEnvio = new List<BEPaquete_456VG>();
                        string sqlPaquetes =
                            "USE EnviosYA_456VG; " +
                            "SELECT " +
                            "  p.peso_456VG, " +
                            "  p.ancho_456VG, " +
                            "  p.alto_456VG, " +
                            "  p.largo_456VG, " +
                            "  p.enviado_456VG " +
                            "FROM Paquetes_456VG p " +
                            "JOIN EnviosPaquetes_456VG ep ON p.codpaq_456VG = ep.codpaq_456VG " +
                            "WHERE ep.codenvio_456VG = @CodEnvio;";
                        using (var cmdP = new SqlCommand(sqlPaquetes, db.Connection))
                        {
                            cmdP.Parameters.AddWithValue("@CodEnvio", codEnvio);

                            using (var readerP = cmdP.ExecuteReader())
                            {
                                while (readerP.Read())
                                {
                                    float peso = (float)readerP.GetDouble(readerP.GetOrdinal("peso_456VG"));
                                    float ancho = (float)readerP.GetDouble(readerP.GetOrdinal("ancho_456VG"));
                                    float alto = (float)readerP.GetDouble(readerP.GetOrdinal("alto_456VG"));
                                    float largo = (float)readerP.GetDouble(readerP.GetOrdinal("largo_456VG"));
                                    bool enviadoPaq = readerP.GetBoolean(readerP.GetOrdinal("enviado_456VG"));
                                    var paquete = new BEPaquete_456VG(
                                        clienteEnvio,
                                        peso,
                                        ancho,
                                        largo,
                                        alto,
                                        enviadoPaq
                                    );
                                    paquetesEnvio.Add(paquete);
                                }
                            }
                        }
                        var envio = new BEEnvío_456VG(
                            clienteEnvio,
                            paquetesEnvio,
                            dniDest,
                            nomDest,
                            apeDest,
                            telDest,
                            codPostal,
                            domEnv,
                            loc,
                            prov,
                            tipoEnv,
                            pagado
                        );
                        var factura = new BEFactura_456VG(envio, fechaF.Add(horaF));
                        listaFacturas.Add(factura);
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

            return listaFacturas;
        }
    }
}
