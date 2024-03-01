using Domain.Enums;
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

        public string grupoTrabajoDesc { get; set; }

        public decimal totalRemuneracion { get; set; }

        public decimal totalReintegro { get; set; }

        public decimal totalDeduccion { get; set; }

        public decimal totalBruto { get; set; }

        public decimal totalDescuento { get; set; }

        public decimal totalSueldo { get; set; }

        public decimal ObtenerTotal(TipoConcepto tipoConcepto)
        {
            switch (tipoConcepto)
            {
                case TipoConcepto.INGRESOS:
                    return totalRemuneracion;

                case TipoConcepto.REINTEGROS:
                    return totalReintegro;

                case TipoConcepto.DEDUCCION:
                    return totalDeduccion;

                case TipoConcepto.DESCUENTOS:
                    return totalDescuento;

                default:
                    throw new Exception();
            }
        }
    }
}
