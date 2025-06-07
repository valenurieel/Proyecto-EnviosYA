using _456VG_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_Servicios
{
    public class Perfil_456VG: Componente_456VG
    {
        public string Nombre456VG { get; set; }
        public string Permisos456VG { get; set; }
        private List<Componente_456VG> Hijos456VG = new List<Componente_456VG>();
        public Perfil_456VG(string nombre)
        {
            Nombre456VG = nombre;
        }
        public override void AgregarHijo456VG(Componente_456VG c)
        {
            Hijos456VG.Add(c);
        }
        public override List<Componente_456VG> ObtenerHijos456VG()
        {
            return Hijos456VG;
        }
        public override void QuitarHijo456VG(Componente_456VG c)
        {
            Hijos456VG.Remove(c);
        }
    }
}
