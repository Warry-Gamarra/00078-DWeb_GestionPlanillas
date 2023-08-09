using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class ValorExternoConceptoServiceFacade : IValorExternoConceptoServiceFacade
    {
        private IPeriodoService _periodoService;
        private ITrabajadorService _trabajadorService;
        private IConceptoService _conceptoService;
        private IProveedorService _proveedorService;
        private IValorExternoConceptoService _valorExternoConceptoService;
        private IPlanillaService _planillaService;
        private IPlantillaPlanillaConceptoService _plantillaPlanillaConceptoService;
        private ITipoDocumentoService _tipoDocumentoService;
        private ICategoriaPlanillaService _categoriaPlanillaService;
        private IPersonaService _personaService;

        private readonly string serverPath;

        public ValorExternoConceptoServiceFacade()
        {
            _periodoService = new PeriodoService();
            _trabajadorService = new TrabajadorService();
            _conceptoService = new ConceptoService();
            _proveedorService = new ProveedorService();
            _valorExternoConceptoService = new ValorExternoConceptoService();
            _planillaService = new PlanillaService();
            _plantillaPlanillaConceptoService = new PlantillaPlanillaConceptoService();
            _tipoDocumentoService = new TipoDocumentoService();
            _categoriaPlanillaService = new CategoriaPlanillaService();
            _personaService = new PersonaService();

            serverPath = WebConfigParams.DirectorioArchivosExternos;
        }

        public Tuple<string, List<ValorExternoLecturaProcesadoDTO>> ObtenerListaValoresDeConceptos(HttpPostedFileBase file)
        {
            string newFileName;
            ILecturaArchivoService lecturaArchivoService;
            List<ValorExternoLecturaDTO> lectura;
            List<ValorExternoLecturaProcesadoDTO> lecturaProcesada;

            try
            {
                newFileName = GuardarArchivo(serverPath, file);

                lecturaArchivoService = GetService(Path.GetExtension(newFileName));

                lectura = lecturaArchivoService.ObtenerListaValoresDeConceptos(Path.Combine(serverPath, newFileName));

                lecturaProcesada = ObtenerLecturaProcesada(lectura);
            }
            catch(IndexOutOfRangeException ex)
            {
                throw new IndexOutOfRangeException("El archivo no tiene la estructura correcta.", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new Tuple<string, List<ValorExternoLecturaProcesadoDTO>>(newFileName, lecturaProcesada);
        }

        public Response GrabarValoresExternos(string fileName, int userID)
        {
            Response response;
            ILecturaArchivoService lecturaArchivoService;
            List<ValorExternoLecturaDTO> lectura;
            List<ValorExternoLecturaProcesadoDTO> lecturaProcesada;
            List<ValorConceptoEntity> registrosAptos;
            
            try
            {
                lecturaArchivoService = GetService(Path.GetExtension(fileName));

                lectura = lecturaArchivoService.ObtenerListaValoresDeConceptos(Path.Combine(serverPath, fileName));

                lecturaProcesada = ObtenerLecturaProcesada(lectura);

                if (lecturaProcesada != null && lecturaProcesada.Count() > 0)
                {
                    registrosAptos = lecturaProcesada
                       .Where(x => x.esRegistroCorrecto)
                       .Select(x => new ValorConceptoEntity()
                       {
                           periodoID = x.periodoID.Value,
                           trabajadorCategoriaPlanillaID = x.trabajadorCategoriaPlanillaID.Value,
                           conceptoID = x.conceptoID.Value,
                           valorConcepto = x.valorConcepto.Value,
                           proveedorID = x.proveedorID.Value
                       }).ToList();

                    if (registrosAptos.Count() > 0)
                    {
                        response = _valorExternoConceptoService.GrabarValoresExternos(registrosAptos, userID);
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

        public List<ValorExternoConceptoModel> ListarValoresExternos(int anio, int mes, int categoriaPlanillaID)
        {
            var listaTrabajadoresConPlanilla = _planillaService.ListarResumenPlanillaTrabajadores(anio, mes, categoriaPlanillaID);

            var lista = _valorExternoConceptoService.ListarValoresExternosConceptos(anio, mes, categoriaPlanillaID)
                .Select(x => Mapper.ValorExternoConceptoDTO_To_ValorExternoConceptoModel(x))
                .ToList();

            lista.ForEach(x => {
                x.tienePlanilla = listaTrabajadoresConPlanilla.Exists(y => y.trabajadorID == x.trabajadorID);
            });

            return lista;
        }

        public ValorExternoConceptoModel ObtenerValorExterno(int conceptoExternoValorID)
        {
            ValorExternoConceptoModel model;

            try
            {
                var dto = _valorExternoConceptoService.ObtenerValorExternoConcepto(conceptoExternoValorID);

                if (dto == null)
                {
                    model = null;
                }
                else
                {
                    model = Mapper.ValorExternoConceptoDTO_To_ValorExternoConceptoModel(dto);

                    model.tienePlanilla = _planillaService.ExistePlanillaTrabajador(
                        model.trabajadorID, model.periodoID, model.categoriaPlanillaID);
                }
            }
            catch (Exception)
            {
                model = null;
            }

            return model;
        }

        public Response ActualizarValorExternoConcepto(int conceptoExternoValorID, decimal valorConcepto, int userID)
        {
            Response response;

            try
            {
                response = _valorExternoConceptoService.ActualizarValorExternoConcepto(conceptoExternoValorID, valorConcepto, userID);
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

        public Response Eliminar(int conceptoExternoValorID, int userID)
        {
            Response response;

            try
            {
                response = _valorExternoConceptoService.Eliminar(conceptoExternoValorID, userID);
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

        private string GuardarArchivo(string serverPath, HttpPostedFileBase file)
        {
            if (serverPath == null || !Directory.Exists(serverPath))
            {
                throw new DirectoryNotFoundException("No existe el directorio para almacenar el archivo.");
            }

            string newFileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);

            string filePath = Path.Combine(serverPath, newFileName);

            file.SaveAs(filePath);

            return newFileName;
        }

        private ILecturaArchivoService GetService(string extension)
        {
            ILecturaArchivoService _lecturaArchivoService;

            switch (extension)
            {
                case ".xls":
                    _lecturaArchivoService = new LecturaArchivoExcelService();
                    break;

                case ".xlsx":
                    _lecturaArchivoService = new LecturaArchivoExcelService();
                    break;

                default:
                    throw new Exception("Tipo de archivo desconocido.");
            }

            return _lecturaArchivoService;
        }

        private List<ValorExternoLecturaProcesadoDTO> ObtenerLecturaProcesada(List<ValorExternoLecturaDTO> lectura)
        {
            var listaTipoDocumento = _tipoDocumentoService.ListaTipoDocumentos();
            
            var listaCategoriaPlanilla = _categoriaPlanillaService.ListarCategoriasPlanillas();

            var listaProveedores = _proveedorService.ListarProveedores();

            var lecturaProcesada = new List<ValorExternoLecturaProcesadoDTO>();

            foreach (var item in lectura)
            {
                PeriodoDTO periodoDTO = null;
                PersonaDTO personaDTO = null;
                TrabajadorCategoriaPlanillaDTO trabajadorDTO = null;
                ConceptoDTO conceptoDTO = null;
                
                var dto = new ValorExternoLecturaProcesadoDTO()
                {
                    anio = item.anio,
                    mes = item.mes,
                    tipoDocumentoID = item.tipoDocumentoID,
                    numDocumento = item.numDocumento,
                    categoriaPlanillaID = item.categoriaPlanillaID,
                    conceptoCod = item.conceptoCod,
                    valorConcepto = item.valorConcepto,
                    proveedorID = item.proveedorID
                };

                dto.esDuplicadoEnArchivo = (lectura.Count(x => x.anio == item.anio && x.anio == item.anio &&
                    x.tipoDocumentoID == item.tipoDocumentoID && x.numDocumento == item.numDocumento && x.categoriaPlanillaID == item.categoriaPlanillaID &&
                    x.conceptoCod == item.conceptoCod) > 1);

                if (dto.esDuplicadoEnArchivo)
                {
                    dto.observaciones.Add("El concepto se encuentra duplicado dentro del archivo para este trabajador.");
                }

                if (dto.anio.HasValue && dto.anio.Value >= 1963 && dto.anio.Value <= 2099)
                {
                    dto.esAnioCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("Año incorrecto.");
                }

                if (dto.mes.HasValue && dto.mes.Value >= 1 && dto.mes.Value <= 12)
                {
                    dto.esMesCorrecto = true;

                    dto.mesDesc = _periodoService.ObtenerMesDesc(dto.mes.Value);
                }
                else
                {
                    dto.observaciones.Add("Mes incorrecto.");

                    dto.mesDesc = "";
                }

                if (dto.esAnioCorrecto && dto.esMesCorrecto)
                {
                    periodoDTO = _periodoService.ObtenerPeriodo(dto.anio.Value, dto.mes.Value);

                    if (periodoDTO != null)
                    {
                        dto.periodoID = periodoDTO.periodoID;

                        dto.mesDesc = periodoDTO.mesDesc;

                        dto.esPeriodoCorrecto = true;
                    }
                    else
                    {
                        dto.observaciones.Add("El periodo no se encuentra registrado en el sistema.");
                    }
                }

                if (dto.tipoDocumentoID.HasValue && listaTipoDocumento.Exists(x => x.tipoDocumentoID == dto.tipoDocumentoID.Value))
                {
                    dto.tipoDocumentoDesc = listaTipoDocumento.First(x => x.tipoDocumentoID == dto.tipoDocumentoID.Value).tipoDocumentoDesc;

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
                    dto.observaciones.Add("Número de documento de identidad incorrecto");
                }
                
                if (dto.categoriaPlanillaID.HasValue && listaCategoriaPlanilla.Exists(x => x.categoriaPlanillaID == dto.categoriaPlanillaID.Value))
                {
                    dto.categoriaPlanillaDesc = listaCategoriaPlanilla.First(x => x.categoriaPlanillaID == dto.categoriaPlanillaID.Value).categoriaPlanillaDesc;

                    dto.esCategoriaPlanillaCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("Categoría de planilla incorrecto.");

                    dto.categoriaPlanillaDesc = "";
                }

                if (dto.esTipoDocumentoCorrecto && dto.esNumDocumentoCorrecto && dto.esCategoriaPlanillaCorrecto)
                {
                    personaDTO = _personaService.ObtenerPersona(dto.tipoDocumentoID.Value, dto.numDocumento);

                    if (personaDTO != null)
                    {
                        dto.datosPersona = String.Format("{0} {1} {2}", personaDTO.apellidoPaterno, personaDTO.apellidoMaterno, personaDTO.nombre);

                        trabajadorDTO = _trabajadorService.ObtenerTrabajadorPorDocumentoYCategoria(dto.tipoDocumentoID.Value, dto.numDocumento, dto.categoriaPlanillaID.Value);

                        if (trabajadorDTO != null)
                        {
                            dto.trabajadorCategoriaPlanillaID = trabajadorDTO.trabajadorCategoriaPlanillaID;

                            dto.trabajadorExiste = true;
                        }
                        else
                        {
                            dto.observaciones.Add("El trabajador no existe en la categoria de planilla indicada.");
                        }
                    }
                    else
                    {
                        dto.observaciones.Add("El trabajador no existe.");
                    }
                }

                if (dto.esPeriodoCorrecto && dto.trabajadorExiste)
                {
                    dto.tienePlanilla = _planillaService.ExistePlanillaTrabajador(trabajadorDTO.trabajadorID, periodoDTO.periodoID, trabajadorDTO.categoriaPlanillaID);

                    if (dto.tienePlanilla)
                    {
                        dto.observaciones.Add("El trabajador ya tiene una planilla generada para este periodo.");
                    }
                }

                if (dto.conceptoCod != null && dto.conceptoCod.Length > 0)
                {
                    conceptoDTO = _conceptoService.ObtenerConcepto(dto.conceptoCod);

                    if (conceptoDTO != null)
                    {
                        dto.conceptoID = conceptoDTO.conceptoID;

                        dto.conceptoDesc = conceptoDTO.conceptoDesc;

                        dto.tipoConceptoDesc = conceptoDTO.tipoConceptoDesc;

                        dto.conceptoExiste = true;

                        if (dto.trabajadorExiste && _valorExternoConceptoService.ObtenerPorTrabajadorCategoriaPlanillaYConcepto(dto.trabajadorCategoriaPlanillaID.Value, dto.conceptoID.Value) != null)
                        {
                            dto.esDuplicadoEnBD = true;

                            dto.observaciones.Add("El registro se encuentra duplicado.");
                        }
                        else
                        {
                            if (dto.esCategoriaPlanillaCorrecto)
                            {
                                var lista = _plantillaPlanillaConceptoService.ListarGrupoDeConceptosAsignados(dto.categoriaPlanillaID.Value, dto.conceptoID.Value);

                                if (lista != null && lista.Count > 0)
                                {
                                    dto.esValorFijo = lista.First().esValorFijo;
                                }
                                else
                                {
                                    dto.observaciones.Add("El concepto no se encuentra asignado a ninguna planilla.");
                                }
                            }

                            if (dto.valorConcepto.HasValue && dto.valorConcepto.Value > 0)
                            {
                                if (dto.esValorFijo.HasValue)
                                {
                                    if (dto.esValorFijo.Value)
                                    {
                                        dto.valorConceptoCorrecto = true;
                                    }
                                    else if (dto.valorConcepto.Value <= 100)
                                    {
                                        dto.valorConceptoCorrecto = true;
                                    }
                                    else
                                    {
                                        dto.observaciones.Add("Valor del concepto incorrecto (porcentaje superior al 100%).");
                                    }
                                }
                                else
                                {
                                    dto.observaciones.Add("No se reconoce si el concepto es un valor fijo o porcentual.");
                                }
                            }
                            else
                            {
                                dto.observaciones.Add("Valor del concepto incorrecto.");
                            }
                        }
                    }
                    else
                    {
                        dto.observaciones.Add("Código de concepto no existe.");
                    }
                }
                else
                {
                    dto.observaciones.Add("Código de concepto incorrecto.");
                }

                if (dto.proveedorID.HasValue && listaProveedores.Exists(x => x.proveedorID == dto.proveedorID.Value))
                {
                    dto.proveedorDesc = listaProveedores.First(x => x.proveedorID == dto.proveedorID.Value).proveedorDesc;

                    dto.esProveedorCorrecto = true;
                }
                else
                {
                    dto.observaciones.Add("Proveedor de información incorrecto.");

                    dto.proveedorDesc = "";
                }

                lecturaProcesada.Add(dto);
            }

            return lecturaProcesada;
        }
    }
}