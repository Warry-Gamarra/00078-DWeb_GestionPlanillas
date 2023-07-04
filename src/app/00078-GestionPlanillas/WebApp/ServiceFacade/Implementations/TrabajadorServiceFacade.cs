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
        private IAdministrativoService _administrativoService;
        private IDocenteService _docenteService;

        public TrabajadorServiceFacade()
        {
            _personaService = new PersonaService();
            _trabajadorService = new TrabajadorService();
            _administrativoService = new AdministrativoService();
            _docenteService = new DocenteService();
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

                if (trabajadorModel.Vinculo.Equals(Vinculo.AdministrativoPermanente) || trabajadorModel.Vinculo.Equals(Vinculo.AdministrativoContratado))
                {
                    var administrativo = _administrativoService.ListarAdministrativoPorTrabajadorID(I_TrabajadorID).First();

                    trabajadorModel.I_NivelRemunerativoID = administrativo.I_NivelRemunerativoID;

                    trabajadorModel.T_NivelRemunerativoDesc = administrativo.T_NivelRemunerativoDesc;

                    trabajadorModel.I_GrupoOcupacionalID = administrativo.I_GrupoOcupacionalID;

                    trabajadorModel.T_GrupoOcupacionalDesc = administrativo.T_GrupoOcupacionalDesc;
                }

                if (trabajadorModel.Vinculo.Equals(Vinculo.DocentePermanente))
                {
                    var docente = _docenteService.ListarDocentePorTrabajadorID(I_TrabajadorID).First();

                    trabajadorModel.I_CategoriaDocenteID = docente.I_CategoriaDocenteID;

                    trabajadorModel.T_CategoriaDocenteDesc = docente.T_CategoriaDocenteDesc;

                    trabajadorModel.I_HorasDocenteID = docente.I_HorasDocenteID;

                    trabajadorModel.I_Horas = String.Format("{0} / {1}", docente.C_DedicacionDocenteCod, docente.I_Horas);
                }
            }

            return trabajadorModel;
        }

        public Response GrabarTrabajador(Operacion operacion, TrabajadorModel model, int userID)
        {
            Response response;

            try
            {
                var trabajadorEntity = new TrabajadorEntity()
                {
                    I_TrabajadorID = model.I_TrabajadorID,
                    I_PersonaID = model.I_PersonaID,
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
