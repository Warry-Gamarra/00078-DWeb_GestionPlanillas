using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface ITrabajadorServiceFacade
    {
        IEnumerable<TrabajadorModel> ListarTrabajadores();

        TrabajadorModel ObtenerTrabajador(int trabajadorID);

        Response GrabarTrabajador(Operacion operacion, TrabajadorModel model, int userID);

        IEnumerable<TrabajadorConPlanillaModel> ListarTrabajadoresConPlanilla(int año, int mes);

        Tuple<string, List<TrabajadorLecturaProcesadoDTO>> ObtenerListaTrabajadores(HttpPostedFileBase file);
    }
}
