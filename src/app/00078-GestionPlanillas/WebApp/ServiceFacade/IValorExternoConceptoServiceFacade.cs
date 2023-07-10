﻿using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IValorExternoConceptoServiceFacade
    {
        Response GrabarValoresExternos(string fileName, int userID);
    }
}