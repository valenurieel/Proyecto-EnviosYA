using _456VG_BE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace _456VG_Servicios
{
    public class ArchivoIMP_456VG
    {
        public string DestinoFactura_456VG { get; set; }
        public string DestinoBitacora_456VG { get; set; }
        public string DestinoReporteInteligente_456VG { get; set; }
        private string ultimaRutaGenerada_456VG;
        public ArchivoIMP_456VG()
        {
            string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EnviosYA");
            DestinoFactura_456VG = Path.Combine(basePath, "Facturas");
            DestinoBitacora_456VG = Path.Combine(basePath, "Bitacoras");
            DestinoReporteInteligente_456VG = Path.Combine(basePath, "Reportes");
            if (!Directory.Exists(DestinoFactura_456VG)) Directory.CreateDirectory(DestinoFactura_456VG);
            if (!Directory.Exists(DestinoBitacora_456VG)) Directory.CreateDirectory(DestinoBitacora_456VG);
            if (!Directory.Exists(DestinoReporteInteligente_456VG)) Directory.CreateDirectory(DestinoReporteInteligente_456VG);
            QuestPDF.Settings.License = LicenseType.Community;
        }
        public void GenerarFacturaDetalladaPDF_456VG(BEFactura_456VG factura)
        {
            try
            {
                string rutaCompleta = Path.Combine(DestinoFactura_456VG, $"Factura_{factura.CodFactura456VG}.pdf");
                ultimaRutaGenerada_456VG = rutaCompleta;
                Document.Create(doc =>
                {
                    doc.Page(page =>
                    {
                        page.Margin(40);
                        page.Header().AlignCenter().Column(col => {
                            string img = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enviosya_mini.jpg");
                            if (File.Exists(img)) col.Item().Width(150).Image(img);
                            else col.Item().Text("ENVIOSYA").FontSize(22).Bold().FontColor(Colors.Red.Medium);
                        });
                        page.Content().PaddingVertical(10).Column(col =>
                        {
                            col.Item().Text($"Factura N°: {factura.CodFactura456VG}").FontSize(10);
                            col.Item().Text($"Fecha de Emisión: {factura.FechaEmision456VG:dd/MM/yyyy}").FontSize(10);
                            col.Item().Text($"Hora de Emisión: {factura.HoraEmision456VG:hh\\:mm\\:ss}").FontSize(10);
                            col.Item().PaddingTop(10).Text("Datos del Cliente").Bold().FontSize(12);
                            col.Item().Text($"DNI: {factura.Envio.Cliente.DNI456VG}");
                            col.Item().Text($"Nombre y Apellido: {factura.Envio.Cliente.Nombre456VG} {factura.Envio.Cliente.Apellido456VG}");
                            col.Item().PaddingTop(10).Text("Datos del Envío").Bold().FontSize(12);
                            col.Item().Text($"Código de Envío: {factura.Envio.CodEnvio456VG}");
                            col.Item().Text($"Tipo de Envío: '{factura.Envio.tipoenvio456VG}'");
                            col.Item().Row(r => {
                                r.AutoItem().Text("Recargo por Zona ");
                                r.RelativeItem().PaddingBottom(2).BorderBottom(1).BorderColor(Colors.Black).ExtendHorizontal();
                                r.AutoItem().Text($" ${220.82:N2}");
                            });
                            decimal basePrice = factura.Envio.Paquetes.Sum(p => 500 + (decimal)p.Peso456VG * 10);
                            decimal recargoTipo = factura.Envio.tipoenvio456VG == "Express" ? basePrice * 0.20m : basePrice * 0.10m;
                            col.Item().Row(r => {
                                r.AutoItem().Text("Recargo por Tipo de Envío ");
                                r.RelativeItem().PaddingBottom(2).BorderBottom(1).BorderColor(Colors.Black).ExtendHorizontal();
                                r.AutoItem().Text($" ${recargoTipo:N2}");
                            });
                            col.Item().PaddingTop(10).Text("Datos del Pago").Bold().FontSize(12);
                            col.Item().Text($"Medio de Pago: {factura.DatosPago?.MedioPago456VG ?? "Crédito"}");
                            col.Item().PaddingTop(10).Text("Detalle de Paquetes").Bold().FontSize(12);
                            col.Item().Table(t =>
                            {
                                t.ColumnsDefinition(c => { c.RelativeColumn(2); c.RelativeColumn(); c.RelativeColumn(); c.RelativeColumn(); c.RelativeColumn(); c.RelativeColumn(); });
                                t.Header(h => {
                                    h.Cell().Element(StyleHeaderUAI).Text("Código");
                                    h.Cell().Element(StyleHeaderUAI).Text("Peso");
                                    h.Cell().Element(StyleHeaderUAI).Text("Alto");
                                    h.Cell().Element(StyleHeaderUAI).Text("Ancho");
                                    h.Cell().Element(StyleHeaderUAI).Text("Largo");
                                    h.Cell().Element(StyleHeaderUAI).Text("Precio");
                                });
                                foreach (var p in factura.Envio.Paquetes)
                                {
                                    t.Cell().Element(StyleCellUAI).Text(p.CodPaq456VG);
                                    t.Cell().Element(StyleCellUAI).Text($"{p.Peso456VG:N2}");
                                    t.Cell().Element(StyleCellUAI).Text($"{p.Alto456VG:N2}");
                                    t.Cell().Element(StyleCellUAI).Text($"{p.Ancho456VG:N2}");
                                    t.Cell().Element(StyleCellUAI).Text($"{p.Largo456VG:N2}");
                                    t.Cell().Element(StyleCellUAI).Text($"${(500 + (decimal)p.Peso456VG * 10):N2}");
                                }
                            });
                            col.Item().PaddingTop(20).Row(r => {
                                r.AutoItem().Text("TOTAL FINAL ").Bold().FontSize(14);
                                r.RelativeItem().PaddingBottom(2).BorderBottom(2).BorderColor(Colors.Black).ExtendHorizontal();
                                r.AutoItem().Text($" ${factura.Envio.Importe456VG:N2}").Bold().FontSize(14);
                            });
                            col.Item().PaddingTop(30).AlignCenter().Text("¡Gracias por confiar en nosotros!").Bold().FontSize(14);
                            col.Item().PaddingTop(20).Column(c => {
                                c.Item().Text("Nuestros Contactos").Bold().FontSize(11).Underline();
                                c.Item().Text("Teléfono: +54 11-2711-8942");
                                c.Item().Text("Email: consultas@enviosya.com.ar");
                                c.Item().Text("Dirección: Asturiano 2345 Temperley, Buenos Aires.");
                            });
                        });
                        page.Footer().AlignCenter().Text(t => t.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).FontSize(9));
                    });
                }).GeneratePdf(rutaCompleta);
            }
            catch (Exception ex) { throw new Exception("Error Factura: " + ex.Message); }
        }
        public string GenerarBitacoraPDF_456VG(IEnumerable<BEEventoBitacora_456VG> eventos, Func<string, string> dniToLogin, string titulo = "BITÁCORA DE EVENTOS", string filtrosAplicados = null)
        {
            string rutaCompleta = Path.Combine(DestinoBitacora_456VG, $"Bitacora_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
            ultimaRutaGenerada_456VG = rutaCompleta;
            Document.Create(doc => {
                doc.Page(page => {
                    page.Margin(25);
                    page.Header().Row(row => {
                        row.RelativeItem().Text(titulo).FontSize(18).Bold().FontColor(Colors.Red.Medium);
                        row.RelativeItem().AlignRight().Text("EnviosYA").FontSize(10);
                    });
                    page.Content().PaddingVertical(10).Column(col => {
                        if (!string.IsNullOrWhiteSpace(filtrosAplicados))
                            col.Item().PaddingBottom(5).Text(filtrosAplicados).FontSize(9).Italic();
                        col.Item().Table(t => {
                            t.ColumnsDefinition(c => { c.RelativeColumn(); c.RelativeColumn(1.5f); c.RelativeColumn(2.5f); c.RelativeColumn(0.5f); });
                            t.Header(h => {
                                h.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Usuario").Bold();
                                h.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Fecha").Bold();
                                h.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Acción").Bold();
                                h.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Crit.").Bold();
                            });
                            foreach (var ev in eventos)
                            {
                                t.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(5).Text(dniToLogin?.Invoke(ev.Usuario456VG) ?? ev.Usuario456VG);
                                t.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(5).Text(ev.Fecha456VG.ToString("g"));
                                t.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(5).Text(ev.Accion456VG ?? "");
                                t.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(5).Text(ev.Criticidad456VG.ToString());
                            }
                        });
                    });
                });
            }).GeneratePdf(rutaCompleta);
            return rutaCompleta;
        }
        public void GenerarReporteInteligentePDF_456VG(BEReporteInteligente_456VG reporte)
        {
            try
            {
                var lng = Lenguaje_456VG.ObtenerInstancia_456VG();
                string rutaCompleta = Path.Combine(DestinoReporteInteligente_456VG, $"Reporte_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
                ultimaRutaGenerada_456VG = rutaCompleta;
                Document.Create(doc => {
                    doc.Page(page => {
                        page.Margin(40);
                        page.Header().Text(lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.Titulo")).FontSize(20).Bold().FontColor(Colors.Red.Medium);
                        page.Content().PaddingVertical(20).Column(col => {
                            col.Spacing(10);
                            col.Item().Text($"{lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.MesAnalizado")}: {reporte.Mes}/{reporte.Año}");
                            col.Item().Text($"{lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.FacturacionMensual")}: ${reporte.FacturacionMensual:N2}");
                            if (reporte.MesesInactividad > 0) col.Item().Text($"{lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.InactivityState")} ({reporte.MesesInactividad} meses)").FontColor(Colors.Orange.Medium).Bold();
                            col.Item().Text($"{lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.ComparedLastClose")}: {reporte.PeriodoReferencia}");
                            string signo = reporte.VariacionVsAnterior >= 0 ? "+" : "";
                            col.Item().PaddingTop(10).Background(Colors.Grey.Lighten4).Padding(10).Text($"{lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.StrategicVar")}: {signo}{reporte.VariacionVsAnterior:N2}%").Bold();
                            col.Item().Text($"{lng.ObtenerTexto_456VG("ReporteInteligente_456VG.Report.TendenciaAnual")}: {reporte.TendenciaAnual.ToUpper()} ({reporte.VariacionAnual:N2}%)");
                        });
                    });
                }).GeneratePdf(rutaCompleta);
            }
            catch (Exception ex) { throw new Exception("Error Reporte: " + ex.Message); }
        }
        private IContainer StyleHeaderUAI(IContainer container) => container.Border(1).BorderColor(Colors.Black).Background(Colors.Grey.Lighten3).Padding(2).AlignCenter();
        private IContainer StyleCellUAI(IContainer container) => container.Border(1).BorderColor(Colors.Black).Padding(2).AlignCenter();
        public void AbrirUltimoPDF_456VG()
        {
            if (!string.IsNullOrEmpty(ultimaRutaGenerada_456VG) && File.Exists(ultimaRutaGenerada_456VG))
                Process.Start(new ProcessStartInfo(ultimaRutaGenerada_456VG) { UseShellExecute = true });
        }
    }
}
