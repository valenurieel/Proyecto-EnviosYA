using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _456VG_Servicios;

namespace _456VG_DAL
{
    public interface ICrud_456VG<T>
    {
        List<T> leerEntidades456VG();
        Resultado_456VG<T> crearEntidad456VG(T obj);
        Resultado_456VG<T> eliminarEntidad456VG(T obj);
        Resultado_456VG<T> actualizarEntidad456VG(T obj);
    }
}
