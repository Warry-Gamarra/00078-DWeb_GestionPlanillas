using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RegimenDTO
    {
        public int I_RegimenID { get; set; }

        public string T_RegimenDesc { get; set; }

        public string C_RegimenCod { get; set; }

        public bool B_Habilitado { get; set; }
    }
}
