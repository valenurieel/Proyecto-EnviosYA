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
            INSERT INTO Usuario (dni, nombre, apellido, fecha_nacimiento, telefono, contraseña, salt, domicilio)
            VALUES (@DNI, @Nombre, @Apellido, @FechaNacimiento, @Telefono, @Contraseña, @Salt, @Domicilio);";
                using (SqlCommand cmd2 = new SqlCommand(queryToCreateUser, db.Connection, trans))
                {
                    cmd2.Parameters.AddWithValue("@DNI", obj.DNI);
                    cmd2.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd2.Parameters.AddWithValue("@Apellido", obj.Apellido);
                    cmd2.Parameters.AddWithValue("@FechaNacimiento", obj.Fecha_nacimiento);
                    cmd2.Parameters.AddWithValue("@Telefono", obj.Teléfono);
                    cmd2.Parameters.AddWithValue("@Contraseña", hashedPassword);
                    cmd2.Parameters.AddWithValue("@Salt", salt);
                    cmd2.Parameters.AddWithValue("@Domicilio", obj.Domicilio);
                    cmd2.ExecuteNonQuery();
                }
                trans.Commit();
                resultado.resultado = true;
                resultado.mensaje = "Usuario creado correctamente.";
                resultado.entidad = null;
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
            throw new NotImplementedException();
        }
        public Resultado_456VG<BEUsuario_456VG> recuperarUsuario(string DNI, string Contraseña)
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
                            DateTime fecha_nacimiento = !lector.IsDBNull(3) ? lector.GetDateTime(3) : DateTime.Now;
                            string telefono = !lector.IsDBNull(4) ? lector.GetString(4) : string.Empty;
                            string contraseñahash = !lector.IsDBNull(5) ? lector.GetString(5) : "";
                            string saltAlmacenado = !lector.IsDBNull(6) ? lector.GetString(6) : "";
                            string domicilio = !lector.IsDBNull(7) ? lector.GetString(7) : "";
                            // Verificar contraseña usando SHA-256
                            bool esContraseñaValida = hasher.VerificarPassword(Contraseña, contraseñahash, saltAlmacenado);
                            if (!esContraseñaValida)
                            {
                                throw new Exception("Contraseña incorrecta");
                            }
                            BEUsuario_456VG usuario = new BEUsuario_456VG(dni, nombre, apellido, fecha_nacimiento, telefono, contraseñahash, saltAlmacenado, domicilio);
                            resultado.resultado = true;
                            resultado.entidad = usuario;
                            resultado.mensaje = "Inicio de sesión correcto";
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
    }
}
