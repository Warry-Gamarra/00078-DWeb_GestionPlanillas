﻿using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IGeneracionArchivoService
    {
        FileContent GenerarExcelDeLecturaValoresDeConceptos(List<ValorExternoLecturaProcesadoDTO> lista);
    }
}
