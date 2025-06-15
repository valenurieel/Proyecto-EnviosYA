using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_Servicios
{
    public class Familia_456VG: Componente_456VG
    {
        public string Nombre456VG { get; set; }
        private readonly List<Componente_456VG> _hijos = new List<Componente_456VG>();
        public Familia_456VG(string nombre)
        {
            Nombre456VG = nombre ?? throw new ArgumentNullException(nameof(nombre));
        }
        //Verifica que no se repitan Permisos en la misma Familia.
        public override void AgregarHijo456VG(Componente_456VG nuevo)
        {
            var existentes = new HashSet<string>(ObtenerTodosNombresPermisos());
            var entrantes = nuevo.ObtenerTodosNombresPermisos();
            var dup = entrantes.Intersect(existentes).ToList();
            if (dup.Any())
                throw new InvalidOperationException(
                    $"La familia «{Nombre456VG}» ya contiene el/los permiso(s): {string.Join(", ", dup)}"
                );
            _hijos.Add(nuevo);
        }
        public override void QuitarHijo456VG(Componente_456VG c)
            => _hijos.Remove(c);
        public override List<Componente_456VG> ObtenerHijos456VG()
            => _hijos;
    }
}
