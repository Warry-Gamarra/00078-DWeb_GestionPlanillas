﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApp.ServiceFacade
{
    public interface ICategoriaPlanillaServiceFacade
    {
        SelectList ListarCategoriasPlanillas(bool incluirDeshabilitados = false);
    }
}
