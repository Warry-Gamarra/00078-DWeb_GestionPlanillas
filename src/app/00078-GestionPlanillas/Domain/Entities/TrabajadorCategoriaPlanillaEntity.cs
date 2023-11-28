using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TrabajadorCategoriaPlanillaEntity
    {
        public int? trabajadorCategoriaPlanillaID { get; set; }

        public int trabajadorID { get; set; }

        public int categoriaPlanillaID { get; set; }

        public int dependenciaID { get; set; }

        public int? grupoTrabajoID { get; set; }
    }
}
