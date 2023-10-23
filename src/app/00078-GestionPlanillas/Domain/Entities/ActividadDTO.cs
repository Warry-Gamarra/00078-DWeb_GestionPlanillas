using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ActividadDTO
    {
        public int actividadID { get; set; }

        public string actividadDesc { get; set; }

        public string actividadCod { get; set; }

        public string actividadCodDesc
        {
            get
            {
                return String.Format("{0} {1}", actividadCod, actividadDesc);
            }
        }
    }
}
