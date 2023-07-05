using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade
{
    public interface IBancoServiceFacade
    {
        SelectList ObtenerComboBancos(bool incluirDeshabilitados = false, int? selectedItem = null);
    }
}