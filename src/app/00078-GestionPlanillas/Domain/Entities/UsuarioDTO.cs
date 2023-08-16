using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UsuarioDTO
    {
        public int userId { get; set; }

        public string userName { get; set; }

        public DateTime? fecActualizaPassword { get; set; }

        public bool cambiaPassword { get; set; }

        public bool estaHabilitado { get; set; }

        public int? usuarioCre { get; set; }

        public int? datosUsuarioID { get; set; }

        public string numDoc { get; set; }

        public string nomPersona { get; set; }

        public string correoUsuario { get; set; }

        public DateTime? fecAlta { get; set; }

        public int roleId { get; set; }

        public string roleName { get; set; }

        public int? dependenciaID { get; set; }
    }
}
