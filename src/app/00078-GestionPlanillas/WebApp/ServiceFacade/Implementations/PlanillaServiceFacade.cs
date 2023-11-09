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

        public List<ResumenPlanillaTrabajadorModel> ListarResumenPlanillaTrabajador(int año, int mes, int idCategoria)
        {
            var lista = new List<ResumenPlanillaTrabajadorModel>();

            lista = _planillaService.ListarResumenPlanillaTrabajadores(año, mes, idCategoria)
                .Select(x => Mapper.ResumenPlanillaTrabajadorDTO_To_ResumenPlanillaTrabajadorModel(x))
                .ToList();

            return lista;
        }

        public Response GenerarPlanilla(List<int> trabajadores, int año, int mes, int categoriaPlanillaID, int userID)
        {
            Response response;

            try
            {
                response = _planillaService.GenerarPlanilla(trabajadores, año, mes, categoriaPlanillaID, userID);
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

        public List<TotalPlanillaDependenciaModel> ListarTotalPlanillaPorDependencia(int año, int mes, int idCategoria)
        {
            var lista = _planillaService.ListarTotalPlanillaPorDependencia(año, mes, idCategoria)
                .Select(x => Mapper.TotalPlanillaDependenciaDTO_To_TotalPlanillaDependenciaModel(x))
                .ToList();

            return lista;
        }

        public IEnumerable<IDictionary<string, object>> ListarResumenSIAF(int año, int mes, int idCategoria)
        {
            var lista = _planillaService.ListarResumenSIAF(año, mes, idCategoria);

            return lista;
        }
    }
}
