using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _456VG_DAL
{
    public class DALPerfil_456VG
    {
        public BasedeDatos_456VG db = new BasedeDatos_456VG();
        public List<string> ObtenerNombresDeFamilias456VG()
        {
            var lista = new List<string>();
            string query = @"
                USE EnviosYA_456VG;
                SELECT Nombre_456VG FROM Permiso_456VG WHERE IsFamilia_456VG = 1";
            try
            {
                db.Connection.Open();
                using (var cmd = new SqlCommand(query, db.Connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        lista.Add(reader.GetString(0));
                }
            }
            finally
            {
                db.Connection.Close();
            }
            return lista;
        }
        // Obtiene todos los permisos (simples o familias) asociados a un rol
        public List<IPerfil_456VG> ObtenerPermisosPorRol456VG(string nombreRol)
        {
            var permisos = new List<IPerfil_456VG>();
            using (var conexion = db.Connection)
            {
                conexion.Open();
                var cmd = new SqlCommand(@"
                    USE EnviosYA_456VG;
                    SELECT p.CodPermiso_456VG, p.Nombre_456VG, p.IsFamilia_456VG
                    FROM Permiso_456VG p
                    INNER JOIN Rol_456VG r ON r.Nombre_456VG = @nombreRol
                    INNER JOIN Rol_Permiso_456VG rp ON rp.CodRol_456VG = r.CodRol_456VG AND rp.CodPermiso_456VG = p.CodPermiso_456VG
                ", conexion);
                cmd.Parameters.AddWithValue("@nombreRol", nombreRol);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int cod = reader.GetInt32(0);
                        string nombre = reader.GetString(1);
                        bool esFamilia = reader.GetBoolean(2);
                        if (esFamilia)
                        {
                            var familia = new FamiliaPermiso_456VG
                            {
                                CodFamilia456VG = cod,
                                Nombre456VG = nombre
                            };
                            familia.Permisos456VG = ObtenerHijosDeFamilia456VG(cod);
                            permisos.Add(familia);
                        }
                        else
                        {
                            permisos.Add(new Permiso_456VG
                            {
                                CodPermisos456VG = cod,
                                Nombre456VG = nombre
                            });
                        }
                    }
                }
            }
            return permisos;
        }
        // Obtiene los hijos directos de una familia
        public List<IPerfil_456VG> ObtenerHijosDeFamilia456VG(int codFamilia)
        {
            var hijos = new List<IPerfil_456VG>();
            using (var conexion = db.Connection)
            {
                conexion.Open();
                var cmd = new SqlCommand(@"
                    USE EnviosYA_456VG;
                    SELECT p.CodPermiso_456VG, p.Nombre_456VG, p.IsFamilia_456VG
                    FROM FamiliaPermiso_456VG fp
                    INNER JOIN Permiso_456VG p ON p.CodPermiso_456VG = fp.CodPermiso_456VG
                    WHERE fp.CodFamilia_456VG = @codFamilia
                ", conexion);

                cmd.Parameters.AddWithValue("@codFamilia", codFamilia);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int cod = reader.GetInt32(0);
                        string nombre = reader.GetString(1);
                        bool esFamilia = reader.GetBoolean(2);
                        if (esFamilia)
                        {
                            var subfamilia = new FamiliaPermiso_456VG
                            {
                                CodFamilia456VG = cod,
                                Nombre456VG = nombre
                            };
                            subfamilia.Permisos456VG = ObtenerHijosDeFamilia456VG(cod);
                            hijos.Add(subfamilia);
                        }
                        else
                        {
                            hijos.Add(new Permiso_456VG
                            {
                                CodPermisos456VG = cod,
                                Nombre456VG = nombre
                            });
                        }
                    }
                }
            }
            return hijos;
        }
        public BEPerfil_456VG ObtenerPerfilCompleto456VG(string nombrePerfil456VG)
        {
            var perfil = new BEPerfil_456VG { Nombre456VG = nombrePerfil456VG };
            List<IPerfil_456VG> permisos = new List<IPerfil_456VG>();
            try
            {
                db.Connection.Open();
                int codRol;
                using (var cmdRol = new SqlCommand("USE EnviosYA_456VG; SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = @nombre", db.Connection))
                {
                    cmdRol.Parameters.AddWithValue("@nombre", nombrePerfil456VG);
                    codRol = Convert.ToInt32(cmdRol.ExecuteScalar());
                }
                using (var cmdPerms = new SqlCommand(@"
                    USE EnviosYA_456VG;
                    SELECT p.CodPermiso_456VG, p.Nombre_456VG, p.IsFamilia_456VG
                    FROM Rol_Permiso_456VG rp
                    JOIN Permiso_456VG p ON rp.CodPermiso_456VG = p.CodPermiso_456VG
                    WHERE rp.CodRol_456VG = @codRol", db.Connection))
                {
                    cmdPerms.Parameters.AddWithValue("@codRol", codRol);
                    using (var reader = cmdPerms.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int codPermiso = reader.GetInt32(0);
                            string nombre = reader.GetString(1);
                            bool esFamilia = reader.GetBoolean(2);

                            if (esFamilia)
                                permisos.Add(ConstruirFamiliaRecursiva456VG(codPermiso, nombre, db.Connection));
                            else
                                permisos.Add(new Permiso_456VG { CodPermiso456VG = codPermiso, Nombre456VG = nombre });
                        }
                    }
                }
                perfil.Permisos456VG = permisos;
            }
            finally
            {
                db.Connection.Close();
            }
            return perfil;
        }
        public bool EliminarPerfil456VG(string nombrePerfil)
        {
            const string query = @"
                USE EnviosYA_456VG;
                DECLARE @CodRol INT = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = @Nombre);
                DECLARE @CodPermiso INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = @Nombre AND IsFamilia_456VG = 0);
                DELETE FROM Rol_Permiso_456VG WHERE CodRol_456VG = @CodRol;
                DELETE FROM Permiso_456VG WHERE CodPermiso_456VG = @CodPermiso;
                DELETE FROM Rol_456VG WHERE CodRol_456VG = @CodRol;
            ";
            try
            {
                db.Connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombrePerfil);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Connection.Close();
            }
        }
        public bool EliminarPermisoDePerfil456VG(string nombrePerfil, int codPermiso)
        {
            const string query = @"
                USE EnviosYA_456VG;
                DELETE FROM Rol_Permiso_456VG
                WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = @nombre)
                AND CodPermiso_456VG = @codPermiso;
            ";
            try
            {
                db.Connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombrePerfil);
                    cmd.Parameters.AddWithValue("@codPermiso", codPermiso);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Connection.Close();
            }
        }
        public bool QuitarPermisoDeFamilia456VG(string nombreFamilia, int codPermiso)
        {
            const string query = @"
                USE EnviosYA_456VG;
                DELETE FROM FamiliaPermiso_456VG
                WHERE CodFamilia_456VG = (
                    SELECT CodPermiso_456VG FROM Permiso_456VG 
                    WHERE Nombre_456VG = @Nombre AND IsFamilia_456VG = 1
                )
                AND CodPermiso_456VG = @codPermiso;
            ";
            try
            {
                db.Connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreFamilia);
                    cmd.Parameters.AddWithValue("@codPermiso", codPermiso);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Connection.Close();
            }
        }
        public bool AgregarFamilia456VG(string nombreFamilia)
        {
            const string query = @"
                USE EnviosYA_456VG;
                IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = @Nombre AND IsFamilia_456VG = 1)
                BEGIN
                    INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG)
                    VALUES (@Nombre, 1);
                END
            ";
            try
            {
                db.Connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreFamilia);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Connection.Close();
            }
        }
        public bool InsertarPermisoEnFamilia456VG(string nombreFamilia, int codPermiso)
        {
            const string query = @"
                USE EnviosYA_456VG;
                DECLARE @codFamilia INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = @Nombre AND IsFamilia_456VG = 1);
                IF NOT EXISTS (
                    SELECT 1 FROM FamiliaPermiso_456VG
                    WHERE CodFamilia_456VG = @codFamilia AND CodPermiso_456VG = @codPermiso
                )
                BEGIN
                    INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG)
                    VALUES (@codFamilia, @codPermiso);
                END
            ";
            try
            {
                db.Connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreFamilia);
                    cmd.Parameters.AddWithValue("@codPermiso", codPermiso);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Connection.Close();
            }
        }
        public int ObtenerCodPermisoPorNombre456VG(string nombre)
        {
            const string query = @"
                USE EnviosYA_456VG;
                SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = @nombre";
            try
            {
                db.Connection.Open();
                using (var cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
            finally
            {
                db.Connection.Close();
            }
        }
        public bool EliminarFamilia456VG(string nombreFamilia) //chequear
        {
            const string query = @"
                USE EnviosYA_456VG;
                DECLARE @CodPermiso INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = @Nombre AND IsFamilia_456VG = 1);
                IF EXISTS (
                    SELECT 1 FROM FamiliaPermiso_456VG WHERE CodFamilia_456VG = @CodPermiso
                )
                BEGIN
                    RAISERROR('No se puede eliminar la familia porque contiene subpermisos o subfamilias.', 16, 1);
                    RETURN;
                END
                DELETE FROM FamiliaPermiso_456VG WHERE CodPermiso_456VG = @CodPermiso OR CodFamilia_456VG = @CodPermiso;
                DELETE FROM Rol_Permiso_456VG WHERE CodPermiso_456VG = @CodPermiso;
                DELETE FROM Permiso_456VG WHERE CodPermiso_456VG = @CodPermiso;
            ";
            try
            {
                db.Connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreFamilia);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
            finally
            {
                db.Connection.Close();
            }
        }
        public bool InsertarPermisoAPerfil456VG(string nombrePerfil, int codPermiso) //chequear
        {
            const string query = @"
                USE EnviosYA_456VG;
                DECLARE @codRol INT = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = @nombre);
                IF NOT EXISTS (
                    SELECT 1 FROM Rol_Permiso_456VG
                    WHERE CodRol_456VG = @codRol AND CodPermiso_456VG = @codPermiso
                )
                BEGIN
                    INSERT INTO Rol_Permiso_456VG (CodRol_456VG, CodPermiso_456VG)
                    VALUES (@codRol, @codPermiso);
                END
            ";
            try
            {
                db.Connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombrePerfil);
                    cmd.Parameters.AddWithValue("@codPermiso", codPermiso);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Connection.Close();
            }
        }
        public bool InsertarPerfil456VG(string nombrePerfil) //chequear
        {
            string query = @"
            USE EnviosYA_456VG;
            INSERT INTO Rol_456VG (Nombre_456VG)
            VALUES (@Nombre);";
            try
            {
                db.Connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombrePerfil);
                    int res = cmd.ExecuteNonQuery();
                    return res > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Connection.Close();
            }
        }
        public List<Permiso_456VG> ListarPermisosSimples456VG()
        {
            List<Permiso_456VG> lista = new List<Permiso_456VG>();
            try
            {
                db.Conectar456VG();
                string query = @"
                    USE EnviosYA_456VG;
                    SELECT CodPermiso_456VG, Nombre_456VG 
                    FROM Permiso_456VG 
                    WHERE IsFamilia_456VG = 0;";
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var permiso = new Permiso_456VG
                            {
                                CodPermiso456VG = Convert.ToInt32(reader["CodPermiso_456VG"]),
                                Nombre456VG = reader["Nombre_456VG"].ToString()
                            };

                            lista.Add(permiso);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar permisos simples", ex);
            }
            finally
            {
                db.Desconectar456VG();
            }
            return lista;
        }
        public List<BEPerfil_456VG> ObtenerPerfilesSimples456VG()
        {
            List<BEPerfil_456VG> lista = new List<BEPerfil_456VG>();
            string query = @"
                USE EnviosYA_456VG;
                SELECT Nombre_456VG
                FROM Rol_456VG;
            ";
            if (!db.Conectar456VG()) return lista;
            using (SqlCommand cmd = new SqlCommand(query, db.Connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var perfil = new BEPerfil_456VG
                        {
                            Nombre456VG = reader["Nombre_456VG"].ToString(),
                            Permisos456VG = new List<IPerfil_456VG>()
                        };
                        lista.Add(perfil);
                    }
                }
            }
            db.Desconectar456VG();
            return lista;
        }
        public FamiliaPermiso_456VG ConstruirFamiliaRecursiva456VG(int codFamilia, string nombreFamilia, SqlConnection conn)
        {
            var familia = new FamiliaPermiso_456VG
            {
                CodPermiso456VG = codFamilia,
                Nombre456VG = nombreFamilia,
                Permisos456VG = new List<IPerfil_456VG>()
            };

            using (var cmd = new SqlCommand(@"
        USE EnviosYA_456VG;
        SELECT p.CodPermiso_456VG, p.Nombre_456VG, p.IsFamilia_456VG
        FROM FamiliaPermiso_456VG fp
        JOIN Permiso_456VG p ON fp.CodPermiso_456VG = p.CodPermiso_456VG
        WHERE fp.CodFamilia_456VG = @codFamilia", conn))
            {
                cmd.Parameters.AddWithValue("@codFamilia", codFamilia);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int codPerm = reader.GetInt32(0);
                        string nombre = reader.GetString(1);
                        bool esFamilia = reader.GetBoolean(2);

                        if (esFamilia)
                            familia.Permisos456VG.Add(ConstruirFamiliaRecursiva456VG(codPerm, nombre, conn));
                        else
                            familia.Permisos456VG.Add(new Permiso_456VG
                            {
                                CodPermiso456VG = codPerm,
                                Nombre456VG = nombre
                            });
                    }
                }
            }

            return familia;
        }

    }
}
