using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DependenciaDTO
    {
        public int dependenciaID { get; set; }

        public string dependenciaDesc { get; set; }

        public string dependenciaCod { get; set; }

        public string dependenciaCodDesc
        {
            get
            {
                return String.Format("{0} {1}", dependenciaCod, dependenciaDesc);
            }
        }

        public bool estaHabilitado { get; set; }
    }
}
