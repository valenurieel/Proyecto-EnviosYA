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
        //Trae Permisos
        public List<BEPermisoComp_456VG> ListaPermisos456VG()
        {
            var lista = new List<BEPermisoComp_456VG>();
            const string sql = @"
                USE EnviosYA_456VG;
                SELECT 
                    id_permisopadre_456VG,
                    id_permisohijo_456VG
                  FROM PermisoPermiso_456VG;
                ";
            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new BEPermisoComp_456VG(
                            reader.GetInt32(0),
                            reader.GetInt32(1)
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
        //Agregar Permisos
        public Resultado_456VG<BEPermisoComp_456VG> aggPermisos456VG(BEPermisoComp_456VG rel)
        {
            var resultado = new Resultado_456VG<BEPermisoComp_456VG>();
            const string sql = @"
                USE EnviosYA_456VG;
                INSERT INTO PermisoPermiso_456VG
                    (id_permisopadre_456VG, id_permisohijo_456VG)
                VALUES
                    (@padre, @hijo);
                ";
            try
            {
                db.Conectar456VG();
                using (var tx = db.Connection.BeginTransaction())
                using (var cmd = new SqlCommand(sql, db.Connection, tx))
                {
                    cmd.Parameters.AddWithValue("@padre", rel.id_permisopadre456VG);
                    cmd.Parameters.AddWithValue("@hijo", rel.id_permisohijo456VG);

                    cmd.ExecuteNonQuery();
                    tx.Commit();

                    resultado.resultado = true;
                    resultado.entidad = rel;
                }
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
