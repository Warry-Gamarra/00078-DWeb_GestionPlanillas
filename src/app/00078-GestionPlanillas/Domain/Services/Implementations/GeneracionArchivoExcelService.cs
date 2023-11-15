using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities;
using Domain.Helpers;
using Domain.Reports;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
            int currentRow;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hoja1");

                worksheet.Column("C").Width = 60;

                var image = worksheet.AddPicture(HttpContext.Current.Server.MapPath("~\\Assets\\images\\logo.png"));

                image.MoveTo(worksheet.Cell(1, 1), worksheet.Cell(2, 2));

                worksheet.Cell(1, 9).SetValue<string>(reporte.fechaConsulta);
                worksheet.Cell(2, 9).SetValue<string>(reporte.horaConsulta);
                worksheet.Cell(4, 1).SetValue<string>(reporte.oficina);
                worksheet.Cell(5, 1).SetValue<string>(reporte.subOficina);

                var titleCell = worksheet.Cell(6, 1);

                titleCell.Value = reporte.titulo;

                worksheet.Range(titleCell, worksheet.Cell(6, 9)).Merge(true);

                currentRow = 8;

                foreach (var actividad in reporte.listaResumenPorActividad)
                {
                    worksheet.Cell(currentRow, 1).SetValue<string>("Actividad");
                    worksheet.Cell(currentRow, 2).SetValue<string>(actividad.actividadCod);
                    worksheet.Cell(currentRow, 3).SetValue<string>("Dependencia");
                    worksheet.Cell(currentRow, 4).SetValue<string>("Remuneración");
                    worksheet.Cell(currentRow, 5).SetValue<string>("Reintegro");
                    worksheet.Cell(currentRow, 6).SetValue<string>("Deducción");
                    worksheet.Cell(currentRow, 7).SetValue<string>("Bruto");
                    worksheet.Cell(currentRow, 8).SetValue<string>("Total Descuento");
                    worksheet.Cell(currentRow, 9).SetValue<string>("Neto a Pagar");

                    currentRow++;

                    foreach (var dependencia in actividad.listaDependencias)
                    {
                        worksheet.Cell(currentRow, 1).SetValue<string>("");
                        worksheet.Cell(currentRow, 2).SetValue<string>("");
                        worksheet.Cell(currentRow, 3).SetValue<string>(dependencia.dependenciaDesc);
                        worksheet.Cell(currentRow, 4).SetValue<decimal>(dependencia.totalRemuneracion);
                        worksheet.Cell(currentRow, 5).SetValue<decimal>(dependencia.totalReintegro);
                        worksheet.Cell(currentRow, 6).SetValue<decimal>(dependencia.totalDeduccion);
                        worksheet.Cell(currentRow, 7).SetValue<decimal>(dependencia.totalBruto);
                        worksheet.Cell(currentRow, 8).SetValue<decimal>(dependencia.totalDescuento);
                        worksheet.Cell(currentRow, 9).SetValue<decimal>(dependencia.totalSueldo);

                        currentRow++;
                    }

                    worksheet.Cell(currentRow, 1).SetValue<string>("Total por Actividad");
                    worksheet.Cell(currentRow, 2).SetValue<string>("");
                    worksheet.Cell(currentRow, 3).SetValue<string>("");
                    worksheet.Cell(currentRow, 4).SetValue<decimal>(actividad.totalRemuneracion);
                    worksheet.Cell(currentRow, 5).SetValue<decimal>(actividad.totalReintegro);
                    worksheet.Cell(currentRow, 6).SetValue<decimal>(actividad.totalDeduccion);
                    worksheet.Cell(currentRow, 7).SetValue<decimal>(actividad.totalBruto);
                    worksheet.Cell(currentRow, 8).SetValue<decimal>(actividad.totalDescuento);
                    worksheet.Cell(currentRow, 9).SetValue<decimal>(actividad.totalSueldo);

                    currentRow++;
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
