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

        private readonly string serverPath;

        public ValorExternoConceptoServiceFacade()
        {
            _periodoService = new PeriodoService();
            _trabajadorService = new TrabajadorService();
            _conceptoService = new ConceptoService();
            _proveedorService = new ProveedorService();
            _valorExternoConceptoService = new ValorExternoConceptoService();
            _planillaService = new PlanillaService();

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

                lecturaProcesada = new List<ValorExternoLecturaProcesadoDTO>();

                foreach (var item in lectura)
                {
                    lecturaProcesada.Add(LecturaProcesada(item));
                }
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

                lecturaProcesada = new List<ValorExternoLecturaProcesadoDTO>();

                foreach (var item in lectura)
                {
                    lecturaProcesada.Add(LecturaProcesada(item));
                }

                registrosAptos = lecturaProcesada
                   .Where(x => x.periodoCorrecto && x.trabajadorExiste && !x.tienePlanilla)
                   .Select(x => new ValorConceptoEntity()
                   {
                       periodoID = x.periodoID.Value,
                       trabajadorID = x.trabajadorID.Value,
                       categoriaPlanillaID = x.categoriaPlanillaID.Value,
                       conceptoID = x.conceptoID.Value,
                       valorConcepto = x.valorConcepto.Value,
                       proveedorID = x.proveedorID.Value
                   }).ToList();

                response = _valorExternoConceptoService.GrabarValoresExternos(registrosAptos, userID);
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

        private ValorExternoLecturaProcesadoDTO LecturaProcesada(ValorExternoLecturaDTO lectura)
        {
            PeriodoDTO periodoDTO = null;
            TrabajadorCategoriaPlanillaDTO trabajadorDTO = null;
            ConceptoDTO conceptoDTO = null;
            ProveedorDTO proveedorDTO = null;

            var model = new ValorExternoLecturaProcesadoDTO()
            {
                anio = lectura.anio,
                mes = lectura.mes,
                tipoDocumentoID = lectura.tipoDocumentoID,
                numDocumento = lectura.numDocumento,
                categoriaPlanillaID = lectura.categoriaPlanillaID,
                conceptoCod = lectura.conceptoCod,
                valorConcepto = lectura.valorConcepto,
                proveedorID = lectura.proveedorID
            };

            if (model.anio.HasValue && model.mes.HasValue)
            {
                periodoDTO = _periodoService.ObtenerPeriodo(model.anio.Value, model.mes.Value);

                if (periodoDTO != null)
                {
                    model.periodoID = periodoDTO.periodoID;

                    model.mesDesc = periodoDTO.mesDesc;

                    model.periodoCorrecto = true;
                }
            }

            if (model.tipoDocumentoID.HasValue && model.numDocumento != null && model.numDocumento.Length > 0 && model.categoriaPlanillaID.HasValue)
            {
                trabajadorDTO = _trabajadorService.ObtenerTrabajadorPorDocumentoYCategoria(model.tipoDocumentoID.Value, model.numDocumento, model.categoriaPlanillaID.Value);

                if (trabajadorDTO != null)
                {
                    model.trabajadorID = trabajadorDTO.trabajadorID;

                    model.tipoDocumentoDesc = trabajadorDTO.tipoDocumentoDesc;

                    model.datosPersona = String.Format("{0} {1} {2}", trabajadorDTO.apellidoPaterno, trabajadorDTO.apellidoMaterno, trabajadorDTO.nombre);

                    model.categoriaPlanillaDesc = trabajadorDTO.categoriaPlanillaDesc;

                    model.trabajadorExiste = true;
                }
            }

            if (model.periodoCorrecto && model.trabajadorExiste)
            {
                model.tienePlanilla = _planillaService.ExistePlanillaTrabajador(trabajadorDTO.trabajadorID, periodoDTO.periodoID, trabajadorDTO.categoriaPlanillaID);
            }

            if (model.conceptoCod != null && model.conceptoCod.Length > 0)
            {
                conceptoDTO = _conceptoService.ObtenerConcepto(model.conceptoCod);

                if (conceptoDTO != null)
                {
                    model.conceptoID = conceptoDTO.conceptoID;

                    model.conceptoDesc = conceptoDTO.conceptoDesc;

                    model.tipoConceptoDesc = conceptoDTO.tipoConceptoDesc;

                    model.conceptoExiste = true;
                }
            }

            if (model.proveedorID.HasValue)
            {
                proveedorDTO = _proveedorService.ObtenerProveedor(model.proveedorID.Value);

                if (proveedorDTO != null)
                {
                    model.proveedorDesc = proveedorDTO.proveedorDesc;

                    model.proveedorExiste = true;
                }
            }

            return model;
        }
    }
}