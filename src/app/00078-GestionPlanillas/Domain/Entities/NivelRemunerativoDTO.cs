using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NivelRemunerativoDTO
    {
        public int I_NivelRemunerativoID { get; set; }

        public string C_NivelRemunerativoCod { get; set; }

        public string T_NivelRemunerativoDesc { get; set; }

        public bool B_Habilitado { get; set; }
    }
}
