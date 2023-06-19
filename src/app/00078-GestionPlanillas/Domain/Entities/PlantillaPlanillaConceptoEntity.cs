using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PlantillaPlanillaConceptoEntity
    {
        public int? plantillaPlanillaConceptoID { get; set; }

        public int plantillaPlanillaID { get; set; }

        public int conceptoID { get; set; }

        public bool esValorFijo { get; set; }

        public bool valorEsExterno { get; set; }

        public decimal? valorConcepto { get; set; }

        public bool aplicarFiltro1 { get; set; }

        public int? filtro1 { get; set; }

        public bool aplicarFiltro2 { get; set; }

        public int? filtro2 { get; set; }

        public DataTable conceptoIncluido { get; set; }
    }
}
