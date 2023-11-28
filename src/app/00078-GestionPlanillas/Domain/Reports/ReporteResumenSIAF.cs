﻿using System;
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

        public string pieTablaAdministrativo
        {
            get
            {
                return "Total Admin";
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

        public string pieTablaDocente
        {
            get
            {
                return "Total Doc";
            }
        }

        public ResumenSIAFDTO resumenDocente { get; set; }

        public string tituloResumenGeneradoraRecursos
        {
            get
            {
                return "GENERADORA DE RECURSOS";
            }
        }

        public string pieTablaGeneradoraRecursos
        {
            get
            {
                return "Total Gene.Recursos";
            }
        }

        public ResumenSIAFDTO resumenGeneradoraRecursos { get; set; }

        public ReporteResumenSIAF(int año, string mes, ResumenSIAFDTO resumenAdministrativo, ResumenSIAFDTO resumenDocente, ResumenSIAFDTO resumenGeneradoraRecursos)
        { 
            titulo = String.Format("RESUMEN SIAF PLANILLA DE HABERES MES DE {0} {1}", mes.ToUpper(), año);

            this.resumenAdministrativo = resumenAdministrativo;

            this.resumenDocente = resumenDocente;

            this.resumenGeneradoraRecursos = resumenGeneradoraRecursos;
        }
    }
}
