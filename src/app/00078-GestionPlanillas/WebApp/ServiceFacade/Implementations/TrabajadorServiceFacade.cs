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

                fileContent = null;//generacionArchivoService.GenerarDescargableDeLecturaCargaDeTrabajadores(lecturaProcesada);
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
            List<TrabajadorEntity> registrosAptos;

            try
            {
                lecturaArchivoService = FileManagement.GetLecturaService(Path.GetExtension(fileName));

                lectura = lecturaArchivoService.ObtenerListaTrabajadores(Path.Combine(serverPath, fileName));

                lecturaProcesada = ObtenerLecturaProcesada(lectura);

                if (lecturaProcesada != null && lecturaProcesada.Count() > 0)
                {
                    registrosAptos = lecturaProcesada
                       .Where(x => x.esRegistroCorrecto)
                       .Select(x => new TrabajadorEntity()
                       {
                           trabajadorCod = x.codigoTrabajador,
                           codigoPlaza = x.codigoPlaza,
                           apellidoPaterno = x.apePaterno,
                           apellidoMaterno = x.apeMaterno,
                           nombre = x.nombres
                       }).ToList();

                    if (registrosAptos.Count() > 0)
                    {
                        response = _trabajadorService.GrabarTrabajador(Operacion.Registrar, registrosAptos.First(), userID);
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

                if (dto.tipoDocumentoCod != null && listaTipoDocumento.Exists(x => x.tipoDocumentoCod == dto.tipoDocumentoCod))
                {
                    dto.tipoDocumentoDesc = listaTipoDocumento.First(x => x.tipoDocumentoCod == dto.tipoDocumentoCod).tipoDocumentoDesc;

                    dto.esTipoDocumentoCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("Tipo de documento de identidad incorrecto.");
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

                    dto.vinculo = (Vinculo)listaVinculos.First(x => x.vinculoCod == dto.vinculoCod).vinculoID;

                    dto.esVinculoCorrecto = true;

                    if (dto.vinculo == Vinculo.AdministrativoPermanente || dto.vinculo == Vinculo.AdministrativoContratado)
                    {
                        if (dto.grupoOcupacionalCod != null && listaGruposOcupacionales.Exists(x => x.grupoOcupacionalCod == dto.grupoOcupacionalCod))
                        {
                            dto.grupoOcupacionalDesc = listaGruposOcupacionales.First(x => x.grupoOcupacionalCod == dto.grupoOcupacionalCod).grupoOcupacionalDesc;

                            dto.esGrupoOcupacionalCorrecto = true;
                        }
                        else
                        {
                            dto.observaciones.Add("El Grupo Ocupacional es incorrecto.");
                        }

                        if (dto.nivelRemunerativoCod != null && listaNivelesRemunerativos.Exists(x => x.nivelRemunerativoCod == dto.nivelRemunerativoCod))
                        {
                            dto.nivelRemunerativoDesc = listaNivelesRemunerativos.First(x => x.nivelRemunerativoCod == dto.nivelRemunerativoCod).nivelRemunerativoDesc;

                            dto.esNivelRemunerativoCorrecto = true;
                        }
                        else
                        {
                            dto.observaciones.Add("El Nivel Remunerativo es incorrecto.");
                        }
                    }
                    else if(dto.vinculo == Vinculo.DocentePermanente || dto.vinculo == Vinculo.DocenteContratado)
                    {
                        if (dto.categoriaDocenteCod != null && listaCategoriasDocente.Exists(x => x.categoriaDocenteCod == dto.categoriaDocenteCod))
                        {
                            dto.categoriaDocenteDesc = listaCategoriasDocente.First(x => x.categoriaDocenteCod == dto.categoriaDocenteCod).categoriaDocenteDesc;

                            dto.esCategoriaDocenteCorrecta = true;
                        }
                        else
                        {
                            dto.observaciones.Add("La Categoría de Docente es incorrecto.");
                        }
                    }
                }
                else
                {
                    dto.observaciones.Add("El Vínculo es incorrecto.");
                }

                if (dto.bancoCod != null && listaBancos.Exists(x => x.bancoCod == dto.bancoCod))
                {
                    dto.bancoDesc = listaBancos.First(x => x.bancoCod == dto.bancoCod).bancoDesc;

                    dto.esBancoCorrecto = true;

                    if (dto.bancoCod != null && dto.tipoCuentaBancaria != null && listaTipCuentasBancarias.Exists(x => x.tipoCuentaBancariaCod == dto.tipoCuentaBancaria))
                    {
                        dto.tipoCuentaBancariaDesc = listaTipCuentasBancarias.First(x => x.tipoCuentaBancariaCod == dto.tipoCuentaBancaria).tipoCuentaBancariaDesc;

                        dto.esTipoCtaBancariaCorrecta = true;
                    }
                    else
                    {
                        dto.observaciones.Add("El Tipo de Cuenta Bancaria es incorrecto.");
                    }
                }
                else
                {
                    dto.observaciones.Add("El Código de Banco no existe en el sistema.");
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

                if (dto.regimenPensionarioCod != null && listaRegimen.Exists(x => x.regimenCod == dto.regimenPensionarioCod))
                {
                    dto.regimenDesc = listaRegimen.First(x => x.regimenCod == dto.regimenPensionarioCod).regimenDesc;

                    dto.esRegimenCorrecto = true;

                    if (dto.afpCod != null && listaAfps.Exists(x => x.afpCod == dto.afpCod))
                    {
                        dto.afpDesc = listaAfps.First(x => x.afpCod == dto.afpCod).afpDesc;

                        dto.esAFPCorrecto = true;
                    }
                    else
                    {
                        dto.observaciones.Add("El Código de AFP no existe en el sistema.");
                    }
                }
                else
                {
                    dto.observaciones.Add("El Régimen Pensionario es incorrecto.");
                }

                lecturaProcesada.Add(dto);
            }

            return lecturaProcesada;
        }
    }
}
