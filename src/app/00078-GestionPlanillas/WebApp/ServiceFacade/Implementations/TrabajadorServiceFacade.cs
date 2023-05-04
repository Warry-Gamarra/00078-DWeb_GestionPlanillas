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
            bool grabar = true;

            try
            {
                if (operacion.Equals(Operacion.Registrar) && _personaService.ObtenerPersona(model.I_TipoDocumentoID, model.C_NumDocumento) != null)
                {
                    grabar = false;
                }

                if (operacion.Equals(Operacion.Actualizar))
                {
                    var personaDTO = _personaService.ObtenerPersona(model.I_TipoDocumentoID, model.C_NumDocumento);

                    if (personaDTO != null && personaDTO.I_PersonaID != model.I_PersonaID)
                    {
                        grabar = false;
                    }
                }

                if (grabar)
                {
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
                else
                {
                    response = new Response()
                    {
                        Success = false,
                        Message = "El Num.Documento se encuentra repetido."
                    };
                }
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

        public List<TrabajadorCategoriaPlanillaModel> ListarTrabajadoresCategoriaPlanilla(int? I_CategoriaPlanillaID = null)
        {
            var lista = _trabajadorService.ListarTrabajadoresCategoriaPlanilla(I_CategoriaPlanillaID)
                .Select(x => Mapper.TrabajadorCategoriaPlanillaDTO_To_TrabajadorCategoriaPlanillaModel(x))
                .ToList();

            return lista;
        }
    }
}
