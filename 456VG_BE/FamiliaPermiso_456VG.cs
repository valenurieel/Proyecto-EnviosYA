using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class FamiliaPermiso_456VG : IPerfil_456VG
    {
        public int CodFamilia456VG { get; set; }
        public string Nombre456VG { get; set; }
        public List<IPerfil_456VG> Permisos456VG { get; set; } = new List<IPerfil_456VG>();
        public int CodPermiso456VG
        {
            get => CodFamilia456VG;
            set => CodFamilia456VG = value;
        }
        public void AgregarPermiso_456VG(IPerfil_456VG permiso)
        {
            Permisos456VG.Add(permiso);
        }
        public void QuitarPermiso_456VG(IPerfil_456VG permiso)
        {
            Permisos456VG.Remove(permiso);
        }
        public List<IPerfil_456VG> ObtenerTodosLosPermisos_456VG()
        {
            List<IPerfil_456VG> resultado = new List<IPerfil_456VG>();
            foreach (var permiso in Permisos456VG)
            {
                resultado.Add(permiso);
                if (permiso is FamiliaPermiso_456VG subFamilia)
                {
                    resultado.AddRange(subFamilia.ObtenerTodosLosPermisos_456VG());
                }
            }
            return resultado;
        }
    }
}
