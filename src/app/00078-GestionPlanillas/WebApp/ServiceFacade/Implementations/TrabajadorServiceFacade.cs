using Domain.Entities;
using Domain.Helpers;
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
        private ITrabajadorService _trabajadorService;

        public TrabajadorServiceFacade()
        {
            _trabajadorService = new TrabajadorService();
        }

        public List<TrabajadorModel> ListarTrabajadores()
        {
            var lista = _trabajadorService.ListarTrabajadores()
                .Select(x => Mapper.TrabajadorDTO_To_TrabajadorModel(x))
                .ToList();

            return lista;
        }

        public Response GrabarTrabajador(TrabajadorModel model, int userID)
        {
            Response response;

            try
            {
                var trabajadorEntity = new TrabajadorEntity()
                {
                    C_TrabajadorCod = model.C_TrabajadorCod,
                    T_ApellidoPaterno = model.T_ApellidoPaterno,
                    T_ApellidoMaterno = model.T_ApellidoMaterno,
                    T_Nombre = model.T_Nombre,
                    I_TipoDocumentoID = model.I_TipoDocumentoID,
                    C_NumDocumento = model.C_NumDocumento,
                    I_RegimenID = model.I_RegimenID.Value,
                    I_EstadoID = model.I_EstadoID.Value,
                    I_VinculoID = model.I_VinculoID.Value
                };

                response = _trabajadorService.GrabarTrabajador(trabajadorEntity, userID);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Success = false,
                    Message = ex.Message
                };
            }            

            return response;
        }
    }
}
