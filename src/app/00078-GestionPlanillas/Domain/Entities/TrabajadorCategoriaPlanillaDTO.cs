using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TrabajadorCategoriaPlanillaDTO
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

        public int estadoID { get; set; }

        public string estadoDesc { get; set; }

        public int vinculoID { get; set; }

        public string vinculoDesc { get; set; }

        public int? trabajadorCategoriaPlanillaID { get; set; }

        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public bool esCategoriaPrincipal { get;set; }

        public int dependenciaID { get; set; }

        public string dependenciaCod { get; set; }

        public string dependenciaDesc { get; set; }

        public int? grupoTrabajoID { get; set; }

        public string grupoTrabajoCod { get; set; }

        public string grupoTrabajoDesc { get; set; }
    }
}
