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

        public IEnumerable<ResumenPlanillaTrabajadorModel> ListarResumenPlanillaTrabajador(int año, int mes, int idCategoria)
        {
            var lista = _planillaService.ListarResumenPlanillaTrabajadores(año, mes, idCategoria)
                .Select(x => Mapper.ResumenPlanillaTrabajadorDTO_To_ResumenPlanillaTrabajadorModel(x));

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

        public IEnumerable<TotalPlanillaDependenciaModel> ListarTotalPlanillaPorDependencia(int año, int mes, int idCategoria)
        {
            var lista = _planillaService.ListarTotalPlanillaPorDependencia(año, mes, idCategoria)
                .Select(x => Mapper.TotalPlanillaDependenciaDTO_To_TotalPlanillaDependenciaModel(x));

            return lista;
        }

        public ResumenSIAFModel ListarResumenSIAF(int año, int mes, int idCategoria)
        {
            var dto = _planillaService.ListarResumenSIAF(año, mes, idCategoria);

            var model = new ResumenSIAFModel(dto.cabecera, dto.detalle);

            return model;
        }
    }
}
