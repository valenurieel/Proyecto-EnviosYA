using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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
        public Resultado_456VG<BEUsuario_456VG> actualizarEntidad(BEUsuario_456VG obj)
        {
            throw new NotImplementedException();
        }
        public Resultado_456VG<BEUsuario_456VG> crearEntidad(BEUsuario_456VG obj)
        {
            Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
            try
            {
                db.Connection.Open();
                var trans = db.Connection.BeginTransaction();
                string queryToSearchUser = "USE EnviosYA; SELECT COUNT(*) FROM Usuario WHERE dni = @DNI";
                using (SqlCommand cmd = new SqlCommand(queryToSearchUser, db.Connection, trans))
                {
                    cmd.Parameters.AddWithValue("@DNI", obj.DNI);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        throw new Exception("Ya existe un usuario con ese DNI");
                    }
                }
                string salt = hasher.GenerarSalt();
                string hashedPassword = hasher.HashPassword(obj.Contraseña, salt);
                string queryToCreateUser = @"
                    USE EnviosYA;
                    INSERT INTO Usuario (dni, nombre, apellido, email, telefono, nombreusuario, contraseña, salt, domicilio, rol, bloqueado, activo)
                    VALUES (@DNI, @Nombre, @Apellido, @Email, @Telefono, @NombreUsuario, @Contraseña, @Salt, @Domicilio, @Rol, @Bloqueado, @Activo);";
                using (SqlCommand cmd2 = new SqlCommand(queryToCreateUser, db.Connection, trans))
                {
                    cmd2.Parameters.AddWithValue("@DNI", obj.DNI);
                    cmd2.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd2.Parameters.AddWithValue("@Apellido", obj.Apellido);
                    cmd2.Parameters.AddWithValue("@Email", obj.Email);
                    cmd2.Parameters.AddWithValue("@Telefono", obj.Teléfono);
                    cmd2.Parameters.AddWithValue("@NombreUsuario", obj.NombreUsuario);
                    cmd2.Parameters.AddWithValue("@Contraseña", hashedPassword);
                    cmd2.Parameters.AddWithValue("@Salt", salt);
                    cmd2.Parameters.AddWithValue("@Domicilio", obj.Domicilio);
                    cmd2.Parameters.AddWithValue("@Rol", obj.Rol);
                    cmd2.Parameters.AddWithValue("@Bloqueado", obj.Bloqueado);
                    cmd2.Parameters.AddWithValue("@Activo", obj.Activo);
                    cmd2.ExecuteNonQuery();
                }
                string queryToInsertPasswordHistory = @"
                    USE EnviosYA;
                    INSERT INTO HistorialContraseñas (dni, contraseñahash, salt, fechacambio, hashsimple) 
                    VALUES (@DniUsuario, @ContraseñaHash, @Salt, @FechaCambio, @HashSimple);";
                using (SqlCommand cmd3 = new SqlCommand(queryToInsertPasswordHistory, db.Connection, trans))
                {
                    cmd3.Parameters.AddWithValue("@DniUsuario", obj.DNI);
                    cmd3.Parameters.AddWithValue("@ContraseñaHash", hashedPassword);
                    cmd3.Parameters.AddWithValue("@Salt", salt);
                    cmd3.Parameters.AddWithValue("@FechaCambio", DateTime.Now);
                    cmd3.Parameters.AddWithValue("@HashSimple", hasher.HashSimple(obj.Contraseña));  // Aquí usamos el HashSimple para comparar las contraseñas simples
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
        public Resultado_456VG<BEUsuario_456VG> eliminarEntidad(BEUsuario_456VG obj)
        {
            throw new NotImplementedException();
        }
        public List<BEUsuario_456VG> leerEntidades()
        {
            List<BEUsuario_456VG> list = new List<BEUsuario_456VG>();
            string sqlQuery = "USE EnviosYA; SELECT * FROM Usuario";
            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos");
                using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
                {
                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            string dni = !lector.IsDBNull(lector.GetOrdinal("dni")) ? lector.GetString(lector.GetOrdinal("dni")) : string.Empty;
                            string name = !lector.IsDBNull(lector.GetOrdinal("nombre")) ? lector.GetString(lector.GetOrdinal("nombre")) : string.Empty;
                            string ape = !lector.IsDBNull(lector.GetOrdinal("apellido")) ? lector.GetString(lector.GetOrdinal("apellido")) : string.Empty;
                            string email = !lector.IsDBNull(lector.GetOrdinal("email")) ? lector.GetString(lector.GetOrdinal("email")) : string.Empty;
                            string tel = !lector.IsDBNull(lector.GetOrdinal("telefono")) ? lector.GetString(lector.GetOrdinal("telefono")) : string.Empty;
                            string nameuser = !lector.IsDBNull(lector.GetOrdinal("nombreusuario")) ? lector.GetString(lector.GetOrdinal("nombreusuario")) : string.Empty;
                            string dom = !lector.IsDBNull(lector.GetOrdinal("domicilio")) ? lector.GetString(lector.GetOrdinal("domicilio")) : string.Empty;
                            string rol = !lector.IsDBNull(lector.GetOrdinal("rol")) ? lector.GetString(lector.GetOrdinal("rol")) : string.Empty;
                            bool bloqueado = !lector.IsDBNull(lector.GetOrdinal("bloqueado")) && lector.GetBoolean(lector.GetOrdinal("bloqueado"));
                            bool activo = !lector.IsDBNull(lector.GetOrdinal("activo")) && lector.GetBoolean(lector.GetOrdinal("activo"));
                            BEUsuario_456VG user = new BEUsuario_456VG(dni, name, ape, email, tel, nameuser, dom, rol, bloqueado, activo);
                            list.Add(user);
                        }
                    }
                }
                bool result2 = db.Desconectar();
                if (!result2) throw new Exception("Error al desconectarse de la base de datos");
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                db.Desconectar();
                return null;
            }
        }
        //public Resultado_456VG<BEUsuario_456VG> recuperarUsuario(string DNI, string Contraseña)
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
        //                    bool esContraseñaValida = hasher.VerificarPassword(Contraseña, contraseñahash, saltAlmacenado);
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
        public Resultado_456VG<BEUsuario_456VG> recuperarUsuarioPorDNI(string DNI)
        {
            Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
            string sqlQuery = "USE EnviosYA; SELECT * FROM Usuario WHERE DNI = @DNI";
            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos");
                using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
                {
                    command.Parameters.AddWithValue("@DNI", DNI);
                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        if (!lector.HasRows)
                        {
                            throw new Exception("No se encontró un usuario con ese DNI");
                        }
                        if (lector.Read())
                        {
                            string dni = !lector.IsDBNull(0) ? lector.GetString(0) : "";
                            string nombre = !lector.IsDBNull(1) ? lector.GetString(1) : "";
                            string apellido = !lector.IsDBNull(2) ? lector.GetString(2) : "";
                            string email = !lector.IsDBNull(3) ? lector.GetString(3) : "";
                            string telefono = !lector.IsDBNull(4) ? lector.GetString(4) : string.Empty;
                            string nombreusuario = !lector.IsDBNull(5) ? lector.GetString(5) : "";
                            string contraseñahash = !lector.IsDBNull(6) ? lector.GetString(6) : "";
                            string saltAlmacenado = !lector.IsDBNull(7) ? lector.GetString(7) : "";
                            string domicilio = !lector.IsDBNull(8) ? lector.GetString(8) : "";
                            string rol = !lector.IsDBNull(9) ? lector.GetString(9) : "";
                            bool bloqueado = !lector.IsDBNull(10) && lector.GetBoolean(10);
                            bool activo = !lector.IsDBNull(11) && lector.GetBoolean(11);
                            BEUsuario_456VG usuario = new BEUsuario_456VG(dni, nombre, apellido, email, telefono, nombreusuario, contraseñahash, saltAlmacenado, domicilio, rol, bloqueado, activo);
                            resultado.resultado = true;
                            resultado.entidad = usuario;
                            resultado.mensaje = "Usuario encontrado correctamente";
                        }
                    }
                }
                db.Desconectar();
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
                resultado.entidad = null;
                db.Desconectar();
                return resultado;
            }
        }
        public Resultado_456VG<bool> bloquearUsuario(BEUsuario_456VG usuario)
        {
            Resultado_456VG<bool> resultado = new Resultado_456VG<bool>();
            string sqlQuery = "USE EnviosYA; UPDATE Usuario SET Bloqueado = 1, Activo = 0 WHERE DNI = @DNI";
            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos");
                using (SqlCommand command = new SqlCommand(sqlQuery, db.Connection))
                {
                    command.Parameters.AddWithValue("@DNI", usuario.DNI);
                    command.ExecuteNonQuery();
                }
                resultado.resultado = true;
                resultado.entidad = true;
                resultado.mensaje = "Usuario bloqueado correctamente";
                db.Desconectar();
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.entidad = false;
                resultado.mensaje = ex.Message;
                db.Desconectar();
                return resultado;
            }
        }
        public Resultado_456VG<bool> cambiarContraseña(BEUsuario_456VG usuario, string nuevaContraseña)
        {
            Resultado_456VG<bool> resultado = new Resultado_456VG<bool>();
            string sqlUpdateUsuario = "USE EnviosYA; UPDATE Usuario SET contraseña = @Contraseña, salt = @Salt WHERE DNI = @DNI";
            string sqlInsertHistorial = "USE EnviosYA; INSERT INTO HistorialContraseñas (dni, contraseñahash, salt, fechacambio, hashsimple) VALUES (@DniUsuario, @ContraseñaHash, @Salt, @FechaCambio, @HashSimple)";
            string sqlSelectHistorial = "USE EnviosYA; SELECT hashsimple FROM HistorialContraseñas WHERE dni = @DniUsuario";
            try
            {
                bool result = db.Conectar();
                if (!result) throw new Exception("Error al conectarse a la base de datos");
                string nuevoSalt = hasher.GenerarSalt();
                string nuevaContraseñaHasheada = hasher.HashPassword(nuevaContraseña, nuevoSalt);
                string nuevaContraseñaHashSimple = hasher.HashSimple(nuevaContraseña);
                // Validar que la nueva contraseña no haya sido usada antes
                using (SqlCommand selectCommand = new SqlCommand(sqlSelectHistorial, db.Connection))
                {
                    selectCommand.Parameters.AddWithValue("@DniUsuario", usuario.DNI);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string hashSimpleAntiguo = reader.GetString(0);
                            if (hashSimpleAntiguo == nuevaContraseñaHashSimple)
                            {
                                throw new Exception("La nueva contraseña ya fue utilizada anteriormente, por favor elija otra.");
                            }
                        }
                    }
                }
                using (SqlCommand insertCommand = new SqlCommand(sqlInsertHistorial, db.Connection))
                {
                    insertCommand.Parameters.AddWithValue("@DniUsuario", usuario.DNI);
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
                    updateCommand.Parameters.AddWithValue("@DNI", usuario.DNI);
                    int filasAfectadas = updateCommand.ExecuteNonQuery();
                    if (filasAfectadas == 0)
                    {
                        throw new Exception("No se encontró el usuario para actualizar la contraseña.");
                    }
                }
                resultado.resultado = true;
                resultado.entidad = true;
                resultado.mensaje = "Contraseña actualizada correctamente.";
                db.Desconectar();
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.entidad = false;
                resultado.mensaje = ex.Message;
                db.Desconectar();
                return resultado;
            }
        }
        public Resultado_456VG<BEUsuario_456VG> desbloquearUsuario(string dni)
        {
            Resultado_456VG<BEUsuario_456VG> resultado = new Resultado_456VG<BEUsuario_456VG>();
            try
            {
                bool conectado = db.Conectar();
                if (!conectado) throw new Exception("Error al conectar con la base de datos.");
                string query = "USE EnviosYA; UPDATE Usuario SET bloqueado = 0 WHERE dni = @DNI";
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@DNI", dni);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró un usuario con ese DNI.");
                    }
                }
                db.Desconectar();
                resultado.resultado = true;
                resultado.mensaje = "Usuario desbloqueado correctamente.";
            }
            catch (Exception ex)
            {
                db.Desconectar();
                resultado.resultado = false;
                resultado.mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
