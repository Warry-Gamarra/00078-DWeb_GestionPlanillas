using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoriaDocenteDTO
    {
        public int categoriaDocenteID { get; set; }

        public string categoriaDocenteDesc { get; set; }

        public string categoriaDocenteCod { get; set; }

        public bool esParaDocenteOrdinario { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
