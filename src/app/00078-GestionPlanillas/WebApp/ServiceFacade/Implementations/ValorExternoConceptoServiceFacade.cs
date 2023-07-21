using Domain.Entities;
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

            serverPath = WebConfigParams.DirectorioArchivosExternos;
        }

        public Tuple<string, List<ValorExternoConceptoModel>> ObtenerListaValoresDeConceptos(HttpPostedFileBase file)
        {
            string newFileName;
            ILecturaArchivoService lecturaArchivoService;
            List<ValorExternoConceptoDTO> lista;
            List<ValorExternoConceptoModel> result;

            try
            {
                newFileName = GuardarArchivo(serverPath, file);

                lecturaArchivoService = GetService(Path.GetExtension(newFileName));

                lista = lecturaArchivoService.ObtenerListaValoresDeConceptos(Path.Combine(serverPath, newFileName));

                var listaTipDocumentos = _tipoDocumentoService.ListaTipoDocumentos();

                var listaConceptos = _conceptoService.ListarConceptos();

                var listaProveedores = _proveedorService.ListarProveedores();

                result = lista.Select(x => new ValorExternoConceptoModel()
                {
                    anio = x.anio,
                    mes = x.mes,
                    mesDesc = _periodoService.ListarMeses(x.anio.HasValue ? x.anio.Value : 0).Where(y => y.mes == x.mes).FirstOrDefault().mesDesc,
                    numDocumento = x.numDocumento,
                    tipoDocumentoID = x.tipoDocumentoID,
                    tipoDocumentoDesc = listaTipDocumentos.Where(y => y.tipoDocumentoID == x.tipoDocumentoID).FirstOrDefault().tipoDocumentoDesc,
                    datosPersona = _personaService.ObtenerPersona((x.tipoDocumentoID.HasValue ? x.tipoDocumentoID.Value : 0), x.numDocumento).nombre,
                    conceptoCod = x.conceptoCod,
                    conceptoDesc = listaConceptos.Where(y => y.conceptoCod == x.conceptoCod).FirstOrDefault().conceptoDesc,
                    valorConcepto = x.valorConcepto,
                    proveedorID = x.proveedorID,
                    proveedorDesc = listaProveedores.Where(y => y.proveedorID == x.proveedorID).FirstOrDefault().proveedorDesc
                }).ToList();
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
            List<ValorExternoConceptoDTO> lista;

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