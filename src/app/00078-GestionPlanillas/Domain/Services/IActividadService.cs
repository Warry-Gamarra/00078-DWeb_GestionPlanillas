using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IActividadService
    {
        Response GrabarActividad(Operacion operacion, ActividadEntity actividadEntity, int userID);

        List<ActividadDTO> ListarActividades();

        ActividadDTO ObtenerActividad(int actividadID);

        Response Eliminar(int actividadID, int userID);
    }
}
