using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _456VG_BLL
{
    public class BLLDeserializar_456VG
    {
        public Resultado_456VG<DataTable> DeserializarXmlADatatable456VG(string filePath)
        {
            var res = new Resultado_456VG<DataTable>();
            try
            {
                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                {
                    res.resultado = false;
                    res.mensaje = "Archivo no encontrado.";
                    return res;
                }
                var ds = new DataSet();
                ds.ReadXml(filePath);
                var dt = ds.Tables.Count > 0 ? ds.Tables[0] : null;
                if (dt == null || dt.Rows.Count == 0)
                {
                    res.resultado = false;
                    res.mensaje = "El XML no contiene datos.";
                    return res;
                }
                res.resultado = true;
                res.entidad = dt;
                res.mensaje = "XML importado correctamente.";
                return res;
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.mensaje = ex.Message;
                return res;
            }
        }
    }
}
