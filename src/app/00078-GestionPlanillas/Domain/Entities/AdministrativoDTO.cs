using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AdministrativoDTO
    {
        public int trabajadorID { get; set; }

        public string trabajadorCod { get; set; }

        public string nombre { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public int tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc { get; set; }

        public string numDocumento { get; set; }

        public DateTime? fechaIngreso { get; set; }

        public int? regimenID { get; set; }

        public string regimenDesc { get; set; }

        public int? estadoID { get; set; }

        public string estadoDesc { get; set; }

        public int? vinculoID { get; set; }

        public string vinculoDesc { get; set; }

        public int administrativoID { get; set; }

        public int grupoOcupacionalID { get; set; }

        public string grupoOcupacionalCod { get; set; }

        public string grupoOcupacionalDesc { get; set; }

        public int nivelRemunerativoID { get; set; }

        public string nivelRemunerativoCod { get; set; }

        public string nivelRemunerativoDesc { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
