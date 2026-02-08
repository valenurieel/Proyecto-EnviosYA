using _456VG_BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_DAL
{
    public class DALReporteInteligente_456VG
    {
        BasedeDatos_456VG db { get; }
        public DALReporteInteligente_456VG()
        {
            db = new BasedeDatos_456VG();
        }
        public List<BEReporteInteligente_456VG> ObtenerFacturacionMensual()
        {
            List<BEReporteInteligente_456VG> lista = new List<BEReporteInteligente_456VG>();
            string query = @"
                SELECT 
                    YEAR(f.fechaemision_456VG) AS Año,
                    MONTH(f.fechaemision_456VG) AS Mes,
                    SUM(e.importe_456VG) AS FacturacionMensual
                FROM Facturas_456VG f
                INNER JOIN Envios_456VG e 
                        ON f.codenvio_456VG = e.codenvio_456VG
                WHERE f.impreso_456VG = 1
                GROUP BY YEAR(f.fechaemision_456VG), MONTH(f.fechaemision_456VG)
                ORDER BY Año, Mes;
            ";
            try
            {
                db.Conectar456VG();
                SqlCommand cmd = new SqlCommand(query, db.Connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new BEReporteInteligente_456VG(
                            año: reader.GetInt32(0),
                            mes: reader.GetInt32(1),
                            factmen: reader.GetDecimal(2),
                            vs: 0,
                            anual: 0,
                            tende: "",
                            inactividad: 0,
                            refPeriodo: ""
                        ));
                    }
                }
            }
            finally
            {
                db.Desconectar456VG();
            }
            return lista;
        }
    }
}
