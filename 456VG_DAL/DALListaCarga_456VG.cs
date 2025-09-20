using _456VG_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_DAL
{
    public class DALListaCarga_456VG
    {
        public bool InsertarLista456VG(BEListaCarga_456VG lista)
        {
            string query = $@"
            INSERT INTO ListaCarga_456VG (
            codlista_456VG, fechacreacion_456VG, tipozona_456VG,
            tipoenvio_456VG, cantenvios_456VG, cantpaquetes_456VG,
            pesototal_456VG, volumentotal_456VG,
            dni_chofer_456VG, patente_456VG,
            fechasalida_456VG, estadolista_456VG
            ) VALUES (
            '{lista.CodLista456VG}', '{lista.FechaCreacion456VG:yyyy-MM-dd HH:mm:ss}', '{lista.TipoZona456VG}',
            '{lista.TipoEnvio456VG}', {lista.CantEnvios456VG}, {lista.CantPaquetes456VG},
            {lista.PesoTotal456VG.ToString(System.Globalization.CultureInfo.InvariantCulture)},
            {lista.VolumenTotal456VG.ToString(System.Globalization.CultureInfo.InvariantCulture)},
            '{lista.Chofer.DNIChofer456VG}', '{lista.Transporte.Patente456VG}',
            '{lista.FechaSalida456VG:yyyy-MM-dd}', '{lista.EstadoLista456VG}'
            );";
            return new BasedeDatos_456VG().ejecutarQuery456VG(query);
        }
        public List<BEListaCarga_456VG> ObtenerListas456VG()
        {
            // Dejarlo preparado si querés implementar lectura después
            return new List<BEListaCarga_456VG>();
        }
    }
}
