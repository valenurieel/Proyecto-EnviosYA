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
    public class DALSeguimientoEnvío_456VG
    {
        BasedeDatos_456VG db { get; }
        public DALSeguimientoEnvío_456VG() { db = new BasedeDatos_456VG(); }

        public bool ExistePorEnvioImpreso456VG(string codEnvio)
        {
            if (string.IsNullOrWhiteSpace(codEnvio)) return false;
            var db = new BasedeDatos_456VG();
            try
            {
                db.Conectar456VG();
                var sql = @"
                    SELECT TOP 1 1
                    FROM dbo.Seguimientos_456VG
                    WHERE codenvio_456VG = @cod AND impreso_456VG = 1;";
                var cmd = new SqlCommand(sql, db.Connection);
                cmd.Parameters.AddWithValue("@cod", codEnvio);
                var res = cmd.ExecuteScalar();
                return res != null;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Desconectar456VG();
            }
        }
        public string ObtenerCodSeguimientoNoImpresoPorEnvio(string codEnvio)
        {
            var db = new BasedeDatos_456VG();
            try
            {
                db.Conectar456VG();
                var sql = @"SELECT TOP 1 codseguimiento_456VG
                    FROM dbo.Seguimientos_456VG
                    WHERE codenvio_456VG = @cod AND impreso_456VG = 0;";
                var cmd = new SqlCommand(sql, db.Connection);
                cmd.Parameters.AddWithValue("@cod", codEnvio);
                var res = cmd.ExecuteScalar();
                return res == null ? null : res.ToString();
            }
            finally { db.Desconectar456VG(); }
        }
        public Resultado_456VG<BESeguimientoEnvío_456VG> CrearParaEnvio(string codEnvio)
        {
            var r = new Resultado_456VG<BESeguimientoEnvío_456VG>();
            if (string.IsNullOrWhiteSpace(codEnvio))
            {
                r.resultado = false; r.mensaje = "Código de envío inválido.";
                return r;
            }
            bool pagado = false;
            string dniCli = null, dniDest = null, nomDest = null, apeDest = null, telDest = null;
            float codPostal = 0f;
            string dom = null, loc = null, prov = null, tipoEnvio = null;
            decimal importe = 0m;
            try
            {
                db.Connection.Open();
                const string sqlEnvio = @"
                USE EnviosYA_456VG;
                SELECT e.pagado_456VG,
                       e.dni_cli_456VG,
                       e.dni_dest_456VG,
                       e.nombre_dest_456VG,
                       e.apellido_dest_456VG,
                       e.telefono_dest_456VG,
                       e.codpostal_456VG,
                       e.domicilio_456VG,
                       e.localidad_456VG,
                       e.provincia_456VG,
                       e.tipoenvio_456VG,
                       e.importe_456VG
                FROM Envios_456VG e
                WHERE e.codenvio_456VG = @CodEnvio;";
                using (var cmd = new SqlCommand(sqlEnvio, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@CodEnvio", codEnvio.Trim());
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            r.resultado = false; r.mensaje = "El envío no existe.";
                            return r;
                        }
                        pagado = dr.GetBoolean(0);
                        dniCli = dr.GetString(1);
                        dniDest = dr.GetString(2);
                        nomDest = dr.GetString(3);
                        apeDest = dr.GetString(4);
                        telDest = dr.GetString(5);
                        codPostal = Convert.ToSingle(dr.GetDouble(6));
                        dom = dr.GetString(7);
                        loc = dr.GetString(8);
                        prov = dr.GetString(9);
                        tipoEnvio = dr.GetString(10);
                        importe = dr.GetDecimal(11);
                    }
                }
                if (!pagado)
                {
                    r.resultado = false; r.mensaje = "El envío no está pagado.";
                    return r;
                }
                const string sqlFacturaImpresa = @"
                USE EnviosYA_456VG;
                SELECT COUNT(1)
                FROM Facturas_456VG
                WHERE codenvio_456VG = @CodEnvio AND impreso_456VG = 1;";
                using (var cmd = new SqlCommand(sqlFacturaImpresa, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@CodEnvio", codEnvio.Trim());
                    var ok = (int)cmd.ExecuteScalar() > 0;
                    if (!ok)
                    {
                        r.resultado = false; r.mensaje = "No hay factura impresa para este envío.";
                        return r;
                    }
                }
                const string sqlSeg = @"
                USE EnviosYA_456VG;
                SELECT TOP 1 codseguimiento_456VG, fechaemitido_456VG, impreso_456VG
                FROM Seguimientos_456VG
                WHERE codenvio_456VG = @CodEnvio
                ORDER BY fechaemitido_456VG DESC;";
                string codSegExistente = null;
                DateTime fechaSegExistente = default;
                bool impreso = false;
                using (var cmd = new SqlCommand(sqlSeg, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@CodEnvio", codEnvio.Trim());
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            codSegExistente = dr.GetString(0);
                            fechaSegExistente = dr.GetDateTime(1);
                            impreso = dr.GetBoolean(2);
                        }
                    }
                }
                var clienteBE = new BECliente_456VG { DNI456VG = dniCli };
                var envioBE = new BEEnvío_456VG(
                    codEnvio.Trim(),
                    clienteBE,
                    new List<BEPaquete_456VG>(),
                    dniDest, nomDest, apeDest, telDest,
                    codPostal, dom, loc, prov, tipoEnvio,
                    pagado, importe
                );
                if (codSegExistente != null && impreso)
                {
                    r.resultado = false; r.mensaje = "El seguimiento ya fue impreso.";
                    return r;
                }
                if (codSegExistente != null)
                {
                    r.resultado = true;
                    r.entidad = new BESeguimientoEnvío_456VG(envioBE, codSegExistente, fechaSegExistente, false);
                    r.mensaje = "Seguimiento ya existente (no impreso).";
                    return r;
                }
                var seg = new BESeguimientoEnvío_456VG(envioBE, DateTime.Now, false);
                const string sqlInsert = @"
                USE EnviosYA_456VG;
                INSERT INTO Seguimientos_456VG (codseguimiento_456VG, codenvio_456VG, fechaemitido_456VG, impreso_456VG)
                VALUES (@CodSeg, @CodEnvio, @Fecha, 0);";
                using (var cmd = new SqlCommand(sqlInsert, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@CodSeg", seg.CodSeguimientoEnvío456VG);
                    cmd.Parameters.AddWithValue("@CodEnvio", codEnvio.Trim());
                    cmd.Parameters.AddWithValue("@Fecha", seg.FechaEmitido456VG);
                    cmd.ExecuteNonQuery();
                }
                r.resultado = true; r.entidad = seg; r.mensaje = "Seguimiento creado.";
                return r;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    var ya = LeerPorEnvio(codEnvio);
                    if (ya != null && !ya.Impreso456VG)
                    {
                        return new Resultado_456VG<BESeguimientoEnvío_456VG>
                        {
                            resultado = true,
                            entidad = ya,
                            mensaje = "Seguimiento ya existente (no impreso)."
                        };
                    }
                }
                return new Resultado_456VG<BESeguimientoEnvío_456VG> { resultado = false, mensaje = ex.Message };
            }
            catch (Exception ex)
            {
                return new Resultado_456VG<BESeguimientoEnvío_456VG> { resultado = false, mensaje = ex.Message };
            }
            finally
            {
                db.Connection.Close();
            }
        }
        public List<string> LeerCodEnviosElegibles()
        {
            var lista = new List<string>();
            try
            {
                db.Connection.Open();
                const string sql = @"
                USE EnviosYA_456VG;
                SELECT e.codenvio_456VG
                FROM Envios_456VG e
                WHERE e.pagado_456VG = 1
                  AND EXISTS (SELECT 1 FROM Facturas_456VG f WHERE f.codenvio_456VG = e.codenvio_456VG AND f.impreso_456VG = 1)
                  AND NOT EXISTS (SELECT 1 FROM Seguimientos_456VG s WHERE s.codenvio_456VG = e.codenvio_456VG AND s.impreso_456VG = 1);";
                using (var cmd = new SqlCommand(sql, db.Connection))
                using (var dr = cmd.ExecuteReader())
                    while (dr.Read())
                        lista.Add(dr.GetString(0));
            }
            finally { db.Connection.Close(); }
            return lista;
        }
        public bool MarcarImpresoPorEnvio(string codEnvio)
        {
            if (string.IsNullOrWhiteSpace(codEnvio)) return false;
            try
            {
                db.Connection.Open();
                const string sql = @"
                USE EnviosYA_456VG;
                UPDATE Seguimientos_456VG
                SET impreso_456VG = 1
                WHERE codenvio_456VG = @CodEnvio AND impreso_456VG = 0;
                SELECT @@ROWCOUNT;";
                using (var cmd = new SqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@CodEnvio", codEnvio.Trim());
                    var rows = (int)cmd.ExecuteScalar();
                    return rows > 0;
                }
            }
            catch { return false; }
            finally { db.Connection.Close(); }
        }
        private BESeguimientoEnvío_456VG LeerPorEnvio(string codEnvio)
        {
            try
            {
                if (db.Connection.State != System.Data.ConnectionState.Open)
                    db.Connection.Open();
                const string sqlSeg = @"
                USE EnviosYA_456VG;
                SELECT TOP 1 codseguimiento_456VG, fechaemitido_456VG, impreso_456VG
                FROM Seguimientos_456VG
                WHERE codenvio_456VG = @CodEnvio
                ORDER BY fechaemitido_456VG DESC;";
                string codSeg = null; DateTime fecha = default; bool impreso = false;
                using (var cmd = new SqlCommand(sqlSeg, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@CodEnvio", codEnvio.Trim());
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read()) return null;
                        codSeg = dr.GetString(0);
                        fecha = dr.GetDateTime(1);
                        impreso = dr.GetBoolean(2);
                    }
                }
                const string sqlEnvio = @"
                USE EnviosYA_456VG;
                SELECT e.pagado_456VG,
                       e.dni_cli_456VG,
                       e.dni_dest_456VG,
                       e.nombre_dest_456VG,
                       e.apellido_dest_456VG,
                       e.telefono_dest_456VG,
                       e.codpostal_456VG,
                       e.domicilio_456VG,
                       e.localidad_456VG,
                       e.provincia_456VG,
                       e.tipoenvio_456VG,
                       e.importe_456VG
                FROM Envios_456VG e
                WHERE e.codenvio_456VG = @CodEnvio;";
                bool pagado; string dniCli, dniDest, nomDest, apeDest, telDest;
                float codPostal; string dom, loc, prov, tipoEnvio; decimal importe;
                using (var cmd = new SqlCommand(sqlEnvio, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@CodEnvio", codEnvio.Trim());
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read()) return null;
                        pagado = dr.GetBoolean(0);
                        dniCli = dr.GetString(1);
                        dniDest = dr.GetString(2);
                        nomDest = dr.GetString(3);
                        apeDest = dr.GetString(4);
                        telDest = dr.GetString(5);
                        codPostal = Convert.ToSingle(dr.GetDouble(6));
                        dom = dr.GetString(7);
                        loc = dr.GetString(8);
                        prov = dr.GetString(9);
                        tipoEnvio = dr.GetString(10);
                        importe = dr.GetDecimal(11);
                    }
                }
                var clienteBE = new BECliente_456VG { DNI456VG = dniCli };
                var envioBE = new BEEnvío_456VG(
                    codEnvio.Trim(),
                    clienteBE,
                    new List<BEPaquete_456VG>(),
                    dniDest, nomDest, apeDest, telDest,
                    codPostal, dom, loc, prov, tipoEnvio,
                    pagado, importe
                );
                return new BESeguimientoEnvío_456VG(envioBE, codSeg, fecha, impreso);
            }
            catch { return null; }
            finally { db.Connection.Close(); }
        }
    }
}