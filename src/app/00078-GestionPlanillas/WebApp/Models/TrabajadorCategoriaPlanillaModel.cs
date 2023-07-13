using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TrabajadorCategoriaPlanillaModel
    {
        public int trabajadorID { get; set; }

        public string trabajadorCod { get; set; }

        public int personaID { get; set; }

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

        public int tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc { get; set; }

        public string numDocumento { get; set; }

        public int estadoID { get; set; }

        public string estadoDesc { get; set; }

        public int vinculoID { get; set; }

        public string vinculoDesc { get; set; }

        public int trabajadorCategoriaPlanillaID { get; set; }

        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public bool seleccionado { get; set; }
    }
}