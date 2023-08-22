using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PeriodoEntity
    {
        public int? periodoID { get; set; }

        public int anio { get; set; }

        public int mes { get; set; }

        public string mesDesc { get; set; }
    }
}
