using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoriaPresupuestalDTO
    {
        public int categoriaPresupuestalID { get; set; }

        public string categoriaPresupDesc { get; set; }

        public string categoriaPresupCod { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
