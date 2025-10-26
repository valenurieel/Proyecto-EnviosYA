using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _456VG_DAL
{
    public class DALTransporte_456VG : ICrud_456VG<BETransporte_456VG>
    {
        BasedeDatos_456VG db { get; }
        public DALTransporte_456VG()
        {
            db = new BasedeDatos_456VG();
        }
        public Resultado_456VG<BETransporte_456VG> crearEntidad456VG(BETransporte_456VG obj)
        {
            var r = new Resultado_456VG<BETransporte_456VG>();
            try
            {
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    const string sql =
                        "USE EnviosYA_456VG; " +
                        "INSERT INTO Transportes_456VG " +
                        "(patente_456VG, marca_456VG, año_456VG, capacidad_peso_456VG, capacidad_volumen_456VG, disponible_456VG, activo_456VG) " +
                        "VALUES (@pat, @marca, @anio, @cpeso, @cvol, @disp, @act);";
                    using (var cmd = new SqlCommand(sql, db.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@pat", obj.Patente456VG);
                        cmd.Parameters.AddWithValue("@marca", obj.Marca456VG);
                        cmd.Parameters.AddWithValue("@anio", obj.Año456VG);
                        cmd.Parameters.AddWithValue("@cpeso", obj.CapacidadPeso456VG);
                        cmd.Parameters.AddWithValue("@cvol", obj.CapacidadVolumen456VG);
                        cmd.Parameters.AddWithValue("@disp", obj.Disponible456VG);
                        cmd.Parameters.AddWithValue("@act", obj.Activo456VG);
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                r.resultado = true; r.entidad = obj; r.mensaje = "Transporte creado correctamente.";
            }
            catch (Exception ex)
            {
                r.resultado = false; r.mensaje = ex.Message;
            }
            finally { db.Connection.Close(); }
            return r;
        }
        public Resultado_456VG<BETransporte_456VG> actualizarEntidad456VG(BETransporte_456VG obj)
        {
            var r = new Resultado_456VG<BETransporte_456VG>();
            try
            {
                if (!db.Conectar456VG()) throw new Exception("No se pudo conectar a la base.");
                const string sql =
                    "USE EnviosYA_456VG; " +
                    "UPDATE Transportes_456VG SET " +
                    " marca_456VG = @marca, año_456VG = @anio, " +
                    " capacidad_peso_456VG = @cpeso, capacidad_volumen_456VG = @cvol, " +
                    " disponible_456VG = @disp " +
                    "WHERE patente_456VG = @pat;";
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@marca", obj.Marca456VG);
                    cmd.Parameters.AddWithValue("@anio", obj.Año456VG);
                    cmd.Parameters.AddWithValue("@cpeso", obj.CapacidadPeso456VG);
                    cmd.Parameters.AddWithValue("@cvol", obj.CapacidadVolumen456VG);
                    cmd.Parameters.AddWithValue("@disp", obj.Disponible456VG);
                    cmd.Parameters.AddWithValue("@pat", obj.Patente456VG);
                    int rows = cmd.ExecuteNonQuery();
                    r.resultado = rows > 0;
                    r.mensaje = rows > 0 ? "Transporte actualizado." : "No se encontró la patente.";
                    r.entidad = rows > 0 ? obj : null;
                }
            }
            catch (Exception ex)
            {
                r.resultado = false; r.mensaje = ex.Message;
            }
            finally { db.Desconectar456VG(); }
            return r;
        }
        public Resultado_456VG<BETransporte_456VG> eliminarEntidad456VG(BETransporte_456VG obj)
        {
            throw new NotImplementedException();
        }
        public List<BETransporte_456VG> leerEntidades456VG()
        {
            var lista = new List<BETransporte_456VG>();
            const string sql =
                "USE EnviosYA_456VG; " +
                "SELECT patente_456VG, marca_456VG, año_456VG, capacidad_peso_456VG, capacidad_volumen_456VG, disponible_456VG, activo_456VG " +
                "FROM Transportes_456VG;";
            try
            {
                if (!db.Conectar456VG()) throw new Exception("No se pudo conectar.");
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var t = new BETransporte_456VG(
                            rd.GetString(rd.GetOrdinal("patente_456VG")),
                            rd.GetString(rd.GetOrdinal("marca_456VG")),
                            rd.GetInt32(rd.GetOrdinal("año_456VG")),
                            Convert.ToSingle(rd.GetDouble(rd.GetOrdinal("capacidad_peso_456VG"))),
                            Convert.ToSingle(rd.GetDouble(rd.GetOrdinal("capacidad_volumen_456VG"))),
                            rd.GetBoolean(rd.GetOrdinal("disponible_456VG")),
                            rd.GetBoolean(rd.GetOrdinal("activo_456VG"))
                        );
                        lista.Add(t);
                    }
                }
            }
            catch
            {
                lista = new List<BETransporte_456VG>();
            }
            finally { db.Desconectar456VG(); }
            return lista;
        }
        public Resultado_456VG<BETransporte_456VG> ActDesacTrans456(string patente, bool nuevoEstadoActivo)
        {
            var r = new Resultado_456VG<BETransporte_456VG>();
            try
            {
                if (!db.Conectar456VG()) throw new Exception("No se pudo conectar.");
                const string sql = "USE EnviosYA_456VG; UPDATE Transportes_456VG SET activo_456VG = @act WHERE patente_456VG = @pat;";
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@act", nuevoEstadoActivo);
                    cmd.Parameters.AddWithValue("@pat", patente);
                    int rows = cmd.ExecuteNonQuery();
                    r.resultado = rows > 0;
                    r.mensaje = rows > 0 ? "Transporte actualizado." : "No se encontró la patente.";
                }
            }
            catch (Exception ex)
            {
                r.resultado = false; r.mensaje = ex.Message;
            }
            finally { db.Desconectar456VG(); }
            return r;
        }
        public Resultado_456VG<BETransporte_456VG> ObtenerTransportePorPatente456VG(string patente)
        {
            var r = new Resultado_456VG<BETransporte_456VG>();
            const string sql =
                "USE EnviosYA_456VG; " +
                "SELECT patente_456VG, marca_456VG, año_456VG, capacidad_peso_456VG, capacidad_volumen_456VG, disponible_456VG, activo_456VG " +
                "FROM Transportes_456VG WHERE patente_456VG = @pat;";
            try
            {
                if (!db.Conectar456VG()) throw new Exception("No se pudo conectar.");
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@pat", patente);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            var t = new BETransporte_456VG(
                                rd.GetString(0),
                                rd.GetString(1),
                                rd.GetInt32(2),
                                Convert.ToSingle(rd.GetDouble(3)),
                                Convert.ToSingle(rd.GetDouble(4)),
                                rd.GetBoolean(5),
                                rd.GetBoolean(6)
                            );
                            r.resultado = true; r.entidad = t; r.mensaje = "Transporte encontrado.";
                        }
                        else
                        {
                            r.resultado = false; r.mensaje = "No existe transporte con esa patente.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                r.resultado = false; r.mensaje = ex.Message;
            }
            finally { db.Desconectar456VG(); }
            return r;
        }
    }
}
