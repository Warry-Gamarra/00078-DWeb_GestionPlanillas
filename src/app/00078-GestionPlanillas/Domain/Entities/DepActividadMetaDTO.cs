using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DepActividadMetaDTO
    {
        public int depActividadMetaID { get; set; }

        public int anio { get; set; }

        public int categoriaPlanillaID { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public int dependenciaID { get; set; }

        public string dependenciaCod { get; set; }

        public string dependenciaDesc { get; set; }

        public string descripcion { get; set; }

        public int actividadID { get; set; }

        public string actividadCod { get; set; }

        public string actividadDesc { get; set; }

        public int metaID { get; set; }

        public string metaCod { get; set; }

        public string metaDesc { get; set; }

        public int categoriaPresupuestalID { get; set; }

        public string categoriaPresupCod { get; set; }

        public string categoriaPresupDesc { get; set; }
    }
}
