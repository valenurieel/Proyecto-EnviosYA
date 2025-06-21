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
        public IEnumerable<string> ObtenerTodosNombresPermisos()
        {
            if (this is Permisos_456VG hoja)
                yield return hoja.Nombre456VG;
            else
                foreach (var hijo in ObtenerHijos456VG() ?? Enumerable.Empty<Componente_456VG>())
                    foreach (var n in hijo.ObtenerTodosNombresPermisos())
                        yield return n;
        }
        public virtual bool IncluyePermiso(string nombrePermiso)
        {
            return ObtenerTodosNombresPermisos().Contains(nombrePermiso);
        }
    }
}
