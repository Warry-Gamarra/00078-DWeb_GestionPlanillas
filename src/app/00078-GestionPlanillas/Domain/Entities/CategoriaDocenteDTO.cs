using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoriaDocenteDTO
    {
        public int I_CategoriaDocenteID { get; set; }

        public string T_CategoriaDocenteDesc { get; set; }

        public string C_CategoriaDocenteCod { get; set; }

        public bool B_Habilitado { get; set; }
    }
}
