using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DepActividadMetaEntity
    {
        public int? depActividadMetaID { get; set; }

        public int anio { get; set; }

        public int categoriaPlanillaID { get; set; }

        public int dependenciaID { get; set; }

        public string descripcion { get; set; }

        public int actividadID { get; set; }

        public int metaID { get; set; }

        public int categoriaPresupuestalID { get; set; }
    }
}
