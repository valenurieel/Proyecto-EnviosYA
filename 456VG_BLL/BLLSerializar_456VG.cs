using _456VG_Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _456VG_BLL
{
    public class BLLSerializar_456VG
    {
        private const string XML_DIR_NAME = "SerializacionXML";
        public string SugerirNombreArchivo456VG(string baseName, string lang)
            => $"{baseName}_{(string.IsNullOrWhiteSpace(lang) ? "ES" : lang)}_{DateTime.Now:yyyyMMdd_HHmmss}.xml";
        public string DirectorioXml456VG()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dir = Path.Combine(baseDir, XML_DIR_NAME);
            Directory.CreateDirectory(dir);
            return dir;
        }
        public DataTable ObtenerDataTableDesdeGrid456VG(DataGridView grid, string tableName)
        {
            var dt = new DataTable(string.IsNullOrWhiteSpace(tableName) ? "Tabla" : tableName);
            foreach (DataGridViewColumn col in grid.Columns)
            {
                string visibleHeader = col.HeaderText;
                dt.Columns.Add(GuardarNombresXML456VG(visibleHeader), typeof(string));
            }
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                var dr = dt.NewRow();
                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    var cell = row.Cells[i]?.Value;
                    dr[i] = cell?.ToString() ?? "";
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public Resultado_456VG<string> SerializarAXml456VG(DataTable dt, string filePath)
        {
            var res = new Resultado_456VG<string>();
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    res.resultado = false;
                    res.mensaje = "No hay datos para exportar.";
                    return res;
                }
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    res.resultado = false;
                    res.mensaje = "Ruta de archivo inválida.";
                    return res;
                }
                var ds = new DataSet("ClientesData");
                ds.Tables.Add(dt);
                ds.WriteXml(filePath, XmlWriteMode.WriteSchema);
                res.resultado = true;
                res.entidad = filePath;
                res.mensaje = "XML exportado correctamente.";
                return res;
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.mensaje = ex.Message;
                return res;
            }
        }
        private static string GuardarNombresXML456VG(string s)
        {
            var noSpaces = Regex.Replace(s ?? "", @"\s+", "_").Trim();
            return string.IsNullOrWhiteSpace(noSpaces) ? "Columna" : noSpaces;
        }
    }
}
