using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEPaquete_456VG
    {
        public int id_paquete { get; set; }
        public string Nombre_rem { get; set; }
        public string Apellido_rem { get; set; }
        public string Teléfono_rem { get; set; }
        public string DNI_rem { get; set; }
        public string Nombre_dest { get; set; }
        public string Apellido_dest { get; set; }
        public string DNI_dest { get; set; }
        public string Teléfono_dest { get; set; }
        public int CodigoPostal_dest { get; set; }
        public string Domicilio_dest { get; set; }
        public string Localidad_dest { get; set; }
        public string Provincia_dest { get; set; }
        public float Peso { get; set; }
        public float Ancho { get; set; }
        public float Largo { get; set; }
        public float Alto { get; set; }
        public string Tipoenvio { get; set; }

        public BEPaquete_456VG(int id_paquete, string Nombre_rem, string Apellido_rem, string Teléfono_rem, string DNI_rem, string Nombre_dest, string Apellido_dest, string DNI_dest, string Teléfono_dest, int CodigoPostal_dest, string Domicilio_dest, string Localidad_dest, string Provincia_dest, float Peso, float Ancho, float Largo, float Alto, string Tipoenvio)
        {
            this.id_paquete= id_paquete;
            this.Nombre_rem = Nombre_rem;
            this.Apellido_rem = Apellido_rem;
            this.Teléfono_rem = Teléfono_rem;
            this.DNI_rem = DNI_rem;
            this.Nombre_dest = Nombre_dest;
            this.Apellido_dest = Apellido_dest;
            this.DNI_dest = DNI_dest;
            this.Teléfono_dest = Teléfono_dest;
            this.CodigoPostal_dest = CodigoPostal_dest;
            this.Domicilio_dest = Domicilio_dest;
            this.Localidad_dest = Localidad_dest;
            this.Provincia_dest = Provincia_dest;
            this.Peso = Peso;
            this.Ancho = Ancho;
            this.Largo = Largo;
            this.Alto = Alto;
            this.Tipoenvio = Tipoenvio;
        }
        public BEPaquete_456VG(string Nombre_rem, string Apellido_rem, string Teléfono_rem, string DNI_rem, string Nombre_dest, string Apellido_dest, string DNI_dest, string Teléfono_dest, int CodigoPostal_dest, string Domicilio_dest, string Localidad_dest, string Provincia_dest, float Peso, float Ancho, float Largo, float Alto, string Tipoenvio)
        {
            this.Nombre_rem = Nombre_rem;
            this.Apellido_rem = Apellido_rem;
            this.Teléfono_rem = Teléfono_rem;
            this.DNI_rem = DNI_rem;
            this.Nombre_dest = Nombre_dest;
            this.Apellido_dest = Apellido_dest;
            this.DNI_dest = DNI_dest;
            this.Teléfono_dest = Teléfono_dest;
            this.CodigoPostal_dest = CodigoPostal_dest;
            this.Domicilio_dest = Domicilio_dest;
            this.Localidad_dest = Localidad_dest;
            this.Provincia_dest = Provincia_dest;
            this.Peso = Peso;
            this.Ancho = Ancho;
            this.Largo = Largo;
            this.Alto = Alto;
            this.Tipoenvio = Tipoenvio;
        }
    }
}
