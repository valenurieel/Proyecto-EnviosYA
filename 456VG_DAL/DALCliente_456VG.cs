using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _456VG_DAL
{
    public class DALCliente_456VG:ICrud_456VG<BECliente_456VG>
    {
        BasedeDatos_456VG db { get; }
        HashSHA256_456VG hasher { get; set; }
        public DALCliente_456VG()
        {
            db = new BasedeDatos_456VG();
            hasher = new HashSHA256_456VG();
        }
        public Resultado_456VG<BECliente_456VG> actualizarEntidad456VG(BECliente_456VG obj)
        {
            throw new Exception();
        }
        public Resultado_456VG<BECliente_456VG> crearEntidad456VG(BECliente_456VG obj)
        {
            throw new Exception();
        }

        public Resultado_456VG<BECliente_456VG> eliminarEntidad456VG(BECliente_456VG obj)
        {
            throw new Exception();
        }
        public List<BECliente_456VG> leerEntidades456VG()
        {
            throw new Exception();
        }
        public Resultado_456VG<BECliente_456VG> ObtenerClientePorDNI456VG(string DNI)
        {
            Resultado_456VG<BECliente_456VG> resultado = new Resultado_456VG<BECliente_456VG>();
            List<BECliente_456VG> list = new List<BECliente_456VG>();
            string sqlQuery = "USE EnviosYA_456VG; SELECT * FROM Clientes_456VG WHERE dni_456VG = @DNI";
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
                            string dni = !lector.IsDBNull(lector.GetOrdinal("dni_456VG")) ? lector.GetInt32(lector.GetOrdinal("dni_456VG")).ToString() : string.Empty;
                            string nombre = !lector.IsDBNull(lector.GetOrdinal("nombre_456VG")) ? lector.GetString(lector.GetOrdinal("nombre_456VG")) : string.Empty;
                            string apellido = !lector.IsDBNull(lector.GetOrdinal("apellido_456VG")) ? lector.GetString(lector.GetOrdinal("apellido_456VG")) : string.Empty;
                            string telefono = !lector.IsDBNull(lector.GetOrdinal("telefono_456VG")) ? lector.GetString(lector.GetOrdinal("telefono_456VG")) : string.Empty;
                            string domicilio = !lector.IsDBNull(lector.GetOrdinal("domicilio_456VG")) ? lector.GetString(lector.GetOrdinal("domicilio_456VG")) : string.Empty;
                            DateTime fechanac = !lector.IsDBNull(lector.GetOrdinal("fechanacimiento_456VG")) ? lector.GetDateTime(lector.GetOrdinal("fechanacimiento_456VG")) : DateTime.MinValue;
                            BECliente_456VG cliente = new BECliente_456VG(dni, nombre, apellido, telefono, domicilio, fechanac);
                            list.Add(cliente);
                        }
                    }
                }
                db.Desconectar456VG();
                if (list.Count > 0)
                {
                    resultado.resultado = true;
                    resultado.entidad = list[0];
                    resultado.mensaje = "Cliente encontrado correctamente";
                }
                else
                {
                    resultado.resultado = false;
                    resultado.mensaje = null;
                    resultado.entidad = null;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.resultado = false;
                resultado.mensaje = $"Error al buscar cliente: {ex.Message}";
                resultado.entidad = null;
                return resultado;
            }
        }
    }
}
