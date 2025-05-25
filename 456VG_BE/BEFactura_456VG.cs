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
        public string DNICli456VG { get; set; }
        public string NombreCli456VG { get; set; }
        public string ApellidoCli456VG { get; set; }
        public string CodPaq456VG { get; set; }
        public string Domicilio456VG { get; set; }
        public string Localidad456VG { get; set; }
        public string Provincia456VG { get; set; }
        public float CodPostal456VG { get; set; }
        public float Importe456VG { get; set; }
        public DateTime FechaEmision456VG { get; set; }
        public BEFactura_456VG(int id, string dnicli, string namecli, string apecli, string codpaq, string domi, string loca, string prov, float codpost, float impo, DateTime fecha)
        {
            this.id_factura456VG = id;
            this.DNICli456VG = dnicli;
            this.NombreCli456VG = namecli;
            this.ApellidoCli456VG = apecli;
            this.CodPostal456VG = codpost;
            this.Domicilio456VG = domi;
            this.Localidad456VG = loca;
            this.Provincia456VG = prov;
            this.CodPaq456VG = codpaq;
            this.Importe456VG = impo;
            this.FechaEmision456VG = fecha;
        }
        public BEFactura_456VG(string dnicli, string namecli, string apecli, string codpaq, string domi, string loca, string prov, float codpost, float impo, DateTime fecha)
        {
            this.DNICli456VG = dnicli;
            this.NombreCli456VG = namecli;
            this.ApellidoCli456VG = apecli;
            this.CodPostal456VG = codpost;
            this.Domicilio456VG = domi;
            this.Localidad456VG = loca;
            this.Provincia456VG = prov;
            this.CodPaq456VG = codpaq;
            this.Importe456VG = impo;
            this.FechaEmision456VG = fecha;
        }
    }
}
