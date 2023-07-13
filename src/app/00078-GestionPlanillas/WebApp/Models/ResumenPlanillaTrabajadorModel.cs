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

        public DateTime? fechaIngreso { get; set; }

        public string fechaIngresoFormat
        {
            get
            {
                return fechaIngreso.HasValue ? fechaIngreso.Value.ToString("dd/MM/yyyy") : "";
            }

        }

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

        public int planillaID { get; set; }

        public int periodoID { get; set; }

        public int anio { get; set; }

        public int mes { get; set; }

        public string mesDesc { get; set; }

        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }
    }
}
