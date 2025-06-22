using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEFamilia_456VG
    {
        public int CodPermiso456VG { get; set; }
        public string nombre456VG { get; set; }
        public BEFamilia_456VG() { }
        public BEFamilia_456VG(int id, string nombre)
        {
            CodPermiso456VG = id;
            nombre456VG = nombre;
        }
    }
}
