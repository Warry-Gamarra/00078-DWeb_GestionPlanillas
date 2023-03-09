using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class TrabajadorServiceFacade : ITrabajadorServiceFacade
    {
        private ITrabajadorService trabajadorService;

        public TrabajadorServiceFacade()
        {
            trabajadorService = new TrabajadorService();
        }

        public List<TrabajadorModel> ListarTrabajadores()
        {
            var lista = new List<TrabajadorModel>();

            lista = trabajadorService.ListarTrabajadores().Select(x => new TrabajadorModel() {
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
                T_VinculoDesc = x.T_VinculoDesc
            }).ToList();

            return lista;
        }
    }
}
