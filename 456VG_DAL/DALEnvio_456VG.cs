using _456VG_BE;
using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _456VG_DAL
{
    public class DALEnvio_456VG: ICrud_456VG<BEEnvío_456VG>
    {
        BasedeDatos_456VG db { get; }
        HashSHA256_456VG hasher { get; set; }
        public DALEnvio_456VG()
        {
            db = new BasedeDatos_456VG();
            hasher = new HashSHA256_456VG();
        }
        public Resultado_456VG<BEEnvío_456VG> actualizarEntidad456VG(BEEnvío_456VG obj)
        {
            throw new Exception();
        }
        public Resultado_456VG<BEEnvío_456VG> crearEntidad456VG(BEEnvío_456VG obj)
        {
            throw new Exception();
        }

        public Resultado_456VG<BEEnvío_456VG> eliminarEntidad456VG(BEEnvío_456VG obj)
        {
            throw new Exception();
        }
        public List<BEEnvío_456VG> leerEntidades456VG()
        {
            throw new Exception();
        }
    }
}
