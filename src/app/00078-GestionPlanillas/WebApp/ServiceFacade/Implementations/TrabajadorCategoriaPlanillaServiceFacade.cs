using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class TrabajadorCategoriaPlanillaServiceFacade : ITrabajadorCategoriaPlanillaServiceFacade
    {
        private ITrabajadorCategoriaPlanillaService _trabajadorCategoriaPlanillaService;
        private IPlanillaService _planillaService;
        private IPeriodoService _periodoService;

        public TrabajadorCategoriaPlanillaServiceFacade()
        {
            _trabajadorCategoriaPlanillaService = new TrabajadorCategoriaPlanillaService();
            _planillaService = new PlanillaService();
            _periodoService = new PeriodoService();
        }

        public Response GrabarTrabajadorCategoriaPlanilla(Operacion operacion, TrabajadorCategoriaPlanillaModel model, int userID)
        {
            Response response;

            try
            {
                var trabajadorCategoriaPlanillaEntity = new TrabajadorCategoriaPlanillaEntity()
                {
                    trabajadorCategoriaPlanillaID = model.trabajadorCategoriaPlanillaID,
                    trabajadorID = model.trabajadorID,
                    categoriaPlanillaID = model.categoriaPlanillaID,
                    dependenciaID = model.dependenciaID,
                    grupoTrabajoID = model.grupoTrabajoID,
                    esJefe = model.esJefe
                };

                response = _trabajadorCategoriaPlanillaService.GrabarTrabajadorCategoriaPlanilla(operacion, trabajadorCategoriaPlanillaEntity, userID);
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

        public List<TrabajadorCategoriaPlanillaModel> ListarCategoriaPlanillaPorTrabajador(int trabajadorID)
        {
            return _trabajadorCategoriaPlanillaService.ListarCategoriaPlanillaPorTrabajador(trabajadorID)
                .Select(x => Mapper.TrabajadorCategoriaPlanillaDTO_To_TrabajadorCategoriaPlanillaModel(x))
                .ToList();
        }

        public List<TrabajadorCategoriaPlanillaModel> ListarTrabajadoresAptos(int anio, int mes, int categoriaPlanillaID)
        {
            var listaTrabajadoresConPlanilla = _planillaService.ListarResumenPlanillaTrabajadores(anio, mes, categoriaPlanillaID);

            var lista = _trabajadorCategoriaPlanillaService.ListarTrabajadoresCategoriaPlanilla(categoriaPlanillaID)
                .Where(x => x.estaHabilitado)
                .Select(x => Mapper.TrabajadorCategoriaPlanillaDTO_To_TrabajadorCategoriaPlanillaModel(x))
                .ToList();

            lista.ForEach(x => {
                x.tienePlanilla = listaTrabajadoresConPlanilla.Any(y => y.trabajadorCategoriaPlanillaID == x.trabajadorCategoriaPlanillaID);
                x.seleccionado = x.estado.Equals(Estado.Activo);
            });

            return lista;
        }

        public FileContent ListarTrabajadoresAptos(int año, int mes, int categoriaPlanillaID, FormatoArchivo formatoArchivo)
        {
            IGeneracionArchivoService generacionArchivoService;
            FileContent fileContent;

            try
            {
                var listaTrabajadoresConPlanilla = _planillaService.ListarResumenPlanillaTrabajadores(año, mes, categoriaPlanillaID);

                var data = _trabajadorCategoriaPlanillaService.ListarTrabajadoresCategoriaPlanilla(categoriaPlanillaID)
                    .Where(x => x.estaHabilitado)
                    .ToList();

                data.ForEach(x => {
                    x.tienePlanilla = listaTrabajadoresConPlanilla.Any(y => y.trabajadorCategoriaPlanillaID == x.trabajadorCategoriaPlanillaID);
                });

                generacionArchivoService = FileManagement.GetGeneracionArchivoService(formatoArchivo);

                fileContent = generacionArchivoService.GenerarDescargableTrabajadoresAptos(data, año, _periodoService.ObtenerMesDesc(mes));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fileContent;
        }

        public List<TrabajadorCategoriaPlanillaModel> ListarTrabajadoresAsignadosCategoria(int categoriaPlanillaID)
        {
            var lista = _trabajadorCategoriaPlanillaService.ListarTrabajadoresCategoriaPlanilla(categoriaPlanillaID)
                .Select(x => Mapper.TrabajadorCategoriaPlanillaDTO_To_TrabajadorCategoriaPlanillaModel(x))
                .ToList();

            return lista;
        }

        public TrabajadorCategoriaPlanillaModel ObtenerTrabajadorCategoriaPlanilla(int trabajadorCategoriaPlanillaID)
        {
            TrabajadorCategoriaPlanillaModel trabajadorCategoriaPlanillaModel;

            var trabajadorCategoriaPlanillaDTO = _trabajadorCategoriaPlanillaService.ObtenerTrabajadorCategoriaPlanilla(trabajadorCategoriaPlanillaID);

            if (trabajadorCategoriaPlanillaDTO == null)
            {
                trabajadorCategoriaPlanillaModel = null;
            }
            else
            {
                trabajadorCategoriaPlanillaModel = Mapper.TrabajadorCategoriaPlanillaDTO_To_TrabajadorCategoriaPlanillaModel(trabajadorCategoriaPlanillaDTO);
            }

            return trabajadorCategoriaPlanillaModel;
        }

        public Response CambiarEstado(int trabajadorCategoriaPlanillaID, bool estaHabilitado, int userID)
        {
            return _trabajadorCategoriaPlanillaService.CambiarEstado(trabajadorCategoriaPlanillaID, estaHabilitado, userID);
        }

        public Response Eliminar(int trabajadorCategoriaPlanillaID, int userID)
        {
            return _trabajadorCategoriaPlanillaService.Eliminar(trabajadorCategoriaPlanillaID, userID);
        }

        public CategoriaPlanilla ObtenerCategoriaPlanillaSegunVinculo(int vinculoID)
        {
            return _trabajadorCategoriaPlanillaService.ObtenerCategoriaPlanillaSegunVinculo(vinculoID);
        }
    }
}