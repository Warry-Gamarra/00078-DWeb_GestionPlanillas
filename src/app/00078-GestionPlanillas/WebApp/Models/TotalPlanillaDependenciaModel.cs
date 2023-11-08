using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TotalPlanillaDependenciaModel
    {
        public string actividadCod { get; set; }

        public string dependenciaCod { get; set; }

        public string dependenciaDesc { get; set; }

        public decimal totalRemuneracion { get; set; }

        public string totalRemuneracionFormat
        {
            get
            {
                return totalRemuneracion.ToString("#,0.00");
            }

        }

        public decimal totalReintegro { get; set; }

        public string totalReintegroFormat
        {
            get
            {
                return totalReintegro.ToString("#,0.00");
            }

        }

        public decimal totalDeduccion { get; set; }

        public string totalDeduccionFormat
        {
            get
            {
                return totalDeduccion.ToString("#,0.00");
            }

        }

        public decimal totalBruto { get; set; }

        public string totalBrutoFormat
        {
            get
            {
                return totalBruto.ToString("#,0.00");
            }
        }

        public decimal totalDescuento { get; set; }

        public string totalDescuentoFormat
        {
            get
            {
                return totalDescuento.ToString("#,0.00");
            }

        }

        public decimal totalSueldo { get; set; }

        public string totalSueldoFormat
        {
            get
            {
                return totalSueldo.ToString("#,0.00");
            }

        }
    }
}