using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TrabajadorLecturaProcesadoDTO
    {
        public string tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc {  get; set; }

        public bool esTipoDocumentoCorrecto { get; set; }

        public string numDocumento { get; set; }

        public bool esNumDocumentoCorrecto { get; set; }

        public string apePaterno { get; set; }

        public string apeMaterno { get; set; }

        public string nombres { get; set; }

        public int? sexo { get; set; }

        public string sexoDesc { get; set; }

        public bool esSexoCorrecto { get; set; }

        public string codigoTrabajador { get; set; }

        public string vinculoCod { get; set; }

        public string vinculoDesc { get; set; }

        public bool esVinculoCorrecto { get; set; }

        public string grupoOcupacionalCod { get; set; }

        public string nivelRemunerativoCod { get; set; }

        public string categoriaDocenteCod { get; set; }

        public string dedicacionDocenteCod { get; set; }

        public int? horasDocente { get; set; }

        public string fechaIngreso { get; set; }

        public string dependenciaCod { get; set; }

        public string dependenciaDesc { get; set; }

        public bool esDependenciaCorrecta { get; set; }

        public string bancoCod { get; set; }

        public string numeroCuentaBancaria { get; set; }

        public string tipoCuentaBancaria { get; set; }

        public string regimenPensionarioCod { get; set; }

        public int? afpCod { get; set; }

        public string cuspp { get; set; }

        public string codigoPlaza { get; set; }

        public bool esRegistroCorrecto
        {
            get
            {
                return esTipoDocumentoCorrecto && esNumDocumentoCorrecto && esSexoCorrecto && esVinculoCorrecto && esDependenciaCorrecta;
            }
        }

        public List<string> observaciones { get; set; }

        public TrabajadorLecturaProcesadoDTO()
        {
            observaciones = new List<string>();
        }
    }
}
