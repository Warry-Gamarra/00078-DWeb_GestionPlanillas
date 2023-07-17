using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TrabajadorEntity
    {
        public int? trabajadorID { get; set; }

        public int? personaID { get; set; }

        public string trabajadorCod { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public string nombre { get; set; }

        public int tipoDocumentoID { get; set; }

        public string numDocumento { get; set; }

        public DateTime? fechaIngreso { get; set; }

        public int regimenID { get; set; }

        public int estadoID { get; set; }

        public int vinculoID { get; set; }

        public int? bancoID { get; set; }

        public string nroCuentaBancaria { get; set; }

        public int? dependenciaID { get; set; }

        public int? afp { get; set; }

        public string cuspp { get; set; }

        public int? categoriaDocenteID { get; set; }

        public int? horasDocenteID { get; set; }

        public int? grupoOcupacionalID { get; set; }

        public int? nivelRemunerativoID { get; set; }
    }
}
