using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _456VG_DAL
{
    public class DALPermisoComp_456VG
    {
        BasedeDatos_456VG db = new BasedeDatos_456VG();

        // todos los permisos del sistema
        public List<BEPerfil_456VG> CargarCBPermisos456VG()
        {
            var lista = new List<BEPerfil_456VG>();
            const string sql = @"
USE EnviosYA_456VG;
SELECT id_permiso_456VG,nombre_456VG,nombre_formulario_456VG
  FROM PermisosComp_456VG
 WHERE isPerfil_456VG = 0;";
            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        lista.Add(new BEPerfil_456VG(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.IsDBNull(2) ? null : reader.GetString(2),
                            is_perfil: false
                        ));
            }
            finally { db.Desconectar456VG(); }
            return lista;
        }

        // relaciones padre→hijo
        public List<BEPermisoComp_456VG> ListaPermisos456VG()
        {
            var lista = new List<BEPermisoComp_456VG>();
            const string sql = @"
USE EnviosYA_456VG;
SELECT parent_id_456VG AS id_permisopadre,
       id_permiso_456VG AS id_permisohijo
  FROM PermisosComp_456VG
 WHERE parent_id_456VG IS NOT NULL;";
            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        lista.Add(new BEPermisoComp_456VG(
                            reader.GetInt32(0),
                            reader.GetInt32(1)
                        ));
            }
            finally { db.Desconectar456VG(); }
            return lista;
        }

        // asignar padre a un permiso
        public Resultado_456VG<BEPermisoComp_456VG> aggPermisos456VG(BEPermisoComp_456VG rel)
        {
            var resultado = new Resultado_456VG<BEPermisoComp_456VG>();
            const string sql = @"
USE EnviosYA_456VG;
UPDATE PermisosComp_456VG
   SET parent_id_456VG = @padre
 WHERE id_permiso_456VG = @hijo;";
            try
            {
                db.Conectar456VG();
                using (var tx = db.Connection.BeginTransaction())
                using (var cmd = new SqlCommand(sql, db.Connection, tx))
                {
                    cmd.Parameters.AddWithValue("@padre", rel.id_permisopadre456VG);
                    cmd.Parameters.AddWithValue("@hijo", rel.id_permisohijo456VG);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new Exception("No se encontró el permiso hijo.");
                    tx.Commit();
                }
                resultado.resultado = true;
                resultado.entidad = rel;
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

        // permisos asociados a un perfil concreto
        public List<BEPerfil_456VG> CargarPermisosPorPerfil(int perfilId)
        {
            var lista = new List<BEPerfil_456VG>();
            const string sql = @"
USE EnviosYA_456VG;
SELECT id_permiso_456VG,nombre_456VG,nombre_formulario_456VG
  FROM PermisosComp_456VG
 WHERE isPerfil_456VG = 0
   AND parent_id_456VG = @perfilId;";
            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@perfilId", perfilId);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                            lista.Add(new BEPerfil_456VG(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.IsDBNull(2) ? null : reader.GetString(2),
                                is_perfil: false
                            ));
                }
            }
            finally { db.Desconectar456VG(); }
            return lista;
        }
    }
}
