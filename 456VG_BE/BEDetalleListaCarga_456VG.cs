using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEDetalleListaCarga_456VG
    {
        public string CodDetListaCarga456VG { get; set; }
        public BEListaCarga_456VG Lista { get; set; }
        public BEEnvío_456VG Envio { get; set; }
        public List<BEPaquete_456VG> Paquetes { get; set; }
        public int CantPaquetes456VG { get; set; }
        public string EstadoCargado { get; set; } = "Pendiente";
        public BEDetalleListaCarga_456VG(string cod, BEListaCarga_456VG lista, BEEnvío_456VG env, List<BEPaquete_456VG> paquetes, int cantpaq, string estado)
        {
            this.CodDetListaCarga456VG = cod;
            this.Lista = lista;
            this.Envio = env;
            this.Paquetes = paquetes;
            this.CantPaquetes456VG = cantpaq;
            this.EstadoCargado = estado;
        }
        public BEDetalleListaCarga_456VG(BEListaCarga_456VG lista, BEEnvío_456VG env, List<BEPaquete_456VG> paquetes, int cantpaq, string estado)
        {
            this.Lista = lista;
            this.Envio = env;
            this.Paquetes = paquetes;
            this.CantPaquetes456VG = cantpaq;
            this.EstadoCargado = estado;
            this.CodDetListaCarga456VG = GenerateCodDetalleLista456VG(lista);
        }
        private string GenerateCodDetalleLista456VG(BEListaCarga_456VG lista)
        {
            string prefijo = "0000";
            if (lista != null && !string.IsNullOrEmpty(lista.CodLista456VG))
            {
                prefijo = lista.CodLista456VG.Length >= 4
                    ? lista.CodLista456VG.Substring(0, 4)
                    : lista.CodLista456VG.PadRight(4, '0');
            }
            string stamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return prefijo + stamp;
        }
    }
}
