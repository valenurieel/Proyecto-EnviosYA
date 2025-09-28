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
        public void Insertar456VG(BEEventoBitacora_456VG e)
        {
            const string sql = @"
            INSERT INTO dbo.BitacoraEvento_456VG
            (dni_456VG, Fecha_456VG, Modulo_456VG, Evento_456VG, Criticidad_456VG)
            VALUES (@pDni, @pFecha, @pModulo, @pEvento, @pCrit);";
            using (var conn = new SqlConnection(BasedeDatos_456VG.conexionDB))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.Add("@pDni", SqlDbType.VarChar, 20).Value = e.Usuario456VG;
                cmd.Parameters.Add("@pFecha", SqlDbType.DateTime).Value = e.Fecha456VG;
                cmd.Parameters.Add("@pModulo", SqlDbType.VarChar, 50).Value = e.Modulo456VG;
                cmd.Parameters.Add("@pEvento", SqlDbType.VarChar, 50).Value = e.Accion456VG;
                cmd.Parameters.Add("@pCrit", SqlDbType.Int).Value = e.Criticidad456VG;
                cmd.ExecuteNonQuery();
            }
        }
        public List<BEEventoBitacora_456VG> Seleccionar456VG(int? criticidad, DateTime? desde, DateTime? hasta, string modulo, string dni, string accion)
        {
            var list = new List<BEEventoBitacora_456VG>();
            const string sql = @"
            SELECT CodBitacora_456VG, dni_456VG, Fecha_456VG, Modulo_456VG, Evento_456VG, Criticidad_456VG
            FROM   dbo.BitacoraEvento_456VG
            WHERE  Fecha_456VG >= DATEADD(DAY, -3, GETDATE())
               AND (@pCrit  IS NULL OR Criticidad_456VG = @pCrit)
               AND (@pDesde IS NULL OR Fecha_456VG >= @pDesde)
               AND (@pHasta IS NULL OR Fecha_456VG <= @pHasta)
               AND (@pModulo IS NULL OR Modulo_456VG = @pModulo)
               AND (@pDni   IS NULL OR dni_456VG     = @pDni)
               AND (@pEvento IS NULL OR Evento_456VG  = @pEvento)
            ORDER BY Fecha_456VG DESC;";
            using (var conn = new SqlConnection(BasedeDatos_456VG.conexionDB))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.Add("@pCrit", SqlDbType.Int).Value = (object)criticidad ?? DBNull.Value;
                cmd.Parameters.Add("@pDesde", SqlDbType.DateTime).Value = (object)desde ?? DBNull.Value;
                cmd.Parameters.Add("@pHasta", SqlDbType.DateTime).Value = (object)hasta ?? DBNull.Value;
                cmd.Parameters.Add("@pModulo", SqlDbType.VarChar, 50).Value = (object)modulo ?? DBNull.Value;
                cmd.Parameters.Add("@pDni", SqlDbType.VarChar, 20).Value = (object)dni ?? DBNull.Value;
                cmd.Parameters.Add("@pEvento", SqlDbType.VarChar, 50).Value = (object)accion ?? DBNull.Value;
                using (var rd = cmd.ExecuteReader())
                    while (rd.Read())
                        list.Add(new BEEventoBitacora_456VG(
                            cod: rd.GetInt32(0),
                            user: rd.GetString(1),
                            mod: rd.GetString(3),
                            acc: rd.GetString(4),
                            criti: (BEEventoBitacora_456VG.NVCriticidad456VG)rd.GetInt32(5),
                            Fecha: rd.GetDateTime(2)
                        ));
            }
            return list;
        }
    }
}
