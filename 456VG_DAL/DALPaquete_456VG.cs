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
    public class DALPaquete_456VG: ICrud_456VG<BEPaquete_456VG>
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
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "UPDATE Paquetes_456VG " +
                        "SET enviado_456VG = @Enviado " +
                        "WHERE id_paquete_456VG = @Id;";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@Enviado", obj.Enviado456VG);
                        cmd.Parameters.AddWithValue("@Id", obj.id_paquete456VG);
                        cmd.ExecuteNonQuery();
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
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "INSERT INTO Paquetes_456VG " +
                        "  (dni_456VG, peso_456VG, ancho_456VG, alto_456VG, largo_456VG, enviado_456VG, codpaq_456VG) " +
                        "VALUES (@Dni, @Peso, @Ancho, @Alto, @Largo, @Enviado, @CodPaq); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@Dni", obj.dnicliente456VG);
                        cmd.Parameters.AddWithValue("@Peso", obj.Peso456VG);
                        cmd.Parameters.AddWithValue("@Ancho", obj.Ancho456VG);
                        cmd.Parameters.AddWithValue("@Alto", obj.Alto456VG);
                        cmd.Parameters.AddWithValue("@Largo", obj.Largo456VG);
                        cmd.Parameters.AddWithValue("@Enviado", obj.Enviado456VG);
                        cmd.Parameters.AddWithValue("@CodPaq", obj.CodPaq456VG);

                        obj.id_paquete456VG = (int)cmd.ExecuteScalar();
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
            throw new Exception();
        }
        public List<BEPaquete_456VG> leerEntidades456VG()
        {
            throw new Exception();
        }
    }
}
