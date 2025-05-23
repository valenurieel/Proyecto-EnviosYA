﻿using _456VG_BE;
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
        public Resultado_456VG<BEUsuario_456VG> actualizarEntidad456VG(BEUsuario_456VG obj)
        {
            Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
            string queryUpdateUser = @"
                    USE EnviosYA_456VG;
                    UPDATE Usuario_456VG 
                    SET 
                        nombre_456VG = @Nombre,
                        apellido_456VG = @Apellido,
                        email_456VG = @Email,
                        telefono_456VG = @Telefono,
                        nombreusuario_456VG = @NombreUsuario,
                        domicilio_456VG = @Domicilio
                    WHERE dni_456VG = @DNI";
            try
            {
                bool result = db.Conectar456VG();
                if (!result) throw new Exception("Error al conectarse a la base de datos");
                using (SqlCommand cmd = new SqlCommand(queryUpdateUser, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre456VG);
                    cmd.Parameters.AddWithValue("@Apellido", obj.Apellido456VG);
                    cmd.Parameters.AddWithValue("@Email", obj.Email456VG);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Teléfono456VG);
                    cmd.Parameters.AddWithValue("@NombreUsuario", obj.NombreUsuario456VG);
                    cmd.Parameters.AddWithValue("@Domicilio", obj.Domicilio456VG);
                    cmd.Parameters.AddWithValue("@DNI", obj.DNI456VG);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
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
        public Resultado_456VG<BEUsuario_456VG> crearEntidad456VG(BEUsuario_456VG obj)
        {
            Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
            try
            {
                db.Connection.Open();
                var trans = db.Connection.BeginTransaction();
                string queryToSearchUser = "USE EnviosYA_456VG; SELECT COUNT(*) FROM Usuario_456VG WHERE dni_456VG = @DNI";
                using (SqlCommand cmd = new SqlCommand(queryToSearchUser, db.Connection, trans))
                {
                    cmd.Parameters.AddWithValue("@DNI", obj.DNI456VG);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        throw new Exception("Ya existe un usuario con ese DNI");
                    }
                }
                string salt = hasher.GenerarSalt456VG();
                string hashedPassword = hasher.HashPassword456VG(obj.Contraseña456VG, salt);
                string queryToCreateUser = @"
                    USE EnviosYA_456VG;
                    INSERT INTO Usuario_456VG (dni_456VG, nombre_456VG, apellido_456VG, email_456VG, telefono_456VG, nombreusuario_456VG, contraseña_456VG, salt_456VG, domicilio_456VG, rol_456VG, bloqueado_456VG, activo_456VG)
                    VALUES (@DNI, @Nombre, @Apellido, @Email, @Telefono, @NombreUsuario, @Contraseña, @Salt, @Domicilio, @Rol, @Bloqueado, @Activo);";
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
                    cmd2.ExecuteNonQuery();
                }
                string queryToInsertPasswordHistory = @"
                    USE EnviosYA_456VG;
                    INSERT INTO HistorialContraseñas_456VG (dni_456VG, contraseñahash_456VG, salt_456VG, fechacambio_456VG, hashsimple_456VG) 
                    VALUES (@DniUsuario, @ContraseñaHash, @Salt, @FechaCambio, @HashSimple);";
                using (SqlCommand cmd3 = new SqlCommand(queryToInsertPasswordHistory, db.Connection, trans))
                {
                    cmd3.Parameters.AddWithValue("@DniUsuario", obj.DNI456VG);
                    cmd3.Parameters.AddWithValue("@ContraseñaHash", hashedPassword);
                    cmd3.Parameters.AddWithValue("@Salt", salt);
                    cmd3.Parameters.AddWithValue("@FechaCambio", DateTime.Now);
                    cmd3.Parameters.AddWithValue("@HashSimple", hasher.HashSimple456VG(obj.Contraseña456VG));  // Aquí usamos el HashSimple para comparar las contraseñas simples
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
                            BEUsuario_456VG user = new BEUsuario_456VG(dni, name, ape, email, tel, nameuser, contraseña, salt, dom, rol, bloqueado, activo);
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
        //public Resultado_456VG<BEUsuario_456VG> recuperarUsuario456VG(string DNI, string Contraseña)
        //{
        //    Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
        //    string sqlQuery = "USE EnviosYA; SELECT * FROM Usuario WHERE DNI = @DNI";
        //    try
        //    {
        //        bool result = db.Conectar();
        //        if (!result) throw new Exception("Error al conectarse a la base de datos");
        //        using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
        //        {
        //            command.Parameters.AddWithValue("@DNI", DNI);

        //            using (SqlDataReader lector = command.ExecuteReader())
        //            {
        //                if (!lector.HasRows)
        //                {
        //                    throw new Exception("No se encontró un usuario con ese DNI");
        //                }
        //                if (lector.Read())
        //                {
        //                    string dni = !lector.IsDBNull(0) ? lector.GetString(0) : "";
        //                    string nombre = !lector.IsDBNull(1) ? lector.GetString(1) : "";
        //                    string apellido = !lector.IsDBNull(2) ? lector.GetString(2) : "";
        //                    string email = !lector.IsDBNull(3) ? lector.GetString(3) : "";
        //                    string telefono = !lector.IsDBNull(4) ? lector.GetString(4) : string.Empty;
        //                    string nombreusuario = !lector.IsDBNull(5) ? lector.GetString(5) : "";
        //                    string contraseñahash = !lector.IsDBNull(6) ? lector.GetString(6) : "";
        //                    string saltAlmacenado = !lector.IsDBNull(7) ? lector.GetString(7) : "";
        //                    string domicilio = !lector.IsDBNull(8) ? lector.GetString(8) : "";
        //                    string rol = !lector.IsDBNull(9) ? lector.GetString(9) : "";
        //                    bool bloqueado = !lector.IsDBNull(10) && lector.GetBoolean(10);
        //                    bool activo = !lector.IsDBNull(11) && lector.GetBoolean(11);
        //                    if (!activo)
        //                    {
        //                        throw new Exception("El Usuario está Bloqueado.");
        //                    }
        //                    // Verificar contraseña usando SHA-256
        //                    bool esContraseñaValida = hasher.VerificarPassword456VG(Contraseña, contraseñahash, saltAlmacenado);
        //                    if (!esContraseñaValida)
        //                    {
        //                        throw new Exception("Contraseña incorrecta");
        //                    }
        //                    BEUsuario_456VG usuario = new BEUsuario_456VG(dni, nombre, apellido, email, telefono, nombreusuario, contraseñahash, saltAlmacenado, domicilio, rol, bloqueado, activo);
        //                    resultado.resultado = true;
        //                    resultado.entidad = usuario;
        //                    resultado.mensaje = "Inicio de sesión correcto";
        //                }
        //            }
        //        }
        //        db.Desconectar();
        //        return resultado;
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.resultado = false;
        //        resultado.mensaje = ex.Message;
        //        resultado.entidad = null;
        //        db.Desconectar();
        //        return resultado;
        //    }
        //}
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
                            BEUsuario_456VG usuario = new BEUsuario_456VG(dni, nombre, apellido, email, telefono, nombreusuario, contraseñahash, salt, domicilio, rol, bloqueado, activo);
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
                // Validar que la nueva contraseña no haya sido usada antes
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
