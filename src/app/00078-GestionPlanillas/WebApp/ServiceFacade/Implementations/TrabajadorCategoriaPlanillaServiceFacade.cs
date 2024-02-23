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

        public TrabajadorCategoriaPlanillaServiceFacade()
        {
            _trabajadorCategoriaPlanillaService = new TrabajadorCategoriaPlanillaService();
            _planillaService = new PlanillaService();
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
                .Select(x => Mapper.TrabajadorCategoriaPlanillaDTO_To_TrabajadorCategoriaPlanillaModel(x))
                .ToList();

            lista.ForEach(x => {
                x.tienePlanilla = listaTrabajadoresConPlanilla.Any(y => y.trabajadorID == x.trabajadorID);
                x.seleccionado = x.estado.Equals(Estado.Activo);
            });

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