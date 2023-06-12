﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ConceptoDTO
    {
        public int conceptoID { get; set; }

        public int tipoConceptoID { get; set; }

        public string tipoConceptoDesc { get; set; }

        public string conceptoCod { get; set; }

        public string conceptoDesc { get; set; }

        public string conceptoAbrv { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
