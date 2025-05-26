using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEPaquete_456VG
    {
        private readonly Random _rnd = new Random();
        private const string _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public int id_paquete456VG { get; set; }
        public string dnicliente456VG { get; set; }
        public BECliente_456VG Cliente { get; set; }
        public float Peso456VG { get; set; }
        public float Ancho456VG { get; set; }
        public float Largo456VG { get; set; }
        public float Alto456VG { get; set; }
        public bool Enviado456VG { get; set; }
        public string CodPaq456VG { get; set; }
        public BEPaquete_456VG(int id_paquete, string dnicli, float Peso, float Ancho, float Largo, float Alto, bool enviado)
        {
            this.id_paquete456VG = id_paquete;
            this.dnicliente456VG = dnicli;
            this.Peso456VG = Peso;
            this.Ancho456VG = Ancho;
            this.Largo456VG = Largo;
            this.Alto456VG = Alto;
            this.Enviado456VG = enviado;
            this.CodPaq456VG = GenerateCodPaq();
        }
        public BEPaquete_456VG(string dnicli, float Peso, float Ancho, float Largo, float Alto, bool enviado)
        {
            this.dnicliente456VG = dnicli;
            this.Peso456VG = Peso;
            this.Ancho456VG = Ancho;
            this.Largo456VG = Largo;
            this.Alto456VG = Alto;
            this.Enviado456VG = enviado;
            this.CodPaq456VG = GenerateCodPaq();
        }
        public string GenerateCodPaq()
        {
            var sb = new StringBuilder(8);
            for (int i = 0; i < 4; i++)
                sb.Append(_letters[_rnd.Next(_letters.Length)]);
            for (int i = 0; i < 4; i++)
                sb.Append(_rnd.Next(10));
            return sb.ToString();
        }
    }
}
