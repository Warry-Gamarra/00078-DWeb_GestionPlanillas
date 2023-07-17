﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ValorExternoConceptoModel
    {
        public int? anio { get; set; }

        public int? mes { get; set; }

        public string mesDesc { get; set; }

        public int? tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc { get; set; }

        public string numDocumento { get; set; }

        public string datosPersona { get; set; }

        public string conceptoCod { get; set; }

        public string conceptoDesc { get; set; }

        public decimal? valorConcepto { get; set; }

        public int? proveedorID { get; set; }

        public string proveedorDesc { get; set; }
    }
}