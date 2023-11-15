using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities;
using Domain.Helpers;
using Domain.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class GeneracionArchivoExcelService : IGeneracionArchivoService
    {
        public FileContent GenerarExcelDeLecturaValoresDeConceptos(List<ValorExternoLecturaProcesadoDTO> lista)
        {
            FileContent fileContent;
            int currentRow;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Resultado");

                worksheet.Columns("B:D").Width = 15;
                worksheet.Columns("E:F").Width = 30;
                worksheet.Column("G").Width = 15;
                worksheet.Column("H").Width = 30;
                worksheet.Columns("I:J").Width = 15;
                worksheet.Columns("K:L").Width = 30;

                currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Año";
                worksheet.Cell(currentRow, 2).Value = "Mes";
                worksheet.Cell(currentRow, 3).Value = "Tip.Doc.";
                worksheet.Cell(currentRow, 4).Value = "Num.Doc.";
                worksheet.Cell(currentRow, 5).Value = "Apellidos y Nombres";
                worksheet.Cell(currentRow, 6).Value = "Planilla";
                worksheet.Cell(currentRow, 7).Value = "Cod.Concepto";
                worksheet.Cell(currentRow, 8).Value = "Descripción";
                worksheet.Cell(currentRow, 9).Value = "Valor Concepto";
                worksheet.Cell(currentRow, 10).Value = "Tipo Concepto";
                worksheet.Cell(currentRow, 11).Value = "Origen";
                worksheet.Cell(currentRow, 12).Value = "Observación";

                foreach (var item in lista)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).SetValue<string>(item.anio.ToString());
                    worksheet.Cell(currentRow, 2).SetValue<string>(String.Format("{0} ({1})", item.mesDesc, item.mes));
                    worksheet.Cell(currentRow, 3).SetValue<string>(String.Format("{0} ({1})", item.tipoDocumentoDesc, item.tipoDocumentoID));
                    worksheet.Cell(currentRow, 4).SetValue<string>(item.numDocumento);
                    worksheet.Cell(currentRow, 5).SetValue<string>(item.datosPersona);
                    worksheet.Cell(currentRow, 6).SetValue<string>(String.Format("{0} ({1})", item.categoriaPlanillaDesc, item.categoriaPlanillaID));
                    worksheet.Cell(currentRow, 7).SetValue<string>(item.conceptoCod);
                    worksheet.Cell(currentRow, 8).SetValue<string>(item.conceptoDesc);
                    worksheet.Cell(currentRow, 9).SetValue<decimal?>(item.valorConcepto);
                    worksheet.Cell(currentRow, 10).SetValue<string>(item.simboloValorConcepto);
                    worksheet.Cell(currentRow, 11).SetValue<string>(String.Format("{0} ({1})", item.proveedorDesc, item.proveedorID));
                    
                    if (!item.esRegistroCorrecto)
                    {
                        var observacionesText = string.Join("\n", item.observaciones);

                        worksheet.Cell(currentRow, 11).SetValue<string>(observacionesText);
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    
                    var content = stream.ToArray();

                    fileContent = new FileContent()
                    {
                        fileContent = content,
                        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName = "Resultado de la lectura de archivo.xlsx"
                    };

                    return fileContent;
                }
            }
        }

        public FileContent GenerarExcelResumenPorActividadYDependencia(ReporteResumenPorActividadYDependencia reporte)
        {
            FileContent fileContent;
            int currentRow = 1;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hoja1");

                foreach (var item in reporte.listaResumenPorActividad)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).SetValue<string>("Actividad");
                    worksheet.Cell(currentRow, 2).SetValue<string>(item.actividadCod);
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    var content = stream.ToArray();

                    fileContent = new FileContent()
                    {
                        fileContent = content,
                        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName = "Reporte Resumen por Dependencia.xlsx"
                    };

                    return fileContent;
                }
            }
        }
    }
}
