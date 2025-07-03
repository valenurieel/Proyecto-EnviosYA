using _456VG_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _456VG_DAL;
using _456VG_Servicios;

namespace _456VG_BLL
{
    public class BLLPerfil_456VG
    {
        DALPerfil_456VG dal;
        public BLLPerfil_456VG()
        {
            dal = new DALPerfil_456VG();
        }
        public BEPerfil_456VG ObtenerPerfilCompleto456VG(string nombrePerfil456VG)
        {
            return dal.ObtenerPerfilCompleto456VG(nombrePerfil456VG);
        }
        public List<Permiso_456VG> ListarPermisosSimples456VG()
        {
            return dal.ListarPermisosSimples456VG();
        }
        public bool CrearPerfil456VG(string nombrePerfil)
        {
            if (string.IsNullOrWhiteSpace(nombrePerfil)) return false;
            return dal.InsertarPerfil456VG(nombrePerfil);
        }
        public bool EliminarPerfil456VG(string nombrePerfil)
        {
            return dal.EliminarPerfil456VG(nombrePerfil);
        }
        public List<string> ObtenerTodasLasFamilias456VG()
        {
            return dal.ObtenerNombresDeFamilias456VG();
        }
        public List<IPerfil_456VG> ObtenerPermisosPorRol456VG(string nombreRol)
        {
            return dal.ObtenerPermisosPorRol456VG(nombreRol);
        }
        public List<IPerfil_456VG> ObtenerHijosDeFamilia456VG(int codFamilia)
        {
            return dal.ObtenerHijosDeFamilia456VG(codFamilia);
        }
        public bool AgregarPermisoAPerfil456VG(string nombrePerfil, IPerfil_456VG permiso)
        {
            var permisosExistentes = ObtenerTodosLosPermisosDePerfil456VG(nombrePerfil);
            var nuevosPermisos = permiso is FamiliaPermiso_456VG familia
                ? familia.ObtenerTodosLosPermisos_456VG()
                : new List<IPerfil_456VG> { permiso };
            foreach (var nuevo in nuevosPermisos)
            {
                if (permisosExistentes.Any(p => p.CodPermiso456VG == nuevo.CodPermiso456VG))
                    return false;
            }
            return dal.InsertarPermisoAPerfil456VG(nombrePerfil, permiso.CodPermiso456VG);
        }
        public List<IPerfil_456VG> ObtenerTodosLosPermisosDePerfil456VG(string nombrePerfil)
        {
            var perfil = ObtenerPerfilCompleto456VG(nombrePerfil);
            return perfil.obtenerPermisos456VG().Cast<IPerfil_456VG>().ToList();
        }
        public bool AgregarFamiliaAPerfil456VG(string nombrePerfil, FamiliaPermiso_456VG familia)
        {
            return AgregarPermisoAPerfil456VG(nombrePerfil, familia);
        }
        public bool QuitarPermisoOFamiliaDePerfil456VG(string nombrePerfil, int codPermiso)
        {
            return dal.EliminarPermisoDePerfil456VG(nombrePerfil, codPermiso);
        }
        public bool CrearFamilia456VG(string nombreFamilia)
        {
            return dal.AgregarFamilia456VG(nombreFamilia);
        }
        public bool EliminarFamilia456VG(string nombreFamilia)
        {
            if (string.IsNullOrWhiteSpace(nombreFamilia)) return false;
            return dal.EliminarFamilia456VG(nombreFamilia);
        }
        public FamiliaPermiso_456VG ObtenerFamiliaCompleta456VG(string nombreFamilia)
        {
            int cod = dal.ObtenerCodPermisoPorNombre456VG(nombreFamilia);
            if (cod <= 0) return null;

            try
            {
                dal.db.Connection.Open(); // <- abrimos la conexión desde la BLL
                return dal.ConstruirFamiliaRecursiva456VG(cod, nombreFamilia, dal.db.Connection);
            }
            finally
            {
                dal.db.Connection.Close(); // <- la cerramos después de la recursión
            }
        }

        public bool AgregarPermisoAFamilia456VG(string nombreFamilia, IPerfil_456VG nuevoPermiso)
        {
            var familia = ObtenerFamiliaCompleta456VG(nombreFamilia);
            if (familia == null) return false;
            var nuevosPermisos = nuevoPermiso is FamiliaPermiso_456VG fam
                ? fam.ObtenerTodosLosPermisos_456VG()
                : new List<IPerfil_456VG> { nuevoPermiso };
            var permisosExistentes = familia.ObtenerTodosLosPermisos_456VG();
            foreach (var p in nuevosPermisos)
            {
                if (permisosExistentes.Any(ep => ep.CodPermiso456VG == p.CodPermiso456VG))
                    return false;
            }
            return dal.InsertarPermisoEnFamilia456VG(nombreFamilia, nuevoPermiso.CodPermiso456VG);
        }
        public int ObtenerCodPermisoPorNombre456VG(string nombre)
        {
            return dal.ObtenerCodPermisoPorNombre456VG(nombre);
        }
        public List<BEPerfil_456VG> CargarCBPerfil456VG()
        {
            return dal.ObtenerPerfilesSimples456VG();
        }
        public bool QuitarPermisoDeFamilia456VG(string nombreFamilia, int codPermiso)
        {
            var familia = ObtenerFamiliaCompleta456VG(nombreFamilia);
            if (familia == null) return false;
            bool esHijoDirecto = familia.Permisos456VG.Any(p => p.CodPermiso456VG == codPermiso);
            if (!esHijoDirecto) return false;
            return dal.QuitarPermisoDeFamilia456VG(nombreFamilia, codPermiso);
        }
    }
}
