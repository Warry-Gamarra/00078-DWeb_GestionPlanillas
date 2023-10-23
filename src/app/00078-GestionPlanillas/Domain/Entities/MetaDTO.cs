using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MetaDTO
    {
        public int metaID { get; set; }

        public string metaDesc { get; set; }

        public string metaCod { get; set; }

        public string metaCodDesc
        {
            get
            {
                return String.Format("{0} {1}", metaCod, metaDesc);
            }
        }
    }
}
