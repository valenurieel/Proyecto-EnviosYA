using _456VG_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_DAL
{
    public class DALDetalleListaCarga_456VG
    {
        public bool InsertarDetalle456VG(BEDetalleListaCarga_456VG det)
        {
            foreach (var paquete in det.Paquetes)
            {
                string query = $@"
                INSERT INTO DetalleListaCarga_456VG (
                coddetalle_456VG, codlista_456VG, codenvio_456VG,
                codpaq_456VG, estado_456VG
                ) VALUES (
                '{det.CodDetListaCarga456VG}', '{det.Lista.CodLista456VG}', '{det.Envio.CodEnvio456VG}',
                '{paquete.CodPaq456VG}', '{det.EstadoCargado}'
                );";
                if (!new BasedeDatos_456VG().ejecutarQuery456VG(query))
                    return false;
            }
            return true;
        }
        public List<BEDetalleListaCarga_456VG> ObtenerDetalles456VG()
        {
            return new List<BEDetalleListaCarga_456VG>();
        }
    }
}
