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
    public class DALPaquete_456VG : ICrud_456VG<BEPaquete_456VG>
    {
        BasedeDatos_456VG db { get; }
        HashSHA256_456VG hasher { get; set; }
        public DALPaquete_456VG()
        {
            db = new BasedeDatos_456VG();
            hasher = new HashSHA256_456VG();
        }
        public Resultado_456VG<BEPaquete_456VG> actualizarEntidad456VG(BEPaquete_456VG obj)
        {
            var resultado = new Resultado_456VG<BEPaquete_456VG>();
            try
            {
                if (obj == null || obj.CodPaq456VG == null)
                    throw new ArgumentException("El paquete o su código no puede ser nulo.");
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "UPDATE Paquetes_456VG " +
                        "SET enviado_456VG = @Enviado " +
                        "WHERE codpaq_456VG = @CodPaq;";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@Enviado", obj.Enviado456VG);
                        cmd.Parameters.AddWithValue("@CodPaq", obj.CodPaq456VG);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas == 0)
                            throw new Exception($"No se encontró ningún paquete con código {obj.CodPaq456VG}.");
                    }
                    tx.Commit();
                }
                resultado.resultado = true;
                resultado.entidad = obj;
                resultado.mensaje = "Paquete marcado como enviado.";
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
        public Resultado_456VG<BEPaquete_456VG> crearEntidad456VG(BEPaquete_456VG obj)
        {
            var resultado = new Resultado_456VG<BEPaquete_456VG>();
            try
            {
                if (obj == null || obj.Cliente == null || string.IsNullOrWhiteSpace(obj.Cliente.DNI456VG))
                    throw new ArgumentException("El paquete y su cliente (con DNI) no pueden ser nulos.");
                if (string.IsNullOrWhiteSpace(obj.CodPaq456VG))
                    throw new Exception("El paquete no tiene asignado un CodPaq.");
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "INSERT INTO Paquetes_456VG " +
                        "  (dni_456VG, peso_456VG, ancho_456VG, alto_456VG, largo_456VG, enviado_456VG, codpaq_456VG) " +
                        "VALUES (@Dni, @Peso, @Ancho, @Alto, @Largo, @Enviado, @CodPaq);";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@Dni", obj.Cliente.DNI456VG);
                        cmd.Parameters.AddWithValue("@Peso", obj.Peso456VG);
                        cmd.Parameters.AddWithValue("@Ancho", obj.Ancho456VG);
                        cmd.Parameters.AddWithValue("@Alto", obj.Alto456VG);
                        cmd.Parameters.AddWithValue("@Largo", obj.Largo456VG);
                        cmd.Parameters.AddWithValue("@Enviado", obj.Enviado456VG);
                        cmd.Parameters.AddWithValue("@CodPaq", obj.CodPaq456VG);
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                resultado.resultado = true;
                resultado.entidad = obj;
                resultado.mensaje = "Paquete creado correctamente.";
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
        public Resultado_456VG<BEPaquete_456VG> eliminarEntidad456VG(BEPaquete_456VG obj)
        {
            throw new NotImplementedException();
        }
        public List<BEPaquete_456VG> leerEntidades456VG()
        {
            var listaPaquetes = new List<BEPaquete_456VG>();

            const string sql =
                "USE EnviosYA_456VG; " +
                "SELECT " +
                "  p.codpaq_456VG, " +
                "  p.dni_456VG AS dniCliente, " +
                "  p.peso_456VG, " +
                "  p.ancho_456VG, " +
                "  p.alto_456VG, " +
                "  p.largo_456VG, " +
                "  p.enviado_456VG, " +
                "  c.nombre_456VG AS cliNombre, " +
                "  c.apellido_456VG AS cliApellido, " +
                "  c.telefono_456VG AS cliTelefono, " +
                "  c.domicilio_456VG AS cliDomicilio, " +
                "  c.fechanacimiento_456VG AS cliFN, " +
                "  c.activo_456VG AS cliActivo " +
                "FROM Paquetes_456VG p " +
                "JOIN Clientes_456VG c ON p.dni_456VG = c.dni_456VG;";

            try
            {
                if (!db.Conectar456VG())
                    throw new Exception("Error al conectar a la base de datos.");

                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string dniCliente = reader.GetString(reader.GetOrdinal("dniCliente"));
                        string cliNombre = reader.GetString(reader.GetOrdinal("cliNombre"));
                        string cliApellido = reader.GetString(reader.GetOrdinal("cliApellido"));
                        string cliTelefono = reader.GetString(reader.GetOrdinal("cliTelefono"));
                        string cliDomicilio = reader.GetString(reader.GetOrdinal("cliDomicilio"));
                        DateTime cliFN = reader.GetDateTime(reader.GetOrdinal("cliFN"));
                        bool cliActivo = reader.GetBoolean(reader.GetOrdinal("cliActivo"));

                        var cliente = new BECliente_456VG(
                            dniCliente,
                            cliNombre,
                            cliApellido,
                            cliTelefono,
                            cliDomicilio,
                            cliFN,
                            cliActivo
                        );
                        string codPaq = reader.GetString(reader.GetOrdinal("codpaq_456VG"));
                        float peso = (float)reader.GetDouble(reader.GetOrdinal("peso_456VG"));
                        float ancho = (float)reader.GetDouble(reader.GetOrdinal("ancho_456VG"));
                        float alto = (float)reader.GetDouble(reader.GetOrdinal("alto_456VG"));
                        float largo = (float)reader.GetDouble(reader.GetOrdinal("largo_456VG"));
                        bool enviadoPaq = reader.GetBoolean(reader.GetOrdinal("enviado_456VG"));
                        var paquete = new BEPaquete_456VG(
                            cliente,
                            peso,
                            ancho,
                            largo,
                            alto,
                            enviadoPaq
                        );
                        paquete.CodPaq456VG = codPaq;
                        listaPaquetes.Add(paquete);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                db.Desconectar456VG();
            }
            return listaPaquetes;
        }
    }
}
