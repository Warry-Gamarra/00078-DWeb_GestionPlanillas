using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PlantillaPlanillaEntity
    {
        public int? plantillaPlanillaID { get; set; }

        public int categoriaPlanillaID { get; set; }

        public string plantillaPlanillaDesc { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
