using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TrabajadorDTO
    {
        public int trabajadorID { get; set; }

        public string trabajadorCod { get; set; }

        public int personaID { get; set; }

        public string nombre { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public int tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc { get; set; }

        public string numDocumento { get; set; }

        public int sexoID { get; set; }

        public string sexoDesc { get; set; }

        public DateTime? fechaIngreso { get; set; }

        public int? regimenID { get; set; }

        public string regimenDesc { get; set; }

        public int? afpID { get; set; }

        public string afpDesc { get; set; }

        public string cuspp { get; set; }

        public int estadoID { get; set; }

        public string estadoDesc { get; set; }

        public int vinculoID { get; set; }

        public string vinculoDesc { get; set; }

        public int? trabajadorDependenciaID { get; set; }

        public int dependenciaID { get; set; }

        public string dependenciaCod { get; set; }

        public string dependenciaDesc { get; set; }

        public int? cuentaBancariaID { get; set; }

        public string nroCuentaBancaria { get; set; }

        public int? tipoCuentaBancariaID { get; set; }

        public int? bancoID { get; set; }

        public string bancoDesc { get; set; }

        public string bancoAbrv { get; set; }
    }
}
