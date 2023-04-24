using Domain.Entities;
using Domain.Enums;
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
        private IPersonaService _personaService;
        private ITrabajadorService _trabajadorService;

        public TrabajadorServiceFacade()
        {
            _personaService = new PersonaService();
            _trabajadorService = new TrabajadorService();
        }

        public List<TrabajadorModel> ListarTrabajadores()
        {
            var lista = _trabajadorService.ListarTrabajadores()
                .Select(x => Mapper.TrabajadorDTO_To_TrabajadorModel(x))
                .ToList();

            return lista;
        }

        public TrabajadorModel ObtenerTrabajador(int I_TrabajadorID)
        {
            TrabajadorModel trabajadorModel;

            var trabajadorDTO = _trabajadorService.ObtenerTrabajador(I_TrabajadorID);

            if (trabajadorDTO == null)
            {
                trabajadorModel = null;
            }
            else
            {
                trabajadorModel = Mapper.TrabajadorDTO_To_TrabajadorModel(trabajadorDTO);
            }

            return trabajadorModel;
        }

        public Response GrabarTrabajador(Operacion operacion, TrabajadorModel model, int userID)
        {
            Response response;

            try
            {
                if (operacion.Equals(Operacion.Registrar) && _personaService.ObtenerPersona(model.I_TipoDocumentoID, model.C_NumDocumento) != null)
                {
                    throw new Exception("El Num.Documento se encuentra repetido.");
                }

                var trabajadorEntity = new TrabajadorEntity()
                {
                    I_TrabajadorID = model.I_TrabajadorID,
                    C_TrabajadorCod = model.C_TrabajadorCod,
                    T_ApellidoPaterno = model.T_ApellidoPaterno,
                    T_ApellidoMaterno = model.T_ApellidoMaterno,
                    T_Nombre = model.T_Nombre,
                    I_TipoDocumentoID = model.I_TipoDocumentoID,
                    C_NumDocumento = model.C_NumDocumento,
                    D_FechaIngreso = model.D_FechaIngreso,
                    I_RegimenID = model.I_RegimenID.Value,
                    I_EstadoID = model.I_EstadoID,
                    I_VinculoID = model.I_VinculoID,
                    I_BancoID = model.I_BancoID,
                    T_NroCuentaBancaria = model.T_NroCuentaBancaria,
                    I_DependenciaID = model.I_DependenciaID,
                    I_Afp = model.I_AfpID,
                    T_Cuspp = model.T_Cuspp,
                    I_CategoriaDocenteID = model.I_CategoriaDocenteID,
                    I_HorasDocenteID = model.I_HorasDocenteID,
                    I_GrupoOcupacionalID = model.I_GrupoOcupacionalID,
                    I_NivelRemunerativoID = model.I_NivelRemunerativoID
                };

                response = _trabajadorService.GrabarTrabajador(operacion, trabajadorEntity, userID);
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
