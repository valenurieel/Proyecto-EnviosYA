using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_Servicios
{
    public class Permisos_456VG: Componente_456VG
    {
        public string Nombre456VG { get; set; }
        public Permisos_456VG(string nombre)
        {
            Nombre456VG = nombre;
        }
        public override void AgregarHijo456VG(Componente_456VG c) { }
        public override List<Componente_456VG> ObtenerHijos456VG()
        {
            return new List<Componente_456VG>();
        }
        public override void QuitarHijo456VG(Componente_456VG c) { }
    }
}
