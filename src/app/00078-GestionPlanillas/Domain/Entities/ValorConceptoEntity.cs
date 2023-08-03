﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ValorConceptoEntity
    {
        public int periodoID { get; set; }

        public int trabajadorID { get; set; }

        public int categoriaPlanillaID { get; set; }

        public int conceptoID { get; set; }

        public decimal valorConcepto { get; set; }

        public int proveedorID { get; set; }
    }
}
