using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _456VG_Servicios
{
    public class Forms_456VG
    {
        //Habilita Menues y submenues según los permisos del Usuario.
        public void HabilitarMenusPorPermisos(ToolStripMenuItem menuItem, List<Permiso_456VG> permisos)
        {
            var permisoFormulario = permisos.FirstOrDefault(p => p.NombreFormulario456VG == menuItem.Name);
            if (permisoFormulario == null)
            {
                menuItem.Enabled = false;
            }
            else
            {
                menuItem.Enabled = true;
            }
            foreach (ToolStripMenuItem subMenuItem in menuItem.DropDownItems)
            {
                var permisoFormulario2 = permisos.FirstOrDefault(p => p.NombreFormulario456VG == subMenuItem.Name);
                if (permisoFormulario2 != null)
                {
                    subMenuItem.Enabled = true;
                }
                else
                {
                    subMenuItem.Enabled = false;
                }
            }
        }
    }
}
