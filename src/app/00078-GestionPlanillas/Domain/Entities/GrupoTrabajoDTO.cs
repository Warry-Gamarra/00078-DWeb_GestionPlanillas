using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GrupoTrabajoDTO
    {
        public int grupoTrabajoID { get; set; }

        public string grupoTrabajoDesc { get; set; }

        public string grupoTrabajoCod { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
