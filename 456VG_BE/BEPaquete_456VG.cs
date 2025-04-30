using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEPaquete_456VG
    {
        //REHACER
        public int id_paquete456VG { get; set; }
        public string Nombre_rem456VG { get; set; }
        public string Apellido_rem456VG { get; set; }
        public string Teléfono_rem456VG { get; set; }
        public string DNI_rem456VG { get; set; }
        public string Nombre_dest456VG { get; set; }
        public string Apellido_dest456VG { get; set; }
        public string DNI_dest456VG { get; set; }
        public string Teléfono_dest456VG { get; set; }
        public int CodigoPostal_dest456VG { get; set; }
        public string Domicilio_dest456VG { get; set; }
        public string Localidad_dest456VG { get; set; }
        public string Provincia_dest456VG { get; set; }
        public float Peso456VG { get; set; }
        public float Ancho456VG { get; set; }
        public float Largo456VG { get; set; }
        public float Alto456VG { get; set; }
        public string Tipoenvio456VG { get; set; }

        public BEPaquete_456VG(int id_paquete, string Nombre_rem, string Apellido_rem, string Teléfono_rem, string DNI_rem, string Nombre_dest, string Apellido_dest, string DNI_dest, string Teléfono_dest, int CodigoPostal_dest, string Domicilio_dest, string Localidad_dest, string Provincia_dest, float Peso, float Ancho, float Largo, float Alto, string Tipoenvio)
        {
            this.id_paquete456VG = id_paquete;
            this.Nombre_rem456VG = Nombre_rem;
            this.Apellido_rem456VG = Apellido_rem;
            this.Teléfono_rem456VG = Teléfono_rem;
            this.DNI_rem456VG = DNI_rem;
            this.Nombre_dest456VG = Nombre_dest;
            this.Apellido_dest456VG = Apellido_dest;
            this.DNI_dest456VG = DNI_dest;
            this.Teléfono_dest456VG = Teléfono_dest;
            this.CodigoPostal_dest456VG = CodigoPostal_dest;
            this.Domicilio_dest456VG = Domicilio_dest;
            this.Localidad_dest456VG = Localidad_dest;
            this.Provincia_dest456VG = Provincia_dest;
            this.Peso456VG = Peso;
            this.Ancho456VG = Ancho;
            this.Largo456VG = Largo;
            this.Alto456VG = Alto;
            this.Tipoenvio456VG = Tipoenvio;
        }
        public BEPaquete_456VG(string Nombre_rem, string Apellido_rem, string Teléfono_rem, string DNI_rem, string Nombre_dest, string Apellido_dest, string DNI_dest, string Teléfono_dest, int CodigoPostal_dest, string Domicilio_dest, string Localidad_dest, string Provincia_dest, float Peso, float Ancho, float Largo, float Alto, string Tipoenvio)
        {
            this.Nombre_rem456VG = Nombre_rem;
            this.Apellido_rem456VG = Apellido_rem;
            this.Teléfono_rem456VG = Teléfono_rem;
            this.DNI_rem456VG = DNI_rem;
            this.Nombre_dest456VG = Nombre_dest;
            this.Apellido_dest456VG = Apellido_dest;
            this.DNI_dest456VG = DNI_dest;
            this.Teléfono_dest456VG = Teléfono_dest;
            this.CodigoPostal_dest456VG = CodigoPostal_dest;
            this.Domicilio_dest456VG = Domicilio_dest;
            this.Localidad_dest456VG = Localidad_dest;
            this.Provincia_dest456VG = Provincia_dest;
            this.Peso456VG = Peso;
            this.Ancho456VG = Ancho;
            this.Largo456VG = Largo;
            this.Alto456VG = Alto;
            this.Tipoenvio456VG = Tipoenvio;
        }
    }
}
