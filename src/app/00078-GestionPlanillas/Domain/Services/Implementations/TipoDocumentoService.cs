﻿using Data.Tables;
using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        public List<TipoDocumentoDTO> ListaTipoDocumentos(bool incluirDeshabilitados = false)
        {
            var lista = TC_TipoDocumento.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_TipoDocumento_To_TipoDocumentoDTO(x))
                .ToList();

            return result;
        }
    }
}
