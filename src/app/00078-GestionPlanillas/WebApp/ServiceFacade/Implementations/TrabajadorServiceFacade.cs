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
        private IGrupoOcupacionalService _grupoOcupacionalService;
        private INivelRemunerativoService _nivoRemunerativoService;
        private ICategoriaDocenteService _categoriaDocenteService;
        private IAfpService _afpService;

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
            List<TrabajadorModel> trabajadoresModel;
            List<TrabajadorEntity> registrosAptos;

            try
            {
                lecturaArchivoService = FileManagement.GetLecturaService(Path.GetExtension(fileName));

                lectura = lecturaArchivoService.ObtenerListaTrabajadores(Path.Combine(serverPath, fileName));

                lecturaProcesada = ObtenerLecturaProcesada(lectura);

                if (lecturaProcesada != null && lecturaProcesada.Count() > 0)
                {
                    trabajadoresModel = lecturaProcesada
                       .Where(x => x.esRegistroCorrecto)
                       .Select(x => new TrabajadorModel()
                       {
                           trabajadorCod = x.codigoTrabajador,
                           codigoPlaza = x.codigoPlaza,
                           nombre = x.nombres,
                           apellidoPaterno = x.apePaterno,
                           apellidoMaterno = x.apeMaterno,
                           tipoDocumentoID = x.tipoDocumentoID.Value,
                           numDocumento = x.numDocumento,
                           sexoID = x.sexoID.Value,
                           fechaIngreso = x.fechaIngresoDT.Value,
                           regimenID = x.regimenID,
                           afpID = x.afpID,
                           cuspp = x.cuspp,
                           //ESTADO
                           vinculoID = x.vinculoID.Value,
                           dependenciaID = x.dependenciaID.Value,
                           bancoID = x.bancoID,
                           nroCuentaBancaria = x.numeroCuentaBancaria,
                           tipoCuentaBancariaID = x.tipoCuentaBancariaID,
                           categoriaDocenteID = x.categoriaDocenteID,
                           grupoOcupacionalID = x.grupoOcupacionalID,
                           nivelRemunerativoID = x.nivelRemunerativoID
                       }).ToList();

                    if (trabajadoresModel.Count() > 0)
                    {
                        response = _trabajadorService.GrabarTrabajador(Operacion.Registrar, new TrabajadorEntity(), userID);
                    }
                    else
                    {
                        response = new Response()
                        {
                            Message = "No hay registros aptos para ser grabados."
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
            var listaDependencias = _dependenciaService.ListarDependencias();
            var listaRegimen = _regimenService.ListarRegimenes();
            var listaAfps = _afpService.ListarAfps();
            var listaBancos = _bancoService.ListarBancos();
            var listaTipCuentasBancarias = _bancoService.ListarTipoCuentasBancarias();

            VinculoDTO vinculoDTO;
            RegimenDTO regimenDTO;
            DateTime dateTimeOut;
            var lecturaProcesada = new List<TrabajadorLecturaProcesadoDTO>();

            foreach (var item in lectura)
            {
                var dto = new TrabajadorLecturaProcesadoDTO()
                {
                    tipoDocumentoCod = item.tipoDocumentoCod,
                    numDocumento = item.numDocumento,
                    apePaterno = item.apePaterno,
                    apeMaterno = item.apeMaterno,
                    nombres = item.nombres,
                    sexoCod = item.sexoCod,
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
                    tipoCuentaBancariaCod = item.tipoCuentaBancariaCod,
                    regimenPensionarioCod = item.regimenPensionarioCod,
                    afpCod = item.afpCod,
                    cuspp = item.cuspp,
                    codigoPlaza = item.codigoPlaza
                };

                if (String.IsNullOrEmpty(dto.tipoDocumentoCod))
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

                if (String.IsNullOrEmpty(dto.numDocumento))
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

                if (String.IsNullOrEmpty(dto.apePaterno))
                {
                    dto.observaciones.Add("El Apellido paterno es obligatorio.");
                }
                else if (dto.apePaterno.Trim().Length > 0 && dto.apePaterno.Trim().Length <= 50)
                {
                    dto.esApePaternoCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("La cantidad de caracteres del Apellido paterno es incorrecto.");
                }

                if (String.IsNullOrEmpty(dto.nombres))
                {
                    dto.observaciones.Add("El Nombre es obligatorio.");
                }
                else if (dto.nombres.Trim().Length > 0 && dto.nombres.Trim().Length <= 50)
                {
                    dto.esNombreCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("La cantidad de caracteres del Nombre es incorrecto.");
                }

                if (String.IsNullOrEmpty(dto.sexoCod))
                {
                    dto.observaciones.Add("El campo Sexo es obligatorio.");
                }
                else if(listaSexos.Exists(x => x.sexoCod == dto.sexoCod.Trim()))
                {
                    dto.sexoDesc = listaSexos.First(x => x.sexoCod == dto.sexoCod.Trim())
                                    .sexoDesc;

                    dto.sexoID = listaSexos.First(x => x.sexoCod == dto.sexoCod.Trim())
                                    .sexoID;

                    dto.esSexoCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("El código del campo Sexo no existe en el sistema.");
                }

                if (String.IsNullOrEmpty(dto.codigoTrabajador))
                {
                    dto.observaciones.Add("El Código de Trabajador es obligatorio.");
                }
                else if (dto.codigoTrabajador.Trim().Length > 0 && dto.codigoTrabajador.Trim().Length <= 20)
                {
                    dto.esCodigoTrabajadorCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("La cantidad de caracteres del Código de trabajador es incorrecto.");
                }

                if (String.IsNullOrEmpty(dto.vinculoCod))
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

                        dto.esVinculoCorrecto = true;

                        if (dto.vinculo == Vinculo.AdministrativoPermanente || dto.vinculo == Vinculo.AdministrativoContratado)
                        {
                            dto.categoriaDocenteCod = null;
                            dto.esCategoriaDocenteCorrecta = true;

                            dto.dedicacionDocenteCod = null;
                            dto.esDedicacionDocenteCorrecta = true;

                            dto.horasDocente = null;
                            dto.esHorasDocentesCorrecta = true;

                            if (String.IsNullOrEmpty(dto.grupoOcupacionalCod))
                            {
                                dto.observaciones.Add("El Grupo ocupacional es obligatorio.");
                            }
                            else if (listaGruposOcupacionales.Exists(x => x.grupoOcupacionalCod == dto.grupoOcupacionalCod.Trim()))
                            {
                                dto.grupoOcupacionalDesc = listaGruposOcupacionales.First(x => x.grupoOcupacionalCod == dto.grupoOcupacionalCod.Trim())
                                                        .grupoOcupacionalDesc;

                                dto.grupoOcupacionalID = listaGruposOcupacionales.First(x => x.grupoOcupacionalCod == dto.grupoOcupacionalCod.Trim())
                                                        .grupoOcupacionalID;

                                dto.esGrupoOcupacionalCorrecto = true;
                            }
                            else
                            {
                                dto.observaciones.Add("El código del Grupo Ocupacional no existe en el sistema.");
                            }

                            if (String.IsNullOrEmpty(dto.nivelRemunerativoCod))
                            {
                                dto.observaciones.Add("El Nivel Remunerativo es obligatorio.");
                            }
                            else if (listaNivelesRemunerativos.Exists(x => x.nivelRemunerativoCod == dto.nivelRemunerativoCod.Trim()))
                            {
                                dto.nivelRemunerativoDesc = listaNivelesRemunerativos.First(x => x.nivelRemunerativoCod == dto.nivelRemunerativoCod.Trim())
                                                            .nivelRemunerativoDesc;

                                dto.nivelRemunerativoID = listaNivelesRemunerativos.First(x => x.nivelRemunerativoCod == dto.nivelRemunerativoCod.Trim())
                                                            .nivelRemunerativoID;

                                dto.esNivelRemunerativoCorrecto = true;
                            }
                            else
                            {
                                dto.observaciones.Add("El código del Nivel Remunerativo no existe en el sistema.");
                            }
                        }
                        else if (dto.vinculo == Vinculo.DocentePermanente || dto.vinculo == Vinculo.DocenteContratado)
                        {
                            dto.grupoOcupacionalCod = null;
                            dto. esGrupoOcupacionalCorrecto = true;

                            dto.nivelRemunerativoCod = null;
                            dto.esNivelRemunerativoCorrecto = true;

                            if (String.IsNullOrEmpty(dto.categoriaDocenteCod))
                            {
                                dto.observaciones.Add("La Categoría de Docente es obligatoria.");
                            }
                            else if(listaCategoriasDocente.Exists(x => x.categoriaDocenteCod == dto.categoriaDocenteCod.Trim()))
                            {
                                dto.categoriaDocenteDesc = listaCategoriasDocente.First(x => x.categoriaDocenteCod == dto.categoriaDocenteCod.Trim())
                                                            .categoriaDocenteDesc;

                                dto.categoriaDocenteID = listaCategoriasDocente.First(x => x.categoriaDocenteCod == dto.categoriaDocenteCod.Trim())
                                                            .categoriaDocenteID;

                                dto.esCategoriaDocenteCorrecta = true;
                            }
                            else
                            {
                                dto.observaciones.Add("La Categoría de Docente no existe en el sistema.");
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

                if (String.IsNullOrEmpty(dto.fechaIngreso))
                {
                    dto.observaciones.Add("La Fecha de ingreso es obligatoria.");
                }
                else
                {
                    dto.esFechaIngresoCorrecto = DateTime.TryParseExact(dto.fechaIngreso, "dd/MM/yyyy",
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

                if (String.IsNullOrEmpty(dto.dependenciaCod))
                {
                    dto.observaciones.Add("La Dependencia es obligatoria.");
                }
                else if(listaDependencias.Exists(x => x.dependenciaCod == dto.dependenciaCod.Trim()))
                {
                    dto.dependenciaDesc = listaDependencias.First(x => x.dependenciaCod == dto.dependenciaCod.Trim())
                                        .dependenciaDesc;

                    dto.dependenciaID = listaDependencias.First(x => x.dependenciaCod == dto.dependenciaCod.Trim())
                                        .dependenciaID;

                    dto.esDependenciaCorrecta = true;
                }
                else
                {
                    dto.observaciones.Add("El código de Dependencia no existe en el sistema.");
                }

                if (!String.IsNullOrEmpty(dto.bancoCod))
                {
                    if (listaBancos.Exists(x => x.bancoCod == dto.bancoCod.Trim()))
                    {
                        dto.bancoDesc = listaBancos.First(x => x.bancoCod == dto.bancoCod.Trim()).bancoDesc;

                        dto.bancoID = listaBancos.First(x => x.bancoCod == dto.bancoCod.Trim()).bancoID;

                        dto.esBancoCorrecto = true;

                        if (String.IsNullOrEmpty(dto.numeroCuentaBancaria))
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
                        
                        if (String.IsNullOrEmpty(dto.tipoCuentaBancariaCod))
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
                }

                if (!String.IsNullOrEmpty(dto.codigoPlaza))
                {
                    if (dto.codigoPlaza.Trim().Length > 0 && dto.codigoPlaza.Trim().Length <= 6)
                    {
                        dto.esCodigoPlazaCorrecto = true;
                    }
                    else
                    {
                        dto.observaciones.Add("La cantidad de caracteres del Código de Plaza es incorreco.");
                    }
                }
                
                lecturaProcesada.Add(dto);
            }

            return lecturaProcesada;
        }
    }
}
