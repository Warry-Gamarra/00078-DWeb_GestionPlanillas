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
    public interface IMetaService
    {
        Response GrabarMeta(Operacion operacion, MetaEntity metaEntity, int userID);

        List<MetaDTO> ListarMetas();

        MetaDTO ObtenerMeta(int metaID);

        Response Eliminar(int metaID, int userID);
    }
}
