using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApp.Models
{
    public class UsuarioModel
    {
        public int? userId { get; set; }

        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string userName { get; set; }

        public DateTime? fecActualizaPassword { get; set; }

        public bool cambiaPassword { get; set; }

        public bool estaHabilitado { get; set; }

        public int? usuarioCre { get; set; }

        public int? datosUsuarioID { get; set; }

        public string numDoc { get; set; }

        [Display(Name = "Apellidos y Nombres")]
        [Required]
        [StringLength(200, ErrorMessage = "El campo {0} tiene una longitud máxima de {1} caracteres")]
        public string nomPersona { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "El campo {0} tiene una longitud máxima de {1} caracteres")]
        [Display(Name = "Correo electrónico")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "El Correo electrónico no tiene el formato correcto")]
        public string correoUsuario { get; set; }

        public DateTime? fecAlta { get; set; }

        [Required]
        [Display(Name = "Rol de usuario")]
        public int roleId { get; set; }

        public string roleName { get; set; }

        [Display(Name = "Dependencia")]
        public int? dependenciaID { get; set; }
    }
}