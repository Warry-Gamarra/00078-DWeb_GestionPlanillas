using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GrupoOcupacionalDTO
    {
        public int grupoOcupacionalID { get; set; }

        public string grupoOcupacionalCod { get; set; }

        public string grupoOcupacionalDesc { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
