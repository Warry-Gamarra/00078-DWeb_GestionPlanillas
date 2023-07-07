using Domain.Entities;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.ServiceFacade.Implementations
{
    public class LecturaArchivo : ILecturaArchivo
    {
        public List<ValorExternoConceptoDTO> ObtenerListaValoresDeConceptos(HttpPostedFileBase file)
        {
            string serverPath;
            string filePath;
            ILecturaArchivoService lecturaArchivoService;
            List<ValorExternoConceptoDTO> lista;

            try
            {
                serverPath = ConfigurationManager.AppSettings["DirectorioArchivosExternos"];

                filePath = GuardarArchivo(serverPath, file);

                lecturaArchivoService = Get(Path.GetExtension(filePath));

                lista = lecturaArchivoService.ObtenerListaValoresDeConceptos(filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lista;
        }

        private string GuardarArchivo(string serverPath, HttpPostedFileBase file)
        {
            if (!Directory.Exists(serverPath))
            {
                throw new DirectoryNotFoundException("No existe el directorio para almacenar el archivo.");
            }

            string fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);

            string filePath = Path.Combine(serverPath, fileName);

            file.SaveAs(filePath);

            return filePath;
        }

        private ILecturaArchivoService Get(string extension)
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