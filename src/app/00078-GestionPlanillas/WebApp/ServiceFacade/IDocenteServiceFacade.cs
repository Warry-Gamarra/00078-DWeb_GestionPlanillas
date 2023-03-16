using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IDocenteServiceFacade
    {
        List<DocenteModel> ListarDocentes();

        DocenteModel ObtenerDocentePorID(int docenteID);

        List<DocenteModel> ListarDocentePorTrabajadorID(int trabajadorID);
    }
}
