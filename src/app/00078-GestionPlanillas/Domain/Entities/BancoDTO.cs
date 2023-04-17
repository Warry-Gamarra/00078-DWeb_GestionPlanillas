using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BancoDTO
    {
        public int I_BancoID { get; set; }

        public string T_BancoDesc { get; set; }

        public string T_BancoAbrv { get; set; }

        public bool B_Habilitado { get; set; }

        public bool B_Eliminado { get; set; }
    }
}
