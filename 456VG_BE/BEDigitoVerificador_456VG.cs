using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEDigitoVerificador_456VG
    {
        public int IDDigitoVerificador456VG { get; set; }
        public string NombreTabla456VG { get; set; }
        public string DVVertical456VG { get; set; }
        public string DVHorizontal456VG { get; set; }
        public BEDigitoVerificador_456VG(int id, string name, string dvv, string dvh)
        {
            this.IDDigitoVerificador456VG = id;
            this.NombreTabla456VG = name;
            this.DVVertical456VG = dvv;
            this.DVHorizontal456VG = dvh;
        }
        public BEDigitoVerificador_456VG(string name, string dvv, string dvh)
        {
            this.NombreTabla456VG = name;
            this.DVVertical456VG = dvv;
            this.DVHorizontal456VG = dvh;
        }
    }
}
