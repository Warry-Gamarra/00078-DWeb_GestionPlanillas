using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VinculoDTO
    {
        public int I_VinculoID { get; set; }

        public string T_VinculoDesc { get; set; }

        public string C_VinculoCod { get; set; }

        public bool B_Habilitado { get; set; }

        public bool B_Eliminado { get; set; }
    }
}
