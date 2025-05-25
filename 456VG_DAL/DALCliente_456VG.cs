using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
