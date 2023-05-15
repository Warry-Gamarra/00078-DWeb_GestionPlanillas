﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ConceptoDTO
    {
        public int I_ConceptoID { get; set; }

        public int I_TipoConceptoID { get; set; }

        public string T_TipoConceptoDesc { get; set; }

        public string C_ConceptoCod { get; set; }

        public string T_ConceptoDesc { get; set; }

        public bool B_Habilitado { get; set; }
    }
}
