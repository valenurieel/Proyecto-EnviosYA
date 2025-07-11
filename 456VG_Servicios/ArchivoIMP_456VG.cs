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
        public void GenerarFacturaDetalladaPDF_456VG(BEFactura_456VG factura)
        {
            try
            {
                string nombreArchivo = $"Factura_{factura.CodFactura456VG}.pdf";
                string rutaCompleta = Path.Combine(DestinoFactura_456VG, nombreArchivo);
                ultimaRutaGenerada_456VG = rutaCompleta;
                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(rutaCompleta, FileMode.Create));
                doc.Open();
                var fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
                var fontSeccion = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                var fontTexto = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                string rutaImagen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enviosya_mini.jpg");
                if (File.Exists(rutaImagen))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(rutaImagen);
                    logo.ScaleToFit(150f, 75f);
                    logo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(logo);
                    doc.Add(Chunk.NEWLINE);
                }
                doc.Add(Chunk.NEWLINE);
                doc.Add(new iTextSharp.text.Paragraph($"Factura N°: {factura.CodFactura456VG}", fontTexto));
                doc.Add(new iTextSharp.text.Paragraph($"Fecha de Emisión: {factura.FechaEmision456VG:dd/MM/yyyy}", fontTexto));
                doc.Add(new iTextSharp.text.Paragraph($"Hora de Emisión: {factura.HoraEmision456VG}", fontTexto));
                doc.Add(Chunk.NEWLINE);
                var cliente = factura.Envio.Cliente;
                doc.Add(new iTextSharp.text.Paragraph("Datos del Cliente", fontSeccion));
                doc.Add(new iTextSharp.text.Paragraph($"DNI: {cliente.DNI456VG}", fontTexto));
                doc.Add(new iTextSharp.text.Paragraph($"Nombre y Apellido: {cliente.Nombre456VG} {cliente.Apellido456VG}", fontTexto));
                doc.Add(Chunk.NEWLINE);
                string provincia = factura.Envio.Provincia456VG?.Trim().ToLower() ?? "";
                float factorZona;
                var zonaAlta = new HashSet<string>
                {
                    "mendoza", "san luis", "cordoba", "córdoba", "tucuman", "tucumán", "san juan", "la rioja",
                    "santa fe", "entre rios", "entre ríos", "corrientes", "misiones", "jujuy", "salta", "formosa",
                    "chaco", "santiago del estero", "catamarca"
                };
                if (provincia == "buenos aires")
                    factorZona = 1.1f;
                else if (zonaAlta.Contains(provincia))
                    factorZona = 1.3f;
                else
                    factorZona = 1.5f;
                float factorEnvio = factura.Envio.tipoenvio456VG?.Equals("express", StringComparison.OrdinalIgnoreCase) == true ? 1.2f : 1.0f;
                const float tarifaBase = 500f;
                doc.Add(new iTextSharp.text.Paragraph("Datos del Envío", fontSeccion));
                doc.Add(new iTextSharp.text.Paragraph($"Código de Envío: {factura.Envio.CodEnvio456VG}", fontTexto));
                doc.Add(new iTextSharp.text.Paragraph($"Tipo de Envío: '{factura.Envio.tipoenvio456VG}'", fontTexto));
                decimal totalBase = 0;
                foreach (var p in factura.Envio.Paquetes)
                {
                    float volumen = (p.Alto456VG * p.Ancho456VG * p.Largo456VG) / 1000f;
                    float basePeso = p.Peso456VG * 10f;
                    float baseVol = volumen * 0.5f;
                    float subtotal = tarifaBase + basePeso + baseVol;
                    totalBase += (decimal)Math.Round(subtotal, 2);
                }
                decimal recargoZona = totalBase * (decimal)(factorZona - 1);
                decimal recargoTipo = (totalBase + recargoZona) * (decimal)(factorEnvio - 1);
                doc.Add(new iTextSharp.text.Paragraph($"Recargo por Zona: ...........................................................................................................${recargoZona:F2}", fontTexto));
                doc.Add(new iTextSharp.text.Paragraph($"Recargo por Tipo de Envío: .............................................................................................${recargoTipo:F2}", fontTexto));
                doc.Add(Chunk.NEWLINE);
                doc.Add(new iTextSharp.text.Paragraph("Datos del Pago", fontSeccion));
                doc.Add(new iTextSharp.text.Paragraph($"Medio de Pago: {factura.DatosPago.MedioPago456VG}", fontTexto));
                doc.Add(Chunk.NEWLINE);
                doc.Add(new iTextSharp.text.Paragraph("Detalle de Paquetes", fontSeccion));
                PdfPTable tabla = new PdfPTable(6);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(new float[] { 2.5f, 1.3f, 1.3f, 1.3f, 1.3f, 1.5f });
                tabla.AddCell("Código");
                tabla.AddCell("Peso");
                tabla.AddCell("Alto");
                tabla.AddCell("Ancho");
                tabla.AddCell("Largo");
                tabla.AddCell("Precio");
                foreach (var p in factura.Envio.Paquetes)
                {
                    float volumen = (p.Alto456VG * p.Ancho456VG * p.Largo456VG) / 1000f;
                    float basePeso = p.Peso456VG * 10f;
                    float baseVol = volumen * 0.5f;
                    float subtotal = tarifaBase + basePeso + baseVol;
                    tabla.AddCell(p.CodPaq456VG);
                    tabla.AddCell(p.Peso456VG.ToString("F2"));
                    tabla.AddCell(p.Alto456VG.ToString("F2"));
                    tabla.AddCell(p.Ancho456VG.ToString("F2"));
                    tabla.AddCell(p.Largo456VG.ToString("F2"));
                    tabla.AddCell($"${Math.Round(subtotal, 2):F2}");
                }
                decimal totalFinal = totalBase + recargoZona + recargoTipo;
                doc.Add(tabla);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new iTextSharp.text.Paragraph($"TOTAL FINAL: ........................................................................${totalFinal:F2}", fontSeccion));
                doc.Add(Chunk.NEWLINE);
                var agradecimiento = new iTextSharp.text.Paragraph("¡Gracias por confiar en nosotros!", fontSeccion);
                agradecimiento.Alignment = Element.ALIGN_CENTER;
                doc.Add(agradecimiento);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new iTextSharp.text.Paragraph("Nuestros contactos:", fontSeccion));
                doc.Add(new iTextSharp.text.Paragraph("Teléfono: +54 11-2711-8942.", fontTexto));
                doc.Add(new iTextSharp.text.Paragraph("Email: consultas@enviosya.com", fontTexto));
                doc.Add(new iTextSharp.text.Paragraph("Dirección: Asturiano 2345 Temperley, Buenos Aires.", fontTexto));
                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar la factura detallada: " + ex.Message);
            }
        }
        public void AbrirUltimoPDF_456VG()
        {
            if (!string.IsNullOrEmpty(ultimaRutaGenerada_456VG) && File.Exists(ultimaRutaGenerada_456VG))
            {
                Process.Start(new ProcessStartInfo(ultimaRutaGenerada_456VG) { UseShellExecute = true });
            }
        }
    }
}
