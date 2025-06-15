using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;

namespace _456VG_Servicios
{
    public class Lenguaje_456VG : ISubject_456VG
    {
        private List<IObserver_456VG> ListaForms_456VG = new List<IObserver_456VG>();
        private Dictionary<string, string> Diccionario_456VG = new Dictionary<string, string>();
        private string idiomaActual_456VG;
        private static Lenguaje_456VG instance_456VG;
        private Lenguaje_456VG() { }
        public static Lenguaje_456VG ObtenerInstancia_456VG()
        {
            if (instance_456VG == null)
            {
                instance_456VG = new Lenguaje_456VG();
            }
            return instance_456VG;
        }
        public void Agregar_456VG(IObserver_456VG observer)
        {
            ListaForms_456VG.Add(observer);
        }
        public void Quitar_456VG(IObserver_456VG observer)
        {
            ListaForms_456VG.Remove(observer);
        }
        public void Notificar_456VG()
        {
            foreach (IObserver_456VG observer in ListaForms_456VG)
            {
                observer.ActualizarIdioma_456VG();
            }
        }
        public string IdiomaActual_456VG
        {
            get => idiomaActual_456VG;
            set
            {
                idiomaActual_456VG = value;
                CargarIdioma_456VG();
                Notificar_456VG();
            }
        }
        private void CargarIdioma_456VG()
        {
            try
            {
                string nombreArchivo = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    $"{(idiomaActual_456VG == "ES" ? "Español" : "Inglés")}.json"
                );
                if (!File.Exists(nombreArchivo))
                {
                    Console.WriteLine($"El archivo de idioma '{nombreArchivo}' no existe.");
                    Diccionario_456VG = new Dictionary<string, string>();
                    return;
                }
                string jsonString = File.ReadAllText(nombreArchivo);
                Diccionario_456VG = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString)
                                   ?? new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el archivo de idioma: {ex.Message}");
                Diccionario_456VG = new Dictionary<string, string>();
            }
        }
        public string ObtenerTexto_456VG(string key)
        {
            return (Diccionario_456VG != null && Diccionario_456VG.ContainsKey(key))
                   ? Diccionario_456VG[key]
                   : key;
        }
        public void CambiarIdiomaControles_456VG(Control frm)
        {
            try
            {
                frm.Text = ObtenerTexto_456VG(frm.Name + ".Text");
                foreach (Control c in frm.Controls)
                {
                    if (c is Button || c is Label || c is RadioButton || c is CheckBox)
                    {
                        c.Text = ObtenerTexto_456VG(frm.Name + "." + c.Name);
                    }
                    if (c is ComboBox cb)
                    {
                        string prefijo = frm.Name + ".Item.";
                        cb.Items.Clear();

                        foreach (var kvp in Diccionario_456VG)
                        {
                            if (kvp.Key.StartsWith(prefijo))
                            {
                                cb.Items.Add(kvp.Value);
                            }
                        }
                    }
                    if (c is MenuStrip m)
                    {
                        foreach (ToolStripMenuItem item in m.Items)
                        {
                            item.Text = ObtenerTexto_456VG(frm.Name + "." + item.Name);
                            CambiarIdiomaMenuStrip_456VG(item.DropDownItems, frm);
                        }
                    }
                    if (c.Controls.Count > 0)
                    {
                        CambiarIdiomaControles_456VG(c);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar el idioma de los controles: {ex.Message}");
            }
        }
        private void CambiarIdiomaMenuStrip_456VG(ToolStripItemCollection items, Control frm)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem subItem)
                {
                    subItem.Text = ObtenerTexto_456VG(frm.Name + "." + subItem.Name);
                    CambiarIdiomaMenuStrip_456VG(subItem.DropDownItems, frm);
                }
            }
        }
        public static string ObtenerEtiqueta_456VG(string NombreControl)
        {
            return ObtenerInstancia_456VG().ObtenerTexto_456VG(NombreControl);
        }
    }
}
