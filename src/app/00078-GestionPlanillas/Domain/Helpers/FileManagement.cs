using Domain.Services.Implementations;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Domain.Enums;

namespace Domain.Helpers
{
    public static class FileManagement
    {
        public static string GuardarArchivo(string serverPath, HttpPostedFileBase file)
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

        public static ILecturaArchivoService GetLecturaService(string extension)
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

        public static IGeneracionArchivoService GetGeneracionArchivoService(FormatoArchivo formatoArchivo)
        {
            IGeneracionArchivoService generacionArchivoService;

            switch (formatoArchivo)
            {
                case FormatoArchivo.XLSX:

                    generacionArchivoService = new GeneracionArchivoExcelService();

                    break;

                default:
                    throw new NotImplementedException("Tipo de archivo no implementado.");
            }

            return generacionArchivoService;
        }
    }
}
