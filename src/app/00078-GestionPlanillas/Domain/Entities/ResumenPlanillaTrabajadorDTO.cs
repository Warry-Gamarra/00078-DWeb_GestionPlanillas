using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ResumenPlanillaTrabajadorDTO
    {
        public int trabajadorID { get; set; }

        public string trabajadorCod { get; set; }

        public string nombre { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public int tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc { get; set; }

        public string numDocumento { get; set; }

        public int? regimenID { get; set; }

        public string regimenDesc { get; set; }

        public int? estadoID { get; set; }

        public string estadoDesc { get; set; }

        public int? vinculoID { get; set; }

        public string vinculoDesc { get; set; }

        public int trabajadorPlanillaID { get; set; }

        public decimal totalRemuneracion { get; set; }

        public decimal totalReintegro { get; set; }

        public decimal totalDeduccion { get; set; }

        public decimal totalBruto { get;set; }

        public decimal totalDescuento { get; set; }

        public decimal totalSueldo { get; set; }

        public int planillaID { get; set; }

        public int periodoID { get; set; }

        public int anio { get; set; }

        public int mes { get; set; }

        public string mesDesc { get; set; }

        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public int trabajadorCategoriaPlanillaID { get; set; }
    }
}
