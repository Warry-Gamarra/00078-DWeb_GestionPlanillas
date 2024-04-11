using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TrabajadorCategoriaPlanillaModel
    {
        public int trabajadorID { get; set; }

        public string trabajadorCod { get; set; }

        public string codigoPlaza { get; set; }

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

        [DisplayName("Tip.Documento")]
        public int tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc { get; set; }

        [DisplayName("Num.Documento")]
        public string numDocumento { get; set; }

        public int estadoID { get; set; }

        public Estado estado
        {
            get
            {
                return (Estado)estadoID;
            }
        }

        public string estadoDesc { get; set; }

        public int vinculoID { get; set; }

        public string vinculoDesc { get; set; }

        public int trabajadorCategoriaPlanillaID { get; set; }

        [DisplayName("Planilla")]
        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public bool esCategoriaPrincipal { get; set; }

        public bool seleccionado { get; set; }

        public bool tienePlanilla { get; set; }


        [DisplayName("Dependencia")]
        public int dependenciaID { get; set; }

        public string dependenciaCod { get; set; }

        public string dependenciaDesc { get; set; }

        [DisplayName("Descripción")]
        public int? grupoTrabajoID { get; set; }

        public string grupoTrabajoCod { get; set; }

        public string grupoTrabajoDesc { get; set; }

        public bool estaHabilitado { get; set; }

        public bool esJefe {  get; set; }
    }
}