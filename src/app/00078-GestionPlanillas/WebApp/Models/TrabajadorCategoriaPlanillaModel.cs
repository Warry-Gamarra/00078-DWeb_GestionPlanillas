using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TrabajadorCategoriaPlanillaModel
    {
        public int I_TrabajadorID { get; set; }

        public string C_TrabajadorCod { get; set; }

        public int I_PersonaID { get; set; }

        public string T_Nombre { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public string T_ApellidosNombre
        {
            get
            {
                return String.Format("{0} {1}, {2}", T_ApellidoPaterno, T_ApellidoMaterno, T_Nombre);
            }
        }

        public int I_TipoDocumentoID { get; set; }

        public string T_TipoDocumentoDesc { get; set; }

        public string C_NumDocumento { get; set; }

        public int I_EstadoID { get; set; }

        public string T_EstadoDesc { get; set; }

        public int I_VinculoID { get; set; }

        public string T_VinculoDesc { get; set; }

        public int I_TrabajadorCategoriaPlanillaID { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }
    }
}