using Domain.Entities;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class LecturaArchivo : ILecturaArchivo
    {
        private IPeriodoService _periodoService;
        private ITipoDocumentoService _tipoDocumentoService;
        private IPersonaService _personaService;
        private IConceptoService _conceptoService;
        private IProveedorService _proveedorService;

        public LecturaArchivo()
        {
            _periodoService = new PeriodoService();
            _tipoDocumentoService = new TipoDocumentoService();
            _personaService = new PersonaService();
            _conceptoService = new ConceptoService();
            _proveedorService = new ProveedorService();
        }

        public Tuple<string, List<ValorExternoConceptoModel>> ObtenerListaValoresDeConceptos(HttpPostedFileBase file)
        {
            string serverPath;
            string newFileName;
            ILecturaArchivoService lecturaArchivoService;
            List<ValorExternoConceptoDTO> lista;
            List<ValorExternoConceptoModel> result;

            try
            {
                serverPath = WebConfigParams.DirectorioArchivosExternos;

                newFileName = GuardarArchivo(serverPath, file);

                lecturaArchivoService = GetService(Path.GetExtension(Path.Combine(serverPath, newFileName)));

                lista = lecturaArchivoService.ObtenerListaValoresDeConceptos(Path.Combine(serverPath, newFileName));

                var listaTipDocumentos = _tipoDocumentoService.ListaTipoDocumentos();

                var listaConceptos = _conceptoService.ListarConceptos();

                var listaProveedores = _proveedorService.ListarProveedores();

                result = lista.Select(x => new ValorExternoConceptoModel() {
                    anio = x.anio,
                    mes = x.mes,
                    mesDesc = _periodoService.ListarMeses(x.anio.HasValue ? x.anio.Value : 0).Where(y => y.I_Mes == x.mes).FirstOrDefault().T_MesDesc,
                    numDocumento = x.numDocumento,
                    tipoDocumentoID = x.tipoDocumentoID,
                    tipoDocumentoDesc = listaTipDocumentos.Where(y => y.I_TipoDocumentoID == x.tipoDocumentoID).FirstOrDefault().T_TipoDocumentoDesc,
                    datosPersona = _personaService.ObtenerPersona((x.tipoDocumentoID.HasValue ? x.tipoDocumentoID.Value : 0), x.numDocumento).nombre,
                    conceptoCod = x.conceptoCod,
                    conceptoDesc = listaConceptos.Where(y => y.conceptoCod == y.conceptoCod).FirstOrDefault().conceptoDesc,
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

            switch(extension) 
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