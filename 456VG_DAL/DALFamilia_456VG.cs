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
    public class DALFamilia_456VG: ICrud_456VG<BEFamilia_456VG>
    {
        BasedeDatos_456VG db { get; }
        HashSHA256_456VG hasher { get; set; }
        public DALFamilia_456VG()
        {
            db = new BasedeDatos_456VG();
            hasher = new HashSHA256_456VG();
        }
        //Lee todas las Familias.
        public List<BEFamilia_456VG> leerEntidades456VG()
        {
            var lista = new List<BEFamilia_456VG>();
            const string sql = @"
        SELECT id_permiso_456VG, nombre_456VG
          FROM PermisosComp_456VG
         WHERE isPerfil_456VG = 0
           AND nombre_formulario_456VG IS NULL;";
            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        lista.Add(new BEFamilia_456VG(
                            reader.GetInt32(0),
                            reader.GetString(1)
                        ));
            }
            finally
            {
                db.Desconectar456VG();
            }

            return lista;
        }
        //Crea Familia
        public Resultado_456VG<BEFamilia_456VG> crearEntidad456VG(BEFamilia_456VG be)
        {
            var resultado = new Resultado_456VG<BEFamilia_456VG>();
            var yaExiste = leerEntidades456VG()
                .Any(f => f.nombre456VG.Equals(be.nombre456VG, StringComparison.OrdinalIgnoreCase));
            if (yaExiste)
            {
                resultado.resultado = false;
                resultado.mensaje = "Ya existe una familia con ese nombre.";
                return resultado;
            }
            const string sql = @"
        INSERT INTO PermisosComp_456VG
            (nombre_456VG, nombre_formulario_456VG, isPerfil_456VG)
        VALUES
            (@nombre, NULL, 0);
        SELECT SCOPE_IDENTITY();";
            try
            {
                db.Conectar456VG();
                using (var tx = db.Connection.BeginTransaction())
                using (var cmd = new SqlCommand(sql, db.Connection, tx))
                {
                    cmd.Parameters.AddWithValue("@nombre", be.nombre456VG);
                    var id = Convert.ToInt32(cmd.ExecuteScalar());
                    tx.Commit();
                    be.id_permiso456VG = id;
                    resultado.resultado = true;
                    resultado.entidad = be;
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
        public Resultado_456VG<BEFamilia_456VG> actualizarEntidad456VG(BEFamilia_456VG be)
        {
            throw new Exception();
        }
        //Elimina Familia y Permisos asociados
        public Resultado_456VG<BEFamilia_456VG> eliminarEntidad456VG(BEFamilia_456VG be)
        {
            var resultado = new Resultado_456VG<BEFamilia_456VG>();
            const string verificarUsoSql = @"
        SELECT COUNT(*) 
          FROM PermisoPermiso_456VG 
         WHERE id_permisohijo_456VG = @id;";
            const string deleteSql = @"
        DELETE FROM PermisoPermiso_456VG
         WHERE id_permisopadre_456VG = @id;

        DELETE FROM PermisosComp_456VG
         WHERE id_permiso_456VG = @id AND isPerfil_456VG = 0;
    ";
            try
            {
                db.Conectar456VG();
                using (var tx = db.Connection.BeginTransaction())
                {
                    using (var verificarCmd = new SqlCommand(verificarUsoSql, db.Connection, tx))
                    {
                        verificarCmd.Parameters.AddWithValue("@id", be.id_permiso456VG);
                        int usos = (int)verificarCmd.ExecuteScalar();
                        if (usos > 0)
                        {
                            resultado.resultado = false;
                            resultado.mensaje = "No se puede eliminar la familia porque está siendo utilizada por otra entidad.";
                            return resultado;
                        }
                    }
                    using (var deleteCmd = new SqlCommand(deleteSql, db.Connection, tx))
                    {
                        deleteCmd.Parameters.AddWithValue("@id", be.id_permiso456VG);
                        int filas = deleteCmd.ExecuteNonQuery();
                        resultado.resultado = filas > 0;
                        resultado.entidad = be;

                        if (!resultado.resultado)
                            resultado.mensaje = "No se encontró la familia o no pudo eliminarse.";
                    }
                    tx.Commit();
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
        //Trae Permisos de Familia
        public List<BEPermisoComp_456VG> ObtenerRelacionesDeFamilia456VG(int idFamiliaPadre)
        {
            var lista = new List<BEPermisoComp_456VG>();
            const string sql = @"
                SELECT id_permisopadre_456VG, id_permisohijo_456VG
                  FROM PermisoPermiso_456VG
                 WHERE id_permisopadre_456VG = @padre;";
            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@padre", idFamiliaPadre);
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
            }
            finally
            {
                db.Desconectar456VG();
            }
            return lista;
        }
        //Agregar Permiso a Familia
        public Resultado_456VG<BEPermisoComp_456VG> AgregarHijo456VG(int idPadre, int idHijo)
        {
            var resultado = new Resultado_456VG<BEPermisoComp_456VG>();
            var existentes = new HashSet<int>();
            foreach (var rel in ObtenerRelacionesDeFamilia456VG(idPadre))
                existentes.Add(rel.id_permisohijo456VG);
            if (existentes.Contains(idHijo))
            {
                resultado.resultado = false;
                resultado.mensaje = "Ya existe ese permiso o subfamilia en la familia.";
                return resultado;
            }
            const string sql = @"
                INSERT INTO PermisoPermiso_456VG (id_permisopadre_456VG, id_permisohijo_456VG)
                VALUES (@padre, @hijo);";
            try
            {
                db.Conectar456VG();
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@padre", idPadre);
                    cmd.Parameters.AddWithValue("@hijo", idHijo);
                    cmd.ExecuteNonQuery();
                    var beRel = new BEPermisoComp_456VG(idPadre, idHijo);
                    resultado.resultado = true;
                    resultado.entidad = beRel;
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
