using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TotalPlanillaDependenciaDTO
    {
        public string actividadCod { get; set; }

        public string dependenciaCod { get; set; }

        public string dependenciaDesc { get; set; }

        public decimal totalRemuneracion { get; set; }

        public decimal totalReintegro { get; set; }

        public decimal totalDeduccion { get; set; }

        public decimal totalBruto { get; set; }

        public decimal totalDescuento { get; set; }

        public decimal totalSueldo { get; set; }
    }
}
