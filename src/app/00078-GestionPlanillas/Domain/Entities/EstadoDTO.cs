using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EstadoDTO
    {
        public int I_EstadoID { get; set; }

        public string T_EstadoDesc { get; set; }

        public string C_EstadoCod { get; set; }

        public bool B_Habilitado { get; set; }

        public bool B_Eliminado { get; set; }
    }
}
