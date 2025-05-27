using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEFactura_456VG
    {
        public int id_factura456VG { get; set; }
        public int id_envio456VG { get; set; }
        public BEEnvío_456VG Envio { get; set; }
        public int id_paquete456VG { get; set; }
        public BEPaquete_456VG Paquete { get; set; }
        public string DNICli456VG { get; set; }
        public BECliente_456VG Cliente { get; set; }
        public DateTime FechaEmision456VG { get; set; }
        public BEFactura_456VG(int id, int idenv, int idpaq, string dnicli, DateTime fecha)
        {
            this.id_factura456VG = id;
            this.id_envio456VG = idenv;
            this.id_paquete456VG = idpaq;
            this.DNICli456VG = dnicli;
            this.FechaEmision456VG = fecha;
        }
        public BEFactura_456VG(int idenv, int idpaq, string dnicli, DateTime fecha)
        {
            this.id_envio456VG = idenv;
            this.id_paquete456VG = idpaq;
            this.DNICli456VG = dnicli;
            this.FechaEmision456VG = fecha;
        }
    }
}
