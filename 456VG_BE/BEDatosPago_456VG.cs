using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEDatosPago_456VG
    {
        public BECliente_456VG Cliente_456VG { get; set; }
        public string MedioPago456VG { get; set; }
        public string NumTarjeta { get; set; }
        public string Titular { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string CVC { get; set; }
        public BEDatosPago_456VG(BECliente_456VG cliente, string mediopago, string numtarj, string titu, DateTime fvenc, string cvc)
        {
            this.Cliente_456VG = cliente;
            this.MedioPago456VG = mediopago;
            this.NumTarjeta = numtarj;
            this.Titular = titu;
            this.FechaVencimiento = fvenc;
            this.CVC = cvc;
        }
    }
}
