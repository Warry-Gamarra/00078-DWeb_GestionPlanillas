using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Reports
{
    public class ReporteResumenPorActividadYDependencia
    {
        public string titulo { get; }

        public string oficina { get { return "Oficina de Recursos Humanos"; } }

        public string subOficina { get { return "Oficina de Remuneraciones y Pensiones"; } }

        public string fechaConsulta { get; }

        public string horaConsulta { get; }

        public decimal totalRemuneracion
        {
            get
            {
                return listaResumenPorActividad.Sum(x => x.totalRemuneracion);
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
                return listaResumenPorActividad.Sum(x => x.totalReintegro);
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
                return listaResumenPorActividad.Sum(x => x.totalDeduccion);
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
                return listaResumenPorActividad.Sum(x => x.totalBruto);
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
                return listaResumenPorActividad.Sum(x => x.totalDescuento);
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
                return listaResumenPorActividad.Sum(x => x.totalSueldo);
            }
        }

        public string totalSueldoFormat
        {
            get
            {
                return totalSueldo.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public List<ResumenPorActividadDTO> listaResumenPorActividad { get; }

        public ReporteResumenPorActividadYDependencia(int año, string mes, string clasePlanilla,
            IEnumerable<ResumenPorActividadYDependenciaDTO> listaResumenPorActividad)
        {
            titulo = String.Format("RESUMEN POR DEPENDENCIA - PLANILLA {0} {1} {2}", clasePlanilla.ToUpper(), mes.ToUpper(), año);

            fechaConsulta = DateTime.Now.ToString(Formats.BASIC_DATE);

            horaConsulta = DateTime.Now.ToString(Formats.BASIC_TIME);

            this.listaResumenPorActividad = new List<ResumenPorActividadDTO>();

            foreach (var item in listaResumenPorActividad.GroupBy(x => x.actividadCod))
            {
                var dependencias = new ResumenPorActividadDTO(item.Key, item);

                this.listaResumenPorActividad.Add(dependencias);
            }
        }
    }
}
