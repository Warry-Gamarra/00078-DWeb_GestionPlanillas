using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DependenciaDTO
    {
        public int I_DependenciaID { get; set; }

        public string T_DependenciaDesc { get; set; }

        public string C_DependenciaCod { get; set; }

        public string T_DependenciaCodDesc
        {
            get
            {
                return String.Format("{0} {1}", C_DependenciaCod, T_DependenciaDesc);
            }
        }

        public bool B_Habilitado { get; set; }
    }
}
