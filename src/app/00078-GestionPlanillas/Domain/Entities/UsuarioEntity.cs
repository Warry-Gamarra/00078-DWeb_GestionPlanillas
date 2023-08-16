using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UsuarioEntity
    {
        public int? userId { get; set; }

        public string numDoc { get; set; }

        public string nomPersona { get; set; }

        public string correoUsuario { get; set; }

        public int roleId { get; set; }

        public string roleName { get; set; }

        public int? dependenciaID { get; set; }
    }
}
