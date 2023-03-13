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
        private IPlanillaService planillaService;

        public PlanillaServiceFacade()
        {
            planillaService = new PlanillaService();
        }

        public List<ResumenPlanillaTrabajadorModel> ListarResumenPlanillaTrabajador()
        {
            var lista = new List<ResumenPlanillaTrabajadorModel>();

            lista = planillaService.ListarResumenPlanillaTrabajadores().Select(x => new ResumenPlanillaTrabajadorModel() {
                I_TrabajadorID = x.I_TrabajadorID,
                T_Nombre = x.T_Nombre,
                T_ApellidoPaterno = x.T_ApellidoPaterno,
                T_ApellidoMaterno = x.T_ApellidoMaterno,
                I_TipoDocumentoID = x.I_TipoDocumentoID,
                T_TipoDocumentoDesc = x.T_TipoDocumentoDesc,
                C_NumDocumento = x.C_NumDocumento,
                D_FechaIngreso = x.D_FechaIngreso,
                I_RegimenID = x.I_RegimenID,
                T_RegimenDesc = x.T_RegimenDesc,
                I_EstadoID = x.I_EstadoID,
                T_EstadoDesc = x.T_EstadoDesc,
                I_VinculoID = x.I_VinculoID,
                T_VinculoDesc = x.T_VinculoDesc,
                I_TrabajadorPlanillaID = x.I_TrabajadorPlanillaID,
                I_TotalRemuneracion = x.I_TotalRemuneracion,
                I_TotalDescuento = x.I_TotalDescuento,
                I_TotalReintegro = x.I_TotalReintegro,
                I_TotalDeduccion = x.I_TotalDeduccion,
                I_TotalSueldo = x.I_TotalSueldo,
                I_PlanillaID = x.I_PlanillaID,
                I_PeriodoID = x.I_PeriodoID,
                I_Anio = x.I_Anio,
                T_MesDesc = x.T_MesDesc,
                I_CategoriaPlanillaID = x.I_CategoriaPlanillaID,
                T_CategoriaPlanillaDesc = x.T_CategoriaPlanillaDesc
            }).ToList();

            return lista;
        }
    }
}
