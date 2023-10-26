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
    public interface IDepActividadMetaService
    {
        Response GrabarDepActividadMeta(Operacion operacion, DepActividadMetaEntity depActividadMetaEntity, int userID);

        List<DepActividadMetaDTO> ListarDepActividadMetas();

        DepActividadMetaDTO ObtenerDepActividadMeta(int depActividadMetaID);

        Response Eliminar(int depActividadMetaID, int userID);
    }
}
