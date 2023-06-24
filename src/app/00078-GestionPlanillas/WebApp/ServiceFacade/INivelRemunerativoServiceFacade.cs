using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApp.ServiceFacade
{
    public interface INivelRemunerativoServiceFacade
    {
        SelectList ObtenerComboNivelesRemunerativos(bool incluirDeshabilitados = false, int? selectedItem = null);

        List<NivelRemunerativoDTO> ListarNivelesRemunerativos(bool incluirDeshabilitados = false);
    }
}
