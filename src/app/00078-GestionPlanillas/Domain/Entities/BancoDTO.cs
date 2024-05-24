using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BancoDTO
    {
        public int bancoID { get; set; }

        public string bancoDesc { get; set; }

        public string bancoAbrv { get; set; }

        public string bancoCod { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
