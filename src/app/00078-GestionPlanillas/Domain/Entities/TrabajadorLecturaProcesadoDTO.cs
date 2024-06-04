using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TrabajadorLecturaProcesadoDTO
    {
        public string tipoDocumentoCod { get; set; }

        public int? tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc {  get; set; }

        public bool esTipoDocumentoCorrecto { get; set; }

        public string numDocumento { get; set; }

        public bool esNumDocumentoCorrecto { get; set; }

        public string apePaterno { get; set; }

        public bool esApePaternoCorrecto { get; set; }

        public string apeMaterno { get; set; }

        public string nombres { get; set; }

        public bool esNombreCorrecto { get; set; }

        public string sexoCod { get; set; }

        public int? sexoID { get; set; }

        public string sexoDesc { get; set; }

        public bool esSexoCorrecto { get; set; }

        public string codigoTrabajador { get; set; }

        public bool esCodigoTrabajadorCorrecto { get; set; }

        public string vinculoCod { get; set; }

        public int? vinculoID { get; set; }

        public string vinculoDesc { get; set; }

        public Vinculo vinculo {  get; set; }

        public bool esVinculoCorrecto { get; set; }

        public string grupoOcupacionalCod { get; set; }

        public int? grupoOcupacionalID { get; set; }

        public string grupoOcupacionalDesc { get; set; }

        public bool esGrupoOcupacionalCorrecto { get; set; }

        public string nivelRemunerativoCod { get; set; }

        public int? nivelRemunerativoID { get; set; }

        public string nivelRemunerativoDesc { get; set; }

        public bool esNivelRemunerativoCorrecto {  get; set; }

        public string categoriaDocenteCod { get; set; }

        public int? categoriaDocenteID { get; set; }

        public string categoriaDocenteDesc { get; set; }

        public bool esCategoriaDocenteCorrecta {  get; set; }

        public string dedicacionDocenteCod { get; set; }

        public int? dedicacionDocenteID { get; set; }

        public string dedicacionDocenteDesc { get; set; }

        public bool esDedicacionDocenteCorrecta { get; set; }

        public int? horasDocente { get; set; }

        public int? horasDocenteID { get; set; }

        public bool esHorasDocentesCorrecta { get; set; }

        public string fechaIngreso { get; set; }

        public DateTime? fechaIngresoDT { get; set; }

        public bool esFechaIngresoCorrecto { get; set; }

        public string dependenciaCod { get; set; }

        public int? dependenciaID { get; set; }

        public string dependenciaDesc { get; set; }

        public bool esDependenciaCorrecta { get; set; }

        public string bancoCod { get; set; }

        public int? bancoID { get; set; }

        public string bancoDesc { get; set; }

        public bool esBancoCorrecto { get; set; }

        public string numeroCuentaBancaria { get; set; }

        public bool esNumeroCuentaBancariaCorrecto {  get; set; }

        public string tipoCuentaBancariaCod { get; set; }

        public int? tipoCuentaBancariaID { get; set; }

        public string tipoCuentaBancariaDesc { get; set; }

        public bool esTipoCtaBancariaCorrecta { get; set; }

        public string regimenPensionarioCod { get; set; }

        public int? regimenID { get; set; }

        public string regimenDesc { get; set; }

        public bool esRegimenCorrecto { get; set; }

        public string afpCod { get; set; }

        public int? afpID { get; set; }

        public string afpDesc { get; set; }

        public bool esAFPCorrecto { get; set; }

        public string cuspp { get; set; }

        public bool esCusppCorrecto { get; set; }

        public string codigoPlaza { get; set; }

        public bool esCodigoPlazaCorrecto {  get; set; }

        public string estadoTrabajadorCod { get; set; }

        public int? estadoTrabajadorID { get; set; }

        public string estadoTrabajadorDesc { get; set; }

        public bool esEstadoTrabajadorCorrecto { get; set; }

        public bool esNumDocIdentDuplicadoEnArchivo { get; set; }

        public bool esCodTrabajadorDuplicadoEnArchivo { get; set; }

        public bool esCodPlazaDuplicadoEnArchivo { get; set; }

        public bool esRegistroCasiCorrecto
        {
            get
            {
                return !esNumDocIdentDuplicadoEnArchivo && !esCodTrabajadorDuplicadoEnArchivo && !esCodPlazaDuplicadoEnArchivo &&
                    esTipoDocumentoCorrecto && esNumDocumentoCorrecto &&
                    esApePaternoCorrecto && esNombreCorrecto && esSexoCorrecto &&
                    esCodigoTrabajadorCorrecto && esVinculoCorrecto &&
                    esGrupoOcupacionalCorrecto && esNivelRemunerativoCorrecto &&
                    esCategoriaDocenteCorrecta && esDedicacionDocenteCorrecta && esHorasDocentesCorrecta &&
                    esFechaIngresoCorrecto && esDependenciaCorrecta && 
                    esBancoCorrecto && esNumeroCuentaBancariaCorrecto && esTipoCtaBancariaCorrecta &&
                    esRegimenCorrecto && esAFPCorrecto && esCusppCorrecto &&
                    esCodigoPlazaCorrecto && esEstadoTrabajadorCorrecto;
            }
        }

        public bool esModelStateValid { get; set; }

        public bool esNumDocIdentDuplicadoEnBD { get; set; }

        public bool esCodPlazaDuplicadoEnBD { get; set; }

        public bool esRegistroCorrecto
        {
            get
            {
                return esRegistroCasiCorrecto && esModelStateValid && !esNumDocIdentDuplicadoEnBD && !esCodPlazaDuplicadoEnBD;
            }
        }

        public List<string> observaciones { get; set; }

        public TrabajadorLecturaProcesadoDTO()
        {
            observaciones = new List<string>();
        }
    }
}
