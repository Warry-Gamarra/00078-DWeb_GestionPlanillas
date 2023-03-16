using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IDocenteService
    {
        List<DocenteDTO> ListarDocentes();

        DocenteDTO ObtenerDocentePorID(int docenteID);

        List<DocenteDTO> ListarDocentePorTrabajadorID(int trabajadorID);
    }
}
