using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class CategoriaPlanillaGeneradaParaTrabajadorModel
    {
        public int planillaID { get; set; }

        public int trabajadorPlanillaID { get; set; }

        public int año { get; set; }

        public int mes { get; set; }

        public string mesDesc { get; set; }

        public string dependenciaDesc { get; set; }

        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public string clasePlanillaDesc { get; set; }

        public string tipoPlanillaDesc { get; set; }
    }
}