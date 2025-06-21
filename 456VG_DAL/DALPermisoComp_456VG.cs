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
        //elimina permiso de perfil
        public Resultado_456VG<int> eliminarRelacion456VG(int idPadre, int idHijo)
        {
            var resultado = new Resultado_456VG<int>();
            const string sql = @"
                USE EnviosYA_456VG;
                DELETE FROM PermisoPermiso_456VG
                 WHERE codpermisopadre_456VG = @idPadre
                   AND codpermisohijo_456VG = @idHijo;
            ";
            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@idPadre", idPadre);
                    cmd.Parameters.AddWithValue("@idHijo", idHijo);
                    int filas = cmd.ExecuteNonQuery();
                    resultado.resultado = filas > 0;
                    if (!resultado.resultado)
                        resultado.mensaje = "No se encontró la relación para eliminar.";
                    else
                        resultado.entidad = idHijo;
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
        //Trae Permisos
        public List<BEPermisoComp_456VG> ListaPermisos456VG()
        {
            var lista = new List<BEPermisoComp_456VG>();
            const string sql = @"
                USE EnviosYA_456VG;
                SELECT 
                    codpermisopadre_456VG,
                    codpermisohijo_456VG
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
                    (codpermisopadre_456VG, codpermisohijo_456VG)
                VALUES
                    (@padre, @hijo);
                ";
            try
            {
                db.Conectar456VG();
                using (var tx = db.Connection.BeginTransaction())
                using (var cmd = new SqlCommand(sql, db.Connection, tx))
                {
                    cmd.Parameters.AddWithValue("@padre", rel.CodPermisoPadre456VG);
                    cmd.Parameters.AddWithValue("@hijo", rel.CodPermisoHijo456VG);

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
