using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class FileContent
    {
        public byte[] fileContent { get; set; }

        public string contentType { get; set; }

        public string fileName { get; set; }
    }
}
