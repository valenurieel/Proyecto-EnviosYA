using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_BLL
{
    public interface IEntidades_456VG<T>
    {
        List<T> leerEntidades();
        Resultado_456VG<T> crearEntidad(T obj);
        Resultado_456VG<T> eliminarEntidad(T obj);
        Resultado_456VG<T> actualizarEntidad(T obj);
    }
}
