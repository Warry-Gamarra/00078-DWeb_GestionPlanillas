using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ResumenPorActividadDTO
    {
        public string actividadCod { get; }

        public decimal totalRemuneracion
        {
            get
            {
                return listaDependencias.Sum(x => x.totalRemuneracion);
            }
        }

        public string totalRemuneracionFormat
        {
            get
            {
                return totalRemuneracion.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal totalReintegro
        {
            get
            {
                return listaDependencias.Sum(x => x.totalReintegro);
            }
        }

        public string totalReintegroFormat
        {
            get
            {
                return totalReintegro.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal totalDeduccion
        {
            get
            {
                return listaDependencias.Sum(x => x.totalDeduccion);
            }
        }

        public string totalDeduccionFormat
        {
            get
            {
                return totalDeduccion.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal totalBruto
        {
            get
            {
                return listaDependencias.Sum(x => x.totalBruto);
            }
        }

        public string totalBrutoFormat
        {
            get
            {
                return totalBruto.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal totalDescuento
        {
            get
            {
                return listaDependencias.Sum(x => x.totalDescuento);
            }
        }

        public string totalDescuentoFormat
        {
            get
            {
                return totalDescuento.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal totalSueldo
        {
            get
            {
                return listaDependencias.Sum(x => x.totalSueldo);
            }
        }

        public string totalSueldoFormat
        {
            get
            {
                return totalSueldo.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public IEnumerable<ResumenPorActividadYDependenciaDTO> listaDependencias { get; }

        public ResumenPorActividadDTO(string actividadCod, IEnumerable<ResumenPorActividadYDependenciaDTO> listaDependencias)
        {
            this.actividadCod = actividadCod;

            this.listaDependencias = listaDependencias;
        }
    }
}
