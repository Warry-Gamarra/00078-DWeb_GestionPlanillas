using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IDepActividadMetaServiceFacade
    {
        Response GrabarDepActividadMeta(Operacion operacion, DepActividadMetaModel model, int userID);

        List<DepActividadMetaModel> ListarDepActividadMetas();

        DepActividadMetaModel ObtenerDepActividadMeta(int depActividadMetaID);

        Response Eliminar(int depActividadMetaID, int userID);
    }
}
