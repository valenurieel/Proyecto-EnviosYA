using iTextSharp.text.pdf.codec.wmf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _456VG_BE
{
    public class BEEnvío_456VG
    {
        public BECliente_456VG Cliente { get; set; } //cliente
        public List<BEPaquete_456VG> Paquetes { get; set; } = new List<BEPaquete_456VG>(); //paquetes del cliente en el envío
        public string DNIDest456VG { get; set; }
        public string NombreDest456VG { get; set; }
        public string ApellidoDest456VG { get; set; }
        public string TeléfonoDest456VG { get; set; }
        public float CodPostal456VG { get; set; }
        public string Domicilio456VG { get; set; }
        public string Localidad456VG { get; set; }
        public string Provincia456VG { get; set; }
        public string tipoenvio456VG { get; set; }
        public decimal Importe456VG { get; private set; }
        public bool Pagado456VG { get; set; } //si está pagado el envío
        public string CodEnvio456VG { get; set; }
        public DateTime FechaEntregaProgramada456VG { get; set; }
        public string EstadoEnvio456VG { get; set; } = "Pendiente de Entrega"; //estado
        public BEEnvío_456VG(string codEnvio, BECliente_456VG cliente, List<BEPaquete_456VG> paquetes, string dniDest, string nomDest, string apeDest, string telDest, float codPostal, string dom, string loc, string prov, string tipoEnvio, bool pagado, decimal importe, string estado, DateTime fechaentrega)
        {
            this.CodEnvio456VG = codEnvio;
            this.Cliente = cliente;
            this.Paquetes = paquetes;
            this.DNIDest456VG = dniDest;
            this.NombreDest456VG = nomDest;
            this.ApellidoDest456VG = apeDest;
            this.TeléfonoDest456VG = telDest;
            this.CodPostal456VG = codPostal;
            this.Domicilio456VG = dom;
            this.Localidad456VG = loc;
            this.Provincia456VG = prov;
            this.tipoenvio456VG = tipoEnvio;
            this.Pagado456VG = pagado;
            this.Importe456VG = importe;
            this.EstadoEnvio456VG = estado;
            this.FechaEntregaProgramada456VG = fechaentrega;
        }
        public BEEnvío_456VG(BECliente_456VG cliente, List<BEPaquete_456VG> paquetes, string dniDest, string nomDest, string apeDest, string telDest, float codPostal, string dom, string loc, string prov, string tipoEnvio, bool pagado, string estado, DateTime fechaentrega)
        {
            this.Cliente = cliente;
            this.Paquetes = paquetes;
            this.DNIDest456VG = dniDest;
            this.NombreDest456VG = nomDest;
            this.ApellidoDest456VG = apeDest;
            this.TeléfonoDest456VG = telDest;
            this.CodPostal456VG = codPostal;
            this.Domicilio456VG = dom;
            this.Localidad456VG = loc;
            this.Provincia456VG = prov;
            this.tipoenvio456VG = tipoEnvio;
            this.Pagado456VG = pagado;
            this.CodEnvio456VG = GenerateCodEnvio456VG();
            this.Importe456VG = CalcularImporte456VG();
            this.EstadoEnvio456VG = estado;
            this.FechaEntregaProgramada456VG = fechaentrega;
        }
        //genero codigo de envio (cant paquete, 3 dig de DNI, 3 dig de Nombre y hora)
        private string GenerateCodEnvio456VG()
        {
            int cantidadPaquetes = Paquetes.Count;
            string dni = (Cliente.DNI456VG ?? "").Length >= 3
                ? Cliente.DNI456VG.Substring(0, 3).ToUpper()
                : (Cliente.DNI456VG ?? "").ToUpper().PadRight(3, 'X');
            string nombre = (Cliente.Nombre456VG ?? "").Length >= 3
                ? Cliente.Nombre456VG.Substring(0, 3).ToUpper()
                : (Cliente.Nombre456VG ?? "").ToUpper().PadRight(3, 'X');
            string sufijoTime = DateTime.Now.ToString("HHmmssfff");
            return $"{cantidadPaquetes}{dni}{nombre}{sufijoTime}";
        }
        //calculo importe del envio (destino, tipo de envío, paquete(detalles), tarifa Base)
        public decimal CalcularImporte456VG()
        {
            decimal subtotalPaquetes = 0m;
            string prov = Provincia456VG?.Trim().ToLower() ?? "";
            float factorZona;
            var zonaAlta = new HashSet<string>
            {
                "mendoza", "san luis", "cordoba", "córdoba", "tucuman", "tucumán", "san juan", "la rioja",
                "santa fe", "santa fé", "entre rios", "entre ríos", "corrientes", "misiones", "jujuy", "salta", "formosa",
                "chaco", "santiago del estero", "catamarca"
            };
            if (prov == "buenos aires")
                factorZona = 1.1f;
            else if (zonaAlta.Contains(prov))
                factorZona = 1.3f;
            else
                factorZona = 1.5f;
            float factorEnvio = tipoenvio456VG?.Equals("express", StringComparison.OrdinalIgnoreCase) == true
                                ? 1.20f
                                : 1.0f;
            const float tarifaBase = 500f;
            foreach (var paquete in Paquetes)
            {
                float volumen = (paquete.Alto456VG * paquete.Ancho456VG * paquete.Largo456VG) / 1000f;
                float basePeso = paquete.Peso456VG * 10f;
                float baseVolumen = volumen * 0.5f;
                float precioPaquete = tarifaBase + basePeso + baseVolumen;
                subtotalPaquetes += Math.Round((decimal)precioPaquete, 2);
            }
            decimal totalConZona = subtotalPaquetes * (decimal)factorZona;
            decimal totalFinal = totalConZona * (decimal)factorEnvio;
            return Math.Round(totalFinal, 2);
        }
    }
}
