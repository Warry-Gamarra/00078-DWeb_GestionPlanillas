using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IMetaServiceFacade
    {
        Response GrabarMeta(Operacion operacion, MetaModel model, int userID);

        List<MetaModel> ListarMetas();

        SelectList ObtenerComboMetas(int? selectedItem = null);

        MetaModel ObtenerMeta(int metaID);

        Response Eliminar(int metaID, int userID);
    }
}