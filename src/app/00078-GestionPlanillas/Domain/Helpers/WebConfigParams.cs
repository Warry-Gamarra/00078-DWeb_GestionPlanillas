using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Domain.Helpers
{
    public static class WebConfigParams
    {
        public static string DirectorioArchivosExternos
        {
            get
            {
                return ConfigurationManager.AppSettings["DirectorioArchivosExternos"];
            }
        }

        public static string DirectorioCargaTrabajadores
        {
            get
            {
                return ConfigurationManager.AppSettings["DirectorioCargaTrabajadores"];
            }
        }
    }
}
