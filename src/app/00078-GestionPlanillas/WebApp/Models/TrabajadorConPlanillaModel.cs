using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TrabajadorConPlanillaModel
    {
        public int trabajadorID { get; set; }

        public string trabajadorCod { get; set; }

        public string nombre { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public string apellidosNombre
        {
            get
            {
                return String.Format("{0} {1}, {2}", apellidoPaterno, apellidoMaterno, nombre);
            }
        }

        public string tipoDocumentoDesc { get; set; }

        public string numDocumento { get; set; }

        public string estadoDesc { get; set; }

        public string vinculoDesc { get; set; }

        public int año { get; set; }

        public int mes { get; set; }
    }
}