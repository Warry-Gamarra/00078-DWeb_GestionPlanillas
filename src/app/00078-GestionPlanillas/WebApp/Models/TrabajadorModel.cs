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
        public int? I_TrabajadorID { get; set; }

        [DisplayName("Código")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string C_TrabajadorCod { get; set; }

        [DisplayName("Nombres")]
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string T_Nombre { get; set; }

        [DisplayName("Ape.Paterno")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string T_ApellidoPaterno { get; set; }

        [DisplayName("Ape.Materno")]
        public string T_ApellidoMaterno { get; set; }

        public string T_ApellidosNombre
        { 
            get { return String.Format("{0} {1}, {2}", T_ApellidoPaterno, T_ApellidoMaterno, T_Nombre) ; }
        }

        [DisplayName("Doc.Identidad")]
        public int I_TipoDocumentoID { get; set; }

        public string T_TipoDocumentoDesc { get; set; }

        [Required(ErrorMessage = "El Num.Documento es obligatorio.")]
        public string C_NumDocumento { get; set; }

        [DisplayName("Fec.Ingreso")]
        public DateTime? D_FechaIngreso { get; set; }

        public string T_FechaIngreso
        {
            get
            {
                return D_FechaIngreso.HasValue ? D_FechaIngreso.Value.ToString("dd/MM/yyyy") : "";
            }

        }

        [DisplayName("Régimen")]
        public int? I_RegimenID { get; set; }

        public string T_RegimenDesc { get; set; }

        [DisplayName("AFP")]
        public int? I_AfpID { get; set; }

        public string T_AfpDesc { get; set; }

        [DisplayName("CUSPP")]
        public string T_Cuspp { get; set; }

        [DisplayName("Estado")]
        public int I_EstadoID { get; set; }

        public string T_EstadoDesc { get; set; }

        [DisplayName("Vínculo")]
        public int I_VinculoID { get; set; }

        public Vinculo Vinculo
        {
            get 
            {
                return (Vinculo)I_VinculoID;
            }
        }

        public string T_VinculoDesc { get; set; }

        public int? I_TrabajadorDependenciaID { get; set; }

        [DisplayName("Dependencia")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public int? I_DependenciaID { get; set; }

        public string C_DependenciaCod { get; set; }

        public string T_DependenciaDesc { get; set; }

        public int? I_CuentaBancariaID { get; set; }

        [Required(ErrorMessage = "El Num.Cuenta es obligatorio.")]
        public string T_NroCuentaBancaria { get; set; }

        [DisplayName("Cta.Banco")]
        [Required(ErrorMessage = "Seleccionar la {0}.")]
        public int? I_BancoID { get; set; }

        public string T_BancoDesc { get; set; }

        public string T_BancoAbrv { get; set; }

        [DisplayName("Cat.Docente")]
        public int? I_CategoriaDocenteID { get; set; }

        public string T_CategoriaDocenteDesc { get; set; }

        [DisplayName("Ded.Docente/Horas")]
        public int? I_HorasDocenteID { get; set; }

        public string I_Horas { get; set; }

        [DisplayName("Grup.Ocupacional")]
        public int? I_GrupoOcupacionalID { get; set; }

        public string T_GrupoOcupacionalDesc { get; set; }

        [DisplayName("Niv.Remunerativo")]
        public int? I_NivelRemunerativoID { get; set; }

        public string T_NivelRemunerativoDesc { get; set; }
    }
}
