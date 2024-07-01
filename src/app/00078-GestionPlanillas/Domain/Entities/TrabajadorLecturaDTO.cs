using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TrabajadorLecturaDTO
    {
        public string operacion {  get; set; }

        public string tipoDocumentoCod { get; set; }

        public string tipoDocumentoDesc { get; set; }

        public string numDocumento { get; set; }

        public string apePaterno { get; set; }

        public string apeMaterno { get; set; }

        public string nombres { get; set; }

        public string sexoCod { get; set; }

        public string codigoTrabajador { get; set; }

        public string vinculoCod { get; set; }

        public string grupoOcupacionalCod { get; set; }

        public string nivelRemunerativoCod { get; set; }

        public string categoriaDocenteCod { get; set; }

        public string dedicacionDocenteCod { get; set; }

        public int? horasDocente { get; set; }

        public string fechaIngreso { get; set; }

        public string dependenciaCod { get; set; }

        public string bancoCod { get; set; }

        public string numeroCuentaBancaria { get; set; }

        public string tipoCuentaBancariaCod { get; set; }

        public string regimenPensionarioCod { get; set; }

        public string afpCod { get; set; }

        public string cuspp {  get; set; }

        public string codigoPlaza { get; set; }

        public string estadoTrabajadorCod { get; set; }
    }
}
