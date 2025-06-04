using System;
using System.Linq;

namespace _456VG_BE
{
    public class BEFactura_456VG
    {
        public string CodFactura456VG { get; private set; }
        public BEEnvío_456VG Envio { get; set; }
        public DateTime FechaEmision456VG { get; private set; }
        public TimeSpan HoraEmision456VG { get; private set; }
        public BEFactura_456VG(BEEnvío_456VG envio, DateTime fechaHoraEmision)
        {
            this.Envio = envio;
            this.FechaEmision456VG = fechaHoraEmision.Date;
            this.HoraEmision456VG = fechaHoraEmision.TimeOfDay;
            this.CodFactura456VG = GenerateCodFactura456VG();
        }
        private string GenerateCodFactura456VG()
        {
            int cantidadPaquetes = Envio.Paquetes.Count;
            string dni = (Envio.Cliente.DNI456VG ?? "").Length >= 3
                ? Envio.Cliente.DNI456VG.Substring(0, 3).ToUpper()
                : (Envio.Cliente.DNI456VG ?? "").ToUpper().PadRight(3, 'X');
            string nombre = (Envio.Cliente.Nombre456VG ?? "").Length >= 3
                ? Envio.Cliente.Nombre456VG.Substring(0, 3).ToUpper()
                : (Envio.Cliente.Nombre456VG ?? "").ToUpper().PadRight(3, 'X');
            string dia = FechaEmision456VG.Day.ToString("D2");
            string mes = FechaEmision456VG.Month.ToString("D2");
            string año = FechaEmision456VG.Year.ToString("D4");
            return $"{cantidadPaquetes}{dni}{nombre}{dia}{mes}{año}";
        }
    }
}
