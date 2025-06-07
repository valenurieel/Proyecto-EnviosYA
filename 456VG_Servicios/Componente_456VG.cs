using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_Servicios
{
    public abstract class Componente_456VG
    {
        public abstract void AgregarHijo456VG(Componente_456VG c);
        public abstract void QuitarHijo456VG(Componente_456VG c);
        public abstract List<Componente_456VG> ObtenerHijos456VG();
    }
}
