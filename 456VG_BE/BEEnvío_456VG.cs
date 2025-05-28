using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEEnvío_456VG
    {
        public int id_envio456VG { get; set; }
        public int id_paquete456VG { get; set; }
        public BEPaquete_456VG Paquete { get; set; }
        public string DNICli456VG { get; set; }
        public BECliente_456VG Cliente { get; set; }
        public string DNIDest456VG { get; set; }
        public string NombreDest456VG { get; set; }
        public string ApellidoDest456VG { get; set; }
        public string TeléfonoDest456VG { get; set; }
        public float CodPostal456VG { get; set; }
        public string Domicilio456VG { get; set; }
        public string Localidad456VG { get; set; }
        public string Provincia456VG { get; set; }
        public string tipoenvio456VG { get; set; }
        public decimal Importe456VG { get; set; }
        public bool Pagado456VG { get; set; }

        public BEEnvío_456VG(int id, int idpaq, string dnicli, string dnidest, string namedest, string apedest, string teldest, float cp, string dom, string loc, string prov, string tenvio, decimal impo, bool pagado)
        {
            this.id_envio456VG = id;
            this.id_paquete456VG = idpaq;
            this.DNICli456VG = dnicli;
            this.DNIDest456VG = dnidest;
            this.NombreDest456VG = namedest;
            this.ApellidoDest456VG = apedest;
            this.TeléfonoDest456VG = teldest;
            this.CodPostal456VG = cp;
            this.Domicilio456VG = dom;
            this.Localidad456VG = loc;
            this.Provincia456VG = prov;
            this.tipoenvio456VG = tenvio;
            this.Importe456VG = impo;
            this.Pagado456VG = pagado;
        }
        public BEEnvío_456VG(int idpaq, string dnicli, string dnidest, string namedest, string apedest, string teldest, float cp, string dom, string loc, string prov, string tenvio, decimal impo, bool pagado)
        {
            this.id_paquete456VG = idpaq;
            this.DNICli456VG = dnicli;
            this.DNIDest456VG = dnidest;
            this.NombreDest456VG = namedest;
            this.ApellidoDest456VG = apedest;
            this.TeléfonoDest456VG = teldest;
            this.CodPostal456VG = cp;
            this.Domicilio456VG = dom;
            this.Localidad456VG = loc;
            this.Provincia456VG = prov;
            this.tipoenvio456VG = tenvio;
            this.Importe456VG = impo;
            this.Pagado456VG = pagado;
        }
        public decimal CalcularImporte456VG()
        {
            float volumen = (Paquete.Alto456VG * Paquete.Ancho456VG * Paquete.Largo456VG) / 1000f;
            float basePeso = Paquete.Peso456VG * 10f;
            float baseVolumen = volumen * 0.5f;
            float tarifaBase = 500f;
            float factorZona = 1.0f;
            string prov = Provincia456VG?.ToLower() ?? "";
            string loc = Localidad456VG?.ToLower() ?? "";
            if (prov.Contains("tierra del fuego") || prov.Contains("neuquén"))
                factorZona = 1.5f;
            else if (prov.Contains("buenos aires") && !loc.Contains("capital"))
                factorZona = 1.2f;
            else if (loc.Contains("caba") || loc.Contains("capital federal"))
                factorZona = 1.0f;
            else
                factorZona = 1.3f;
            float factorEnvio = tipoenvio456VG?.Equals("express", StringComparison.OrdinalIgnoreCase) == true
                                ? 1.20f
                                : 1.0f;
            float bruto = (tarifaBase + basePeso + baseVolumen) * factorZona * factorEnvio;
            return (decimal)Math.Round(bruto, 2);
        }
    }
}
