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


            // Iterar sobre los submenús de forma recursiva
            foreach (ToolStripMenuItem subMenuItem in menuItem.DropDownItems)
            {
                // Verificar si el submenú tiene permisos
                var permisoFormulario2 = permisos.FirstOrDefault(p => p.NombreFormulario456VG == subMenuItem.Name);

                if (permisoFormulario2 != null)
                {
                    // Si tiene permiso, habilitarlo
                    subMenuItem.Enabled = true;
                }
                else
                {
                    // Si no tiene permiso, desabilitarlo
                    subMenuItem.Enabled = false;
                }

                // Llamar recursivamente si tiene submenús
                //if (subMenuItem.HasDropDownItems)
                //{
                //    HabilitarMenusPorPermisos(subMenuItem, permisos);
                //}
            }
        }
    }
}
