using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_Servicios
{
    public interface ISubject_456VG
    {
        void Agregar_456VG(IObserver_456VG observer);
        void Quitar_456VG(IObserver_456VG observer);

        void Notificar_456VG();
    }
}
