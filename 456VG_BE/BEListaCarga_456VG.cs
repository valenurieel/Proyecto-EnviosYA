using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BE
{
    public class BEListaCarga_456VG
    {
        public string CodLista456VG { get; set; }
        public DateTime FechaCreacion456VG { get; set; }
        public string TipoZona456VG { get; set; }
        public string TipoEnvio456VG { get; set; }
        public int CantEnvios456VG { get; set; }
        public int CantPaquetes456VG { get; set; }
        public float PesoTotal456VG { get; set; }
        public float VolumenTotal456VG { get; set; }
        public BEChofer_456VG Chofer { get; set; }
        public BETransporte_456VG Transporte { get; set; }
        public DateTime FechaSalida456VG { get; set; }
        public string EstadoLista456VG { get; set; } = "Abierta"; //estado

        public BEListaCarga_456VG(string cod, DateTime fechacre, string tzona, string tenv, int canenv, int cantpaq, float peso, float vol, BEChofer_456VG chof, BETransporte_456VG trans, DateTime fsalida, string estado)
        {
            this.CodLista456VG = cod;
            this.FechaCreacion456VG = fechacre;
            this.TipoZona456VG= tzona;
            this.TipoEnvio456VG = tenv;
            this.CantEnvios456VG = canenv;
            this.CantPaquetes456VG = cantpaq;
            this.PesoTotal456VG = peso;
            this.VolumenTotal456VG = vol;
            this.Chofer= chof;
            this.Transporte = trans;
            this.FechaSalida456VG = fsalida;
            this.EstadoLista456VG= estado;
        }
        public BEListaCarga_456VG(DateTime fechacre, string tzona, string tenv, int canenv, int cantpaq, float peso, float vol, BEChofer_456VG chof, BETransporte_456VG trans, DateTime fsalida, string estado)
        {
            this.FechaCreacion456VG = fechacre;
            this.TipoZona456VG = tzona;
            this.TipoEnvio456VG = tenv;
            this.CantEnvios456VG = canenv;
            this.CantPaquetes456VG = cantpaq;
            this.PesoTotal456VG = peso;
            this.VolumenTotal456VG = vol;
            this.Chofer = chof;
            this.Transporte = trans;
            this.FechaSalida456VG = fsalida;
            this.EstadoLista456VG = estado;
            this.CodLista456VG = GenerateCodLista456VG(canenv, fechacre);
        }
        private string GenerateCodLista456VG(int cantEnvios, DateTime fechaCreacion)
        {
            string prefijoCant = cantEnvios.ToString("D2");
            string stamp = fechaCreacion.ToString("yyyyMMddHHmmssfff");
            return prefijoCant + stamp;
        }
    }
}
