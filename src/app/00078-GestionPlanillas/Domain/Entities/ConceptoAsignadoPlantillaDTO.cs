﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ConceptoAsignadoPlantillaDTO
    {
        public int plantillaPlanillaConceptoID { get; set; }

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

        public int conceptoID { get; set; }

        public string conceptoCod { get; set; }

        public string conceptoDesc { get; set; }

        public string conceptoAbrv { get; set; }

        public bool esValorFijo { get; set; }

        public bool valorEsExterno { get; set; }

        public decimal? valorConcepto { get; set; }

        public bool aplicarFiltro1 { get; set; }

        public int? filtro1 { get; set; }

        public bool aplicarFiltro2 { get; set; }

        public int? filtro2 { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
