using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IPlantillaPlanillaConceptoServiceFacade
    {
        Response GrabarPlantillaPlanillaConcepto(Operacion operacion, ConceptoAsignadoPlantillaModel model, int userID);

        List<ConceptoAsignadoPlantillaModel> ListarConceptosAsignados(int plantillaPlanillaID);

        ConceptoAsignadoPlantillaModel ObtenerPlantillaPlanillaConcepto(int plantillaPlanillaConceptoID);

        Response CambiarEstado(int plantillaPlanillaConceptoID, bool estaHabilitado, int userID);
    }
}
