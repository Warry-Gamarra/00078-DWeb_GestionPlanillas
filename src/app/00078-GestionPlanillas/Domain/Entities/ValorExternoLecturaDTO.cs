using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ValorExternoLecturaDTO
    {
        public int? anio { get; set; }

        public int? mes { get; set; }

        public int? tipoDocumentoID { get; set; }

        public string numDocumento { get; set; }

        public int? categoriaPlanillaID { get; set; }

        public string conceptoCod { get; set; }

        public decimal? valorConcepto { get; set; }

        public int? proveedorID { get; set; }
    }
}
