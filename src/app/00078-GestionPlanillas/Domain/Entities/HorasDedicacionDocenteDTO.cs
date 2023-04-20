using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HorasDedicacionDocenteDTO
    {
        public int I_HorasDocenteID { get; set; }

        public int I_Horas { get; set; }

        public int I_DedicacionDocenteID { get; set; }

        public string T_DedicacionDocenteDesc { get; set; }

        public string C_DedicacionDocenteCod { get; set; }

        public bool B_HoraHabilitado { get; set; }

        public bool B_DedicacionHabilitado { get; set; }
    }
}
