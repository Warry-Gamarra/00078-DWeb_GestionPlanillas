using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IPeriodoServiceFacade
    {
        List<int> ListarAños();

        SelectList ObtenerComboAños(int? selectedItem = null);

        SelectList ObtenerComboMesesSegunAño(int año, int? selectedItem = null);

        Response GrabarAño(int año);

        Response GrabarPeriodo(Operacion operacion, PeriodoModel model, int userID);

        List<PeriodoModel> ListarPeriodos();

        PeriodoModel ObtenerPeriodo(int periodoID);

        SelectList ObtenerComboMeses(int? selectedItem = null);

        Response Eliminar(int periodoID, int userID);
    }
}
