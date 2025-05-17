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
        public string DNICli456VG { get; set; }
        public string NombreCli456VG { get; set; }
        public string ApellidoCli456VG { get; set; }
        public string TeléfonoCli456VG { get; set; }
        public string DNIDest456VG { get; set; }
        public string NombreDest456VG { get; set; }
        public string ApellidoDest456VG { get; set; }
        public string TeléfonoDest456VG { get; set; }
        public float CodPostal456VG { get; set; }
        public string Domicilio456VG { get; set; }
        public string Localidad456VG { get; set; }
        public string Provincia456VG { get; set; }
        public float Peso456VG { get; set; }
        public float Ancho456VG { get; set; }
        public float Alto456VG { get; set; }
        public float Largo456VG { get; set; }
        public float Importe456VG { get; set; }
        public bool Pagado456VG { get; set; }

        public BEEnvío_456VG(int id, string dnicli, string namecli, string apecli, string telcli, string dnidest, string namedest, string apedest, string teldest, float cp, string dom, string loc, string prov, float peso, float ancho, float alto, float largo, float impo, bool pagado)
        {
            this.id_envio456VG = id;
            this.DNICli456VG = dnicli;
            this.NombreCli456VG = namecli;
            this.ApellidoCli456VG = apecli;
            this.TeléfonoCli456VG = telcli;
            this.DNIDest456VG = dnidest;
            this.NombreDest456VG = namedest;
            this.ApellidoDest456VG = apedest;
            this.TeléfonoDest456VG = teldest;
            this.CodPostal456VG = cp;
            this.Domicilio456VG = dom;
            this.Localidad456VG = loc;
            this.Provincia456VG = prov;
            this.Peso456VG = peso;
            this.Ancho456VG = ancho;
            this.Alto456VG = alto;
            this.Largo456VG = largo;
            this.Importe456VG = impo;
            this.Pagado456VG = pagado;
        }
        public BEEnvío_456VG(string dnicli, string namecli, string apecli, string telcli, string dnidest, string namedest, string apedest, string teldest, float cp, string dom, string loc, string prov, float peso, float ancho, float alto, float largo, float impo, bool pagado)
        {
            this.DNICli456VG = dnicli;
            this.NombreCli456VG = namecli;
            this.ApellidoCli456VG = apecli;
            this.TeléfonoCli456VG = telcli;
            this.DNIDest456VG = dnidest;
            this.NombreDest456VG = namedest;
            this.ApellidoDest456VG = apedest;
            this.TeléfonoDest456VG = teldest;
            this.CodPostal456VG = cp;
            this.Domicilio456VG = dom;
            this.Localidad456VG = loc;
            this.Provincia456VG = prov;
            this.Peso456VG = peso;
            this.Ancho456VG = ancho;
            this.Alto456VG = alto;
            this.Largo456VG = largo;
            this.Importe456VG = impo;
            this.Pagado456VG = pagado;
        }
    }
}
