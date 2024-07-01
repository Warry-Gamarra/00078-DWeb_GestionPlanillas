using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private IGrupoOcupacionalService _grupoOcupacionalService;
        private INivelRemunerativoService _nivoRemunerativoService;
        private ICategoriaDocenteService _categoriaDocenteService;
        private IAfpService _afpService;
        private IEstadoService _estadoService;
        private IDedicacionDocenteService _dedicacionDocenteService;
        private IHorasDocenteService _horasDocenteService;
        private ITrabajadorCategoriaPlanillaService _trabajadorCategoriaPlanillaService;

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
            _grupoOcupacionalService = new GrupoOcupacionalService();
            _nivoRemunerativoService = new NivelRemunerativoService();
            _categoriaDocenteService = new CategoriaDocenteService();
            _afpService = new AfpService();
            _estadoService = new EstadoService();
            _dedicacionDocenteService = new DedicacionDocenteService();
            _horasDocenteService = new HorasDocenteService();
            _trabajadorCategoriaPlanillaService = new TrabajadorCategoriaPlanillaService();

            serverPath = WebConfigParams.DirectorioCargaTrabajadores;
        }

        public IEnumerable<TrabajadorModel> ListarTrabajadores()
        {
            var lista = _trabajadorService.ListarTrabajadores()
                .Select(x => Mapper.TrabajadorDTO_To_TrabajadorModel(x));

            return lista;
        }

        public FileContent ListarTrabajadores(FormatoArchivo formatoArchivo)
        {
            IGeneracionArchivoService generacionArchivoService;
            FileContent fileContent;

            try
            {
                var data = _trabajadorService.ListarTrabajadores();

                generacionArchivoService = FileManagement.GetGeneracionArchivoService(formatoArchivo);

                fileContent = generacionArchivoService.GenerarDescargableListaTrabajadores(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return fileContent;
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

        public FileContent ObtenerResultadoLectura(FormatoArchivo formatoArchivo, string fileName)
        {
            ILecturaArchivoService lecturaArchivoService;
            IGeneracionArchivoService generacionArchivoService;
            List<TrabajadorLecturaDTO> lectura;
            List<TrabajadorLecturaProcesadoDTO> lecturaProcesada;
            FileContent fileContent;

            try
            {
                lecturaArchivoService = FileManagement.GetLecturaService(Path.GetExtension(fileName));

                lectura = lecturaArchivoService.ObtenerListaTrabajadores(Path.Combine(serverPath, fileName));

                lecturaProcesada = ObtenerLecturaProcesada(lectura);

                generacionArchivoService = FileManagement.GetGeneracionArchivoService(formatoArchivo);

                fileContent = generacionArchivoService.GenerarDescargableDeLecturaCargaDeTrabajadores(lecturaProcesada);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fileContent;
        }

        public Response GrabarValoresExternos(string fileName, int userID)
        {
            Response response;
            ILecturaArchivoService lecturaArchivoService;
            List<TrabajadorLecturaDTO> lectura;
            List<TrabajadorLecturaProcesadoDTO> lecturaProcesada;
            TrabajadorEntity trabajadorEntity;
            TrabajadorModel trabajadorOld = null;

            try
            {
                lecturaArchivoService = FileManagement.GetLecturaService(Path.GetExtension(fileName));

                lectura = lecturaArchivoService.ObtenerListaTrabajadores(Path.Combine(serverPath, fileName));

                lecturaProcesada = ObtenerLecturaProcesada(lectura);

                if (lecturaProcesada != null && lecturaProcesada.Count() > 0)
                {
                    if (lecturaProcesada.Where(x => x.esRegistroCorrecto).Count() > 0)
                    {
                        foreach (var item in lecturaProcesada.Where(x => x.esRegistroCorrecto))
                        {
                            if (item.operacion == Operacion.Actualizar)
                            {
                                trabajadorOld = this.ObtenerTrabajador(item.trabajadorID.Value);

                                trabajadorEntity = new TrabajadorEntity()
                                {
                                    trabajadorID = item.trabajadorID,
                                    trabajadorCod = String.IsNullOrWhiteSpace(item.codigoTrabajador) || item.codigoTrabajador == "*" ? trabajadorOld.trabajadorCod : item.codigoTrabajador,
                                    codigoPlaza = String.IsNullOrWhiteSpace(item.codigoPlaza) ? trabajadorOld.codigoPlaza : item.codigoPlaza,
                                    nombre = String.IsNullOrWhiteSpace(item.nombres) || item.nombres == "*" ? trabajadorOld.nombre : item.nombres,
                                    apellidoPaterno = String.IsNullOrWhiteSpace(item.apePaterno) || item.apePaterno == "*" ? trabajadorOld.apellidoPaterno : item.apePaterno,
                                    apellidoMaterno = String.IsNullOrWhiteSpace(item.apeMaterno) ? trabajadorOld.apellidoMaterno : item.apeMaterno,
                                    tipoDocumentoID = item.tipoDocumentoID.HasValue ? item.tipoDocumentoID.Value : trabajadorOld.tipoDocumentoID,
                                    numDocumento = String.IsNullOrWhiteSpace(item.numDocumento) ? trabajadorOld.numDocumento : item.numDocumento,
                                    sexoID = (item.sexoID.HasValue && item.sexoID.Value > 0) ? item.sexoID.Value : trabajadorOld.sexoID,
                                    fechaIngreso = item.fechaIngresoDT.HasValue ? item.fechaIngresoDT.Value : trabajadorOld.fechaIngreso,
                                    regimenID = item.regimenID.HasValue ? item.regimenID.Value : trabajadorOld.regimenID.Value,
                                    estadoID = (item.estadoTrabajadorID.HasValue && item.estadoTrabajadorID.Value > 0) ? item.estadoTrabajadorID.Value : trabajadorOld.estadoID,
                                    vinculoID = item.vinculoID.HasValue ? item.vinculoID.Value : trabajadorOld.vinculoID,
                                    bancoID = item.bancoID.HasValue ? item.bancoID.Value : trabajadorOld.bancoID,
                                    nroCuentaBancaria = String.IsNullOrWhiteSpace(item.numeroCuentaBancaria) ? trabajadorOld.nroCuentaBancaria : item.numeroCuentaBancaria,
                                    tipoCuentaBancariaID = item.tipoCuentaBancariaID.HasValue ? item.tipoCuentaBancariaID.Value : trabajadorOld.tipoCuentaBancariaID,
                                    dependenciaID = (item.dependenciaID.HasValue && item.dependenciaID.Value > 0) ? item.dependenciaID.Value : trabajadorOld.dependenciaID,
                                    afp = item.afpID.HasValue ? item.afpID.Value : trabajadorOld.afpID,
                                    cuspp = String.IsNullOrWhiteSpace(item.cuspp) ? trabajadorOld.cuspp : item.cuspp,
                                    categoriaDocenteID = item.categoriaDocenteID.HasValue ? item.categoriaDocenteID.Value : trabajadorOld.categoriaDocenteID,
                                    horasDocenteID = item.horasDocenteID.HasValue ? item.horasDocenteID.Value : trabajadorOld.horasDocenteID,
                                    grupoOcupacionalID = item.grupoOcupacionalID.HasValue ? item.grupoOcupacionalID.Value : trabajadorOld.grupoOcupacionalID,
                                    nivelRemunerativoID = item.nivelRemunerativoID.HasValue ? item.nivelRemunerativoID.Value : trabajadorOld.nivelRemunerativoID
                                };
                            }
                            else
                            {
                                trabajadorEntity = new TrabajadorEntity()
                                {
                                    trabajadorCod = item.codigoTrabajador,
                                    codigoPlaza = item.codigoPlaza,
                                    nombre = item.nombres,
                                    apellidoPaterno = item.apePaterno,
                                    apellidoMaterno = item.apeMaterno,
                                    tipoDocumentoID = item.tipoDocumentoID.Value,
                                    numDocumento = item.numDocumento,
                                    sexoID = item.sexoID.Value,
                                    fechaIngreso = item.fechaIngresoDT,
                                    regimenID = item.regimenID.Value,
                                    estadoID = item.estadoTrabajadorID.Value,
                                    vinculoID = item.vinculoID.Value,
                                    bancoID = item.bancoID,
                                    nroCuentaBancaria = item.numeroCuentaBancaria,
                                    tipoCuentaBancariaID = item.tipoCuentaBancariaID,
                                    dependenciaID = item.dependenciaID.Value,
                                    afp = item.afpID,
                                    cuspp = item.cuspp,
                                    categoriaDocenteID = item.categoriaDocenteID,
                                    horasDocenteID = item.horasDocenteID,
                                    grupoOcupacionalID = item.grupoOcupacionalID,
                                    nivelRemunerativoID = item.nivelRemunerativoID
                                };
                            }
                            
                            _trabajadorService.GrabarTrabajador(item.operacion, trabajadorEntity, userID);
                        }

                        response = new Response()
                        {
                            Success = true,
                            Message = "La información del archivo se registró correctamente."
                        };

                    }
                    else
                    {
                        response = new Response()
                        {
                            Message = "No se puede registrar la información por que todos los registros están observados."
                        };
                    }
                }
                else
                {
                    response = new Response()
                    {
                        Message = "No hay registros en el archivo."
                    };
                }
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

        private List<TrabajadorLecturaProcesadoDTO> ObtenerLecturaProcesada(List<TrabajadorLecturaDTO> lectura)
        {
            var listaTipoDocumento = _tipoDocumentoService.ListaTipoDocumentos();
            var listaSexos = _personaService.ListarSexos();
            var listaVinculos = _vinculoService.ListarVinculos();
            var listaGruposOcupacionales = _grupoOcupacionalService.ListarGruposOcupacionales();
            var listaNivelesRemunerativos = _nivoRemunerativoService.ListarNivelesRemunerativos();
            var listaCategoriasDocente = _categoriaDocenteService.ListarCategoriasDocente(null);
            var listaDedicacionDocenteOrd = _dedicacionDocenteService.ListarDedicacionDocente(esParaDocenteOrdinario: true);
            var listaDedicacionDocenteContr = _dedicacionDocenteService.ListarDedicacionDocente(esParaDocenteOrdinario: false);
            var listaHorasDedicacionDocente = _horasDocenteService.ListarHorasDedicacionDocente(null);
            var listaDependencias = _dependenciaService.ListarDependencias();
            var listaRegimen = _regimenService.ListarRegimenes();
            var listaAfps = _afpService.ListarAfps();
            var listaBancos = _bancoService.ListarBancos();
            var listaTipCuentasBancarias = _bancoService.ListarTipoCuentasBancarias();
            var listaEstados = _estadoService.ListarEstados();

            Operacion operacion;
            VinculoDTO vinculoDTO;
            RegimenDTO regimenDTO;
            DateTime dateTimeOut;
            bool existeDedicacion;
            string dedicacionDocenteDesc;
            int? dedicacionDocenteID;
            int categoriaPlanillaID;
            TrabajadorCategoriaPlanillaDTO trabajadorCategoria;

            var lecturaProcesada = new List<TrabajadorLecturaProcesadoDTO>();

            foreach (var item in lectura)
            {
                var dto = Mapper.TrabajadorLecturaProcesado(item);

                categoriaPlanillaID = 0;

                trabajadorCategoria = null;

                if (Enum.TryParse(dto.operacionDesc, out operacion))
                {
                    dto.operacion = operacion;

                    dto.esOperacionCorrecta = true;
                }
                else
                {
                    dto.observaciones.Add("No se reconoce la acción a realizar.");
                }

                if (dto.esOperacionCorrecta)
                {
                    if (String.IsNullOrWhiteSpace(dto.tipoDocumentoCod))
                    {
                        dto.observaciones.Add("El Tipo de documento es obligatorio.");
                    }
                    else if (listaTipoDocumento.Exists(x => x.tipoDocumentoCod == dto.tipoDocumentoCod.Trim()))
                    {
                        dto.tipoDocumentoDesc = listaTipoDocumento.First(x => x.tipoDocumentoCod == dto.tipoDocumentoCod)
                                                .tipoDocumentoDesc;

                        dto.tipoDocumentoID = listaTipoDocumento.First(x => x.tipoDocumentoCod == dto.tipoDocumentoCod)
                                                .tipoDocumentoID;

                        dto.esTipoDocumentoCorrecto = true;
                    }
                    else
                    {
                        dto.observaciones.Add("El código del Tipo de documento no existe en el sistema.");
                    }

                    if (String.IsNullOrWhiteSpace(dto.numDocumento))
                    {
                        dto.observaciones.Add("El Número de documento es obligatorio.");
                    }
                    else if (dto.numDocumento.Trim().Length > 0 && dto.numDocumento.Trim().Length <= 20)
                    {
                        dto.esNumDocumentoCorrecto = true;
                    }
                    else
                    {
                        dto.observaciones.Add("La cantidad de caracteres del Número de documento es incorrecto.");
                    }

                    if (dto.esTipoDocumentoCorrecto && dto.esNumDocumentoCorrecto)
                    {
                        dto.esNumDocIdentDuplicadoEnArchivo = lectura.Count(x => !String.IsNullOrWhiteSpace(x.numDocumento) && x.numDocumento == dto.numDocumento && 
                            x.tipoDocumentoCod == dto.tipoDocumentoCod) > 1;

                        if (dto.esNumDocIdentDuplicadoEnArchivo)
                        {
                            dto.observaciones.Add("El Documento de Identidad se repite en el archivo.");
                        }
                    }

                    if (dto.operacion == Operacion.Registrar && String.IsNullOrWhiteSpace(dto.apePaterno))
                    {
                        dto.observaciones.Add("El Apellido paterno es obligatorio.");
                    }
                    else if (
                        (dto.operacion == Operacion.Registrar && dto.apePaterno.Trim().Length > 0 && dto.apePaterno.Trim().Length <= 50) || 
                        (dto.operacion == Operacion.Actualizar && (dto.apePaterno == null || (dto.apePaterno.Trim().Length > 0 && dto.apePaterno.Trim().Length <= 50)))
                        )
                    {
                        dto.esApePaternoCorrecto = true;

                        dto.apePaterno = dto.apePaterno ?? "*";
                    }
                    else
                    {
                        dto.observaciones.Add("La cantidad de caracteres del Apellido paterno es incorrecto.");
                    }

                    if (dto.operacion == Operacion.Registrar && String.IsNullOrWhiteSpace(dto.nombres))
                    {
                        dto.observaciones.Add("El Nombre es obligatorio.");
                    }
                    else if (
                        (dto.operacion == Operacion.Registrar && dto.nombres.Trim().Length > 0 && dto.nombres.Trim().Length <= 50) ||
                        (dto.operacion == Operacion.Actualizar && (dto.nombres == null || (dto.nombres.Trim().Length > 0 && dto.nombres.Trim().Length <= 50)))
                        )
                    {
                        dto.esNombreCorrecto = true;

                        dto.nombres = dto.nombres ?? "*";
                    }
                    else
                    {
                        dto.observaciones.Add("La cantidad de caracteres del Nombre es incorrecto.");
                    }

                    if (dto.operacion == Operacion.Registrar && String.IsNullOrWhiteSpace(dto.sexoCod))
                    {
                        dto.observaciones.Add("El campo Sexo es obligatorio.");
                    }
                    else if (
                        (dto.operacion == Operacion.Registrar && listaSexos.Exists(x => x.sexoCod == dto.sexoCod.Trim())) ||
                        (dto.operacion == Operacion.Actualizar && (dto.sexoCod == null || listaSexos.Exists(x => x.sexoCod == dto.sexoCod.Trim())))
                        )
                    {
                        dto.esSexoCorrecto = true;

                        if (dto.sexoCod != null)
                        {
                            dto.sexoDesc = listaSexos.First(x => x.sexoCod == dto.sexoCod.Trim())
                                        .sexoDesc;

                            dto.sexoID = listaSexos.First(x => x.sexoCod == dto.sexoCod.Trim())
                                            .sexoID;
                        }
                        else
                        {
                            dto.sexoID = 0;
                        }
                    }
                    else
                    {
                        dto.observaciones.Add("El código del campo Sexo no existe en el sistema.");
                    }

                    if (dto.operacion == Operacion.Registrar && String.IsNullOrWhiteSpace(dto.codigoTrabajador))
                    {
                        dto.observaciones.Add("El Código de Trabajador es obligatorio.");
                    }
                    else if (
                        (dto.operacion == Operacion.Registrar && dto.codigoTrabajador.Trim().Length > 0 && dto.codigoTrabajador.Trim().Length <= 20) ||
                        (dto.operacion == Operacion.Actualizar && (dto.codigoTrabajador == null || (dto.codigoTrabajador.Trim().Length > 0 && dto.codigoTrabajador.Trim().Length <= 20)))
                        )
                    {
                        dto.esCodigoTrabajadorCorrecto = true;

                        if (dto.codigoTrabajador != null)
                        {
                            dto.esCodTrabajadorDuplicadoEnArchivo = lectura.Count(x => !String.IsNullOrWhiteSpace(x.codigoTrabajador) &&
                            x.codigoTrabajador.Trim() == dto.codigoTrabajador.Trim()) > 1;

                            if (dto.esCodTrabajadorDuplicadoEnArchivo)
                            {
                                dto.observaciones.Add("El Código de Trabajador se repite en el archivo.");
                            }
                        }
                        else
                        {
                            dto.codigoTrabajador = "*";
                        }
                    }
                    else
                    {
                        dto.observaciones.Add("La cantidad de caracteres del Código de trabajador es incorrecto.");
                    }

                    if (String.IsNullOrWhiteSpace(dto.vinculoCod))
                    {
                        dto.observaciones.Add("El Vínculo de Trabajador es obligatorio.");
                    }
                    else
                    {
                        if (listaVinculos.Exists(x => x.vinculoCod == dto.vinculoCod.Trim()))
                        {
                            vinculoDTO = listaVinculos.First(x => x.vinculoCod == dto.vinculoCod.Trim());

                            dto.vinculoDesc = vinculoDTO.vinculoDesc;

                            dto.vinculoID = vinculoDTO.vinculoID;

                            dto.vinculo = (Vinculo)vinculoDTO.vinculoID;

                            categoriaPlanillaID = (int)_trabajadorCategoriaPlanillaService.ObtenerCategoriaPlanillaSegunVinculo(dto.vinculoID.Value);

                            dto.esVinculoCorrecto = true;

                            if (dto.operacion == Operacion.Actualizar && dto.esTipoDocumentoCorrecto && dto.esNumDocumentoCorrecto)
                            {
                                trabajadorCategoria = _trabajadorCategoriaPlanillaService.ObtenerTrabajadorPorDocumentoYCategoria(dto.tipoDocumentoID.Value, dto.numDocumento, categoriaPlanillaID);

                                if (trabajadorCategoria != null)
                                {
                                    dto.trabajadorID = trabajadorCategoria.trabajadorID;
                                }
                                else
                                {
                                    dto.esTrabajadorIDCorrecto = false;

                                    dto.observaciones.Add("El trabajador no existe en el sistema.");
                                }
                            }

                            if (dto.vinculo == Vinculo.AdministrativoPermanente || dto.vinculo == Vinculo.AdministrativoContratado)
                            {
                                dto.categoriaDocenteCod = null;
                                dto.esCategoriaDocenteCorrecta = true;

                                dto.dedicacionDocenteCod = null;
                                dto.esDedicacionDocenteCorrecta = true;

                                dto.horasDocente = null;
                                dto.esHorasDocentesCorrecta = true;

                                if (dto.operacion == Operacion.Registrar && String.IsNullOrWhiteSpace(dto.grupoOcupacionalCod))
                                {
                                    dto.observaciones.Add("El Grupo ocupacional es obligatorio.");
                                }
                                else if (
                                    (dto.operacion == Operacion.Registrar && listaGruposOcupacionales.Exists(x => x.grupoOcupacionalCod == dto.grupoOcupacionalCod.Trim())) ||
                                    (dto.operacion == Operacion.Actualizar && (dto.grupoOcupacionalCod == null || listaGruposOcupacionales.Exists(x => x.grupoOcupacionalCod == dto.grupoOcupacionalCod.Trim())))
                                    )
                                {
                                    dto.esGrupoOcupacionalCorrecto = true;

                                    if (dto.grupoOcupacionalCod != null)
                                    {
                                        dto.grupoOcupacionalDesc = listaGruposOcupacionales.First(x => x.grupoOcupacionalCod == dto.grupoOcupacionalCod.Trim())
                                                            .grupoOcupacionalDesc;

                                        dto.grupoOcupacionalID = listaGruposOcupacionales.First(x => x.grupoOcupacionalCod == dto.grupoOcupacionalCod.Trim())
                                                                .grupoOcupacionalID;
                                    }
                                }
                                else
                                {
                                    dto.observaciones.Add("El código del Grupo Ocupacional no existe en el sistema.");
                                }

                                if (dto.operacion == Operacion.Registrar && String.IsNullOrWhiteSpace(dto.nivelRemunerativoCod))
                                {
                                    dto.observaciones.Add("El Nivel Remunerativo es obligatorio.");
                                }
                                else if (
                                    (dto.operacion == Operacion.Registrar && listaNivelesRemunerativos.Exists(x => x.nivelRemunerativoCod == dto.nivelRemunerativoCod.Trim())) ||
                                    (dto.operacion == Operacion.Actualizar && (dto.nivelRemunerativoCod == null || listaNivelesRemunerativos.Exists(x => x.nivelRemunerativoCod == dto.nivelRemunerativoCod.Trim())))
                                    )
                                {
                                    dto.esNivelRemunerativoCorrecto = true;

                                    if (dto.nivelRemunerativoCod != null)
                                    {
                                        dto.nivelRemunerativoDesc = listaNivelesRemunerativos.First(x => x.nivelRemunerativoCod == dto.nivelRemunerativoCod.Trim())
                                                                .nivelRemunerativoDesc;

                                        dto.nivelRemunerativoID = listaNivelesRemunerativos.First(x => x.nivelRemunerativoCod == dto.nivelRemunerativoCod.Trim())
                                                                    .nivelRemunerativoID;
                                    }
                                }
                                else
                                {
                                    dto.observaciones.Add("El código del Nivel Remunerativo no existe en el sistema.");
                                }
                            }
                            else if (dto.vinculo == Vinculo.DocentePermanente || dto.vinculo == Vinculo.DocenteContratado)
                            {
                                dto.grupoOcupacionalCod = null;
                                dto.esGrupoOcupacionalCorrecto = true;

                                dto.nivelRemunerativoCod = null;
                                dto.esNivelRemunerativoCorrecto = true;

                                if (dto.operacion == Operacion.Registrar && String.IsNullOrWhiteSpace(dto.categoriaDocenteCod))
                                {
                                    dto.observaciones.Add("La Categoría de Docente es obligatoria.");
                                }
                                else if (
                                    (dto.operacion == Operacion.Registrar && listaCategoriasDocente.Exists(x => x.categoriaDocenteCod == dto.categoriaDocenteCod.Trim())) ||
                                    (dto.operacion == Operacion.Actualizar && (dto.categoriaDocenteCod == null || listaCategoriasDocente.Exists(x => x.categoriaDocenteCod == dto.categoriaDocenteCod.Trim())))
                                    )
                                {
                                    dto.esCategoriaDocenteCorrecta = true;

                                    if (dto.categoriaDocenteCod != null)
                                    {
                                        dto.categoriaDocenteDesc = listaCategoriasDocente.First(x => x.categoriaDocenteCod == dto.categoriaDocenteCod.Trim())
                                                                .categoriaDocenteDesc;

                                        dto.categoriaDocenteID = listaCategoriasDocente.First(x => x.categoriaDocenteCod == dto.categoriaDocenteCod.Trim())
                                                                    .categoriaDocenteID;
                                    }
                                }
                                else
                                {
                                    dto.observaciones.Add("La Categoría de Docente no existe en el sistema.");
                                }

                                if (dto.operacion == Operacion.Registrar && String.IsNullOrWhiteSpace(dto.dedicacionDocenteCod))
                                {
                                    dto.observaciones.Add("La Dedicación de Docente es obligatoria.");
                                }
                                else
                                {
                                    dedicacionDocenteDesc = null;
                                    dedicacionDocenteID = null;

                                    if (dto.operacion == Operacion.Registrar || (dto.operacion == Operacion.Actualizar && dto.dedicacionDocenteCod != null))
                                    {
                                        if (dto.vinculo == Vinculo.DocentePermanente)
                                        {
                                            existeDedicacion = listaDedicacionDocenteOrd.Exists(x => x.dedicacionDocenteCod == dto.dedicacionDocenteCod.Trim());

                                            if (existeDedicacion)
                                            {
                                                dedicacionDocenteDesc = listaDedicacionDocenteOrd
                                                                    .First(x => x.dedicacionDocenteCod == dto.dedicacionDocenteCod.Trim())
                                                                    .dedicacionDocenteDesc;

                                                dedicacionDocenteID = listaDedicacionDocenteOrd
                                                                        .First(x => x.dedicacionDocenteCod == dto.dedicacionDocenteCod.Trim())
                                                                        .dedicacionDocenteID;
                                            }
                                        }
                                        else
                                        {
                                            existeDedicacion = listaDedicacionDocenteContr.Exists(x => x.dedicacionDocenteCod == dto.dedicacionDocenteCod.Trim());

                                            if (existeDedicacion)
                                            {
                                                dedicacionDocenteDesc = listaDedicacionDocenteContr
                                                                    .First(x => x.dedicacionDocenteCod == dto.dedicacionDocenteCod.Trim())
                                                                    .dedicacionDocenteDesc;

                                                dedicacionDocenteID = listaDedicacionDocenteContr
                                                                        .First(x => x.dedicacionDocenteCod == dto.dedicacionDocenteCod.Trim())
                                                                        .dedicacionDocenteID;
                                            }
                                        }

                                        if (existeDedicacion)
                                        {
                                            dto.dedicacionDocenteDesc = dedicacionDocenteDesc;

                                            dto.dedicacionDocenteID = dedicacionDocenteID.Value;

                                            dto.esDedicacionDocenteCorrecta = true;

                                            if (!dto.horasDocente.HasValue)
                                            {
                                                dto.observaciones.Add("La Hora docente es obligatoria.");
                                            }
                                            else if (listaHorasDedicacionDocente.Exists(x => x.dedicacionDocenteID == dedicacionDocenteID.Value && x.horas == dto.horasDocente.Value))
                                            {
                                                dto.horasDocenteID = listaHorasDedicacionDocente
                                                                    .First(x => x.dedicacionDocenteID == dedicacionDocenteID.Value && x.horas == dto.horasDocente.Value)
                                                                    .horasDocenteID;

                                                dto.esHorasDocentesCorrecta = true;
                                            }
                                            else
                                            {
                                                dto.observaciones.Add("La Hora no existe en el sistema.");
                                            }
                                        }
                                        else
                                        {
                                            dto.observaciones.Add("La Dedicación de Docente no existe en el sistema.");
                                        }
                                    }
                                    else
                                    {
                                        if (dto.horasDocente.HasValue)
                                        {
                                            dto.observaciones.Add("La Dedicación de Docente es obligatoria.");
                                        }
                                        else
                                        {
                                            dto.esDedicacionDocenteCorrecta = true;
                                            dto.esHorasDocentesCorrecta = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                dto.grupoOcupacionalCod = null;
                                dto.esGrupoOcupacionalCorrecto = true;

                                dto.nivelRemunerativoCod = null;
                                dto.esNivelRemunerativoCorrecto = true;

                                dto.categoriaDocenteCod = null;
                                dto.esCategoriaDocenteCorrecta = true;

                                dto.dedicacionDocenteCod = null;
                                dto.esDedicacionDocenteCorrecta = true;

                                dto.horasDocente = null;
                                dto.esHorasDocentesCorrecta = true;
                            }
                        }
                        else
                        {
                            dto.observaciones.Add("El código del Vínculo de trabajador no existe en el sistema.");
                        }
                    }

                    if (dto.operacion == Operacion.Registrar && String.IsNullOrWhiteSpace(dto.fechaIngreso))
                    {
                        dto.observaciones.Add("La Fecha de ingreso es obligatoria.");
                    }
                    else
                    {
                        if (dto.operacion == Operacion.Registrar)
                        {
                            dto.esFechaIngresoCorrecto = DateTime.TryParseExact(dto.fechaIngreso.Length > 10 ? dto.fechaIngreso.Substring(0, 10) : dto.fechaIngreso, Formats.BASIC_DATE,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None,
                                out dateTimeOut);

                            if (dto.esFechaIngresoCorrecto)
                            {
                                dto.fechaIngresoDT = dateTimeOut;

                                dto.esFechaIngresoCorrecto = true;
                            }
                            else
                            {
                                dto.observaciones.Add("El formato de la Fecha de ingreso es incorrecta.");
                            }
                        }
                        else
                        {
                            if (dto.fechaIngreso == null)
                            {
                                dto.esFechaIngresoCorrecto = true;
                            }
                            else
                            {
                                dto.esFechaIngresoCorrecto = DateTime.TryParseExact(dto.fechaIngreso.Length > 10 ? dto.fechaIngreso.Substring(0, 10) : dto.fechaIngreso, "dd/MM/yyyy",
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    System.Globalization.DateTimeStyles.None,
                                    out dateTimeOut);

                                if (dto.esFechaIngresoCorrecto)
                                {
                                    dto.fechaIngresoDT = dateTimeOut;

                                    dto.esFechaIngresoCorrecto = true;
                                }
                                else
                                {
                                    dto.observaciones.Add("El formato de la Fecha de ingreso es incorrecta.");
                                } 
                            }
                        }
                    }

                    if (dto.operacion == Operacion.Registrar && String.IsNullOrWhiteSpace(dto.dependenciaCod))
                    {
                        dto.observaciones.Add("La Dependencia es obligatoria.");
                    }
                    else if (
                        (dto.operacion == Operacion.Registrar && listaDependencias.Exists(x => x.dependenciaCod == dto.dependenciaCod.Trim())) ||
                        (dto.operacion == Operacion.Actualizar && (dto.dependenciaCod == null || listaDependencias.Exists(x => x.dependenciaCod == dto.dependenciaCod.Trim())))
                        )
                    {
                        dto.esDependenciaCorrecta = true;

                        if (dto.dependenciaCod != null)
                        {
                            dto.dependenciaDesc = listaDependencias.First(x => x.dependenciaCod == dto.dependenciaCod.Trim())
                                            .dependenciaDesc;

                            dto.dependenciaID = listaDependencias.First(x => x.dependenciaCod == dto.dependenciaCod.Trim())
                                                .dependenciaID;
                        }
                        else
                        {
                            dto.dependenciaID = 0;
                        }
                    }
                    else
                    {
                        dto.observaciones.Add("El código de Dependencia no existe en el sistema.");
                    }

                    if (dto.operacion == Operacion.Registrar)
                    {
                        if (!String.IsNullOrWhiteSpace(dto.bancoCod))
                        {
                            if (listaBancos.Exists(x => x.bancoCod == dto.bancoCod.Trim()))
                            {
                                dto.bancoDesc = listaBancos.First(x => x.bancoCod == dto.bancoCod.Trim()).bancoDesc;

                                dto.bancoID = listaBancos.First(x => x.bancoCod == dto.bancoCod.Trim()).bancoID;

                                dto.esBancoCorrecto = true;

                                if (String.IsNullOrWhiteSpace(dto.numeroCuentaBancaria))
                                {
                                    dto.observaciones.Add("El Número de Cuenta Bancaria es Obligatorio.");
                                }
                                else if (dto.numeroCuentaBancaria.Trim().Length > 0 && dto.numeroCuentaBancaria.Trim().Length <= 50)
                                {
                                    dto.esNumeroCuentaBancariaCorrecto = true;
                                }
                                else
                                {
                                    dto.observaciones.Add("La cantidad de caracteres del Número de Cuenta Bancaria es incorrecto.");
                                }

                                if (String.IsNullOrWhiteSpace(dto.tipoCuentaBancariaCod))
                                {
                                    dto.observaciones.Add("El Tipo de Cuenta Bancaria es obligatorio.");
                                }
                                else if (listaTipCuentasBancarias.Exists(x => x.tipoCuentaBancariaCod == dto.tipoCuentaBancariaCod.Trim()))
                                {
                                    dto.tipoCuentaBancariaDesc = listaTipCuentasBancarias.First(x => x.tipoCuentaBancariaCod == dto.tipoCuentaBancariaCod.Trim())
                                                                .tipoCuentaBancariaDesc;

                                    dto.tipoCuentaBancariaID = listaTipCuentasBancarias.First(x => x.tipoCuentaBancariaCod == dto.tipoCuentaBancariaCod.Trim())
                                                                .tipoCuentaBancariaID;

                                    dto.esTipoCtaBancariaCorrecta = true;
                                }
                                else
                                {
                                    dto.observaciones.Add("El Tipo de Cuenta Bancaria no existe en el sistema.");
                                }
                            }
                            else
                            {
                                dto.observaciones.Add("El código de Banco no existe en el sistema.");
                            }
                        }
                        else
                        {
                            dto.bancoCod = null;
                            dto.esBancoCorrecto = true;

                            dto.numeroCuentaBancaria = null;
                            dto.esNumeroCuentaBancariaCorrecto = true;

                            dto.tipoCuentaBancariaCod = null;
                            dto.esTipoCtaBancariaCorrecta = true;
                        }

                        if (!String.IsNullOrEmpty(dto.regimenPensionarioCod))
                        {
                            if (listaRegimen.Exists(x => x.regimenCod == dto.regimenPensionarioCod.Trim()))
                            {
                                regimenDTO = listaRegimen.First(x => x.regimenCod == dto.regimenPensionarioCod.Trim());

                                dto.regimenDesc = regimenDTO.regimenDesc;

                                dto.regimenID = regimenDTO.regimenID;

                                dto.esRegimenCorrecto = true;

                                if (regimenDTO.regimenID == (int)RegimenPensionario.SPP)
                                {
                                    if (String.IsNullOrEmpty(dto.afpCod))
                                    {
                                        dto.observaciones.Add("El código de AFP es obligatorio.");
                                    }
                                    else if (listaAfps.Exists(x => x.afpCod == dto.afpCod.Trim()))
                                    {
                                        dto.afpDesc = listaAfps.First(x => x.afpCod == dto.afpCod.Trim()).afpDesc;

                                        dto.afpID = listaAfps.First(x => x.afpCod == dto.afpCod.Trim()).afpID;

                                        dto.esAFPCorrecto = true;
                                    }
                                    else
                                    {
                                        dto.observaciones.Add("El código de AFP no existe en el sistema.");
                                    }

                                    if (String.IsNullOrEmpty(dto.cuspp))
                                    {
                                        dto.observaciones.Add("El CUSPP es obligatorio.");
                                    }
                                    else if (dto.cuspp.Trim().Length > 0 && dto.cuspp.Trim().Length <= 20)
                                    {
                                        dto.esCusppCorrecto = true;
                                    }
                                    else
                                    {
                                        dto.observaciones.Add("La cantidad de caracteres del CUSPP es incorrecto.");
                                    }
                                }
                                else
                                {
                                    dto.afpCod = null;
                                    dto.esAFPCorrecto = true;

                                    dto.cuspp = null;
                                    dto.esCusppCorrecto = true;
                                }
                            }
                            else
                            {
                                dto.observaciones.Add("El código de Régimen Pensionario no existe en el sistema.");
                            }
                        }
                        else
                        {
                            dto.regimenPensionarioCod = null;
                            dto.esRegimenCorrecto = true;

                            dto.afpCod = null;
                            dto.esAFPCorrecto = true;

                            dto.cuspp = null;
                            dto.esCusppCorrecto = true;
                        }
                    }
                    else
                    {
                        if (dto.esTrabajadorIDCorrecto)
                        {
                            if (String.IsNullOrWhiteSpace(dto.bancoCod))
                            {
                                dto.bancoCod = null;
                                dto.esBancoCorrecto = true;
                            }
                            else
                            {
                                if (listaBancos.Exists(x => x.bancoCod == dto.bancoCod.Trim()))
                                {
                                    dto.bancoDesc = listaBancos.First(x => x.bancoCod == dto.bancoCod.Trim()).bancoDesc;

                                    dto.bancoID = listaBancos.First(x => x.bancoCod == dto.bancoCod.Trim()).bancoID;

                                    dto.esBancoCorrecto = true;
                                }
                                else
                                {
                                    dto.observaciones.Add("El código de Banco no existe en el sistema.");
                                }
                            }

                            if (String.IsNullOrWhiteSpace(dto.numeroCuentaBancaria))
                            {
                                dto.numeroCuentaBancaria = null;
                                dto.esNumeroCuentaBancariaCorrecto = true;
                            }
                            else
                            {
                                if (dto.numeroCuentaBancaria.Trim().Length > 0 && dto.numeroCuentaBancaria.Trim().Length <= 50)
                                {
                                    dto.esNumeroCuentaBancariaCorrecto = true;
                                }
                                else
                                {
                                    dto.observaciones.Add("La cantidad de caracteres del Número de Cuenta Bancaria es incorrecto.");
                                }
                            }

                            if (String.IsNullOrWhiteSpace(dto.tipoCuentaBancariaCod))
                            {
                                dto.tipoCuentaBancariaCod = null;
                                dto.esTipoCtaBancariaCorrecta = true;
                            }
                            else
                            {
                                if (listaTipCuentasBancarias.Exists(x => x.tipoCuentaBancariaCod == dto.tipoCuentaBancariaCod.Trim()))
                                {
                                    dto.tipoCuentaBancariaDesc = listaTipCuentasBancarias.First(x => x.tipoCuentaBancariaCod == dto.tipoCuentaBancariaCod.Trim())
                                                                .tipoCuentaBancariaDesc;

                                    dto.tipoCuentaBancariaID = listaTipCuentasBancarias.First(x => x.tipoCuentaBancariaCod == dto.tipoCuentaBancariaCod.Trim())
                                                                .tipoCuentaBancariaID;

                                    dto.esTipoCtaBancariaCorrecta = true;
                                }
                                else
                                {
                                    dto.observaciones.Add("El Tipo de Cuenta Bancaria no existe en el sistema.");
                                }
                            }

                            if (String.IsNullOrWhiteSpace(dto.regimenPensionarioCod))
                            {
                                dto.regimenPensionarioCod = null;
                                dto.esRegimenCorrecto = true;
                            }
                            else
                            {
                                if (listaRegimen.Exists(x => x.regimenCod == dto.regimenPensionarioCod.Trim()))
                                {
                                    regimenDTO = listaRegimen.First(x => x.regimenCod == dto.regimenPensionarioCod.Trim());

                                    dto.regimenDesc = regimenDTO.regimenDesc;

                                    dto.regimenID = regimenDTO.regimenID;

                                    dto.esRegimenCorrecto = true;
                                }
                                else
                                {
                                    dto.observaciones.Add("El código de Régimen Pensionario no existe en el sistema.");
                                }
                            }

                            if (String.IsNullOrWhiteSpace(dto.afpCod))
                            {
                                dto.afpCod = null;
                                dto.esAFPCorrecto = true;
                            }
                            else
                            {
                                if (dto.esRegimenCorrecto)
                                {
                                    if (dto.regimenPensionarioCod == null)
                                    {
                                        if (trabajadorCategoria.regimenID == (int)RegimenPensionario.SPP)
                                        {
                                            if (listaAfps.Exists(x => x.afpCod == dto.afpCod.Trim()))
                                            {
                                                dto.afpDesc = listaAfps.First(x => x.afpCod == dto.afpCod.Trim()).afpDesc;

                                                dto.afpID = listaAfps.First(x => x.afpCod == dto.afpCod.Trim()).afpID;

                                                dto.esAFPCorrecto = true;
                                            }
                                            else
                                            {
                                                dto.observaciones.Add("El código de AFP no existe en el sistema.");
                                            }
                                        }
                                        else
                                        {
                                            dto.observaciones.Add("No se puede registrar el AFP para un regimen diferente al del SPP.");
                                        }
                                    }
                                    else
                                    {
                                        if (dto.regimenID == (int)RegimenPensionario.SPP)
                                        {
                                            if (listaAfps.Exists(x => x.afpCod == dto.afpCod.Trim()))
                                            {
                                                dto.afpDesc = listaAfps.First(x => x.afpCod == dto.afpCod.Trim()).afpDesc;

                                                dto.afpID = listaAfps.First(x => x.afpCod == dto.afpCod.Trim()).afpID;

                                                dto.esAFPCorrecto = true;
                                            }
                                            else
                                            {
                                                dto.observaciones.Add("El código de AFP no existe en el sistema.");
                                            }
                                        }
                                        else
                                        {
                                            dto.observaciones.Add("No se puede registrar el AFP para un regimen diferente al del SPP.");
                                        }
                                    }
                                }   
                            }

                            if (String.IsNullOrWhiteSpace(dto.cuspp))
                            {
                                dto.cuspp = null;
                                dto.esCusppCorrecto = true;
                            }
                            else
                            {
                                if (dto.esRegimenCorrecto)
                                {
                                    if (dto.regimenPensionarioCod == null)
                                    {
                                        if (trabajadorCategoria.regimenID == (int)RegimenPensionario.SPP)
                                        {
                                            if (dto.cuspp.Trim().Length > 0 && dto.cuspp.Trim().Length <= 20)
                                            {
                                                dto.esCusppCorrecto = true;
                                            }
                                            else
                                            {
                                                dto.observaciones.Add("La cantidad de caracteres del CUSPP es incorrecto.");
                                            }
                                        }
                                        else
                                        {
                                            dto.observaciones.Add("No se puede registrar el CUSPP para un regimen diferente al del SPP.");
                                        }
                                    }
                                    else
                                    {
                                        if (dto.regimenID == (int)RegimenPensionario.SPP)
                                        {
                                            if (dto.cuspp.Trim().Length > 0 && dto.cuspp.Trim().Length <= 20)
                                            {
                                                dto.esCusppCorrecto = true;
                                            }
                                            else
                                            {
                                                dto.observaciones.Add("La cantidad de caracteres del CUSPP es incorrecto.");
                                            }
                                        }
                                        else
                                        {
                                            dto.observaciones.Add("No se puede registrar el CUSPP para un regimen diferente al del SPP.");
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(dto.codigoPlaza))
                    {
                        if (dto.codigoPlaza.Trim().Length == 6)
                        {
                            dto.esCodigoPlazaCorrecto = true;

                            dto.esCodPlazaDuplicadoEnArchivo = lectura.Count(x => !String.IsNullOrWhiteSpace(x.codigoPlaza) &&
                                x.codigoPlaza.Trim() == dto.codigoPlaza.Trim()) > 1;

                            if (dto.esCodPlazaDuplicadoEnArchivo)
                            {
                                dto.observaciones.Add("El Código de plaza se repite en el archivo.");
                            }
                        }
                        else
                        {
                            dto.observaciones.Add("La cantidad de caracteres del Código de Plaza es incorreco.");
                        }
                    }
                    else
                    {
                        dto.codigoPlaza = null;
                        dto.esCodigoPlazaCorrecto = true;
                    }

                    if (dto.operacion == Operacion.Registrar && String.IsNullOrWhiteSpace(dto.estadoTrabajadorCod))
                    {
                        dto.observaciones.Add("El campo Estado es obligatorio.");
                    }
                    else if (
                        (dto.operacion == Operacion.Registrar && listaEstados.Exists(x => x.estadoCod == dto.estadoTrabajadorCod.Trim())) ||
                        (dto.operacion == Operacion.Actualizar && (dto.estadoTrabajadorCod == null || listaEstados.Exists(x => x.estadoCod == dto.estadoTrabajadorCod.Trim())))
                        )
                    {
                        dto.esEstadoTrabajadorCorrecto = true;

                        if (dto.estadoTrabajadorCod != null)
                        {
                            dto.estadoTrabajadorDesc = listaEstados.First(x => x.estadoCod == dto.estadoTrabajadorCod.Trim())
                                        .estadoDesc;

                            dto.estadoTrabajadorID = listaEstados.First(x => x.estadoCod == dto.estadoTrabajadorCod.Trim())
                                            .estadoID;
                        }
                        else
                        {
                            dto.estadoTrabajadorID = 0;
                        }
                    }
                    else
                    {
                        dto.observaciones.Add("El código del Estado del Trabajador no existe en el sistema.");
                    }

                    if (dto.esRegistroCasiCorrecto)
                    {
                        var model = Mapper.TrabajadorModel(dto);

                        var results = new List<ValidationResult>();

                        var context = new ValidationContext(model, null, null);

                        dto.esModelStateValid = Validator.TryValidateObject(model, context, results, true);

                        if (!dto.esModelStateValid)
                        {
                            foreach (var error in results)
                            {
                                dto.observaciones.Add(error.ErrorMessage);
                            }
                        }
                        else
                        {
                            if (_trabajadorService.EsNumeroDocumentoIdentidadDuplicado(dto.tipoDocumentoID.Value, dto.numDocumento, categoriaPlanillaID, dto.trabajadorID))
                            {
                                dto.esNumDocIdentDuplicadoEnBD = true;

                                dto.observaciones.Add("El Número de Documento de Identida se repite en el sistema.");
                            }

                            if (_trabajadorService.EsCodigoPlazaDuplicado(dto.codigoPlaza, dto.trabajadorID))
                            {
                                dto.esCodPlazaDuplicadoEnBD = true;

                                dto.observaciones.Add("El Código de Plaza se repite en el sistema.");
                            }
                        }
                    }
                }

                lecturaProcesada.Add(dto);
            }

            return lecturaProcesada;
        }
    }
}
