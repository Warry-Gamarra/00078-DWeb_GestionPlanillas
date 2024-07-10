using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ValorExternoConceptoDTO
    {
        public int conceptoExternoValorID { get; set; }

        public int periodoID { get; set; }

        public int anio { get; set; }

        public int mes { get; set; }

        public string mesDesc { get; set; }

        public int trabajadorID { get; set; }

        public string tipoDocumentoDesc { get; set; }

        public string numDocumento { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public string nombre { get; set; }

        public int trabajadorCategoriaPlanillaID { get; set; }

        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public int conceptoID { get; set; }

        public string conceptoCod { get; set; }

        public string conceptoDesc { get; set; }

        public string tipoConceptoDesc { get; set; }

        public decimal valorConcepto { get; set; }

        public int proveedorID { get; set; }

        public string proveedorDesc { get; set; }

        public bool tienePlanilla {  get; set; }

        public string trabajadorCod { get; set; }

        public string vinculoCod { get; set; }

        public string vinculoDesc { get; set; }

        public string estadoDesc { get; set; }

        public string tipoDocumentoCod { get; set; }
    }
}
