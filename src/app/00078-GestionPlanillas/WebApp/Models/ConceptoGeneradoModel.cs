using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ConceptoGeneradoModel
    {
        public int trabajadorPlanillaID { get; set; }

        public int tipoConceptoID { get; set; }

        public string tipoConceptoDesc { get; set; }

        public string conceptoCod { get; set; }

        public string conceptoDesc { get; set; }

        public string conceptoAbrv { get; set; }

        public decimal monto { get; set; }

        public string montoFormateado
        {
            get
            {
                return monto.ToString(Formats.BASIC_DECIMAL);
            }
        }
    }
}