using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoriaPlanillaGeneradaParaTrabajadorDTO
    {
        public int planillaID { get; set; }

        public int trabajadorPlanillaID { get; set; }

        public int año { get; set; }

        public int mes { get; set; }

        public string mesDesc { get; set; }

        public string dependenciaDesc { get; set; }

        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public string clasePlanillaDesc { get; set; }

        public string tipoPlanillaDesc { get; set; }
    }
}
