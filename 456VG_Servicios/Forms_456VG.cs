using _456VG_BE;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _456VG_Servicios
{
    public class Forms_456VG
    {
        public void DeshabilitarTodosLosMenus(ToolStripMenuItem item)
        {
            item.Enabled = false;
            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                if (subItem is ToolStripMenuItem subMenuItem)
                {
                    DeshabilitarTodosLosMenus(subMenuItem);
                }
            }
        }
        public void HabilitarMenusPorPermisos(ToolStripMenuItem menuItem, List<Permiso_456VG> permisos)
        {
            var nombres = permisos.Select(p => p.Nombre456VG).Distinct().ToList();
            HabilitarMenusRecursivo(menuItem, nombres);
        }
        private void HabilitarMenusRecursivo(ToolStripMenuItem menuItem, List<string> nombresDePermisos)
        {
            menuItem.Enabled = nombresDePermisos.Contains(menuItem.Name);

            foreach (ToolStripItem subItem in menuItem.DropDownItems)
            {
                if (subItem is ToolStripMenuItem subMenuItem)
                {
                    HabilitarMenusRecursivo(subMenuItem, nombresDePermisos);
                }
            }
        }
    }
}
