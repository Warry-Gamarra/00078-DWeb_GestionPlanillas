using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GrupoOcupacionalDTO
    {
        public int I_GrupoOcupacionalID { get; set; }

        public string T_GrupoOcupacionalCod { get; set; }

        public string T_GrupoOcupacionalDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public bool B_Eliminado { get; set; }
    }
}
