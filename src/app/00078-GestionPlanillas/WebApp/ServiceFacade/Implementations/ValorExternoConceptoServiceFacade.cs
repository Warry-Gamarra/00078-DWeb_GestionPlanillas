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
        private ITipoDocumentoService _tipoDocumentoService;
        private IPersonaService _personaService;
        private ITrabajadorService _trabajadorService;
        private IConceptoService _conceptoService;
        private IProveedorService _proveedorService;
        private IValorExternoConceptoService _valorExternoConceptoService;
        private ICategoriaPlanillaService _categoriaPlanillaService;

        private readonly string serverPath;

        public ValorExternoConceptoServiceFacade()
        {
            _periodoService = new PeriodoService();
            _tipoDocumentoService = new TipoDocumentoService();
            _personaService = new PersonaService();
            _trabajadorService = new TrabajadorService();
            _conceptoService = new ConceptoService();
            _proveedorService = new ProveedorService();
            _valorExternoConceptoService = new ValorExternoConceptoService();
            _categoriaPlanillaService = new CategoriaPlanillaService();

            serverPath = WebConfigParams.DirectorioArchivosExternos;
        }

        public Tuple<string, List<ValorExternoConceptoModel>> ObtenerListaValoresDeConceptos(HttpPostedFileBase file)
        {
            string newFileName;
            ILecturaArchivoService lecturaArchivoService;
            List<ValorExternoLecturaDTO> lista;
            List<ValorExternoConceptoModel> result;
            ValorExternoConceptoModel model;

            try
            {
                newFileName = GuardarArchivo(serverPath, file);

                lecturaArchivoService = GetService(Path.GetExtension(newFileName));

                lista = lecturaArchivoService.ObtenerListaValoresDeConceptos(Path.Combine(serverPath, newFileName));

                var listaTipDocumentos = _tipoDocumentoService.ListaTipoDocumentos();

                var listaConceptos = _conceptoService.ListarConceptos();

                var listaProveedores = _proveedorService.ListarProveedores();

                var listaCategoria = _categoriaPlanillaService.ListarCategoriasPlanillas();

                result = new List<ValorExternoConceptoModel>();

                foreach (var item in lista)
                {
                    model = new ValorExternoConceptoModel();

                    model.anio = item.anio;

                    model.mes = item.mes;

                    model.mesDesc = _periodoService.ListarMeses(item.anio.HasValue ? item.anio.Value : 0).Where(y => y.mes == item.mes).FirstOrDefault().mesDesc;

                    model.numDocumento = item.numDocumento;

                    model.tipoDocumentoID = item.tipoDocumentoID;

                    model.tipoDocumentoDesc = listaTipDocumentos.Where(y => y.tipoDocumentoID == item.tipoDocumentoID).FirstOrDefault().tipoDocumentoDesc;

                    var persona = _personaService.ObtenerPersona((item.tipoDocumentoID.HasValue ? item.tipoDocumentoID.Value : 0), item.numDocumento);

                    model.datosPersona = String.Format("{0} {1} {2}", persona.apellidoPaterno, persona.apellidoMaterno, persona.nombre);

                    model.categoriaPlanillaID = item.categoriaPlanillaID;

                    model.categoriaPlanillaDesc = listaCategoria.Where(x => x.categoriaPlanillaID == item.categoriaPlanillaID).FirstOrDefault().categoriaPlanillaDesc;

                    model.conceptoCod = item.conceptoCod;

                    model.conceptoDesc = listaConceptos.Where(y => y.conceptoCod == item.conceptoCod).FirstOrDefault().conceptoDesc;

                    model.valorConcepto = item.valorConcepto;

                    model.proveedorID = item.proveedorID;

                    model.proveedorDesc = listaProveedores.Where(y => y.proveedorID == item.proveedorID).FirstOrDefault().proveedorDesc;

                    result.Add(model);
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

            return new Tuple<string, List<ValorExternoConceptoModel>>(newFileName, result);
        }

        public Response GrabarValoresExternos(string fileName, int userID)
        {
            Response response;
            ILecturaArchivoService lecturaArchivoService;
            List<ValorExternoLecturaDTO> lista;

            try
            {
                var listaTipDocumentos = _tipoDocumentoService.ListaTipoDocumentos();

                var listaConceptos = _conceptoService.ListarConceptos();

                var listaProveedores = _proveedorService.ListarProveedores();

                lecturaArchivoService = GetService(Path.GetExtension(fileName));

                lista = lecturaArchivoService.ObtenerListaValoresDeConceptos(Path.Combine(serverPath, fileName));

                var registros = lista
                    .Where(x => x.tipoDocumentoID.HasValue && x.anio.HasValue && x.mes.HasValue && x.valorConcepto.HasValue && x.proveedorID.HasValue)
                    .Select(x => new ValorConceptoEntity() {
                        trabajadorID = _trabajadorService.ObtenerTrabajadorPorDocIdentidad(x.tipoDocumentoID.Value, x.numDocumento).First().trabajadorID,
                        periodoID = _periodoService.ObtenerPeriodo(x.anio.Value, x.mes.Value).periodoID,
                        conceptoID = listaConceptos.Where(y => y.conceptoCod == x.conceptoCod).FirstOrDefault().conceptoID,
                        valorConcepto = x.valorConcepto.Value,
                        proveedorID = x.proveedorID.Value
                    })
                    .ToList();

                response = _valorExternoConceptoService.GrabarValoresExternos(registros, userID);
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
            var lista = _valorExternoConceptoService.ListarValoresExternosConceptos(anio, mes, categoriaPlanillaID)
                .Select(x => Mapper.ValorExternoConceptoDTO_To_ValorExternoConceptoModel(x))
                .ToList();

            return lista;
        }

        public ValorExternoConceptoModel ObtenerValorExterno(int conceptoExternoValorID)
        {
            ValorExternoConceptoModel valorExternoConceptoModel;

            var valorExternoConceptoDTO = _valorExternoConceptoService.ObtenerValorExternoConcepto(conceptoExternoValorID);

            if (valorExternoConceptoDTO == null)
            {
                valorExternoConceptoModel = null;
            }
            else
            {
                valorExternoConceptoModel = Mapper.ValorExternoConceptoDTO_To_ValorExternoConceptoModel(valorExternoConceptoDTO);
            }

            return valorExternoConceptoModel;
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
            var result = _valorExternoConceptoService.Eliminar(conceptoExternoValorID, userID);

            return result;
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
    }
}