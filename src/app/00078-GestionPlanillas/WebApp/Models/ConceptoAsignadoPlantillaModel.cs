using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ConceptoAsignadoPlantillaModel
    {
        public int? plantillaPlanillaConceptoID { get; set; }

        public int plantillaPlanillaID { get; set; }

        public string plantillaPlanillaDesc { get; set; }

        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public int clasePlanillaID { get; set; }

        public string clasePlanillaDesc { get; set; }

        public int tipoConceptoID { get; set; }

        public string tipoConceptoDesc { get; set; }

        public bool esAdicion { get; set; }

        public bool incluirEnTotalBruto { get; set; }

        [DisplayName("Concepto")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public int conceptoID { get; set; }

        public string conceptoCod { get; set; }

        public string conceptoDesc { get; set; }

        public bool esValorFijo { get; set; }

        [DisplayName("¿El Monto se obtendrá de un archivo externo?")]
        public bool valorEsExterno { get; set; }

        [DisplayName("Monto/Porcentaje")]
        public decimal? valorConcepto { get; set; }

        public string valorConceptoFormateado
        {
            get
            {
                if (esValorFijo)
                {
                    return valorEsExterno ? "" : valorConcepto.HasValue ? ("S/. " + valorConcepto.Value.ToString()) : "";
                }
                else
                {
                    return valorEsExterno ? "" : valorConcepto.HasValue ? (valorConcepto.Value.ToString() + " %") : "";
                }
                
            }
        }

        public bool aplicarFiltro1 { get; set; }

        public int? filtro1 { get; set; }

        public bool aplicarFiltro2 { get; set; }

        public int? filtro2 { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
