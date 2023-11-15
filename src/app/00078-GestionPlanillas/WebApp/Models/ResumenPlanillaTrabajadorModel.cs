﻿using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ResumenPlanillaTrabajadorModel
    {
        public int trabajadorID { get; set; }

        public string trabajadorCod { get; set; }

        public string nombre { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public string apellidosNombre
        {
            get { return String.Format("{0} {1}, {2}", apellidoPaterno, apellidoMaterno, nombre); }
        }

        public int tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc { get; set; }

        public string numDocumento { get; set; }

        public int? regimenID { get; set; }

        public string regimenDesc { get; set; }

        public int? estadoID { get; set; }

        public string estadoDesc { get; set; }

        public int? vinculoID { get; set; }

        public string vinculoDesc { get; set; }

        public int trabajadorPlanillaID { get; set; }

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

        public int planillaID { get; set; }

        public int periodoID { get; set; }

        public int anio { get; set; }

        public int mes { get; set; }

        public string mesDesc { get; set; }

        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }
    }
}
