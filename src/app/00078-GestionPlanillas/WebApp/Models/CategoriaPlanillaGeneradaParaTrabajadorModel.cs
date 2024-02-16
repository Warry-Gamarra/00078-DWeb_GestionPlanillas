using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class CategoriaPlanillaGeneradaParaTrabajadorModel
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

        public decimal totalRemuneracion { get; set; }

        public string totalRemuneracionFormateado
        {
            get
            {
                return totalRemuneracion.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal totalReintegro { get; set; }

        public string totalReintegroFormateado
        {
            get
            {
                return totalReintegro.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal totalDeduccion { get; set; }

        public string totalDeduccionFormateado
        {
            get
            {
                return totalDeduccion.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal totalBruto { get; set; }

        public string totalBrutoFormateado
        {
            get
            {
                return totalBruto.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal totalDescuento { get; set; }

        public string totalDescuentoFormateado
        {
            get
            {
                return totalDescuento.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal totalSueldo { get; set; }

        public string totalSueldoFormateado
        {
            get
            {
                return totalSueldo.ToString(Formats.BASIC_DECIMAL);
            }
        }

        public decimal ObtenerTotal(TipoConcepto tipoConcepto)
        {
            switch(tipoConcepto)
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