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
        public string DestinoSeguimiento_456VG { get; set; }
        public string DestinoBitacora_456VG { get; set; }
        private string ultimaRutaGenerada_456VG;
        public ArchivoIMP_456VG()
        {
            DestinoFactura_456VG = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FacturasPDFs");
            if (!Directory.Exists(DestinoFactura_456VG))
            {
                Directory.CreateDirectory(DestinoFactura_456VG);
            }
            DestinoSeguimiento_456VG = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeguimientoEnvíosPDFs");
            if (!Directory.Exists(DestinoSeguimiento_456VG))
            {
                Directory.CreateDirectory(DestinoSeguimiento_456VG);
            }
            DestinoBitacora_456VG = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BitacoraPDFs");
            if (!Directory.Exists(DestinoBitacora_456VG))
            {
                Directory.CreateDirectory(DestinoBitacora_456VG);
            }
        }
        //Factura
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
        //Seguimiento de Envío
        public void GenerarSeguimientoEnvioPDF_456VG(BESeguimientoEnvío_456VG seguimiento, BEEnvío_456VG envioCompleto = null)
        {
            if (seguimiento == null) throw new ArgumentNullException(nameof(seguimiento));
            var envio = envioCompleto ?? seguimiento.Envio ?? throw new ArgumentException("El envío no puede ser nulo.");
            string nombreArchivo = $"Etiqueta_{seguimiento.CodSeguimientoEnvío456VG}.pdf";
            string rutaCompleta = Path.Combine(DestinoSeguimiento_456VG, nombreArchivo);
            ultimaRutaGenerada_456VG = rutaCompleta;
            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
            var writer = PdfWriter.GetInstance(doc, new FileStream(rutaCompleta, FileMode.Create));
            doc.Open();
            var fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
            var fontSeccion = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var fontTexto = FontFactory.GetFont(FontFactory.HELVETICA, 11);
            string rutaImagen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enviosya_mini.jpg");
            if (File.Exists(rutaImagen))
            {
                var logo = Image.GetInstance(rutaImagen);
                logo.ScaleToFit(140f, 70f);
                logo.Alignment = Element.ALIGN_LEFT;
                doc.Add(logo);
            }
            var titulo = new iTextSharp.text.Paragraph("ETIQUETA DE ENVÍO", fontTitulo) { Alignment = Element.ALIGN_LEFT };
            doc.Add(titulo);
            doc.Add(Chunk.NEWLINE);
            doc.Add(new iTextSharp.text.Paragraph($"Código de Seguimiento: {seguimiento.CodSeguimientoEnvío456VG}", fontTexto));
            doc.Add(new iTextSharp.text.Paragraph($"Envío: {envio.CodEnvio456VG}", fontTexto));
            doc.Add(new iTextSharp.text.Paragraph($"Cantidad de Paquetes: {envio.Paquetes?.Count ?? 0}", fontTexto));
            var codigosPaquetes = (envio.Paquetes != null && envio.Paquetes.Count > 0)
                ? string.Join(", ", envio.Paquetes.Select(p => p.CodPaq456VG))
                : "—";
            doc.Add(new iTextSharp.text.Paragraph($"Códigos de Paquete: {codigosPaquetes}", fontTexto));
            doc.Add(new iTextSharp.text.Paragraph($"Fecha: {seguimiento.FechaEmitido456VG:dd/MM/yyyy HH:mm}", fontTexto));
            doc.Add(Chunk.NEWLINE);
            PdfPTable tablaCodes = new PdfPTable(2) { WidthPercentage = 100 };
            tablaCodes.SetWidths(new float[] { 2f, 1f });
            var bc128 = new Barcode128 { Code = seguimiento.CodSeguimientoEnvío456VG, StartStopText = false, CodeType = Barcode.CODE128, BarHeight = 40f };
            var imgBar = bc128.CreateImageWithBarcode(writer.DirectContent, BaseColor.BLACK, BaseColor.BLACK);
            imgBar.ScaleToFit(360f, 60f);
            var cellBar = new PdfPCell(imgBar) { HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.NO_BORDER, PaddingBottom = 6f };
            var qrTxt = $"CODSEG:{seguimiento.CodSeguimientoEnvío456VG}|ENV:{envio.CodEnvio456VG}";
            var qr = new BarcodeQRCode(qrTxt, 150, 150, null);
            var imgQr = qr.GetImage();
            imgQr.ScaleToFit(110f, 110f);
            var cellQr = new PdfPCell(imgQr) { HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.NO_BORDER };
            tablaCodes.AddCell(cellBar);
            tablaCodes.AddCell(cellQr);
            doc.Add(tablaCodes);
            doc.Add(Chunk.NEWLINE);
            PdfPTable tablaDR = new PdfPTable(2) { WidthPercentage = 100 };
            tablaDR.SetWidths(new float[] { 1.4f, 1.0f });
            PdfPCell celTituloDest = new PdfPCell(new Phrase("Destinatario", fontSeccion)) { HorizontalAlignment = Element.ALIGN_LEFT, BackgroundColor = new BaseColor(230, 230, 230), Padding = 6f };
            PdfPCell celTituloRem = new PdfPCell(new Phrase("Remitente", fontSeccion)) { HorizontalAlignment = Element.ALIGN_LEFT, BackgroundColor = new BaseColor(230, 230, 230), Padding = 6f };
            tablaDR.AddCell(celTituloDest);
            tablaDR.AddCell(celTituloRem);
            var pDest = new iTextSharp.text.Paragraph { new Phrase($"{envio.NombreDest456VG} {envio.ApellidoDest456VG}\n", fontTexto), new Phrase($"DNI: {envio.DNIDest456VG}\n", fontTexto), new Phrase($"Tel: {envio.TeléfonoDest456VG}\n", fontTexto), new Phrase($"{envio.Domicilio456VG}\n", fontTexto), new Phrase($"{envio.Localidad456VG}, {envio.Provincia456VG}\n", fontTexto), new Phrase($"CP: {envio.CodPostal456VG}", fontTexto) };
            var celDest = new PdfPCell { Padding = 8f };
            celDest.AddElement(pDest);
            var cli = envio.Cliente;
            var pRem = new iTextSharp.text.Paragraph { new Phrase($"{cli?.Nombre456VG} {cli?.Apellido456VG}\n", fontTexto), new Phrase($"DNI: {cli?.DNI456VG}\n", fontTexto), new Phrase($"Tel: {cli?.Teléfono456VG}\n", fontTexto) };
            var celRem = new PdfPCell { Padding = 8f };
            celRem.AddElement(pRem);
            tablaDR.AddCell(celDest);
            tablaDR.AddCell(celRem);
            doc.Add(tablaDR);
            doc.Add(Chunk.NEWLINE);
            doc.Close();
        }
        public string GenerarBitacoraPDF_456VG(IEnumerable<BEEventoBitacora_456VG> eventos, Func<string, string> dniToLogin, Func<string, string> tradModulo, Func<string, string> tradAccion, string titulo = "BITÁCORA DE EVENTOS", string filtrosAplicados = null)
        {
            if (eventos == null || !eventos.Any())
                throw new Exception("No hay datos para imprimir.");
            string nombreArchivo = $"Bitacora_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string rutaCompleta = Path.Combine(DestinoBitacora_456VG, nombreArchivo);
            ultimaRutaGenerada_456VG = rutaCompleta;
            var doc = new Document(PageSize.A4.Rotate(), 36, 36, 36, 36);
            var writer = PdfWriter.GetInstance(doc, new FileStream(rutaCompleta, FileMode.Create));
            doc.Open();
            var fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            var fontSeccion = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var fontTexto = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            string rutaImagen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enviosya_mini.jpg");
            if (File.Exists(rutaImagen))
            {
                var logo = iTextSharp.text.Image.GetInstance(rutaImagen);
                logo.ScaleToFit(140f, 70f);
                logo.Alignment = Element.ALIGN_LEFT;
                doc.Add(logo);
            }
            doc.Add(new iTextSharp.text.Paragraph(titulo, fontTitulo));
            doc.Add(new iTextSharp.text.Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fontTexto));
            if (!string.IsNullOrWhiteSpace(filtrosAplicados))
                doc.Add(new iTextSharp.text.Paragraph($"Filtros: {filtrosAplicados}", fontTexto));
            doc.Add(Chunk.NEWLINE);
            var tabla = new PdfPTable(6) { WidthPercentage = 100 };
            tabla.SetWidths(new float[] { 2.2f, 1.2f, 1.1f, 2.0f, 3.0f, 1.1f });
            tabla.HeaderRows = 1;
            tabla.SplitLate = false;
            tabla.SplitRows = true;
            BaseColor grisHeader = new BaseColor(230, 230, 230);
            PdfPCell H(string t) => new PdfPCell(new Phrase(t, fontSeccion))
            { BackgroundColor = grisHeader, Padding = 5f, HorizontalAlignment = Element.ALIGN_LEFT };
            tabla.AddCell(H("Login"));
            tabla.AddCell(H("Fecha"));
            tabla.AddCell(H("Hora"));
            tabla.AddCell(H("Módulo"));
            tabla.AddCell(H("Acción"));
            tabla.AddCell(H("Criticidad"));
            PdfPCell C(string t, int align = Element.ALIGN_LEFT) =>
                new PdfPCell(new Phrase(t ?? "", fontTexto)) { Padding = 4f, HorizontalAlignment = align };
            foreach (var ev in eventos)
            {
                var login = dniToLogin?.Invoke(ev.Usuario456VG) ?? ev.Usuario456VG;
                var modulo = tradModulo?.Invoke(ev.Modulo456VG ?? "") ?? ev.Modulo456VG ?? "";
                var accion = tradAccion?.Invoke(ev.Accion456VG ?? "") ?? ev.Accion456VG ?? "";
                tabla.AddCell(C(login));
                tabla.AddCell(C(ev.Fecha456VG.ToString("dd/MM/yyyy")));
                tabla.AddCell(C(ev.Fecha456VG.ToString("HH:mm:ss")));
                tabla.AddCell(C(modulo));
                tabla.AddCell(C(accion));
                tabla.AddCell(C(ev.Criticidad456VG.ToString(), Element.ALIGN_CENTER));
            }
            doc.Add(tabla);
            doc.Close();
            return rutaCompleta;
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
