using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_DAL
{
    public class DALPaquete_456VG: ICrud_456VG<BEPaquete_456VG>
    {
        BasedeDatos_456VG db { get; }
        HashSHA256_456VG hasher { get; set; }
        public DALPaquete_456VG()
        {
            db = new BasedeDatos_456VG();
            hasher = new HashSHA256_456VG();
        }
        public Resultado_456VG<BEPaquete_456VG> actualizarEntidad456VG(BEPaquete_456VG obj)
        {
            throw new Exception();
        }
        public Resultado_456VG<BEPaquete_456VG> crearEntidad456VG(BEPaquete_456VG obj)
        {
            throw new Exception();
        }

        public Resultado_456VG<BEPaquete_456VG> eliminarEntidad456VG(BEPaquete_456VG obj)
        {
            throw new Exception();
        }
        public List<BEPaquete_456VG> leerEntidades456VG()
        {
            throw new Exception();
        }
    }
}
