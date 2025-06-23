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
                        rol_456VG          = @Rol
                    WHERE dni_456VG = @DNI;
                ";
            try
            {
                bool result = db.Conectar456VG();
                if (!result)
                    throw new Exception("Error al conectarse a la base de datos");
                using (SqlCommand cmd = new SqlCommand(queryUpdateUser, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre456VG);
                    cmd.Parameters.AddWithValue("@Apellido", obj.Apellido456VG);
                    cmd.Parameters.AddWithValue("@Email", obj.Email456VG);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Teléfono456VG);
                    cmd.Parameters.AddWithValue("@NombreUsuario", obj.NombreUsuario456VG);
                    cmd.Parameters.AddWithValue("@Domicilio", obj.Domicilio456VG);
                    cmd.Parameters.AddWithValue("@Rol", obj.Rol456VG);
                    cmd.Parameters.AddWithValue("@DNI", obj.DNI456VG);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(obj.Rol456VG))
                        {
                            string updatePermisos = @"
                                DELETE FROM UsuarioPermiso_456VG
                                WHERE dni_456VG = @DNI
                                  AND codpermiso_456VG IN (
                                      SELECT codpermiso_456VG FROM PermisosComp_456VG WHERE isPerfil_456VG = 1
                                  );
                                INSERT INTO UsuarioPermiso_456VG (dni_456VG, codpermiso_456VG)
                                SELECT @DNI, codpermiso_456VG
                                  FROM PermisosComp_456VG
                                 WHERE nombre_456VG = @Rol
                                   AND isPerfil_456VG = 1;
                            ";
                            using (SqlCommand cmdPermiso = new SqlCommand(updatePermisos, db.Connection))
                            {
                                cmdPermiso.Parameters.AddWithValue("@DNI", obj.DNI456VG);
                                cmdPermiso.Parameters.AddWithValue("@Rol", obj.Rol456VG);
                                cmdPermiso.ExecuteNonQuery();
                            }
                        }
                        resultado.resultado = true;
                        resultado.mensaje = "Usuario actualizado correctamente.";
                        resultado.entidad = obj;
                    }
                    else
                    {
                        resultado.resultado = false;
                        resultado.mensaje = "No se encontró el usuario con el DNI proporcionado.";
                        resultado.entidad = null;
                    }
                }
                db.Desconectar456VG();
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = "Error al actualizar usuario: " + ex.Message;
                resultado.entidad = null;
            }
            return resultado;
        }
        //Recupera a partir del Perfil de User, sus Permisos.
        public List<Permiso_456VG> obtenerPermisosUsuario456VG(string dniUsuario)
        {
            var permisos = new List<Permiso_456VG>();
            var procesados = new HashSet<int>();
            try
            {
                if (!db.Conectar456VG())
                    throw new Exception("Error al conectarse a la base de datos");
                const string sqlDirectos = @"
                    USE EnviosYA_456VG;
                    SELECT 
                        P.codpermiso_456VG,
                        P.nombre_456VG,
                        P.nombre_formulario_456VG,
                        P.isPerfil_456VG
                    FROM UsuarioPermiso_456VG UP
                    JOIN PermisosComp_456VG P 
                      ON UP.codpermiso_456VG = P.codpermiso_456VG
                    WHERE UP.dni_456VG = @dni;";
                using (var cmd = new SqlCommand(sqlDirectos, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@dni", dniUsuario);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idPermiso = reader.GetInt32(reader.GetOrdinal("codpermiso_456VG"));
                            string nombre = reader.GetString(reader.GetOrdinal("nombre_456VG"));
                            string formulario = reader.IsDBNull(reader.GetOrdinal("nombre_formulario_456VG"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("nombre_formulario_456VG"));
                            bool isPerfil = reader.GetBoolean(reader.GetOrdinal("isPerfil_456VG"));
                            var permiso = new Permiso_456VG(nombre, formulario, isPerfil);
                            permisos.Add(permiso);
                            if (!procesados.Contains(idPermiso))
                            {
                                procesados.Add(idPermiso);
                                permisos.AddRange(ObtenerPermisosHijos456VG(idPermiso, procesados));
                            }
                        }
                    }
                }
                db.Desconectar456VG();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener permisos de usuario: " + ex.Message);
            }
            return permisos;
        }
        //Obtiene Permisos y Familias + sus Permisos del Perfil que tiene el Usuario.
        public List<Permiso_456VG> ObtenerPermisosHijos456VG(int idPermisoPadre, HashSet<int> yaProcesados)
        {
            var permisosHijos = new List<Permiso_456VG>();
            const string sqlHijos = @"
                USE EnviosYA_456VG;
                SELECT
                    P.codpermiso_456VG,
                    P.nombre_456VG,
                    P.nombre_formulario_456VG,
                    P.isPerfil_456VG
                FROM PermisoPermiso_456VG PP
                JOIN PermisosComp_456VG P 
                  ON PP.codpermisohijo_456VG = P.codpermiso_456VG
                WHERE PP.codpermisopadre_456VG = @padreId;";
            using (var cmd = new SqlCommand(sqlHijos, db.Connection))
            {
                cmd.Parameters.AddWithValue("@padreId", idPermisoPadre);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idHijo = reader.GetInt32(reader.GetOrdinal("codpermiso_456VG"));
                        string nombre = reader.GetString(reader.GetOrdinal("nombre_456VG"));
                        string formulario = reader.IsDBNull(reader.GetOrdinal("nombre_formulario_456VG"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("nombre_formulario_456VG"));
                        bool isPerfil = reader.GetBoolean(reader.GetOrdinal("isPerfil_456VG"));

                        var permiso = new Permiso_456VG(nombre, formulario, isPerfil);
                        permisosHijos.Add(permiso);
                        if (!yaProcesados.Contains(idHijo))
                        {
                            yaProcesados.Add(idHijo);
                            permisosHijos.AddRange(ObtenerPermisosHijos456VG(idHijo, yaProcesados));
                        }
                    }
                }
            }
            return permisosHijos;
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
                     bloqueado_456VG, activo_456VG, idioma_456VG)
                    VALUES 
                    (@DNI, @Nombre, @Apellido, @Email, @Telefono,
                     @NombreUsuario, @Contraseña, @Salt, @Domicilio, @Rol,
                     @Bloqueado, @Activo, @Idioma);";
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
                    cmd2.Parameters.AddWithValue("@Rol", obj.Rol456VG);
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
                int idPerfil = -1;
                string queryGetIdPerfil = @"
                    USE EnviosYA_456VG;
                    SELECT TOP 1 codpermiso_456VG
                      FROM PermisosComp_456VG
                     WHERE nombre_456VG = @NombrePerfil AND isPerfil_456VG = 1;";
                using (SqlCommand cmd4 = new SqlCommand(queryGetIdPerfil, db.Connection, trans))
                {
                    cmd4.Parameters.AddWithValue("@NombrePerfil", obj.Rol456VG);
                    object result = cmd4.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        throw new Exception("No se encontró el perfil con el nombre especificado.");
                    idPerfil = Convert.ToInt32(result);
                }
                string queryInsertUsuarioPermiso = @"
                    USE EnviosYA_456VG;
                    INSERT INTO UsuarioPermiso_456VG (dni_456VG, codpermiso_456VG)
                    VALUES (@DNI, @IdPermiso);";
                using (SqlCommand cmdRol = new SqlCommand(queryInsertUsuarioPermiso, db.Connection, trans))
                {
                    cmdRol.Parameters.AddWithValue("@DNI", obj.DNI456VG);
                    cmdRol.Parameters.AddWithValue("@IdPermiso", idPerfil);
                    cmdRol.ExecuteNonQuery();
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
                            string dni = !lector.IsDBNull(lector.GetOrdinal("dni_456VG")) ? lector.GetString(lector.GetOrdinal("dni_456VG")) : string.Empty;
                            string name = !lector.IsDBNull(lector.GetOrdinal("nombre_456VG")) ? lector.GetString(lector.GetOrdinal("nombre_456VG")) : string.Empty;
                            string ape = !lector.IsDBNull(lector.GetOrdinal("apellido_456VG")) ? lector.GetString(lector.GetOrdinal("apellido_456VG")) : string.Empty;
                            string email = !lector.IsDBNull(lector.GetOrdinal("email_456VG")) ? lector.GetString(lector.GetOrdinal("email_456VG")) : string.Empty;
                            string tel = !lector.IsDBNull(lector.GetOrdinal("telefono_456VG")) ? lector.GetString(lector.GetOrdinal("telefono_456VG")) : string.Empty;
                            string nameuser = !lector.IsDBNull(lector.GetOrdinal("nombreusuario_456VG")) ? lector.GetString(lector.GetOrdinal("nombreusuario_456VG")) : string.Empty;
                            string contraseña = !lector.IsDBNull(lector.GetOrdinal("contraseña_456VG")) ? lector.GetString(lector.GetOrdinal("contraseña_456VG")) : string.Empty;
                            string salt = !lector.IsDBNull(lector.GetOrdinal("salt_456VG")) ? lector.GetString(lector.GetOrdinal("salt_456VG")) : string.Empty;
                            string dom = !lector.IsDBNull(lector.GetOrdinal("domicilio_456VG")) ? lector.GetString(lector.GetOrdinal("domicilio_456VG")) : string.Empty;
                            string rol = !lector.IsDBNull(lector.GetOrdinal("rol_456VG")) ? lector.GetString(lector.GetOrdinal("rol_456VG")) : string.Empty;
                            bool bloqueado = !lector.IsDBNull(lector.GetOrdinal("bloqueado_456VG")) && lector.GetBoolean(lector.GetOrdinal("bloqueado_456VG"));
                            bool activo = !lector.IsDBNull(lector.GetOrdinal("activo_456VG")) && lector.GetBoolean(lector.GetOrdinal("activo_456VG"));
                            string idioma = !lector.IsDBNull(lector.GetOrdinal("idioma_456VG")) ? lector.GetString(lector.GetOrdinal("idioma_456VG")) : string.Empty;
                            BEUsuario_456VG user = new BEUsuario_456VG(dni, name, ape, email, tel, nameuser, contraseña, salt, dom, rol, bloqueado, activo, idioma);
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
            List<BEUsuario_456VG> list = new List<BEUsuario_456VG>();
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
                        while (lector.Read())
                        {
                            string dni = !lector.IsDBNull(lector.GetOrdinal("dni_456VG")) ? lector.GetString(lector.GetOrdinal("dni_456VG")) : string.Empty;
                            string nombre = !lector.IsDBNull(lector.GetOrdinal("nombre_456VG")) ? lector.GetString(lector.GetOrdinal("nombre_456VG")) : string.Empty;
                            string apellido = !lector.IsDBNull(lector.GetOrdinal("apellido_456VG")) ? lector.GetString(lector.GetOrdinal("apellido_456VG")) : string.Empty;
                            string email = !lector.IsDBNull(lector.GetOrdinal("email_456VG")) ? lector.GetString(lector.GetOrdinal("email_456VG")) : string.Empty;
                            string telefono = !lector.IsDBNull(lector.GetOrdinal("telefono_456VG")) ? lector.GetString(lector.GetOrdinal("telefono_456VG")) : string.Empty;
                            string nombreusuario = !lector.IsDBNull(lector.GetOrdinal("nombreusuario_456VG")) ? lector.GetString(lector.GetOrdinal("nombreusuario_456VG")) : string.Empty;
                            string contraseñahash = !lector.IsDBNull(lector.GetOrdinal("contraseña_456VG")) ? lector.GetString(lector.GetOrdinal("contraseña_456VG")) : string.Empty;
                            string salt = !lector.IsDBNull(lector.GetOrdinal("salt_456VG")) ? lector.GetString(lector.GetOrdinal("salt_456VG")) : string.Empty;
                            string domicilio = !lector.IsDBNull(lector.GetOrdinal("domicilio_456VG")) ? lector.GetString(lector.GetOrdinal("domicilio_456VG")) : string.Empty;
                            string rol = !lector.IsDBNull(lector.GetOrdinal("rol_456VG")) ? lector.GetString(lector.GetOrdinal("rol_456VG")) : string.Empty;
                            bool bloqueado = !lector.IsDBNull(lector.GetOrdinal("bloqueado_456VG")) && lector.GetBoolean(lector.GetOrdinal("bloqueado_456VG"));
                            bool activo = !lector.IsDBNull(lector.GetOrdinal("activo_456VG")) && lector.GetBoolean(lector.GetOrdinal("activo_456VG"));
                            string idioma = !lector.IsDBNull(lector.GetOrdinal("idioma_456VG")) ? lector.GetString(lector.GetOrdinal("idioma_456VG")) : string.Empty;
                            BEUsuario_456VG usuario = new BEUsuario_456VG(dni, nombre, apellido, email, telefono, nombreusuario, contraseñahash, salt, domicilio, rol, bloqueado, activo, idioma);
                            list.Add(usuario);
                        }
                    }
                }
                db.Desconectar456VG();
                if (list.Count > 0)
                {
                    resultado.resultado = true;
                    resultado.entidad = list[0];
                    resultado.mensaje = "Usuario encontrado correctamente";
                }
                else
                {
                    resultado.resultado = false;
                    resultado.mensaje = "No se encontró un usuario con ese DNI";
                    resultado.entidad = null;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                resultado.entidad = null;
                db.Desconectar456VG();
                return resultado;
            }
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
