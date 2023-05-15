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
        List<ConceptoModel> ListarConceptos();

        ConceptoModel ObtenerConcepto(int I_ConceptoID);

        Response GrabarConcepto(Operacion operacion, ConceptoModel model, int userID);
    }
}