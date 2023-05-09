using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class PlanillaServiceFacade : IPlanillaServiceFacade
    {
        private IPlanillaService _planillaService;

        public PlanillaServiceFacade()
        {
            _planillaService = new PlanillaService();
        }

        public List<ResumenPlanillaTrabajadorModel> ListarResumenPlanillaTrabajador()
        {
            var lista = new List<ResumenPlanillaTrabajadorModel>();

            lista = _planillaService.ListarResumenPlanillaTrabajadores()
                .Select(x => Mapper.ResumenPlanillaTrabajadorDTO_To_ResumenPlanillaTrabajadorModel(x))
                .ToList();

            return lista;
        }

        public Response GenerarPlanilla(List<int> trabajadores, int I_Anio, int I_Mes, int I_CategoriaPlanillaID, int userID)
        {
            Response response;

            try
            {
                response = _planillaService.GenerarPlanilla(trabajadores, I_Anio, I_Mes, I_CategoriaPlanillaID, userID);
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
