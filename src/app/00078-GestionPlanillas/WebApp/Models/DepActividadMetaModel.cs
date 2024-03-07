using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class DepActividadMetaModel
    {
        public int? depActividadMetaID { get; set; }

        [DisplayName("Año")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public int anio { get; set; }

        [DisplayName("Categoría de Planilla")]
        [Required(ErrorMessage = "El {0} es obligatoria.")]
        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        [DisplayName("Dependencia")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public int dependenciaID { get; set; }

        public string dependenciaCod { get; set; }

        public string dependenciaDesc { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public string descripcion { get; set; }

        [DisplayName("Actividad")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public int actividadID { get; set; }

        public string actividadCod { get; set; }

        public string actividadDesc { get; set; }

        [DisplayName("Meta")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public int metaID { get; set; }

        public string metaCod { get; set; }

        public string metaDesc { get; set; }

        [DisplayName("Categoría presupuestal")]
        public int categoriaPresupuestalID { get; set; }

        public string categoriaPresupCod { get; set; }

        public string categoriaPresupDesc { get; set; }
    }
}