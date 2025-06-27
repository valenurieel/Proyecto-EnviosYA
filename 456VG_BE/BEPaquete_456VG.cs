using System;
using System.Text;

namespace _456VG_BE
{
    public class BEPaquete_456VG
    {
        public BECliente_456VG Cliente { get; set; } //cliente
        public float Peso456VG { get; set; }
        public float Ancho456VG { get; set; }
        public float Largo456VG { get; set; }
        public float Alto456VG { get; set; }
        public bool Enviado456VG { get; set; }
        public string CodPaq456VG { get; set; }
        public BEPaquete_456VG(string codPaq, BECliente_456VG cliente, float peso, float ancho, float largo, float alto, bool enviado)
        {
            this.CodPaq456VG = codPaq;
            this.Cliente = cliente;
            this.Peso456VG = peso;
            this.Ancho456VG = ancho;
            this.Largo456VG = largo;
            this.Alto456VG = alto;
            this.Enviado456VG = enviado;
        }
        public BEPaquete_456VG(BECliente_456VG cliente, float peso, float ancho, float largo, float alto, bool enviado)
        {
            this.Cliente = cliente;
            this.Peso456VG = peso;
            this.Ancho456VG = ancho;
            this.Largo456VG = largo;
            this.Alto456VG = alto;
            this.Enviado456VG = enviado;
            this.CodPaq456VG = GenerateCodPaq456VG();
        }
        //genera codigo del paquete (3 dig de DNI, 3 dig de Nombre y hora)
        private string GenerateCodPaq456VG()
        {
            string dni = (Cliente.DNI456VG ?? "").Length >= 3
                ? Cliente.DNI456VG.Substring(0, 3).ToUpper()
                : Cliente.DNI456VG.ToUpper().PadRight(3, 'X');
            string nombre = (Cliente.Nombre456VG ?? "").Length >= 3
                ? Cliente.Nombre456VG.Substring(0, 3).ToUpper()
                : Cliente.Nombre456VG.ToUpper().PadRight(3, 'X');
            string sufijoTime = DateTime.Now.ToString("HHmmssfff");
            return $"{dni}{nombre}{sufijoTime}";
        }
    }
}
