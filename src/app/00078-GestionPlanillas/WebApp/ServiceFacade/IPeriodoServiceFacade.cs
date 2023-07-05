using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApp.ServiceFacade
{
    public interface IPeriodoServiceFacade
    {
        SelectList ObtenerComboAños(int? selectedItem = null);

        SelectList ObtenerComboMeses(int I_Anio, int? selectedItem = null);
    }
}
