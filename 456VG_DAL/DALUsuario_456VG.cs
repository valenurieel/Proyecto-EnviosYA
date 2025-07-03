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
    public class DALUsuario_456VG : ICrud_456VG<BEUsuario_456VG>
    {
        BasedeDatos_456VG db { get; }
        HashSHA256_456VG hasher { get; set; }
        public DALUsuario_456VG()
        {
            db = new BasedeDatos_456VG();
            hasher = new HashSHA256_456VG();
        }
        public BEUsuario_456VG recuperarUsuarioConPerfil456VG(string dni)
        {
            BEUsuario_456VG usuario = null;

            string queryUsuario = @"
    USE EnviosYA_456VG;
    SELECT u.*, r.Nombre_456VG AS nombre_rol_456VG
    FROM Usuario_456VG u
    INNER JOIN Rol_456VG r ON u.CodRol_456VG = r.CodRol_456VG
    WHERE u.dni_456VG = @dni;
";

            using (SqlCommand cmd = new SqlCommand(queryUsuario, db.Connection))
            {
                cmd.Parameters.AddWithValue("@dni", dni);
                db.Connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string nombreRol = reader["nombre_rol_456VG"].ToString();

                        // Instancia básica del usuario
                        usuario = new BEUsuario_456VG(
                            dni,
                            reader["nombre_456VG"].ToString(),
                            reader["apellido_456VG"].ToString(),
                            reader["email_456VG"].ToString(),
                            reader["telefono_456VG"].ToString(),
                            reader["nombreusuario_456VG"].ToString(),
                            reader["domicilio_456VG"].ToString(),
                            new BEPerfil_456VG { Nombre456VG = nombreRol }
                        )
                        {
                            Activo456VG = Convert.ToBoolean(reader["activo_456VG"]),
                            Bloqueado456VG = Convert.ToBoolean(reader["bloqueado_456VG"]),
                            Idioma456VG = reader["idioma_456VG"].ToString()
                        };
                    }
                }
                db.Connection.Close();
            }

            if (usuario != null)
            {
                // 🧠 Aca cargamos el perfil completo desde DALPerfil_456VG
                DALPerfil_456VG dalPerfil = new DALPerfil_456VG();
                usuario.Rol456VG = dalPerfil.ObtenerPerfilCompleto456VG(usuario.Rol456VG.Nombre456VG);
            }

            return usuario;
        }
        //Recupera el Idioma que tiene el Usuario.
        public string RecuperarIdioma456VG(string dniUsuario)
        {
            const string sqlQuery = @"
                USE EnviosYA_456VG;
                SELECT idioma_456VG 
                FROM Usuario_456VG 
                WHERE dni_456VG = @DNI;
            ";
            try
            {
                if (!db.Conectar456VG())
                    throw new Exception("No se pudo conectar a la base de datos.");
                using (var cmd = new SqlCommand(sqlQuery, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@DNI", dniUsuario);
                    object result = cmd.ExecuteScalar();
                    db.Desconectar456VG();
                    if (result == null || result == DBNull.Value)
                        return "ES";
                    string idioma = Convert.ToString(result);
                    return string.IsNullOrWhiteSpace(idioma) ? "ES" : idioma;
                }
            }
            catch
            {
                try { db.Desconectar456VG(); } catch {}
                return "ES";
            }
        }
        //Cambia el Idioma del Usuario en BD.
        public bool modificarIdioma456VG(BEUsuario_456VG user, string idiomaNuevo)
        {
                    const string query = @"
                USE EnviosYA_456VG;
                UPDATE Usuario_456VG
                SET idioma_456VG = @Idioma
                WHERE dni_456VG = @DNI;
            ";
            try
            {
                bool conectado = db.Conectar456VG();
                if (!conectado)
                    throw new Exception("No se pudo conectar a la base de datos.");
                using (var cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Idioma", idiomaNuevo);
                    cmd.Parameters.AddWithValue("@DNI", user.DNI456VG);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    db.Desconectar456VG();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                try { db.Desconectar456VG(); } catch { }
                return false;
            }
        }
        public Resultado_456VG<BEUsuario_456VG> actualizarEntidad456VG(BEUsuario_456VG obj)
        {
            Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
            string queryUpdateUser = @"
        USE EnviosYA_456VG;
        UPDATE Usuario_456VG 
        SET 
            nombre_456VG       = @Nombre,
            apellido_456VG     = @Apellido,
            email_456VG        = @Email,
            telefono_456VG     = @Telefono,
            nombreusuario_456VG= @NombreUsuario,
            domicilio_456VG    = @Domicilio,
            rol_456VG          = @Rol,
            CodRol_456VG       = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = @Rol)
        WHERE dni_456VG = @DNI;
    ";

            try
            {
                bool result = db.Conectar456VG();
                if (!result)
                    throw new Exception("Error al conectarse a la base de datos");

                var trans = db.Connection.BeginTransaction();

                using (SqlCommand cmd = new SqlCommand(queryUpdateUser, db.Connection, trans))
                {
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre456VG);
                    cmd.Parameters.AddWithValue("@Apellido", obj.Apellido456VG);
                    cmd.Parameters.AddWithValue("@Email", obj.Email456VG);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Teléfono456VG);
                    cmd.Parameters.AddWithValue("@NombreUsuario", obj.NombreUsuario456VG);
                    cmd.Parameters.AddWithValue("@Domicilio", obj.Domicilio456VG);
                    cmd.Parameters.AddWithValue("@Rol", obj.Rol456VG.Nombre456VG);
                    cmd.Parameters.AddWithValue("@DNI", obj.DNI456VG);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        string updatePermisos = @"
                    DELETE FROM UsuarioPermiso_456VG
                    WHERE dni_456VG = @DNI
                      AND codpermiso_456VG IN (
                          SELECT codpermiso_456VG 
                          FROM PermisosComp_456VG 
                          WHERE isPerfil_456VG = 1
                      );

                    INSERT INTO UsuarioPermiso_456VG (dni_456VG, codpermiso_456VG)
                    SELECT @DNI, codpermiso_456VG
                    FROM PermisosComp_456VG
                    WHERE nombre_456VG = @Rol
                      AND isPerfil_456VG = 1;
                ";

                        using (SqlCommand cmdPermiso = new SqlCommand(updatePermisos, db.Connection, trans))
                        {
                            cmdPermiso.Parameters.AddWithValue("@DNI", obj.DNI456VG);
                            cmdPermiso.Parameters.AddWithValue("@Rol", obj.Rol456VG.Nombre456VG);
                            cmdPermiso.ExecuteNonQuery();
                        }

                        trans.Commit();

                        resultado.resultado = true;
                        resultado.mensaje = "Usuario actualizado correctamente.";
                        resultado.entidad = obj;
                    }
                    else
                    {
                        trans.Rollback();
                        resultado.resultado = false;
                        resultado.mensaje = "No se encontró el usuario con el DNI proporcionado.";
                        resultado.entidad = null;
                    }
                }

                db.Desconectar456VG();
            }
            catch (Exception ex)
            {
                try { db.Connection?.BeginTransaction()?.Rollback(); } catch { }
                db.Desconectar456VG();
                resultado.resultado = false;
                resultado.mensaje = "Error al actualizar usuario: " + ex.Message;
                resultado.entidad = null;
            }

            return resultado;
        }

        public Resultado_456VG<BEUsuario_456VG> crearEntidad456VG(BEUsuario_456VG obj)
        {
            Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
            try
            {
                db.Connection.Open();
                var trans = db.Connection.BeginTransaction();
                string queryToSearchUser = @"
                    USE EnviosYA_456VG;
                    SELECT COUNT(*) FROM Usuario_456VG WHERE dni_456VG = @DNI;";
                using (SqlCommand cmd = new SqlCommand(queryToSearchUser, db.Connection, trans))
                {
                    cmd.Parameters.AddWithValue("@DNI", obj.DNI456VG);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                        throw new Exception("Ya existe un usuario con ese DNI");
                }
                string salt = hasher.GenerarSalt456VG();
                string hashedPassword = hasher.HashPassword456VG(obj.Contraseña456VG, salt);
                string queryToCreateUser = @"
                    USE EnviosYA_456VG;
                    INSERT INTO Usuario_456VG 
                    (dni_456VG, nombre_456VG, apellido_456VG, email_456VG, telefono_456VG,
                     nombreusuario_456VG, contraseña_456VG, salt_456VG, domicilio_456VG, rol_456VG,
                     bloqueado_456VG, activo_456VG, idioma_456VG, CodRol_456VG)
                    VALUES 
                    (@DNI, @Nombre, @Apellido, @Email, @Telefono,
                     @NombreUsuario, @Contraseña, @Salt, @Domicilio, @Rol,
                     @Bloqueado, @Activo, @Idioma, 
                     (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = @Rol));";
                using (SqlCommand cmd2 = new SqlCommand(queryToCreateUser, db.Connection, trans))
                {
                    cmd2.Parameters.AddWithValue("@DNI", obj.DNI456VG);
                    cmd2.Parameters.AddWithValue("@Nombre", obj.Nombre456VG);
                    cmd2.Parameters.AddWithValue("@Apellido", obj.Apellido456VG);
                    cmd2.Parameters.AddWithValue("@Email", obj.Email456VG);
                    cmd2.Parameters.AddWithValue("@Telefono", obj.Teléfono456VG);
                    cmd2.Parameters.AddWithValue("@NombreUsuario", obj.NombreUsuario456VG);
                    cmd2.Parameters.AddWithValue("@Contraseña", hashedPassword);
                    cmd2.Parameters.AddWithValue("@Salt", salt);
                    cmd2.Parameters.AddWithValue("@Domicilio", obj.Domicilio456VG);
                    cmd2.Parameters.AddWithValue("@Rol", obj.Rol456VG.Nombre456VG);
                    cmd2.Parameters.AddWithValue("@Bloqueado", obj.Bloqueado456VG);
                    cmd2.Parameters.AddWithValue("@Activo", obj.Activo456VG);
                    cmd2.Parameters.AddWithValue("@Idioma", obj.Idioma456VG);
                    cmd2.ExecuteNonQuery();
                }
                string queryToInsertPasswordHistory = @"
                    USE EnviosYA_456VG;
                    INSERT INTO HistorialContraseñas_456VG 
                    (dni_456VG, contraseñahash_456VG, salt_456VG, fechacambio_456VG, hashsimple_456VG)
                    VALUES 
                    (@DniUsuario, @ContraseñaHash, @Salt, @FechaCambio, @HashSimple);";
                using (SqlCommand cmd3 = new SqlCommand(queryToInsertPasswordHistory, db.Connection, trans))
                {
                    cmd3.Parameters.AddWithValue("@DniUsuario", obj.DNI456VG);
                    cmd3.Parameters.AddWithValue("@ContraseñaHash", hashedPassword);
                    cmd3.Parameters.AddWithValue("@Salt", salt);
                    cmd3.Parameters.AddWithValue("@FechaCambio", DateTime.Now);
                    cmd3.Parameters.AddWithValue("@HashSimple", hasher.HashSimple456VG(obj.Contraseña456VG));
                    cmd3.ExecuteNonQuery();
                }
                trans.Commit();
                resultado.resultado = true;
                resultado.mensaje = "Usuario creado correctamente.";
                resultado.entidad = obj;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                resultado.entidad = null;
                try { db.Connection?.BeginTransaction()?.Rollback(); } catch { }
            }
            finally
            {
                db.Connection.Close();
            }
            return resultado;
        }
        public Resultado_456VG<BEUsuario_456VG> ActDesacUsuario456(string dni, bool nuevoEstadoActivo)
        {
            Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
            string query = "USE EnviosYA_456VG; UPDATE Usuario_456VG SET Activo_456VG = @Activo WHERE DNI_456VG = @DNI";
            try
            {
                bool conectado = db.Conectar456VG();
                if (!conectado) throw new Exception("Error al conectarse a la base de datos");
                var trans = db.Connection.BeginTransaction();
                using (SqlCommand comando = new SqlCommand(query, db.Connection, trans))
                {
                    comando.Parameters.AddWithValue("@Activo", nuevoEstadoActivo);
                    comando.Parameters.AddWithValue("@DNI", dni);
                    int filasAfectadas = comando.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        trans.Commit();
                        resultado.resultado = true;
                        resultado.mensaje = "Usuario actualizado correctamente.";
                        resultado.entidad = null;
                    }
                    else
                    {
                        trans.Rollback();
                        resultado.resultado = false;
                        resultado.mensaje = "No se encontró el usuario con ese DNI.";
                    }
                }
                db.Desconectar456VG();
            }
            catch (Exception ex)
            {
                try { db.Connection?.BeginTransaction()?.Rollback(); } catch { }
                db.Desconectar456VG();
                resultado.resultado = false;
                resultado.mensaje = "Error al actualizar el estado del usuario: " + ex.Message;
            }
            return resultado;
        }
        public Resultado_456VG<BEUsuario_456VG> eliminarEntidad456VG(BEUsuario_456VG obj)
        {
            Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
            string queryEliminarHistorialContraseñas = "USE EnviosYA_456VG; DELETE FROM HistorialContraseñas_456VG WHERE dni_456VG = @DNI";
            string queryEliminarUsuario = "USE EnviosYA_456VG; DELETE FROM Usuario_456VG WHERE dni_456VG = @DNI";
            try
            {
                bool conectado = db.Conectar456VG();
                if (!conectado) throw new Exception("Error al conectarse a la base de datos");
                var trans = db.Connection.BeginTransaction();
                using (SqlCommand comandoHistorial = new SqlCommand(queryEliminarHistorialContraseñas, db.Connection, trans))
                {
                    comandoHistorial.Parameters.AddWithValue("@DNI", obj.DNI456VG);
                    comandoHistorial.ExecuteNonQuery();
                }
                using (SqlCommand comandoUsuario = new SqlCommand(queryEliminarUsuario, db.Connection, trans))
                {
                    comandoUsuario.Parameters.AddWithValue("@DNI", obj.DNI456VG);
                    int filasAfectadas = comandoUsuario.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        resultado.resultado = true;
                        resultado.mensaje = "Eliminación exitosa.";
                        resultado.entidad = obj;
                    }
                    else
                    {
                        resultado.resultado = false;
                        resultado.mensaje = "No se encontró el usuario con ese DNI.";
                        resultado.entidad = null;
                    }
                }
                trans.Commit();
                db.Desconectar456VG();
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = "Error al eliminar: " + ex.Message;
                resultado.entidad = null;
                try { db.Connection?.BeginTransaction()?.Rollback(); } catch { }
                db.Desconectar456VG();
            }
            return resultado;
        }
        public List<BEUsuario_456VG> leerEntidades456VG()
        {
            List<BEUsuario_456VG> list = new List<BEUsuario_456VG>();
            string sqlQuery = "USE EnviosYA_456VG; SELECT * FROM Usuario_456VG";
            try
            {
                bool result = db.Conectar456VG();
                if (!result) throw new Exception("Error al conectarse a la base de datos");
                using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
                {
                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            string dni = lector["dni_456VG"].ToString();
                            string nombreRol = lector["rol_456VG"].ToString();
                            DALPerfil_456VG dalPerfil = new DALPerfil_456VG();
                            BEPerfil_456VG perfil = dalPerfil.ObtenerPerfilCompleto456VG(nombreRol);
                            BEUsuario_456VG user = new BEUsuario_456VG(
                                dni,
                                lector["nombre_456VG"].ToString(),
                                lector["apellido_456VG"].ToString(),
                                lector["email_456VG"].ToString(),
                                lector["telefono_456VG"].ToString(),
                                lector["nombreusuario_456VG"].ToString(),
                                lector["contraseña_456VG"].ToString(),
                                lector["salt_456VG"].ToString(),
                                lector["domicilio_456VG"].ToString(),
                                perfil,
                                Convert.ToBoolean(lector["bloqueado_456VG"]),
                                Convert.ToBoolean(lector["activo_456VG"]),
                                lector["idioma_456VG"].ToString()
                            );
                            list.Add(user);
                        }
                    }
                }
                bool result2 = db.Desconectar456VG();
                if (!result2) throw new Exception("Error al desconectarse de la base de datos");
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar456VG();
                return null;
            }
        }
        public Resultado_456VG<BEUsuario_456VG> recuperarUsuarioPorDNI456VG(string DNI)
        {
            Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
            string sqlQuery = "USE EnviosYA_456VG; SELECT * FROM Usuario_456VG WHERE DNI_456VG = @DNI";
            try
            {
                bool result = db.Conectar456VG();
                if (!result) throw new Exception("Error al conectarse a la base de datos");

                using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
                {
                    command.Parameters.AddWithValue("@DNI", DNI);
                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            string dni = lector["dni_456VG"] as string ?? "";
                            string nombre = lector["nombre_456VG"] as string ?? "";
                            string apellido = lector["apellido_456VG"] as string ?? "";
                            string email = lector["email_456VG"] as string ?? "";
                            string telefono = lector["telefono_456VG"] as string ?? "";
                            string nombreusuario = lector["nombreusuario_456VG"] as string ?? "";
                            string contraseñahash = lector["contraseña_456VG"] as string ?? "";
                            string salt = lector["salt_456VG"] as string ?? "";
                            string domicilio = lector["domicilio_456VG"] as string ?? "";
                            string rolNombre = lector["rol_456VG"] as string ?? "";
                            bool bloqueado = !lector.IsDBNull(lector.GetOrdinal("bloqueado_456VG")) && lector.GetBoolean(lector.GetOrdinal("bloqueado_456VG"));
                            bool activo = !lector.IsDBNull(lector.GetOrdinal("activo_456VG")) && lector.GetBoolean(lector.GetOrdinal("activo_456VG"));
                            string idioma = lector["idioma_456VG"] as string ?? "";
                            DALPerfil_456VG dalPerfil = new DALPerfil_456VG();
                            BEPerfil_456VG perfil = dalPerfil.ObtenerPerfilCompleto456VG(rolNombre);
                            BEUsuario_456VG usuario = new BEUsuario_456VG(dni, nombre, apellido, email, telefono, nombreusuario, contraseñahash, salt, domicilio, perfil, bloqueado, activo, idioma);
                            resultado.entidad = usuario;
                            resultado.resultado = true;
                            resultado.mensaje = "Usuario encontrado correctamente";
                        }
                        else
                        {
                            resultado.resultado = false;
                            resultado.mensaje = "No se encontró un usuario con ese DNI";
                            resultado.entidad = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                resultado.entidad = null;
            }
            finally
            {
                db.Desconectar456VG();
            }
            return resultado;
        }
        public Resultado_456VG<bool> bloquearUsuario456VG(BEUsuario_456VG usuario)
        {
            Resultado_456VG<bool> resultado = new Resultado_456VG<bool>();
            string sqlQuery = "USE EnviosYA_456VG; UPDATE Usuario_456VG SET Bloqueado_456VG = 1 WHERE DNI_456VG = @DNI";
            try
            {
                bool result = db.Conectar456VG();
                if (!result) throw new Exception("Error al conectarse a la base de datos");
                using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
                {
                    command.Parameters.AddWithValue("@DNI", usuario.DNI456VG);
                    command.ExecuteNonQuery();
                }
                resultado.resultado = true;
                resultado.entidad = true;
                resultado.mensaje = "Usuario bloqueado correctamente";
                db.Desconectar456VG();
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.entidad = false;
                resultado.mensaje = ex.Message;
                db.Desconectar456VG();
                return resultado;
            }
        }
        public Resultado_456VG<bool> cambiarContraseña456VG(BEUsuario_456VG usuario, string nuevaContraseña)
        {
            Resultado_456VG<bool> resultado = new Resultado_456VG<bool>();
            string sqlUpdateUsuario = "USE EnviosYA_456VG; UPDATE Usuario_456VG SET contraseña_456VG = @Contraseña, salt_456VG = @Salt WHERE DNI_456VG = @DNI";
            string sqlInsertHistorial = "USE EnviosYA_456VG; INSERT INTO HistorialContraseñas_456VG (dni_456VG, contraseñahash_456VG, salt_456VG, fechacambio_456VG, hashsimple_456VG) VALUES (@DniUsuario, @ContraseñaHash, @Salt, @FechaCambio, @HashSimple)";
            string sqlSelectHistorial = "USE EnviosYA_456VG; SELECT hashsimple_456VG FROM HistorialContraseñas_456VG WHERE dni_456VG = @DniUsuario";
            try
            {
                bool result = db.Conectar456VG();
                if (!result) throw new Exception("Error al conectarse a la base de datos");
                string nuevoSalt = hasher.GenerarSalt456VG();
                string nuevaContraseñaHasheada = hasher.HashPassword456VG(nuevaContraseña, nuevoSalt);
                string nuevaContraseñaHashSimple = hasher.HashSimple456VG(nuevaContraseña);
                using (SqlCommand selectCommand = new SqlCommand(sqlSelectHistorial, db.Connection))
                {
                    selectCommand.Parameters.AddWithValue("@DniUsuario", usuario.DNI456VG);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string hashSimpleAntiguo = reader.GetString(0);
                            if (hashSimpleAntiguo == nuevaContraseñaHashSimple)
                            {
                                throw new Exception("La Nueva Contraseña ya fue utilizada anteriormente, por favor elija otra.");
                            }
                        }
                    }
                }
                using (SqlCommand insertCommand = new SqlCommand(sqlInsertHistorial, db.Connection))
                {
                    insertCommand.Parameters.AddWithValue("@DniUsuario", usuario.DNI456VG);
                    insertCommand.Parameters.AddWithValue("@ContraseñaHash", nuevaContraseñaHasheada);
                    insertCommand.Parameters.AddWithValue("@Salt", nuevoSalt);
                    insertCommand.Parameters.AddWithValue("@FechaCambio", DateTime.Now);
                    insertCommand.Parameters.AddWithValue("@HashSimple", nuevaContraseñaHashSimple);
                    insertCommand.ExecuteNonQuery();
                }
                using (SqlCommand updateCommand = new SqlCommand(sqlUpdateUsuario, db.Connection))
                {
                    updateCommand.Parameters.AddWithValue("@Contraseña", nuevaContraseñaHasheada);
                    updateCommand.Parameters.AddWithValue("@Salt", nuevoSalt);
                    updateCommand.Parameters.AddWithValue("@DNI", usuario.DNI456VG);
                    int filasAfectadas = updateCommand.ExecuteNonQuery();
                    if (filasAfectadas == 0)
                    {
                        throw new Exception("No se encontró el usuario para actualizar la contraseña.");
                    }
                }
                resultado.resultado = true;
                resultado.entidad = true;
                resultado.mensaje = "Contraseña actualizada correctamente.";
                db.Desconectar456VG();
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.entidad = false;
                resultado.mensaje = ex.Message;
                db.Desconectar456VG();
                return resultado;
            }
        }
        public Resultado_456VG<BEUsuario_456VG> desbloquearUsuario456VG(string dni)
        {
            Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
            try
            {
                bool conectado = db.Conectar456VG();
                if (!conectado) throw new Exception("Error al conectar con la base de datos.");
                string query = "USE EnviosYA_456VG; UPDATE Usuario_456VG SET bloqueado_456VG = 0 WHERE dni_456VG = @DNI";
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@DNI", dni);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró un usuario con ese DNI.");
                    }
                }
                db.Desconectar456VG();
                resultado.resultado = true;
                resultado.mensaje = "Usuario desbloqueado correctamente.";
            }
            catch (Exception ex)
            {
                db.Desconectar456VG();
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
