using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
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

        public string codigoPlaza { get; set; }

        public int personaID { get; set; }

        [DisplayName("Nombres")]
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [CustomValidation(typeof(TrabajadorModel), "ValidarNombresYApellidos")]
        public string nombre { get; set; }

        [DisplayName("Ape.Paterno")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [CustomValidation(typeof(TrabajadorModel), "ValidarNombresYApellidos")]
        public string apellidoPaterno { get; set; }

        [DisplayName("Ape.Materno")]
        [CustomValidation(typeof(TrabajadorModel), "ValidarNombresYApellidos")]
        public string apellidoMaterno { get; set; }

        public string apellidosNombre
        { 
            get { return String.Format("{0} {1}, {2}", apellidoPaterno, apellidoMaterno, nombre) ; }
        }

        [DisplayName("Doc.Identidad")]
        public int tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc { get; set; }

        [Required(ErrorMessage = "El Num.Documento es obligatorio.")]
        [CustomValidation(typeof(TrabajadorModel), "ValidarLongitudNumeroDocumento")]
        [CustomValidation(typeof(TrabajadorModel), "ValidarCaracteresNumeroDocumento")]
        public string numDocumento { get; set; }

        [Required(ErrorMessage = "El campo Sexo es obligatorio.")]
        [DisplayName("Sexo")]
        public int sexoID { get; set; }

        public string sexoDesc { get; set; }

        [DisplayName("Fec.Ingreso")]
        [CustomValidation(typeof(TrabajadorModel), "ValidarFechaIngreso")]
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
        [CustomValidation(typeof(TrabajadorModel), "ValidarAfpObligatorio")]
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

        [CustomValidation(typeof(TrabajadorModel), "ValidarNumeroCuentaBancaria")]
        public string nroCuentaBancaria { get; set; }

        [DisplayName("Tipo Cuenta Bancaria")]
        [CustomValidation(typeof(TrabajadorModel), "ValidarTipoCuentaBancaria")]
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

        public static ValidationResult ValidarLongitudNumeroDocumento(string numDocumento, ValidationContext context)
        {
            var trabajador = (TrabajadorModel)context.ObjectInstance;

            int longitudMinima, longitudMaxima;

            if (trabajador.tipoDocumentoID == (int)TipoDocumentoIdentidad.DNI)
            {
                longitudMinima = 8;
                longitudMaxima = 8;
            }
            else
            {
                longitudMinima = 1;
                longitudMaxima = 20;
            }

            if (numDocumento.Length < longitudMinima || numDocumento.Length > longitudMaxima)
            {
                return new ValidationResult("La cantidad de caracteres del número de documento es incorrecta.");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidarCaracteresNumeroDocumento(string numDocumento, ValidationContext context)
        {
            var trabajador = (TrabajadorModel)context.ObjectInstance;

            if (trabajador.tipoDocumentoID == (int)TipoDocumentoIdentidad.DNI)
            {
                if (!EsNumero(numDocumento))
                {
                    return new ValidationResult("El DNI sólo debe contener números.");
                }
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidarFechaIngreso(DateTime? fechaIngreso)
        {
            if (fechaIngreso != null)
            {
                if (fechaIngreso.Value > DateTime.Today)
                {
                    return new ValidationResult("La fecha de ingreso no puede ser mayor a la fecha de hoy.");
                }
            }
            
            return ValidationResult.Success;
        }

        public static ValidationResult ValidarNombresYApellidos(string datopersonal, ValidationContext context)
        {
            if (!String.IsNullOrEmpty(datopersonal))
            {
                Regex regex = new Regex(@"^[^\d]+$");

                if (!regex.IsMatch(datopersonal))
                {
                    string nombreCampo = context.DisplayName;

                    return new ValidationResult($"El campo {nombreCampo} no puede contener números.");
                }
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidarAfpObligatorio(int? afpID, ValidationContext context)
        {
            var trabajador = (TrabajadorModel)context.ObjectInstance;

            if (trabajador.regimenID.HasValue && trabajador.regimenID.Value == (int)RegimenPensionario.SPP && !afpID.HasValue)
            {
                return new ValidationResult("El campo AFP es obligatorio.");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidarNumeroCuentaBancaria(string nroCuentaBancaria, ValidationContext context)
        {
            var trabajador = (TrabajadorModel)context.ObjectInstance;

            if (trabajador.bancoID.HasValue)
            {
                if (String.IsNullOrEmpty(nroCuentaBancaria))
                {
                    return new ValidationResult("El nro. de cuenta bancaria es obligatorio.");
                }
                else if (!EsNumero(nroCuentaBancaria))
                {
                    return new ValidationResult("El nro. de cuenta bancaria sólo debe contener números.");
                }
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidarTipoCuentaBancaria(int? tipoCuentaBancariaID, ValidationContext context)
        {
            var trabajador = (TrabajadorModel)context.ObjectInstance;

            if (trabajador.bancoID.HasValue && !tipoCuentaBancariaID.HasValue)
            {
                return new ValidationResult("El tipo de cuenta bancaria es obligatorio.");
            }

            return ValidationResult.Success;
        }

        private static bool EsNumero(string cadena)
        {
            foreach (char c in cadena)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
