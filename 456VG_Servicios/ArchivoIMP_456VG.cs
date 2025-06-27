using _456VG_BE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace _456VG_Servicios
{
    public class ArchivoIMP_456VG
    {
        public string DestinoFactura_456VG { get; set; }
        private string ultimaRutaGenerada_456VG;
        public ArchivoIMP_456VG()
        {
            DestinoFactura_456VG = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDFs");
            if (!Directory.Exists(DestinoFactura_456VG))
            {
                Directory.CreateDirectory(DestinoFactura_456VG);
            }
        }
        public bool GenerarFacturasPDF_456VG(List<BEFactura_456VG> facturas)
        {
            try
            {
                string nombreArchivo = GenerarNombreArchivo_456VG();
                string rutaCompleta = Path.Combine(DestinoFactura_456VG, nombreArchivo);
                ultimaRutaGenerada_456VG = rutaCompleta;
                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(rutaCompleta, FileMode.Create));
                doc.Open();
                string titulo = facturas.Count == 1 ?
                    $"Factura: {facturas[0].CodFactura456VG}" :
                    "Listado de Facturas";
                doc.Add(new iTextSharp.text.Paragraph(titulo));
                doc.Add(new iTextSharp.text.Paragraph($"Fecha de generación: {DateTime.Now}"));
                doc.Add(new iTextSharp.text.Paragraph(" "));
                PdfPTable tabla = new PdfPTable(9);
                tabla.WidthPercentage = 100;
                float[] anchosColumnas = new float[] { 15f, 15f, 15f, 15f, 10f, 10f, 15f, 15f, 10f };
                tabla.SetWidths(anchosColumnas);
                string[] headers = { "Factura", "Envío", "DNI", "Cliente", "Paquetes", "Medio de Pago", "Importe", "Fecha", "Hora" };
                foreach (var h in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(h));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabla.AddCell(cell);
                }
                foreach (var f in facturas)
                {
                    tabla.AddCell(f.CodFactura456VG);
                    tabla.AddCell(f.Envio.CodEnvio456VG);
                    tabla.AddCell(f.Envio.Cliente.DNI456VG);
                    tabla.AddCell($"{f.Envio.Cliente.Nombre456VG} {f.Envio.Cliente.Apellido456VG}");
                    tabla.AddCell(f.Envio.Paquetes?.Count.ToString() ?? "0");
                    tabla.AddCell(f.DatosPago?.MedioPago456VG ?? "Sin datos");
                    tabla.AddCell(f.Envio.Importe456VG.ToString("C"));
                    tabla.AddCell(f.FechaEmision456VG.ToShortDateString());
                    tabla.AddCell(f.HoraEmision456VG.ToString(@"hh\:mm"));
                }
                doc.Add(tabla);
                doc.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el PDF de facturas: " + ex.Message);
            }
        }
        public void AbrirUltimoPDF_456VG()
        {
            if (!string.IsNullOrEmpty(ultimaRutaGenerada_456VG) && File.Exists(ultimaRutaGenerada_456VG))
            {
                Process.Start(new ProcessStartInfo(ultimaRutaGenerada_456VG) { UseShellExecute = true });
            }
        }
        private string GenerarNombreArchivo_456VG()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string baseName = $"Facturas_{timestamp}";
            string filePath = Path.Combine(DestinoFactura_456VG, baseName + ".pdf");
            int contador = 1;
            while (File.Exists(filePath))
            {
                filePath = Path.Combine(DestinoFactura_456VG, $"{baseName}_{contador}.pdf");
                contador++;
            }
            return Path.GetFileName(filePath);
        }
    }
}
