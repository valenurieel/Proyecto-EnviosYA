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
            throw new Exception();
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
                        "INSERT INTO Envios_456VG " +
                        "(id_paquete_456VG, dni_cli_456VG, apellido_dest_456VG, nombre_dest_456VG, telefono_dest_456VG, " +
                        " dni_dest_456VG, localidad_456VG, provincia_456VG, domicilio_456VG, importe_456VG, pagado_456VG, codpostal_456VG) " +
                        "VALUES (@IdPaq, @DniCli, @ApeDest, @NomDest, @TelDest, @DniDest, @Loc, @Prov, @Dom, @Imp, @Pag, @CP); " +
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

                        obj.id_envio456VG = (int)cmd.ExecuteScalar();
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
            throw new Exception();
        }
        public List<BEEnvío_456VG> leerEntidades456VG()
        {
            throw new Exception();
        }
    }
}
