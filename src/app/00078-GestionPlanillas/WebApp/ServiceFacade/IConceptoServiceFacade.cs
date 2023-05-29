using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IConceptoServiceFacade
    {
        Response GrabarConcepto(Operacion operacion, ConceptoModel model, int userID);

        List<ConceptoModel> ListarConceptos();

        ConceptoModel ObtenerConcepto(int conceptoID);

        Response CambiarEstado(int conceptoID, bool estaHabilitado, int userID);
    }
}