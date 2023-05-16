﻿using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IConceptoService
    {
        Response GrabarConcepto(Operacion operacion, ConceptoEntity conceptoEntity, int userID);

        List<ConceptoDTO> ListarConceptos();

        ConceptoDTO ObtenerConcepto(int conceptoID);

        Response CambiarEstado(int conceptoID, bool estadHabilitado, int userID);
    }
}
