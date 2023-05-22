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
    public interface IPlantillaPlanillaServiceFacade
    {
        Response GrabarPlantillaPlanilla(Operacion operacion, PlantillaPlanillaModel model, int userID);

        List<PlantillaPlanillaModel> ListarPlantillasPlanilla();

        PlantillaPlanillaModel ObtenerPlantillaPlanilla(int plantillaPlanillaID);

        Response CambiarEstado(int plantillaPlanillaID, bool estadHabilitado, int userID);
    }
}
