using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_DAL
{
    public class DALEventoBitacora_456VG
    {
        BasedeDatos_456VG db { get; }
        public DALEventoBitacora_456VG()
        {
            db = new BasedeDatos_456VG();
        }
        public void Insertar456VG(BEEventoBitacora_456VG e)
        {
            const string sql = @"
            INSERT INTO dbo.BitacoraEvento_456VG
            (dni_456VG, Fecha_456VG, Modulo_456VG, Evento_456VG, Criticidad_456VG)
            VALUES (@dni, @fecha, @mod, @evt, @crit);";
            using (var conn = new SqlConnection(BasedeDatos_456VG.conexionDB))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@dni", SqlDbType.VarChar, 20).Value = e.Usuario456VG;
                    cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = e.Fecha456VG;
                    cmd.Parameters.Add("@mod", SqlDbType.VarChar, 50).Value = e.Modulo456VG;
                    cmd.Parameters.Add("@evt", SqlDbType.VarChar, 50).Value = e.Accion456VG;
                    cmd.Parameters.Add("@crit", SqlDbType.Int).Value = e.Criticidad456VG;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<BEEventoBitacora_456VG> Seleccionar456VG(int? criticidad, DateTime? desde, DateTime? hasta, string modulo, string dni, string accion)
        {
            var list = new List<BEEventoBitacora_456VG>();
            const string sql = @"
            SELECT CodBitacora_456VG, dni_456VG, Fecha_456VG, Modulo_456VG, Evento_456VG, Criticidad_456VG
            FROM   dbo.BitacoraEvento_456VG
            WHERE  (@crit  IS NULL OR Criticidad_456VG = @crit)
               AND (@desde IS NULL OR Fecha_456VG >= @desde)
               AND (@hasta IS NULL OR Fecha_456VG <= @hasta)
               AND (@mod   IS NULL OR Modulo_456VG = @mod)
               AND (@dni   IS NULL OR dni_456VG     = @dni)
               AND (@evt   IS NULL OR Evento_456VG  = @evt)
            ORDER BY Fecha_456VG DESC;";
            using (var conn = new SqlConnection(BasedeDatos_456VG.conexionDB))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@crit", SqlDbType.Int).Value = (object)criticidad ?? DBNull.Value;
                    cmd.Parameters.Add("@desde", SqlDbType.DateTime).Value = (object)desde ?? DBNull.Value;
                    cmd.Parameters.Add("@hasta", SqlDbType.DateTime).Value = (object)hasta ?? DBNull.Value;
                    cmd.Parameters.Add("@mod", SqlDbType.VarChar, 50).Value = (object)modulo ?? DBNull.Value;
                    cmd.Parameters.Add("@dni", SqlDbType.VarChar, 20).Value = (object)dni ?? DBNull.Value;
                    cmd.Parameters.Add("@evt", SqlDbType.VarChar, 50).Value = (object)accion ?? DBNull.Value;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            list.Add(new BEEventoBitacora_456VG(
                                cod: rd.GetInt32(0),
                                user: rd.GetString(1),
                                mod: rd.GetString(3),
                                acc: rd.GetString(4),
                                criti: (BEEventoBitacora_456VG.NVCriticidad456VG)rd.GetInt32(5),
                                Fecha: rd.GetDateTime(2)
                            ));
                        }
                    }
                }
            }
            return list;
        }
        public List<string> SelectCmbUsers456VG()
        {
            var r = new List<string>();
            const string sql = "SELECT dni_456VG FROM dbo.Usuario_456VG ORDER BY dni_456VG;";
            using (var conn = new SqlConnection(BasedeDatos_456VG.conexionDB))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) r.Add(rd.GetString(0));
                }
            }
            return r;
        }
    }
}
