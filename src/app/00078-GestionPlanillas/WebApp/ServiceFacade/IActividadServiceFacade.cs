using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IActividadServiceFacade
    {
        Response GrabarActividad(Operacion operacion, ActividadModel model, int userID);

        List<ActividadModel> ListarActividades();

        SelectList ObtenerComboActividades(int? selectedItem = null);

        ActividadModel ObtenerActividad(int actividadID);

        Response Eliminar(int actividadID, int userID);
    }
}