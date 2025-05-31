using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _456VG_DAL
{
    public class DALEnvio_456VG: ICrud_456VG<BEEnvío_456VG>
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
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "UPDATE Envios_456VG " +
                        "SET pagado_456VG = @Pagado " +
                        "WHERE id_envio_456VG = @Id;";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@Pagado", obj.Pagado456VG);
                        cmd.Parameters.AddWithValue("@Id", obj.id_envio456VG);
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                resultado.resultado = true;
                resultado.entidad = obj;
                resultado.mensaje = "Envío marcado como Pagado.";
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
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "INSERT INTO Envios_456VG (id_paquete_456VG, dni_cli_456VG, apellido_dest_456VG, nombre_dest_456VG, telefono_dest_456VG, dni_dest_456VG, localidad_456VG, provincia_456VG, domicilio_456VG, importe_456VG, pagado_456VG, codpostal_456VG, tipoenvio_456VG) " +
                        "VALUES (@IdPaq, @DniCli, @ApeDest, @NomDest, @TelDest, @DniDest, @Loc, @Prov, @Dom, @Imp, @Pag, @CP, @Tipo); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@IdPaq", obj.id_paquete456VG);
                        cmd.Parameters.AddWithValue("@DniCli", obj.DNICli456VG);
                        cmd.Parameters.AddWithValue("@ApeDest", obj.ApellidoDest456VG);
                        cmd.Parameters.AddWithValue("@NomDest", obj.NombreDest456VG);
                        cmd.Parameters.AddWithValue("@TelDest", obj.TeléfonoDest456VG);
                        cmd.Parameters.AddWithValue("@DniDest", obj.DNIDest456VG);
                        cmd.Parameters.AddWithValue("@Loc", obj.Localidad456VG);
                        cmd.Parameters.AddWithValue("@Prov", obj.Provincia456VG);
                        cmd.Parameters.AddWithValue("@Dom", obj.Domicilio456VG);
                        cmd.Parameters.AddWithValue("@Imp", obj.Importe456VG);
                        cmd.Parameters.AddWithValue("@Pag", obj.Pagado456VG);
                        cmd.Parameters.AddWithValue("@CP", obj.CodPostal456VG);
                        cmd.Parameters.AddWithValue("@Tipo", obj.tipoenvio456VG);
                        obj.id_envio456VG = (int)cmd.ExecuteScalar();
                    }
                    tx.Commit();
                }
                resultado.resultado = true; resultado.entidad = obj; resultado.mensaje = "Envío creado correctamente.";
            }
            catch (Exception ex)
            {
                resultado.resultado = false; resultado.mensaje = ex.Message;
            }
            finally
            {
                db.Connection.Close();
            }
            return resultado;
        }
        public Resultado_456VG<BEEnvío_456VG> eliminarEntidad456VG(BEEnvío_456VG obj)
        {
            throw new Exception();
        }
        public List<BEEnvío_456VG> leerEntidades456VG() // esto en FACTURA
        {
            var list = new List<BEEnvío_456VG>();
            const string sql = "USE EnviosYA_456VG; SELECT e.id_envio_456VG, e.id_paquete_456VG, e.dni_cli_456VG, e.dni_dest_456VG, e.nombre_dest_456VG, e.apellido_dest_456VG, e.telefono_dest_456VG, e.provincia_456VG, e.localidad_456VG, e.domicilio_456VG, e.codpostal_456VG, e.tipoenvio_456VG, e.importe_456VG, e.pagado_456VG, p.peso_456VG, p.ancho_456VG, p.alto_456VG, p.largo_456VG, p.enviado_456VG, p.codpaq_456VG, c.nombre_456VG AS cliNombre, c.apellido_456VG AS cliApellido, c.telefono_456VG AS cliTelefono, c.domicilio_456VG AS cliDomicilio, c.fechanacimiento_456VG FROM Envios_456VG e JOIN Paquetes_456VG p ON e.id_paquete_456VG = p.id_paquete_456VG JOIN Clientes_456VG c ON e.dni_cli_456VG = c.dni_456VG;";
            try
            {
                if (!db.Conectar456VG()) throw new Exception("Error al conectar BD");
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        int idEnvio = r.GetInt32(r.GetOrdinal("id_envio_456VG")), idPaq = r.GetInt32(r.GetOrdinal("id_paquete_456VG"));
                        string dniCli = r.GetString(r.GetOrdinal("dni_cli_456VG")), dniDest = r.GetString(r.GetOrdinal("dni_dest_456VG")), nomDest = r.GetString(r.GetOrdinal("nombre_dest_456VG")), apeDest = r.GetString(r.GetOrdinal("apellido_dest_456VG")), telDest = r.GetString(r.GetOrdinal("telefono_dest_456VG")), prov = r.GetString(r.GetOrdinal("provincia_456VG")), loc = r.GetString(r.GetOrdinal("localidad_456VG")), dom = r.GetString(r.GetOrdinal("domicilio_456VG")), tipoEnv = r.GetString(r.GetOrdinal("tipoenvio_456VG")), codPaq = r.GetString(r.GetOrdinal("codpaq_456VG")), cliNom = r.GetString(r.GetOrdinal("cliNombre")), cliApe = r.GetString(r.GetOrdinal("cliApellido")), cliTel = r.GetString(r.GetOrdinal("cliTelefono")), cliDom = r.GetString(r.GetOrdinal("cliDomicilio"));
                        float cp = (float)r.GetDouble(r.GetOrdinal("codpostal_456VG")), peso = (float)r.GetDouble(r.GetOrdinal("peso_456VG")), ancho = (float)r.GetDouble(r.GetOrdinal("ancho_456VG")), alto = (float)r.GetDouble(r.GetOrdinal("alto_456VG")), largo = (float)r.GetDouble(r.GetOrdinal("largo_456VG"));
                        bool pagado = r.GetBoolean(r.GetOrdinal("pagado_456VG")), enviado = r.GetBoolean(r.GetOrdinal("enviado_456VG"));
                        DateTime cliFN = r.GetDateTime(r.GetOrdinal("fechanacimiento_456VG"));
                        var paquete = new BEPaquete_456VG(idPaq, dniCli, peso, ancho, largo, alto, enviado) { CodPaq456VG = codPaq, Cliente = new BECliente_456VG(dniCli, cliNom, cliApe, cliTel, cliDom, cliFN) };
                        var envio = new BEEnvío_456VG(idEnvio, idPaq, dniCli, dniDest, nomDest, apeDest, telDest, cp, dom, loc, prov, tipoEnv, r.GetDecimal(r.GetOrdinal("importe_456VG")), pagado) { Paquete = paquete, Cliente = paquete.Cliente };
                        list.Add(envio);
                    }
                }
            }
            catch
            {
                // Opcional: registrar/loguear la excepción
            }
            finally
            {
                db.Desconectar456VG();
            }
            return list;
        }
    }
}
