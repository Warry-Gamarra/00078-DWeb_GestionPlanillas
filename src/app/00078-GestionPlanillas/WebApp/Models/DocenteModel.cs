using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class DocenteModel
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

        public int docenteID { get; set; }

        public int categoriaDocenteID { get; set; }

        public string categoriaDocenteCod { get; set; }

        public string categoriaDocenteDesc { get; set; }

        public int horasDocenteID { get; set; }

        public int horas { get; set; }

        public int dedicacionDocenteID { get; set; }

        public string dedicacionDocenteCod { get; set; }

        public string dedicacionDocenteDesc { get; set; }
    }
}
