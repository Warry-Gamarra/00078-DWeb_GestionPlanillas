using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApp.ServiceFacade
{
    public interface ILecturaArchivo
    {
        List<ValorExternoConceptoDTO> ObtenerListaValoresDeConceptos(HttpPostedFileBase file);
    }
}
