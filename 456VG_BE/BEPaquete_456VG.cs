using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEPaquete_456VG
    {
        public int id_paquete456VG { get; set; }
        public float Peso456VG { get; set; }
        public float Ancho456VG { get; set; }
        public float Largo456VG { get; set; }
        public float Alto456VG { get; set; }

        public BEPaquete_456VG(int id_paquete, float Peso, float Ancho, float Largo, float Alto)
        {
            this.id_paquete456VG = id_paquete;
            this.Peso456VG = Peso;
            this.Ancho456VG = Ancho;
            this.Largo456VG = Largo;
            this.Alto456VG = Alto;
        }
        public BEPaquete_456VG(float Peso, float Ancho, float Largo, float Alto)
        {
            this.Peso456VG = Peso;
            this.Ancho456VG = Ancho;
            this.Largo456VG = Largo;
            this.Alto456VG = Alto;
        }
    }
}
