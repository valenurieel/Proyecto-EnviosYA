using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_DAL
{
    public class DALListaCarga_456VG
    {
        BasedeDatos_456VG db { get; }
        public DALListaCarga_456VG()
        {
            db = new BasedeDatos_456VG();
        }
        public Resultado_456VG<BEListaCarga_456VG> InsertarLista456VG(BEListaCarga_456VG lista)
        {
            var resultado = new Resultado_456VG<BEListaCarga_456VG>();
            try
            {
                if (lista == null)
                    throw new ArgumentException("La lista de carga no puede ser nula.");
                if (lista.Chofer == null)
                    throw new ArgumentException("Debe asignarse un chofer a la lista de carga.");
                if (lista.Transporte == null)
                    throw new ArgumentException("Debe asignarse un transporte a la lista de carga.");
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql = @"
                        USE EnviosYA_456VG;
                        INSERT INTO ListaCarga_456VG (
                            codlista_456VG, fechacreacion_456VG, tipozona_456VG,
                            tipoenvio_456VG, cantenvios_456VG, cantpaquetes_456VG,
                            pesototal_456VG, volumentotal_456VG,
                            dni_chofer_456VG, patente_transporte_456VG,
                            fechasalida_456VG, estadolista_456VG
                        )
                        VALUES (
                            @CodLista, @FechaCreacion, @TipoZona, @TipoEnvio,
                            @CantEnvios, @CantPaquetes, @PesoTotal, @VolumenTotal,
                            @DniChofer, @PatenteTransporte, @FechaSalida, @EstadoLista
                        );";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@CodLista", lista.CodLista456VG);
                        cmd.Parameters.AddWithValue("@FechaCreacion", lista.FechaCreacion456VG);
                        cmd.Parameters.AddWithValue("@TipoZona", lista.TipoZona456VG);
                        cmd.Parameters.AddWithValue("@TipoEnvio", lista.TipoEnvio456VG);
                        cmd.Parameters.AddWithValue("@CantEnvios", lista.CantEnvios456VG);
                        cmd.Parameters.AddWithValue("@CantPaquetes", lista.CantPaquetes456VG);
                        cmd.Parameters.AddWithValue("@PesoTotal", lista.PesoTotal456VG.ToString(CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("@VolumenTotal", lista.VolumenTotal456VG.ToString(CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("@DniChofer", lista.Chofer.DNIChofer456VG);
                        cmd.Parameters.AddWithValue("@PatenteTransporte", lista.Transporte.Patente456VG);
                        cmd.Parameters.AddWithValue("@FechaSalida", lista.FechaSalida456VG);
                        cmd.Parameters.AddWithValue("@EstadoLista", lista.EstadoLista456VG);
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }

                resultado.resultado = true;
                resultado.entidad = lista;
                resultado.mensaje = "Lista de carga creada correctamente.";
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = $"Error al crear la lista de carga: {ex.Message}";
            }
            finally
            {
                db.Connection.Close();
            }
            return resultado;
        }
        public List<BEListaCarga_456VG> ObtenerListas456VG()
        {
            var listas = new List<BEListaCarga_456VG>();
            try
            {
                const string sql = @"
                    USE EnviosYA_456VG;
                    SELECT 
                        l.codlista_456VG, l.fechacreacion_456VG, l.tipozona_456VG,
                        l.tipoenvio_456VG, l.cantenvios_456VG, l.cantpaquetes_456VG,
                        l.pesototal_456VG, l.volumentotal_456VG,
                        l.dni_chofer_456VG, c.nombre_456VG AS nomChofer, c.apellido_456VG AS apeChofer,
                        l.patente_transporte_456VG, t.marca_456VG AS marcaTrans,
                        l.fechasalida_456VG, l.estadolista_456VG
                    FROM ListaCarga_456VG l
                    JOIN Choferes_456VG c ON l.dni_chofer_456VG = c.dni_chofer_456VG
                    JOIN Transportes_456VG t ON l.patente_transporte_456VG = t.patente_456VG;";

                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var chofer = new BEChofer_456VG(
                            reader.GetString(reader.GetOrdinal("dni_chofer_456VG")),
                            reader.GetString(reader.GetOrdinal("nomChofer")),
                            reader.GetString(reader.GetOrdinal("apeChofer")),
                            "", true, DateTime.Now, DateTime.Now, true, true
                        );
                        var transporte = new BETransporte_456VG(
                            reader.GetString(reader.GetOrdinal("patente_transporte_456VG")),
                            reader.GetString(reader.GetOrdinal("marcaTrans")),
                            0, 0, 0, true, true
                        );
                        var lista = new BEListaCarga_456VG(
                            reader.GetString(reader.GetOrdinal("codlista_456VG")),
                            reader.GetDateTime(reader.GetOrdinal("fechacreacion_456VG")),
                            reader.GetString(reader.GetOrdinal("tipozona_456VG")),
                            reader.GetString(reader.GetOrdinal("tipoenvio_456VG")),
                            reader.GetInt32(reader.GetOrdinal("cantenvios_456VG")),
                            reader.GetInt32(reader.GetOrdinal("cantpaquetes_456VG")),
                            (float)reader.GetDouble(reader.GetOrdinal("pesototal_456VG")),
                            (float)reader.GetDouble(reader.GetOrdinal("volumentotal_456VG")),
                            chofer, transporte,
                            reader.GetDateTime(reader.GetOrdinal("fechasalida_456VG")),
                            reader.GetString(reader.GetOrdinal("estadolista_456VG"))
                        );
                        listas.Add(lista);
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
            return listas;
        }
        public Resultado_456VG<bool> ActualizarLista456VG(BEListaCarga_456VG lista)
        {
            var resultado = new Resultado_456VG<bool>();
            try
            {
                const string sql = @"
                    USE EnviosYA_456VG;
                    UPDATE ListaCarga_456VG
                    SET 
                        cantenvios_456VG = @CantEnvios,
                        cantpaquetes_456VG = @CantPaquetes,
                        pesototal_456VG = @PesoTotal,
                        volumentotal_456VG = @VolumenTotal,
                        estadolista_456VG = @EstadoLista
                    WHERE codlista_456VG = @CodLista;";

                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@CantEnvios", lista.CantEnvios456VG);
                    cmd.Parameters.AddWithValue("@CantPaquetes", lista.CantPaquetes456VG);
                    cmd.Parameters.AddWithValue("@PesoTotal", lista.PesoTotal456VG.ToString(CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("@VolumenTotal", lista.VolumenTotal456VG.ToString(CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("@EstadoLista", lista.EstadoLista456VG);
                    cmd.Parameters.AddWithValue("@CodLista", lista.CodLista456VG);
                    cmd.ExecuteNonQuery();
                }

                resultado.resultado = true;
                resultado.entidad = true;
                resultado.mensaje = "Lista actualizada correctamente.";
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
