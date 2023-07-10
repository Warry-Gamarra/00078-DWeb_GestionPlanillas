using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.ServiceFacade.Implementations
{
    public class ValorExternoConceptoServiceFacade : IValorExternoConceptoServiceFacade
    {
        public Response GrabarValoresExternos(string fileName, int userID)
        {
            Response response;

            try
            {
                response = null;
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message
                };
            }

            return response;
        }
    }
}