using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApp.ServiceFacade
{
    public interface IAfpServiceFacade
    {
        SelectList ObtenerComboAfps(bool incluirDeshabilitados = false, int? selectedItem = null);
    }
}
