using Data.Views;
using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class DocenteService : IDocenteService
    {
        public List<DocenteDTO> ListarDocentes()
        {
            var lista = VW_Docentes.FindAll().Select(x => Mapper.VW_Docentes_To_DocenteDTO(x)).ToList();

            return lista;
        }
        
        public DocenteDTO ObtenerDocentePorID(int docenteID)
        {
            var entity = VW_Docentes.FindByDocenteID(docenteID);

            if (entity != null)
            {
                return Mapper.VW_Docentes_To_DocenteDTO(entity);
            }

            return null;
        }

        public List<DocenteDTO> ListarDocentePorTrabajadorID(int trabajadorID)
        {
            var lista = VW_Docentes.FindByTrabajadorID(trabajadorID).Select(x => Mapper.VW_Docentes_To_DocenteDTO(x)).ToList();

            return lista;
        }
    }
}
