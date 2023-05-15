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
    public interface IConceptoService
    {
        List<ConceptoDTO> ListarConceptos();

        ConceptoDTO ObtenerConcepto(int I_ConceptoID);

        Response GrabarConcepto(Operacion operacion, ConceptoEntity conceptoEntity, int userID);
    }
}
