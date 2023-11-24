using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HorasDedicacionDocenteDTO
    {
        public int horasDocenteID { get; set; }

        public int horas { get; set; }

        public int dedicacionDocenteID { get; set; }

        public string dedicacionDocenteDesc { get; set; }

        public string dedicacionDocenteCod { get; set; }

        public bool estaHoraHabilitada { get; set; }

        public bool estaDedicacionHabilitada { get; set; }

        public string dedicacionXHorasCorto
        { 
            get
            {
                return String.Format("{0} / {1}", dedicacionDocenteCod, horas.ToString());
            }
        }

        public string dedicacionXHorasLargo
        {
            get
            {
                return String.Format("{0} / {1}", dedicacionDocenteDesc, horas.ToString());
            }
        }

        public bool esParaDocenteOrdinario { get; set; }
    }
}
