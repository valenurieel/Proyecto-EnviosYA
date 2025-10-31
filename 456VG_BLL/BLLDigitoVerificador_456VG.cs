using _456VG_BE;
using _456VG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BLL
{
    public class BLLDigitoVerificador_456VG
    {
        private readonly DALDigitoVerificador_456VG dal;
        public BLLDigitoVerificador_456VG()
        {
            dal = new DALDigitoVerificador_456VG();
        }
        public void ActualizarDV456VG()
        {
            dal.ActualizarDV456VG();
        }
        // Tablas con inconsistencias
        public List<string> DetectarInconsistencias456VG()
        {
            var calculados = dal.CalcularDVGeneral456VG();
            var guardados = dal.LeerDVsGuardados();
            List<string> inconsistencias = new List<string>();
            foreach (var c in calculados)
            {
                var g = guardados.FirstOrDefault(x => x.Tabla == c.Tabla);
                if (g.Tabla == null)
                {
                    inconsistencias.Add($"{c.Tabla}");
                    continue;
                }
                if (c.DVH != g.DVH || c.DVV != g.DVV)
                {
                    inconsistencias.Add($"{c.Tabla}");
                }
            }
            return inconsistencias;
        }
    }
}
