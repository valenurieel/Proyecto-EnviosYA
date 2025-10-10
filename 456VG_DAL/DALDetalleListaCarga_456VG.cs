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
    public class DALDetalleListaCarga_456VG
    {
        BasedeDatos_456VG db { get; }
        public DALDetalleListaCarga_456VG()
        {
            db = new BasedeDatos_456VG();
        }
        public Resultado_456VG<BEDetalleListaCarga_456VG> InsertarDetalle456VG(BEDetalleListaCarga_456VG det)
        {
            var resultado = new Resultado_456VG<BEDetalleListaCarga_456VG>();
            try
            {
                if (det == null)
                    throw new ArgumentException("El detalle no puede ser nulo.");
                if (det.Lista == null || string.IsNullOrWhiteSpace(det.Lista.CodLista456VG))
                    throw new ArgumentException("El detalle debe estar asociado a una lista válida.");
                if (det.Envio == null || string.IsNullOrWhiteSpace(det.Envio.CodEnvio456VG))
                    throw new ArgumentException("El detalle debe estar asociado a un envío válido.");
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql = @"
                        USE EnviosYA_456VG;
                        INSERT INTO DetalleListaCarga_456VG (
                            coddetallelista_456VG, codlista_456VG, codenvio_456VG,
                            cantpaquetes_456VG, estadocargado_456VG
                        )
                        VALUES (
                            @CodDetalle, @CodLista, @CodEnvio, @CantPaq, @Estado
                        );";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@CodDetalle", det.CodDetListaCarga456VG);
                        cmd.Parameters.AddWithValue("@CodLista", det.Lista.CodLista456VG);
                        cmd.Parameters.AddWithValue("@CodEnvio", det.Envio.CodEnvio456VG);
                        cmd.Parameters.AddWithValue("@CantPaq", det.CantPaquetes456VG);
                        cmd.Parameters.AddWithValue("@Estado", det.EstadoCargado);
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
                resultado.resultado = true;
                resultado.entidad = det;
                resultado.mensaje = "Detalle insertado correctamente.";
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = $"Error al insertar el detalle: {ex.Message}";
            }
            finally
            {
                db.Connection.Close();
            }
            return resultado;
        }
        public List<BEDetalleListaCarga_456VG> ObtenerDetalles456VG()
        {
            var detalles = new List<BEDetalleListaCarga_456VG>();

            try
            {
                const string sql = @"
            USE EnviosYA_456VG;
            SELECT 
                d.coddetallelista_456VG, d.codlista_456VG, d.codenvio_456VG,
                d.cantpaquetes_456VG, d.estadocargado_456VG,

                -- Datos de Lista
                l.fechacreacion_456VG, l.tipozona_456VG, l.tipoenvio_456VG,
                l.cantenvios_456VG, l.cantpaquetes_456VG AS lista_paquetes,
                l.pesototal_456VG, l.volumentotal_456VG,
                l.fechasalida_456VG, l.estadolista_456VG,
                c.dni_chofer_456VG, c.nombre_456VG AS chofer_nombre, c.apellido_456VG AS chofer_apellido,
                t.patente_456VG, t.marca_456VG,

                -- Datos del Envío
                e.dni_cli_456VG, e.dni_dest_456VG, e.nombre_dest_456VG, e.apellido_dest_456VG,
                e.telefono_dest_456VG, e.provincia_456VG, e.localidad_456VG, e.domicilio_456VG,
                e.codpostal_456VG, e.tipoenvio_456VG AS envio_tipo, e.importe_456VG,
                e.pagado_456VG, e.estadoenvio_456VG, e.fechaentrega_456VG,
                cli.nombre_456VG AS cli_nombre, cli.apellido_456VG AS cli_apellido, cli.telefono_456VG AS cli_tel,
                cli.domicilio_456VG AS cli_dom, cli.fechanacimiento_456VG AS cli_fn, cli.activo_456VG AS cli_activo

            FROM DetalleListaCarga_456VG d
            JOIN ListaCarga_456VG l ON d.codlista_456VG = l.codlista_456VG
            JOIN Choferes_456VG c ON l.dni_chofer_456VG = c.dni_chofer_456VG
            JOIN Transportes_456VG t ON l.patente_transporte_456VG = t.patente_456VG
            JOIN Envios_456VG e ON d.codenvio_456VG = e.codenvio_456VG
            JOIN Clientes_456VG cli ON e.dni_cli_456VG = cli.dni_456VG;";

                db.Conectar456VG();

                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // === Chofer
                        var chofer = new BEChofer_456VG(
                            reader.GetString(reader.GetOrdinal("dni_chofer_456VG")),
                            reader.GetString(reader.GetOrdinal("chofer_nombre")),
                            reader.GetString(reader.GetOrdinal("chofer_apellido")),
                            "", true, DateTime.Now, DateTime.Now, true, true
                        );

                        // === Transporte
                        var transporte = new BETransporte_456VG(
                            reader.GetString(reader.GetOrdinal("patente_456VG")),
                            reader.GetString(reader.GetOrdinal("marca_456VG")),
                            0, 0, 0, true, true
                        );

                        // === Cliente
                        var cliente = new BECliente_456VG(
                            reader.GetString(reader.GetOrdinal("dni_cli_456VG")),
                            reader.GetString(reader.GetOrdinal("cli_nombre")),
                            reader.GetString(reader.GetOrdinal("cli_apellido")),
                            reader.GetString(reader.GetOrdinal("cli_tel")),
                            reader.GetString(reader.GetOrdinal("cli_dom")),
                            reader.GetDateTime(reader.GetOrdinal("cli_fn")),
                            reader.GetBoolean(reader.GetOrdinal("cli_activo"))
                        );

                        // === Lista
                        var lista = new BEListaCarga_456VG(
                            reader.GetString(reader.GetOrdinal("codlista_456VG")),
                            reader.GetDateTime(reader.GetOrdinal("fechacreacion_456VG")),
                            reader.GetString(reader.GetOrdinal("tipozona_456VG")),
                            reader.GetString(reader.GetOrdinal("tipoenvio_456VG")),
                            reader.GetInt32(reader.GetOrdinal("cantenvios_456VG")),
                            reader.GetInt32(reader.GetOrdinal("lista_paquetes")),
                            (float)reader.GetDouble(reader.GetOrdinal("pesototal_456VG")),
                            (float)reader.GetDouble(reader.GetOrdinal("volumentotal_456VG")),
                            chofer, transporte,
                            reader.GetDateTime(reader.GetOrdinal("fechasalida_456VG")),
                            reader.GetString(reader.GetOrdinal("estadolista_456VG"))
                        );

                        // === Envío
                        var envio = new BEEnvío_456VG(
                            reader.GetString(reader.GetOrdinal("codenvio_456VG")),
                            cliente,
                            new List<BEPaquete_456VG>(),
                            reader.GetString(reader.GetOrdinal("dni_dest_456VG")),
                            reader.GetString(reader.GetOrdinal("nombre_dest_456VG")),
                            reader.GetString(reader.GetOrdinal("apellido_dest_456VG")),
                            reader.GetString(reader.GetOrdinal("telefono_dest_456VG")),
                            (float)reader.GetDouble(reader.GetOrdinal("codpostal_456VG")),
                            reader.GetString(reader.GetOrdinal("domicilio_456VG")),
                            reader.GetString(reader.GetOrdinal("localidad_456VG")),
                            reader.GetString(reader.GetOrdinal("provincia_456VG")),
                            reader.GetString(reader.GetOrdinal("envio_tipo")),
                            reader.GetBoolean(reader.GetOrdinal("pagado_456VG")),
                            reader.GetDecimal(reader.GetOrdinal("importe_456VG")),
                            reader.GetString(reader.GetOrdinal("estadoenvio_456VG")),
                            reader.IsDBNull(reader.GetOrdinal("fechaentrega_456VG"))
                                ? DateTime.MinValue
                                : reader.GetDateTime(reader.GetOrdinal("fechaentrega_456VG"))
                        );

                        // === Detalle
                        var detalle = new BEDetalleListaCarga_456VG(
                            reader.GetString(reader.GetOrdinal("coddetallelista_456VG")),
                            lista,
                            envio,
                            new List<BEPaquete_456VG>(),
                            reader.GetInt32(reader.GetOrdinal("cantpaquetes_456VG")),
                            reader.GetString(reader.GetOrdinal("estadocargado_456VG"))
                        );

                        // === Cargar Paquetes asociados ===
                        string sqlPaq = @"
                    USE EnviosYA_456VG;
                    SELECT 
                        p.codpaq_456VG,
                        p.dni_456VG,
                        p.peso_456VG,
                        p.ancho_456VG,
                        p.largo_456VG,
                        p.alto_456VG,
                        p.enviado_456VG,
                        c.nombre_456VG AS cli_nom,
                        c.apellido_456VG AS cli_ape,
                        c.telefono_456VG AS cli_tel,
                        c.domicilio_456VG AS cli_dom,
                        c.fechanacimiento_456VG AS cli_fn,
                        c.activo_456VG AS cli_activo
                    FROM Paquetes_456VG p
                    JOIN Clientes_456VG c ON p.dni_456VG = c.dni_456VG
                    JOIN EnviosPaquetes_456VG ep ON p.codpaq_456VG = ep.codpaq_456VG
                    WHERE ep.codenvio_456VG = @CodEnvio;";

                        using (var cmdP = new SqlCommand(sqlPaq, db.Connection))
                        {
                            cmdP.Parameters.AddWithValue("@CodEnvio", envio.CodEnvio456VG);
                            using (var readerP = cmdP.ExecuteReader())
                            {
                                while (readerP.Read())
                                {
                                    var cliPaq = new BECliente_456VG(
                                        readerP.GetString(readerP.GetOrdinal("dni_456VG")),
                                        readerP.GetString(readerP.GetOrdinal("cli_nom")),
                                        readerP.GetString(readerP.GetOrdinal("cli_ape")),
                                        readerP.GetString(readerP.GetOrdinal("cli_tel")),
                                        readerP.GetString(readerP.GetOrdinal("cli_dom")),
                                        readerP.GetDateTime(readerP.GetOrdinal("cli_fn")),
                                        readerP.GetBoolean(readerP.GetOrdinal("cli_activo"))
                                    );

                                    var paquete = new BEPaquete_456VG(
                                        readerP.GetString(readerP.GetOrdinal("codpaq_456VG")),
                                        cliPaq,
                                        (float)readerP.GetDouble(readerP.GetOrdinal("peso_456VG")),
                                        (float)readerP.GetDouble(readerP.GetOrdinal("ancho_456VG")),
                                        (float)readerP.GetDouble(readerP.GetOrdinal("largo_456VG")),
                                        (float)readerP.GetDouble(readerP.GetOrdinal("alto_456VG")),
                                        readerP.GetBoolean(readerP.GetOrdinal("enviado_456VG"))
                                    );

                                    detalle.Paquetes.Add(paquete);
                                    envio.Paquetes.Add(paquete);
                                }
                            }
                        }

                        detalles.Add(detalle);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener detalles: {ex.Message}");
            }
            finally
            {
                db.Desconectar456VG();
            }

            return detalles;
        }


        public Resultado_456VG<bool> ActualizarDetalle456VG(BEDetalleListaCarga_456VG det)
        {
            var resultado = new Resultado_456VG<bool>();
            try
            {
                const string sql = @"
                    USE EnviosYA_456VG;
                    UPDATE DetalleListaCarga_456VG
                    SET estadocargado_456VG = @Estado
                    WHERE coddetallelista_456VG = @CodDetalle;";

                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Estado", det.EstadoCargado);
                    cmd.Parameters.AddWithValue("@CodDetalle", det.CodDetListaCarga456VG);
                    cmd.ExecuteNonQuery();
                }

                resultado.resultado = true;
                resultado.entidad = true;
                resultado.mensaje = "Detalle actualizado correctamente.";
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
            }
            finally
            {
                db.Desconectar456VG();
            }
            return resultado;
        }
    }
}
