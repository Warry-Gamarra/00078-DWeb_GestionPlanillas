using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Reports
{
    public class ReporteResumenSIAF
    {
        public string titulo { get; }

        public string tituloResumenAdministrativo
        {
            get
            {
                return "ADMINISTRATIVO";
            }
        }

        public ResumenSIAFDTO resumenAdministrativo { get; set; }

        public string tituloResumenDocente
        {
            get
            {
                return "DOCENTE";
            }
        }

        public ResumenSIAFDTO resumenDocente { get; set; }

        public ReporteResumenSIAF(int año, string mes, ResumenSIAFDTO resumenAdministrativo, ResumenSIAFDTO resumenDocente)
        { 
            titulo = String.Format("RESUMEN SIAF PLANILLA DE HABERES MES DE {0} {1}", mes.ToUpper(), año);

            this.resumenAdministrativo = resumenAdministrativo;

            this.resumenDocente = resumenDocente;
        }
    }
}
