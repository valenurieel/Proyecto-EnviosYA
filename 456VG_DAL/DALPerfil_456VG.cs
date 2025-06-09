using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _456VG_DAL
{
    public class DALPerfil_456VG
    {
        BasedeDatos_456VG db = new BasedeDatos_456VG();

        public Resultado_456VG<BEPerfil_456VG> aggPerfil456VG(BEPerfil_456VG perfil, int? parentId = null)
        {
            var resultado = new Resultado_456VG<BEPerfil_456VG>();
            const string sql = @"
                USE EnviosYA_456VG;
                INSERT INTO PermisosComp_456VG
                    (nombre_456VG, nombre_formulario_456VG, isPerfil_456VG, parent_id_456VG)
                VALUES
                    (@nombre, @formulario, @isPerfil, @parentId);";
            try
            {
                db.Conectar456VG();
                using (var tx = db.Connection.BeginTransaction())
                using (var cmd = new SqlCommand(sql, db.Connection, tx))
                {
                    cmd.Parameters.AddWithValue("@nombre", perfil.nombre456VG);
                    cmd.Parameters.AddWithValue("@formulario", (object)perfil.permiso456VG ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@isPerfil", perfil.is_perfil456VG);
                    cmd.Parameters.AddWithValue("@parentId", parentId.HasValue ? (object)parentId.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                    tx.Commit();
                }
                resultado.resultado = true;
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

        public List<BEPerfil_456VG> CargarCBPerfil456VG()
        {
            var lista = new List<BEPerfil_456VG>();
            const string sql = @"
                USE EnviosYA_456VG;
                SELECT id_permiso_456VG, nombre_456VG
                  FROM PermisosComp_456VG
                 WHERE isPerfil_456VG = 1;";
            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new BEPerfil_456VG(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            permiso: null,
                            is_perfil: true
                        ));
                    }
                }
            }
            finally
            {
                db.Desconectar456VG();
            }
            return lista;
        }

        public List<BEPerfil_456VG> CargarCBPermisos456VG()
        {
            var lista = new List<BEPerfil_456VG>();
            const string sql = @"
                    USE EnviosYA_456VG;
                    SELECT id_permiso_456VG, nombre_456VG, nombre_formulario_456VG
                      FROM PermisosComp_456VG
                     WHERE isPerfil_456VG = 0;";
            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new BEPerfil_456VG(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.IsDBNull(2)
                                ? null
                                : reader.GetString(2),
                            is_perfil: false
                        ));
                    }
                }
            }
            finally
            {
                db.Desconectar456VG();
            }
            return lista;
        }
    }
}
