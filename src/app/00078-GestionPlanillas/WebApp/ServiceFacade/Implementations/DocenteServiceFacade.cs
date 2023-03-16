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
    public class DocenteServiceFacade : IDocenteServiceFacade
    {
        IDocenteService _docenteService;

        public DocenteServiceFacade()
        {
            _docenteService = new DocenteService();
        }

        public List<DocenteModel> ListarDocentes()
        {
            var lista = _docenteService.ListarDocentes()
                .Select(x => Mapper.DocenteDTO_To_DocenteModel(x))
                .ToList();

            return lista;
        }

        public DocenteModel ObtenerDocentePorID(int docenteID)
        {
            var docenteDTO = _docenteService.ObtenerDocentePorID(docenteID);

            if (docenteDTO != null)
            {
                return Mapper.DocenteDTO_To_DocenteModel(docenteDTO);
            }

            return null;
        }

        public List<DocenteModel> ListarDocentePorTrabajadorID(int trabajadorID)
        {
            var lista = _docenteService.ListarDocentePorTrabajadorID(trabajadorID)
                .Select(x => Mapper.DocenteDTO_To_DocenteModel(x))
                .ToList();

            return lista;
        }
    }
}
