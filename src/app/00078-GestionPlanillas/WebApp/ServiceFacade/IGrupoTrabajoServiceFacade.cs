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
    public interface IGrupoTrabajoServiceFacade
    {
        Response GrabarGrupoTrabajo(Operacion operacion, GrupoTrabajoModel model, int userID);

        List<GrupoTrabajoModel> ListarGruposTrabajo();

        SelectList ObtenerComboGruposTrabajo(bool incluirDeshabilitados = false, int? selectedItem = null);

        GrupoTrabajoModel ObtenerGrupoTrabajo(int grupoTrabajoID);

        Response CambiarEstado(int grupoTrabajoID, bool estaHabilitado, int userID);

        Response Eliminar(int grupoTrabajoID, int userID);
    }
}