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
    public interface IConceptoServiceFacade
    {
        Response GrabarConcepto(Operacion operacion, ConceptoModel model, int userID);

        List<ConceptoModel> ListarConceptos();

        SelectList ObtenerComboConceptos(bool incluirDeshabilitados);

        ConceptoModel ObtenerConcepto(int conceptoID);

        Response CambiarEstado(int conceptoID, bool estaHabilitado, int userID);
    }
}