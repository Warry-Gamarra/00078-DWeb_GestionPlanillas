using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ResumenSIAFModel
    {
        public ResumenSIAFModel(IEnumerable<string> cabecera, IEnumerable<IDictionary<string, object>> detalle)
        {
            this.cabecera = cabecera;

            this.detalle = detalle;
        }

        public IEnumerable<string> cabecera { get; }

        public IEnumerable<IDictionary<string, object>> detalle { get; }
    }
}