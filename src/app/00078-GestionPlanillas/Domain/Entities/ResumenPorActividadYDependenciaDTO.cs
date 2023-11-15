using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ResumenPorActividadYDependenciaDTO
    {
        public string actividadCod { get; set; }

        public string dependenciaCod { get; set; }

        public string dependenciaDesc { get; set; }

        public decimal totalRemuneracion { get; set; }

        public string totalRemuneracionFormat
        {
            get
            {
                return totalRemuneracion.ToString(Formats.BASIC_DECIMAL);
            }

        }

        public decimal totalReintegro { get; set; }

        public string totalReintegroFormat
        {
            get
            {
                return totalReintegro.ToString(Formats.BASIC_DECIMAL);
            }

        }

        public decimal totalDeduccion { get; set; }

        public string totalDeduccionFormat
        {
            get
            {
                return totalDeduccion.ToString(Formats.BASIC_DECIMAL);
            }

        }

        public decimal totalBruto { get; set; }

        public string totalBrutoFormat
        {
            get
            {
                return totalBruto.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal totalDescuento { get; set; }

        public string totalDescuentoFormat
        {
            get
            {
                return totalDescuento.ToString(Formats.BASIC_DECIMAL);
            }

        }

        public decimal totalSueldo { get; set; }

        public string totalSueldoFormat
        {
            get
            {
                return totalSueldo.ToString(Formats.BASIC_DECIMAL);
            }

        }
    }
}
