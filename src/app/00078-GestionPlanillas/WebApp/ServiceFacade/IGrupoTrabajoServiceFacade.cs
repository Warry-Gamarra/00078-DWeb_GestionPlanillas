using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade
{
    public interface IGrupoTrabajoServiceFacade
    {
        SelectList ObtenerComboGruposTrabajo(bool incluirDeshabilitados = false, int? selectedItem = null);
    }
}