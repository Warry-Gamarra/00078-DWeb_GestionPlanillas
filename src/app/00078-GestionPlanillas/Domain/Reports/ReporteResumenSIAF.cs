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
      
        public IEnumerable<ResumenSIAFDTO> listaResumenes { get; set; }

        public ReporteResumenSIAF(int año, string mes, IEnumerable<ResumenSIAFDTO> listaResumenes)
        { 
            titulo = String.Format("RESUMEN SIAF PLANILLA DE HABERES MES DE {0} {1}", mes.ToUpper(), año);

            this.listaResumenes = listaResumenes;
        }
    }
}
