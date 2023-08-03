using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ValorExternoLecturaProcesadoDTO
    {
        public int? anio { get; set; }

        public int? mes { get; set; }

        public bool periodoCorrecto { get; set; }

        public int? periodoID { get; set; }

        public string mesDesc { get; set; }

        public int? tipoDocumentoID { get; set; }

        public string numDocumento { get; set; }

        public int? categoriaPlanillaID { get; set; }

        public bool trabajadorExiste { get; set; }

        public int? trabajadorID { get; set; }

        public string datosPersona { get; set; }

        public string tipoDocumentoDesc { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public bool tienePlanilla { get; set; }

        public string conceptoCod { get; set; }

        public bool conceptoExiste { get; set; }

        public int? conceptoID { get; set; }

        public string conceptoDesc { get; set; }

        public string tipoConceptoDesc { get; set; }

        public decimal? valorConcepto { get; set; }

        public bool? esValorFijo { get; set; }

        public bool valorConceptoCorrecto { get; set; }

        public int? proveedorID { get; set; }

        public bool proveedorExiste { get; set; }

        public string proveedorDesc { get; set; }
    }
}
