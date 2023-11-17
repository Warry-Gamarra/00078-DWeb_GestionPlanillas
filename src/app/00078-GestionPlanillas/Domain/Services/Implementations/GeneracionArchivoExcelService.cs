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
using Formats = Domain.Helpers.Formats;

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
                worksheet.PageSetup.PaperSize = XLPaperSize.A4Paper;
                worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                worksheet.PageSetup.Margins.Left = 0.25;
                worksheet.PageSetup.Margins.Right = 0.25;
                worksheet.PageSetup.Margins.Top = 0.25;
                worksheet.PageSetup.Margins.Bottom = 0.25;
                worksheet.PageSetup.Scale = 85;

                worksheet.Column("C").Width = 60;
                worksheet.Columns("D:I").Width = 14;

                var image = worksheet.AddPicture(HttpContext.Current.Server.MapPath("~\\Assets\\images\\logo.png"));
                image.MoveTo(worksheet.Cell(1, 1), worksheet.Cell(3, 3));

                var celdaFecha = worksheet.Cell(1, 9).SetValue<string>(reporte.fechaConsulta);
                celdaFecha.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                var celdaHora = worksheet.Cell(2, 9).SetValue<string>(reporte.horaConsulta);
                celdaHora.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                worksheet.Cell(4, 1).SetValue<string>(reporte.oficina);
                worksheet.Cell(5, 1).SetValue<string>(reporte.subOficina);

                var celdaTitulo = worksheet.Cell(6, 1).SetValue<string>(reporte.titulo);
                celdaTitulo.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                celdaTitulo.Style.Font.Bold = true;
                
                worksheet.Range(celdaTitulo, worksheet.Cell(6, 9)).Merge(true);

                currentRow = 8;

                foreach (var actividad in reporte.listaResumenPorActividad)
                {
                    var celdaActividad = worksheet.Cell(currentRow, 1).SetValue<string>("Actividad");
                    celdaActividad.Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 2).SetValue<string>(actividad.actividadCod);

                    var celdaDependencia = worksheet.Cell(currentRow, 3).SetValue<string>("Dependencia");
                    celdaDependencia.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    celdaDependencia.Style.Font.Bold = true;

                    var celdaRemuneracion = worksheet.Cell(currentRow, 4).SetValue<string>("Remuneración");
                    celdaRemuneracion.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    celdaRemuneracion.Style.Font.Bold = true;
                    celdaRemuneracion.Style.Border.TopBorder = XLBorderStyleValues.Dashed;
                    celdaRemuneracion.Style.Border.BottomBorder = XLBorderStyleValues.Dashed;

                    var celdaReintegro = worksheet.Cell(currentRow, 5).SetValue<string>("Reintegro");
                    celdaReintegro.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    celdaReintegro.Style.Font.Bold = true;
                    celdaReintegro.Style.Border.TopBorder = XLBorderStyleValues.Dashed;
                    celdaReintegro.Style.Border.BottomBorder = XLBorderStyleValues.Dashed;
                    
                    var celdaDeduccion = worksheet.Cell(currentRow, 6).SetValue<string>("Deducción");
                    celdaDeduccion.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    celdaDeduccion.Style.Font.Bold = true;
                    celdaDeduccion.Style.Border.TopBorder = XLBorderStyleValues.Dashed;
                    celdaDeduccion.Style.Border.BottomBorder = XLBorderStyleValues.Dashed;

                    var celdaBruto = worksheet.Cell(currentRow, 7).SetValue<string>("Bruto");
                    celdaBruto.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    celdaBruto.Style.Font.Bold = true;
                    celdaBruto.Style.Border.TopBorder = XLBorderStyleValues.Dashed;
                    celdaBruto.Style.Border.BottomBorder = XLBorderStyleValues.Dashed;
                    
                    var celdaDescuento = worksheet.Cell(currentRow, 8).SetValue<string>("Total Descuento");
                    celdaDescuento.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    celdaDescuento.Style.Font.Bold = true;
                    celdaDescuento.Style.Border.TopBorder = XLBorderStyleValues.Dashed;
                    celdaDescuento.Style.Border.BottomBorder = XLBorderStyleValues.Dashed;

                    var celdaSueldo = worksheet.Cell(currentRow, 9).SetValue<string>("Neto a Pagar");
                    celdaSueldo.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    celdaSueldo.Style.Font.Bold = true;
                    celdaSueldo.Style.Border.TopBorder = XLBorderStyleValues.Dashed;
                    celdaSueldo.Style.Border.BottomBorder = XLBorderStyleValues.Dashed;

                    currentRow++;

                    foreach (var dependencia in actividad.listaDependencias)
                    {
                        worksheet.Cell(currentRow, 1).SetValue<string>("");
                        worksheet.Cell(currentRow, 2).SetValue<string>("");
                        worksheet.Cell(currentRow, 3).SetValue<string>(dependencia.dependenciaDesc);

                        celdaRemuneracion = worksheet.Cell(currentRow, 4).SetValue<decimal>(dependencia.totalRemuneracion);
                        celdaRemuneracion.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                        celdaRemuneracion.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                        celdaReintegro = worksheet.Cell(currentRow, 5).SetValue<decimal>(dependencia.totalReintegro);
                        celdaReintegro.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;

                        celdaDeduccion = worksheet.Cell(currentRow, 6).SetValue<decimal>(dependencia.totalDeduccion);
                        celdaDeduccion.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                        celdaDeduccion.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                        celdaBruto = worksheet.Cell(currentRow, 7).SetValue<decimal>(dependencia.totalBruto);
                        celdaBruto.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                        celdaBruto.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                        celdaDescuento = worksheet.Cell(currentRow, 8).SetValue<decimal>(dependencia.totalDescuento);
                        celdaDescuento.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                        celdaDescuento.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                        celdaSueldo = worksheet.Cell(currentRow, 9).SetValue<decimal>(dependencia.totalSueldo);
                        celdaSueldo.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;

                        currentRow++;
                    }

                    celdaActividad = worksheet.Cell(currentRow, 1).SetValue<string>("Total por Actividad");
                    celdaActividad.Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 2).SetValue<string>("");
                    worksheet.Cell(currentRow, 3).SetValue<string>("");

                    celdaRemuneracion = worksheet.Cell(currentRow, 4).SetValue<decimal>(actividad.totalRemuneracion);
                    celdaRemuneracion.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                    celdaRemuneracion.Style.Font.Bold = true;
                    celdaRemuneracion.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    celdaReintegro = worksheet.Cell(currentRow, 5).SetValue<decimal>(actividad.totalReintegro);
                    celdaReintegro.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                    celdaReintegro.Style.Font.Bold = true;

                    celdaDeduccion = worksheet.Cell(currentRow, 6).SetValue<decimal>(actividad.totalDeduccion);
                    celdaDeduccion.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                    celdaDeduccion.Style.Font.Bold = true;
                    celdaDeduccion.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    celdaBruto = worksheet.Cell(currentRow, 7).SetValue<decimal>(actividad.totalBruto);
                    celdaBruto.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                    celdaBruto.Style.Font.Bold = true;
                    celdaBruto.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    celdaDescuento = worksheet.Cell(currentRow, 8).SetValue<decimal>(actividad.totalDescuento);
                    celdaDescuento.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                    celdaDescuento.Style.Font.Bold = true;
                    celdaDescuento.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    celdaSueldo = worksheet.Cell(currentRow, 9).SetValue<decimal>(actividad.totalSueldo);
                    celdaSueldo.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                    celdaSueldo.Style.Font.Bold = true;

                    currentRow++;
                }

                worksheet.Cell(currentRow, 1).SetValue<string>("");
                worksheet.Cell(currentRow, 2).SetValue<string>("");
                worksheet.Cell(currentRow, 3).SetValue<string>("");
                
                var celdaTotalRemuneracion = worksheet.Cell(currentRow, 4).SetValue<decimal>(reporte.totalRemuneracion);
                celdaTotalRemuneracion.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                celdaTotalRemuneracion.Style.Font.Bold = true;
                celdaTotalRemuneracion.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                celdaTotalRemuneracion.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                celdaTotalRemuneracion.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                var celdaTotalReintegro = worksheet.Cell(currentRow, 5).SetValue<decimal>(reporte.totalReintegro);
                celdaTotalReintegro.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                celdaTotalReintegro.Style.Font.Bold = true;
                celdaTotalReintegro.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                celdaTotalReintegro.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                var celdaTotalDeduccion = worksheet.Cell(currentRow, 6).SetValue<decimal>(reporte.totalDeduccion);
                celdaTotalDeduccion.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                celdaTotalDeduccion.Style.Font.Bold = true;
                celdaTotalDeduccion.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                celdaTotalDeduccion.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                celdaTotalDeduccion.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                var celdaTotalBruto = worksheet.Cell(currentRow, 7).SetValue<decimal>(reporte.totalBruto);
                celdaTotalBruto.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                celdaTotalBruto.Style.Font.Bold = true;
                celdaTotalBruto.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                celdaTotalBruto.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                celdaTotalBruto.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                var celdaTotalDescuento = worksheet.Cell(currentRow, 8).SetValue<decimal>(reporte.totalDescuento);
                celdaTotalDescuento.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                celdaTotalDescuento.Style.Font.Bold = true;
                celdaTotalDescuento.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                celdaTotalDescuento.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                celdaTotalDescuento.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                var celdaTotalSueldo = worksheet.Cell(currentRow, 9).SetValue<decimal>(reporte.totalSueldo);
                celdaTotalSueldo.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                celdaTotalSueldo.Style.Font.Bold = true;
                celdaTotalSueldo.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                celdaTotalSueldo.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

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

        public FileContent GenerarExcelResumenSIAF(ReporteResumenSIAF reporte)
        {
            FileContent fileContent;
            IXLCell cell;
            int currentRow;
            int currentCol;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hoja1");
                
                var celdaTitulo = worksheet.Cell(1, 1).SetValue<string>(reporte.titulo);
                celdaTitulo.Style.Font.Bold = true;

                var celdaTituloAdm = worksheet.Cell(2, 1).SetValue<string>(reporte.tituloResumenAdministrativo);
                celdaTituloAdm.Style.Font.Bold = true;

                currentCol = 1;
                foreach (var columnName in reporte.resumenAdministrativo.cabecera)
                {
                    cell = worksheet.Cell(3, currentCol).SetValue<string>(columnName);
                    cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    currentCol++;
                }

                currentRow = 4;
                currentCol = 1;
                foreach (var item in reporte.resumenAdministrativo.detalle)
                {
                    foreach(var columnName in reporte.resumenAdministrativo.cabecera)
                    {
                        if (columnName == "Actividad" || columnName == "Meta")
                        {
                            cell = worksheet.Cell(currentRow, currentCol).SetValue<string>((string)item[columnName]);
                        }
                        else
                        {
                            cell = worksheet.Cell(currentRow, currentCol).SetValue<decimal>((decimal)item[columnName]);
                            cell.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                        }

                        cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        currentCol++;
                    }
                    currentRow++;
                    currentCol = 1;
                }

                worksheet.Cell(currentRow, currentCol).SetValue<string>(reporte.pieTablaAdministrativo);

                currentRow += 2;
                var celdaTituloDoc = worksheet.Cell(currentRow, 1).SetValue<string>(reporte.tituloResumenDocente);
                celdaTituloDoc.Style.Font.Bold = true;

                currentRow++;
                currentCol = 1;
                foreach (var columnName in reporte.resumenDocente.cabecera)
                {
                    cell = worksheet.Cell(currentRow, currentCol).SetValue<string>(columnName);
                    cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    currentCol++;
                }

                currentRow++;
                currentCol = 1;
                foreach (var item in reporte.resumenDocente.detalle)
                {
                    foreach (var columnName in reporte.resumenDocente.cabecera)
                    {
                        if (columnName == "Actividad" || columnName == "Meta")
                        {
                            cell = worksheet.Cell(currentRow, currentCol).SetValue<string>((string)item[columnName]);
                        }
                        else
                        {
                            cell = worksheet.Cell(currentRow, currentCol).SetValue<decimal>((decimal)item[columnName]);
                            cell.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                        }
    
                        cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        currentCol++;
                    }
                    currentRow++;
                    currentCol = 1;
                }

                worksheet.Cell(currentRow, currentCol).SetValue<string>(reporte.pieTablaDocente);

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    var content = stream.ToArray();

                    fileContent = new FileContent()
                    {
                        fileContent = content,
                        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName = "Resumen SIAF.xlsx"
                    };

                    return fileContent;
                }
            }
        }
    }
}
