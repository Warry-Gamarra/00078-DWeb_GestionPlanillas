using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WebApp.Models
{
    public class TrabajadorModel
    {
        public int? trabajadorID { get; set; }

        [DisplayName("Código")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string trabajadorCod { get; set; }

        public int personaID { get; set; }

        [DisplayName("Nombres")]
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string nombre { get; set; }

        [DisplayName("Ape.Paterno")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string apellidoPaterno { get; set; }

        [DisplayName("Ape.Materno")]
        public string apellidoMaterno { get; set; }

        public string apellidosNombre
        { 
            get { return String.Format("{0} {1}, {2}", apellidoPaterno, apellidoMaterno, nombre) ; }
        }

        [DisplayName("Doc.Identidad")]
        public int tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc { get; set; }

        [Required(ErrorMessage = "El Num.Documento es obligatorio.")]
        public string numDocumento { get; set; }

        [Required(ErrorMessage = "El campo Sexo es obligatorio.")]
        [DisplayName("Sexo")]
        public int sexoID { get; set; }

        public string sexoDesc { get; set; }

        [DisplayName("Fec.Ingreso")]
        public DateTime? fechaIngreso { get; set; }

        public string fechaIngresoFormat
        {
            get
            {
                return fechaIngreso.HasValue ? fechaIngreso.Value.ToString("dd/MM/yyyy") : "";
            }

        }

        [DisplayName("Régimen")]
        public int? regimenID { get; set; }

        public string regimenDesc { get; set; }

        [DisplayName("AFP")]
        public int? afpID { get; set; }

        public string afpDesc { get; set; }

        public string cuspp { get; set; }

        [DisplayName("Estado")]
        public int estadoID { get; set; }

        public string estadoDesc { get; set; }

        [DisplayName("Vínculo")]
        public int vinculoID { get; set; }

        public Vinculo Vinculo
        {
            get 
            {
                return (Vinculo)vinculoID;
            }
        }

        public string vinculoDesc { get; set; }

        [DisplayName("Dependencia")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public int dependenciaID { get; set; }

        public string dependenciaCod { get; set; }

        public string dependenciaDesc { get; set; }

        public int? cuentaBancariaID { get; set; }

        public string nroCuentaBancaria { get; set; }

        [DisplayName("Tipo Cuenta Bancaria")]
        public int? tipoCuentaBancariaID { get; set; }

        [DisplayName("Cta.Banco")]
        public int? bancoID { get; set; }

        public string bancoDesc { get; set; }

        public string bancoAbrv { get; set; }

        public int? categoriaDocenteID { get; set; }

        public string categoriaDocenteDesc { get; set; }

        public int? horasDocenteID { get; set; }

        public string horasDocente { get; set; }

        [DisplayName("Grup.Ocupacional")]
        public int? grupoOcupacionalID { get; set; }

        public string grupoOcupacionalDesc { get; set; }

        [DisplayName("Niv.Remunerativo")]
        public int? nivelRemunerativoID { get; set; }

        public string nivelRemunerativoDesc { get; set; }
    }
}
