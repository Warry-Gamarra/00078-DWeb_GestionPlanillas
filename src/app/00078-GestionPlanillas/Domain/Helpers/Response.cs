using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class Response
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public dynamic Result { get; set; }

        public string Icon
        {
            get
            {
                return Success ? "fa fa-check-circle" : "fa fa-exclamation-triangle";
            }
        }

        public string Color
        {
            get
            {
                return Success ? "success" : "danger";
            }
        }
    }
}
