using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class TrabajadorServiceFacade : ITrabajadorServiceFacade
    {
        private ITrabajadorService _trabajadorService;
        private IAdministrativoService _administrativoService;
        private IDocenteService _docenteService;
        private ITipoDocumentoService _tipoDocumentoService;
        private IPersonaService _personaService;
        private IVinculoService _vinculoService;
        private IRegimenService _regimenService;
        private IBancoService _bancoService;
        private IDependenciaService _dependenciaService;

        private readonly string serverPath;

        public TrabajadorServiceFacade()
        {
            _trabajadorService = new TrabajadorService();
            _administrativoService = new AdministrativoService();
            _docenteService = new DocenteService();
            _tipoDocumentoService = new TipoDocumentoService();
            _personaService = new PersonaService();
            _vinculoService = new VinculoService();
            _regimenService = new RegimenService();
            _bancoService = new BancoService();
            _dependenciaService = new DependenciaService();

            serverPath = WebConfigParams.DirectorioCargaTrabajadores;
        }

        public IEnumerable<TrabajadorModel> ListarTrabajadores()
        {
            var lista = _trabajadorService.ListarTrabajadores()
                .Select(x => Mapper.TrabajadorDTO_To_TrabajadorModel(x));

            return lista;
        }

        public TrabajadorModel ObtenerTrabajador(int trabajadorID)
        {
            TrabajadorModel trabajadorModel;

            var trabajadorDTO = _trabajadorService.ObtenerTrabajador(trabajadorID);

            if (trabajadorDTO == null)
            {
                trabajadorModel = null;
            }
            else
            {
                trabajadorModel = Mapper.TrabajadorDTO_To_TrabajadorModel(trabajadorDTO);

                if (trabajadorModel.Vinculo.Equals(Vinculo.AdministrativoPermanente) || trabajadorModel.Vinculo.Equals(Vinculo.AdministrativoContratado))
                {
                    var administrativo = _administrativoService.ListarAdministrativoPorTrabajadorID(trabajadorID).First();

                    trabajadorModel.nivelRemunerativoID = administrativo.nivelRemunerativoID;

                    trabajadorModel.nivelRemunerativoDesc = administrativo.nivelRemunerativoDesc;

                    trabajadorModel.grupoOcupacionalID = administrativo.grupoOcupacionalID;

                    trabajadorModel.grupoOcupacionalDesc = administrativo.grupoOcupacionalDesc;
                } else if (trabajadorModel.Vinculo.Equals(Vinculo.DocentePermanente) || trabajadorModel.Vinculo.Equals(Vinculo.DocenteContratado))
                {
                    var docente = _docenteService.ListarDocentePorTrabajadorID(trabajadorID).First();

                    trabajadorModel.categoriaDocenteID = docente.categoriaDocenteID;

                    trabajadorModel.categoriaDocenteDesc = docente.categoriaDocenteDesc;

                    trabajadorModel.horasDocenteID = docente.horasDocenteID;

                    trabajadorModel.horasDocente = String.Format("{0} / {1}", docente.dedicacionDocenteCod, docente.horas);
                }
            }

            return trabajadorModel;
        }

        public Response GrabarTrabajador(Operacion operacion, TrabajadorModel model, int userID)
        {
            Response response;

            try
            {
                var trabajadorEntity = new TrabajadorEntity()
                {
                    trabajadorID = model.trabajadorID,
                    personaID = model.personaID,
                    trabajadorCod = model.trabajadorCod,
                    codigoPlaza = model.codigoPlaza == null ? null : model.codigoPlaza.Trim(),
                    apellidoPaterno = model.apellidoPaterno.ToUpperInvariant(),
                    apellidoMaterno = model.apellidoMaterno == null ? null : model.apellidoMaterno.ToUpperInvariant(),
                    nombre = model.nombre.ToUpperInvariant(),
                    tipoDocumentoID = model.tipoDocumentoID,
                    numDocumento = model.numDocumento,
                    sexoID = model.sexoID,
                    fechaIngreso = model.fechaIngreso,
                    regimenID = model.regimenID.Value,
                    estadoID = model.estadoID,
                    vinculoID = model.vinculoID,
                    bancoID = model.bancoID,
                    nroCuentaBancaria = model.nroCuentaBancaria,
                    tipoCuentaBancariaID = model.tipoCuentaBancariaID,
                    dependenciaID = model.dependenciaID,
                    afp = model.afpID,
                    cuspp = model.cuspp,
                    categoriaDocenteID = model.categoriaDocenteID,
                    horasDocenteID = model.horasDocenteID,
                    grupoOcupacionalID = model.grupoOcupacionalID,
                    nivelRemunerativoID = model.nivelRemunerativoID
                };

                response = _trabajadorService.GrabarTrabajador(operacion, trabajadorEntity, userID);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message
                };
            }

            return response;
        }

        public IEnumerable<TrabajadorConPlanillaModel> ListarTrabajadoresConPlanilla(int año, int mes, int categoriaPlanillaID)
        {
            var lista = _trabajadorService.ListarTrabajadoresConPlanilla(año, mes, categoriaPlanillaID)
                .Select(x => Mapper.TrabajadorConPlanillaDTO_To_TrabajadorConPlanillaModel(x));

            return lista;
        }

        public Tuple<string, List<TrabajadorLecturaProcesadoDTO>> ObtenerListaTrabajadores(HttpPostedFileBase file)
        {
            string newFileName;
            ILecturaArchivoService lecturaArchivoService;
            List<TrabajadorLecturaDTO> lectura;
            List<TrabajadorLecturaProcesadoDTO> lecturaProcesada;

            try
            {
                newFileName = FileManagement.GuardarArchivo(serverPath, file);

                lecturaArchivoService = FileManagement.GetLecturaService(Path.GetExtension(newFileName));

                lectura = lecturaArchivoService.ObtenerListaTrabajadores(Path.Combine(serverPath, newFileName));

                lecturaProcesada = ObtenerLecturaProcesada(lectura);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new IndexOutOfRangeException("El archivo no tiene la estructura correcta.", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new Tuple<string, List<TrabajadorLecturaProcesadoDTO>>(newFileName, lecturaProcesada);
        }

        private List<TrabajadorLecturaProcesadoDTO> ObtenerLecturaProcesada(List<TrabajadorLecturaDTO> lectura)
        {
            var listaTipoDocumento = _tipoDocumentoService.ListaTipoDocumentos();

            var listaSexos = _personaService.ListarSexos();

            var listaVinculos = _vinculoService.ListarVinculos();

            var listaDependencias = _dependenciaService.ListarDependencias();

            var listaRegimen = _regimenService.ListarRegimenes();

            var listaBancos = _bancoService.ListarBancos();

            var lecturaProcesada = new List<TrabajadorLecturaProcesadoDTO>();

            foreach (var item in lectura)
            {
                var dto = new TrabajadorLecturaProcesadoDTO()
                {
                    tipoDocumentoID = item.tipoDocumentoID,
                    numDocumento = item.numDocumento,
                    apePaterno = item.apePaterno,
                    apeMaterno = item.apeMaterno,
                    nombres = item.nombres,
                    sexo = item.sexo,
                    codigoTrabajador = item.codigoTrabajador,
                    vinculoCod = item.vinculoCod,
                    grupoOcupacionalCod = item.grupoOcupacionalCod,
                    nivelRemunerativoCod = item.nivelRemunerativoCod,
                    categoriaDocenteCod = item.categoriaDocenteCod,
                    dedicacionDocenteCod = item.dedicacionDocenteCod,
                    horasDocente = item.horasDocente,
                    fechaIngreso = item.fechaIngreso,
                    dependenciaCod = item.dependenciaCod,
                    bancoCod = item.bancoCod,
                    numeroCuentaBancaria = item.numeroCuentaBancaria,
                    tipoCuentaBancaria = item.tipoCuentaBancaria,
                    regimenPensionarioCod = item.regimenPensionarioCod,
                    afpCod = item.afpCod,
                    cuspp = item.cuspp,
                    codigoPlaza = item.codigoPlaza
                };

                if (dto.tipoDocumentoID != null && listaTipoDocumento.Exists(x => x.tipoDocumentoID.ToString() == dto.tipoDocumentoID))
                {
                    dto.tipoDocumentoDesc = listaTipoDocumento.First(x => x.tipoDocumentoID.ToString() == dto.tipoDocumentoID).tipoDocumentoDesc;

                    dto.esTipoDocumentoCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("Tipo de documento de identidad incorrecto.");

                    dto.tipoDocumentoDesc = "";
                }

                if (dto.numDocumento != null && dto.numDocumento.Length > 0)
                {
                    dto.esNumDocumentoCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("Número de documento de identidad incorrecto.");
                }

                if (dto.sexo.HasValue && listaSexos.Exists(x => x.sexoID == dto.sexo.Value))
                {
                    dto.sexoDesc = listaSexos.First(x => x.sexoID == dto.sexo.Value).sexoDesc;

                    dto.esSexoCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("Código de Sexo incorrecto.");
                }

                if (dto.vinculoCod != null && listaVinculos.Exists(x => x.vinculoCod == dto.vinculoCod))
                {
                    dto.vinculoDesc = listaVinculos.First(x => x.vinculoCod == dto.vinculoCod).vinculoDesc;

                    dto.esVinculoCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("El Vínculo es incorrecto.");
                }

                if (dto.dependenciaCod != null && listaDependencias.Exists(x => x.dependenciaCod == dto.dependenciaCod))
                {
                    dto.dependenciaDesc = listaDependencias.First(x => x.dependenciaCod == dto.dependenciaCod).dependenciaDesc;

                    dto.esDependenciaCorrecta = true;
                }
                else
                {
                    dto.observaciones.Add("La Dependencia es incorrecta.");
                }

                lecturaProcesada.Add(dto);
            }

            return lecturaProcesada;
        }
    }
}
