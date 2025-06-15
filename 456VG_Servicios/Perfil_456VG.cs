using _456VG_BE;
using System;
using System.Collections.Generic;
using _456VG_Servicios;
using System.Linq;

namespace _456VG_Servicios
{
    public class Perfil_456VG : Componente_456VG
    {
        public string Nombre456VG { get; set; }
        private List<Componente_456VG> Hijos456VG = new List<Componente_456VG>();
        public Perfil_456VG(string nombre)
        {
            Nombre456VG = nombre;
        }
        public bool IncluyePermiso(string nombrePermiso)
        {
            return ObtenerTodosNombresPermisos().Contains(nombrePermiso);
        }
        //Agrega Perfil y Permisos dentro del Perfil
        public override void AgregarHijo456VG(Componente_456VG nuevo)
        {
            var existentes = new HashSet<string>(ObtenerTodosNombresPermisos());
            var entrantes = nuevo.ObtenerTodosNombresPermisos();
            var duplicados = entrantes.Intersect(existentes).ToList();
            if (duplicados.Any())
            {
                string template = Lenguaje_456VG.ObtenerEtiqueta_456VG("Perfiles_456VG.Msg.PermisoDuplicado");
                string mensaje = string.Format(
                    template,
                    nuevo.GetType().Name,
                    string.Join(", ", duplicados),
                    Nombre456VG
                );
                throw new InvalidOperationException(mensaje);
            }
            Hijos456VG.Add(nuevo);
        }
        public override List<Componente_456VG> ObtenerHijos456VG() => Hijos456VG;
        public override void QuitarHijo456VG(Componente_456VG c)
            => Hijos456VG.Remove(c);
    }
}