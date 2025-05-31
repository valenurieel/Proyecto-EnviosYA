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
    public class DALFactura_456VG:ICrud_456VG<BEFactura_456VG>
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
            throw new Exception();
        }
        public Resultado_456VG<BEFactura_456VG> crearEntidad456VG(BEFactura_456VG obj)
        {
            var resultado = new Resultado_456VG<BEFactura_456VG>();
            try
            {
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "INSERT INTO Facturas_456VG (id_envio_456VG, id_paquete_456VG, dni_cli_456VG, fechaemision_456VG) " +
                        "VALUES (@IdEnv, @IdPaq, @DniCli, @Fecha); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@IdEnv", obj.id_envio456VG);
                        cmd.Parameters.AddWithValue("@IdPaq", obj.id_paquete456VG);
                        cmd.Parameters.AddWithValue("@DniCli", obj.DNICli456VG);
                        cmd.Parameters.AddWithValue("@Fecha", obj.FechaEmision456VG);
                        obj.id_factura456VG = (int)cmd.ExecuteScalar();
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
            throw new Exception();
        }
        public List<BEFactura_456VG> leerEntidades456VG()
        {
            var list = new List<BEFactura_456VG>();
            const string sql =
                "USE EnviosYA_456VG; " +
                "SELECT " +
                "  f.id_factura_456VG, f.id_envio_456VG, f.id_paquete_456VG, f.dni_cli_456VG, f.fechaemision_456VG, " +
                "  e.dni_dest_456VG, e.nombre_dest_456VG, e.apellido_dest_456VG, e.telefono_dest_456VG, e.provincia_456VG, e.localidad_456VG, e.domicilio_456VG, e.tipoenvio_456VG, e.importe_456VG, e.pagado_456VG, " +
                "  p.peso_456VG, p.ancho_456VG, p.alto_456VG, p.largo_456VG, p.enviado_456VG, p.codpaq_456VG, " +
                "  c.nombre_456VG AS cliNombre, c.apellido_456VG AS cliApellido, c.telefono_456VG AS cliTelefono, c.domicilio_456VG AS cliDomicilio, c.fechanacimiento_456VG " +
                "FROM Facturas_456VG f " +
                "JOIN Envios_456VG  e ON f.id_envio_456VG   = e.id_envio_456VG " +
                "JOIN Paquetes_456VG p ON f.id_paquete_456VG = p.id_paquete_456VG " +
                "JOIN Clientes_456VG c ON f.dni_cli_456VG     = c.dni_456VG;";
            try
            {
                if (!db.Conectar456VG()) throw new Exception("Error al conectar BD");
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                    {
                        int idFact = r.GetInt32(r.GetOrdinal("id_factura_456VG"));
                        int idEnv = r.GetInt32(r.GetOrdinal("id_envio_456VG"));
                        int idPaq = r.GetInt32(r.GetOrdinal("id_paquete_456VG"));
                        string dniCli = r.GetString(r.GetOrdinal("dni_cli_456VG"));
                        DateTime fecha = r.GetDateTime(r.GetOrdinal("fechaemision_456VG"));
                        string dniDest = r.GetString(r.GetOrdinal("dni_dest_456VG"));
                        string nomDest = r.GetString(r.GetOrdinal("nombre_dest_456VG"));
                        string apeDest = r.GetString(r.GetOrdinal("apellido_dest_456VG"));
                        string telDest = r.GetString(r.GetOrdinal("telefono_dest_456VG"));
                        string prov = r.GetString(r.GetOrdinal("provincia_456VG"));
                        string loc = r.GetString(r.GetOrdinal("localidad_456VG"));
                        string dom = r.GetString(r.GetOrdinal("domicilio_456VG"));
                        string tipoEnv = r.GetString(r.GetOrdinal("tipoenvio_456VG"));
                        decimal imp = r.GetDecimal(r.GetOrdinal("importe_456VG"));
                        bool pagado = r.GetBoolean(r.GetOrdinal("pagado_456VG"));
                        float peso = (float)r.GetDouble(r.GetOrdinal("peso_456VG"));
                        float ancho = (float)r.GetDouble(r.GetOrdinal("ancho_456VG"));
                        float alto = (float)r.GetDouble(r.GetOrdinal("alto_456VG"));
                        float largo = (float)r.GetDouble(r.GetOrdinal("largo_456VG"));
                        bool enviado = r.GetBoolean(r.GetOrdinal("enviado_456VG"));
                        string codPaq = r.GetString(r.GetOrdinal("codpaq_456VG"));
                        string cliNom = r.GetString(r.GetOrdinal("cliNombre"));
                        string cliApe = r.GetString(r.GetOrdinal("cliApellido"));
                        string cliTel = r.GetString(r.GetOrdinal("cliTelefono"));
                        string cliDom = r.GetString(r.GetOrdinal("cliDomicilio"));
                        DateTime cliFN = r.GetDateTime(r.GetOrdinal("fechanacimiento_456VG"));
                        var paquete = new BEPaquete_456VG(idPaq, dniCli, peso, ancho, largo, alto, enviado) { CodPaq456VG = codPaq };
                        paquete.Cliente = new BECliente_456VG(dniCli, cliNom, cliApe, cliTel, cliDom, cliFN);
                        var envio = new BEEnvío_456VG(idEnv, idPaq, dniCli, dniDest, nomDest, apeDest, telDest, 0f, dom, loc, prov, tipoEnv, imp, pagado)
                        {
                            Paquete = paquete,
                            Cliente = paquete.Cliente
                        };
                        var factura = new BEFactura_456VG(idFact, idEnv, idPaq, dniCli, fecha) { Envio = envio, Paquete = paquete, Cliente = paquete.Cliente };
                        list.Add(factura);
                    }
            }
            catch
            {
            }
            finally
            {
                db.Desconectar456VG();
            }
            return list;
        }
    }
}
