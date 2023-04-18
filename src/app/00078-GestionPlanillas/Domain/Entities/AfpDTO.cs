using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AfpDTO
    {
        public int I_AfpID { get; set; }

        public string T_AfpDesc { get; set; }

        public string C_AfpCod { get; set; }

        public bool B_Habilitado { get; set; }

        public bool B_Eliminado { get; set; }
    }
}
