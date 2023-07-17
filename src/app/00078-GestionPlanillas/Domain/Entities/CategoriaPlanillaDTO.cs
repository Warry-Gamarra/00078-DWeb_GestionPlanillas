using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoriaPlanillaDTO
    {
        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
