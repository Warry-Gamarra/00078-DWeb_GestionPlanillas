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

                    trabajadorModel.nivelRemunerativoID = administrativo.nivelRemunerativoID;

                    trabajadorModel.nivelRemunerativoDesc = administrativo.nivelRemunerativoDesc;

                    trabajadorModel.grupoOcupacionalID = administrativo.grupoOcupacionalID;

                    trabajadorModel.grupoOcupacionalDesc = administrativo.grupoOcupacionalDesc;
                }

                if (trabajadorModel.Vinculo.Equals(Vinculo.DocentePermanente))
                {
                    var docente = _docenteService.ListarDocentePorTrabajadorID(I_TrabajadorID).First();

                    trabajadorModel.categoriaDocenteID = docente.categoriaDocenteID;

                    trabajadorModel.categoriaDocenteDesc = docente.categoriaDocenteDesc;

                    trabajadorModel.horasDocenteID = docente.horasDocenteID;

                    trabajadorModel.horas = String.Format("{0} / {1}", docente.dedicacionDocenteCod, docente.horas);
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
                    I_TrabajadorID = model.trabajadorID,
                    I_PersonaID = model.personaID,
                    C_TrabajadorCod = model.trabajadorCod,
                    T_ApellidoPaterno = model.apellidoPaterno,
                    T_ApellidoMaterno = model.apellidoMaterno,
                    T_Nombre = model.nombre,
                    I_TipoDocumentoID = model.tipoDocumentoID,
                    C_NumDocumento = model.numDocumento,
                    D_FechaIngreso = model.fechaIngreso,
                    I_RegimenID = model.regimenID.Value,
                    I_EstadoID = model.estadoID,
                    I_VinculoID = model.vinculoID,
                    I_BancoID = model.bancoID,
                    T_NroCuentaBancaria = model.nroCuentaBancaria,
                    I_DependenciaID = model.dependenciaID,
                    I_Afp = model.afpID,
                    T_Cuspp = model.cuspp,
                    I_CategoriaDocenteID = model.categoriaDocenteID,
                    I_HorasDocenteID = model.horasDocenteID,
                    I_GrupoOcupacionalID = model.grupoOcupacionalID,
                    I_NivelRemunerativoID = model.nivelRemunerativoID
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
