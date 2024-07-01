using ClosedXML.Excel;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Domain.Services.Implementations
{
    public class GeneracionArchivoExcelService : IGeneracionArchivoService
    {
        public FileContent GenerarDescargableListaTrabajadores(List<TrabajadorDTO> data)
        {
            FileContent fileContent;
            int currentRow;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hoja1");

                worksheet.Columns("A:D").Width = 15;
                worksheet.Column("E").Width = 30;
                worksheet.Columns("F:G").Width = 15;
                worksheet.Columns("H:I").Width = 30;
                worksheet.Column("J").Width = 15;
                worksheet.Columns("K:L").Width = 30;
                worksheet.Column("M").Width = 15;
                worksheet.Columns("N:O").Width = 30;
                worksheet.Column("P").Width = 15;

                currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Cod.Trabajador";
                worksheet.Cell(currentRow, 2).Value = "Cod.Plaza";
                worksheet.Cell(currentRow, 3).Value = "Tip.Doc.";
                worksheet.Cell(currentRow, 4).Value = "Num.Doc.";
                worksheet.Cell(currentRow, 5).Value = "Apellidos y Nombres";
                worksheet.Cell(currentRow, 6).Value = "Sexo";
                worksheet.Cell(currentRow, 7).Value = "Fec.Ingreso";
                worksheet.Cell(currentRow, 8).Value = "Vínculo";
                worksheet.Cell(currentRow, 9).Value = "Dependencia";
                worksheet.Cell(currentRow, 10).Value = "Estado";
                worksheet.Cell(currentRow, 11).Value = "Régimen";
                worksheet.Cell(currentRow, 12).Value = "AFP";
                worksheet.Cell(currentRow, 13).Value = "CUSPP";
                worksheet.Cell(currentRow, 14).Value = "Banco";
                worksheet.Cell(currentRow, 15).Value = "Tip.Cuenta";
                worksheet.Cell(currentRow, 16).Value = "Nro.Cuenta";

                worksheet.Range(worksheet.Cell(currentRow, 1), worksheet.Cell(currentRow, 16)).Style.Font.Bold = true;

                foreach (var item in data)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).SetValue<string>(item.trabajadorCod);
                    worksheet.Cell(currentRow, 2).SetValue<string>(item.codigoPlaza);
                    worksheet.Cell(currentRow, 3).SetValue<string>(item.tipoDocumentoDesc);
                    worksheet.Cell(currentRow, 4).SetValue<string>(item.numDocumento);
                    worksheet.Cell(currentRow, 5).SetValue<string>(String.Format("{0} {1}, {2}", item.apellidoPaterno, item.apellidoMaterno, item.nombre));
                    worksheet.Cell(currentRow, 6).SetValue<string>(item.sexoDesc);
                    
                    worksheet.Cell(currentRow, 7).SetValue<DateTime?>(item.fechaIngreso);
                    worksheet.Cell(currentRow, 7).Style.DateFormat.Format = Formats.BASIC_DATE;

                    worksheet.Cell(currentRow, 8).SetValue<string>(item.vinculoDesc);
                    worksheet.Cell(currentRow, 9).SetValue<string>(item.dependenciaDesc);
                    worksheet.Cell(currentRow, 10).SetValue<string>(item.estadoDesc);
                    worksheet.Cell(currentRow, 11).SetValue<string>(item.regimenDesc);
                    worksheet.Cell(currentRow, 12).SetValue<string>(item.afpDesc);
                    worksheet.Cell(currentRow, 13).SetValue<string>(item.cuspp);
                    worksheet.Cell(currentRow, 14).SetValue<string>(item.bancoDesc);
                    worksheet.Cell(currentRow, 15).SetValue<string>(item.tipoCuentaBancariaDesc);
                    worksheet.Cell(currentRow, 16).SetValue<string>(item.nroCuentaBancaria);
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    var content = stream.ToArray();

                    fileContent = new FileContent()
                    {
                        fileContent = content,
                        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName = "Lista de trabajadores.xlsx"
                    };

                    return fileContent;
                }
            }
        }

        public FileContent GenerarDescargableDeLecturaValoresDeConceptos(List<ValorExternoLecturaProcesadoDTO> lista)
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

        public FileContent GenerarDescargableResumenPlanillaTrabajador(IEnumerable<ResumenPlanillaTrabajadorDTO> data)
        {
            FileContent fileContent;
            int currentRow = 1;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hoja1");

                worksheet.Column("A").Width = 20;
                worksheet.Column("G").Width = 35;
                worksheet.Columns("H:I").Width = 15;

                worksheet.Cell(currentRow, 1).SetValue<string>("Planilla");
                worksheet.Cell(currentRow, 2).SetValue<string>("Año");
                worksheet.Cell(currentRow, 3).SetValue<string>("Mes");
                worksheet.Cell(currentRow, 4).SetValue<string>("Cod.Trabajador");
                worksheet.Cell(currentRow, 5).SetValue<string>("Tip.Doc.");
                worksheet.Cell(currentRow, 6).SetValue<string>("Num.Doc.");
                worksheet.Cell(currentRow, 7).SetValue<string>("Apellidos y Nombres");
                worksheet.Cell(currentRow, 8).SetValue<string>("Estado");
                worksheet.Cell(currentRow, 9).SetValue<string>("Régimen");
                worksheet.Cell(currentRow, 10).SetValue<string>("Remu.");
                worksheet.Cell(currentRow, 11).SetValue<string>("Reint.");
                worksheet.Cell(currentRow, 12).SetValue<string>("Deducc.");
                worksheet.Cell(currentRow, 13).SetValue<string>("Bruto");
                worksheet.Cell(currentRow, 14).SetValue<string>("Desc.");
                worksheet.Cell(currentRow, 15).SetValue<string>("Total");

                worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(currentRow, 15)).Style.Font.Bold = true;

                foreach (var item in data)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).SetValue<string>(item.categoriaPlanillaDesc);
                    worksheet.Cell(currentRow, 2).SetValue<string>(item.anio.ToString());
                    worksheet.Cell(currentRow, 3).SetValue<string>(item.mesDesc);
                    worksheet.Cell(currentRow, 4).SetValue<string>(item.estadoDesc);
                    worksheet.Cell(currentRow, 5).SetValue<string>(item.tipoDocumentoDesc);
                    worksheet.Cell(currentRow, 6).SetValue<string>(item.numDocumento);
                    worksheet.Cell(currentRow, 7).SetValue<string>(String.Format("{0} {1}, {2}", item.apellidoPaterno, item.apellidoMaterno, item.nombre));
                    worksheet.Cell(currentRow, 8).SetValue<string>(item.estadoDesc);
                    worksheet.Cell(currentRow, 9).SetValue<string>(item.regimenDesc);
                    worksheet.Cell(currentRow, 10).SetValue<decimal>(item.totalRemuneracion);
                    worksheet.Cell(currentRow, 11).SetValue<decimal>(item.totalReintegro);
                    worksheet.Cell(currentRow, 12).SetValue<decimal>(item.totalDeduccion);
                    worksheet.Cell(currentRow, 13).SetValue<decimal>(item.totalBruto);
                    worksheet.Cell(currentRow, 14).SetValue<decimal>(item.totalDescuento);
                    worksheet.Cell(currentRow, 15).SetValue<decimal>(item.totalSueldo);
                }

                worksheet.Range(worksheet.Cell(2, 10), worksheet.Cell(currentRow, 15)).Style.NumberFormat.Format = Formats.BASIC_DECIMAL;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    var content = stream.ToArray();

                    fileContent = new FileContent()
                    {
                        fileContent = content,
                        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName = "Reporte totales por trabajador.xlsx"
                    };

                    return fileContent;
                }
            }
        }

        public FileContent GenerarDescargableResumenPorActividadYDependencia(ReporteResumenPorActividadYDependencia reporte)
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

        public FileContent GenerarDescargableResumenSIAF(ReporteResumenSIAF reporte)
        {
            FileContent fileContent;
            IXLCell cell;
            int currentRow = 1;
            int currentCol = 1;
            int firstDataCol;
            string formula;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hoja1");

                var celdaTitulo = worksheet.Cell(currentRow, currentCol).SetValue<string>(reporte.titulo);
                celdaTitulo.Style.Font.Bold = true;

                currentRow++;

                foreach (var resumen in reporte.listaResumenes)
                {
                    if (resumen.detalle != null && resumen.detalle.Count() > 0)
                    {
                        currentCol = 1;

                        var celdaTituloAdm = worksheet.Cell(currentRow, currentCol).SetValue<string>(resumen.titulo);
                        celdaTituloAdm.Style.Font.Bold = true;

                        currentRow++;

                        foreach (var columnName in resumen.cabecera)
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
                        firstDataCol = currentRow;
                        foreach (var item in resumen.detalle)
                        {
                            foreach (var columnName in resumen.cabecera)
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

                        worksheet.Cell(currentRow, currentCol).SetValue<string>(resumen.pieTabla);
                        foreach (var columnName in resumen.cabecera)
                        {
                            if (columnName != "Actividad" && columnName != "Meta")
                            {
                                cell = worksheet.Cell(currentRow, currentCol);
                                formula = string.Format("SUM(R[{0}]C:R[-1]C)", firstDataCol - currentRow);
                                cell.SetFormulaR1C1(formula);
                                cell.Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                                cell.Style.Font.Bold = true;
                                cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;


                            }
                            currentCol++;
                        }

                        currentRow += 2;
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
                        fileName = "Resumen SIAF.xlsx"
                    };

                    return fileContent;
                }
            }
        }

        public FileContent GenerarDescargableDetallePlanillaDeTrabajador(TrabajadorDTO trabajador, IEnumerable<CategoriaPlanillaGeneradaParaTrabajadorDTO> listaCategoriasPlanilla,
            List<ConceptoGeneradoDTO> conceptosGenerados)
        {
            FileContent fileContent;
            int currentRow;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Conceptos");

                worksheet.Column("A").Width = 15;
                worksheet.Column("B").Width = 40;
                worksheet.Column("C").Width = 15;

                currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Código de trabajador";
                worksheet.Cell(currentRow, 2).SetValue<string>(trabajador.trabajadorCod);


                currentRow++;

                worksheet.Cell(currentRow, 1).Value = "Nombres completos";
                worksheet.Cell(currentRow, 2).Value = String.Format("{0} {1}, {2}", trabajador.apellidoPaterno, trabajador.apellidoMaterno, trabajador.nombre).ToUpper();

                currentRow++;

                worksheet.Cell(currentRow, 1).Value = "Tip. doc. identidad";
                worksheet.Cell(currentRow, 2).Value = trabajador.tipoDocumentoDesc.ToUpper();

                currentRow++;

                worksheet.Cell(currentRow, 1).Value = "Num. doc. identidad";
                worksheet.Cell(currentRow, 2).SetValue<string>(trabajador.numDocumento);

                currentRow++;

                worksheet.Cell(currentRow, 1).Value = "Estado";
                worksheet.Cell(currentRow, 2).Value = trabajador.estadoDesc.ToUpper();

                currentRow++;

                worksheet.Cell(currentRow, 1).Value = "Vínculo";
                worksheet.Cell(currentRow, 2).Value = trabajador.vinculoDesc.ToUpper();

                currentRow++;

                worksheet.Cell(currentRow, 1).Value = "Periodo";
                worksheet.Cell(currentRow, 2).SetValue<string>(String.Format("{0} - {1}", listaCategoriasPlanilla.First().año, listaCategoriasPlanilla.First().mesDesc.ToUpper()));

                worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(currentRow, 1)).Style.Font.Bold = true;

                foreach (var categoria in listaCategoriasPlanilla)
                {
                    currentRow++;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Planilla";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = categoria.categoriaPlanillaDesc.ToUpper();

                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Dependencia";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = categoria.dependenciaDesc.ToUpper();

                    if (!string.IsNullOrEmpty(categoria.grupoTrabajoDesc))
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = "Grupo trabajo";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = categoria.grupoTrabajoDesc.ToUpper();
                    }
                    
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Tipo";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = categoria.tipoPlanillaDesc.ToUpper();

                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Clase";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = categoria.clasePlanillaDesc.ToUpper();

                    var listaConcepto = conceptosGenerados.Where(x => x.trabajadorPlanillaID == categoria.trabajadorPlanillaID);

                    foreach (var conceptosAgrupados in listaConcepto.GroupBy(x => new { x.tipoConceptoID, x.tipoConceptoDesc }))
                    {
                        currentRow++;
                        
                        worksheet.Cell(currentRow, 1).Value = String.Format("TOTAL {0}", conceptosAgrupados.Key.tipoConceptoDesc.ToUpper());
                        worksheet.Range(worksheet.Cell(currentRow, 1), worksheet.Cell(currentRow, 2)).Merge(true);
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Range(worksheet.Cell(currentRow, 1), worksheet.Cell(currentRow, 2)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        worksheet.Cell(currentRow, 3).SetValue<decimal>(categoria.ObtenerTotal((TipoConcepto)conceptosAgrupados.Key.tipoConceptoID));
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                        worksheet.Cell(currentRow, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        foreach (var concepto in conceptosAgrupados)
                        {
                            currentRow++;

                            worksheet.Cell(currentRow, 1).SetValue<string>(concepto.conceptoCod);
                            worksheet.Cell(currentRow, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            worksheet.Cell(currentRow, 2).Value = concepto.conceptoDesc.ToUpper();
                            worksheet.Cell(currentRow, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            worksheet.Cell(currentRow, 3).SetValue<decimal>(concepto.monto);
                            worksheet.Cell(currentRow, 3).Style.NumberFormat.Format = Formats.BASIC_DECIMAL;
                            worksheet.Cell(currentRow, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }
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
                        fileName = "Detalle de planilla.xlsx"
                    };

                    return fileContent;
                }
            }
        }

        public FileContent GenerarDescargableDetallePlanillaTrabajadores(ReporteDetallePlanillaTrabajadorDTO reporte)
        {
            FileContent fileContent;
            IXLCell cell;
            int currentRow = 1;
            int currentCol = 1;
            decimal monto;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hoja1");
                worksheet.Column("C").Width = 15;
                worksheet.Column("D").Width = 30;
                worksheet.Columns("E:F").Width = 15;

                if (reporte.detalle != null && reporte.detalle.Count() > 0)
                {
                    foreach (var columnName in reporte.cabecera)
                    {
                        cell = worksheet.Cell(currentRow, currentCol).SetValue<string>(columnName);
                        cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        cell.Style.Font.Bold = true;
                        currentCol++;
                    }

                    currentRow++;
                    currentCol = 1;
                    foreach (var item in reporte.detalle)
                    {
                        foreach (var columnName in reporte.cabecera)
                        {
                            if (columnName == "MES" || columnName == "COD.TRABAJADOR" || columnName == "APELLIDOS Y NOMBRES" || 
                                columnName == "TIP.DOC." || columnName == "NUM.DOC." || columnName == "ESTADO")
                            {
                                cell = worksheet.Cell(currentRow, currentCol).SetValue<string>((string)item[columnName]);
                            }
                            else if(columnName == "AÑO")
                            {
                                cell = worksheet.Cell(currentRow, currentCol).SetValue<int>((int)item[columnName]);
                            }
                            else
                            {
                                monto = item[columnName] == null ? 0 : (decimal)item[columnName];
                                cell = worksheet.Cell(currentRow, currentCol).SetValue<decimal>(monto);
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
                }
                
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    var content = stream.ToArray();

                    fileContent = new FileContent()
                    {
                        fileContent = content,
                        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName = "Detalle de Planilla de Trabajadores.xlsx"
                    };

                    return fileContent;
                }
            }
        }

        public FileContent GenerarDescargableDeLecturaCargaDeTrabajadores(List<TrabajadorLecturaProcesadoDTO> lista)
        {
            FileContent fileContent;
            int currentRow;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Resultado");

                worksheet.Columns("A:V").Width = 15;
                worksheet.Column("Z").Width = 30;

                currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Acción";
                worksheet.Cell(currentRow, 2).Value = "Tip.Doc.";
                worksheet.Cell(currentRow, 3).Value = "Num.Doc.";
                worksheet.Cell(currentRow, 4).Value = "Ape.Paterno";
                worksheet.Cell(currentRow, 5).Value = "Ape.Materno";
                worksheet.Cell(currentRow, 6).Value = "Nombres";
                worksheet.Cell(currentRow, 7).Value = "Sexo";
                worksheet.Cell(currentRow, 8).Value = "Cod.Trabajador";
                worksheet.Cell(currentRow, 9).Value = "Vínculo";
                worksheet.Cell(currentRow, 10).Value = "Grup.Ocupacional";
                worksheet.Cell(currentRow, 11).Value = "Niv.Remunerativo";
                worksheet.Cell(currentRow, 12).Value = "Cat.Docente";
                worksheet.Cell(currentRow, 13).Value = "Ded.Docente";
                worksheet.Cell(currentRow, 14).Value = "Horas Docente";
                worksheet.Cell(currentRow, 15).Value = "Fecha ingreso";
                worksheet.Cell(currentRow, 16).Value = "Dependencia";
                worksheet.Cell(currentRow, 17).Value = "Banco";
                worksheet.Cell(currentRow, 18).Value = "Num.Cuenta";
                worksheet.Cell(currentRow, 19).Value = "Tip.Cuenta";
                worksheet.Cell(currentRow, 20).Value = "Régimen";
                worksheet.Cell(currentRow, 21).Value = "AFP";
                worksheet.Cell(currentRow, 22).Value = "CUSPP";
                worksheet.Cell(currentRow, 23).Value = "Cod.Plaza";
                worksheet.Cell(currentRow, 24).Value = "Observación";

                foreach (var item in lista)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).SetValue<string>(item.operacionDesc);
                    worksheet.Cell(currentRow, 2).SetValue<string>(item.tipoDocumentoCod);
                    worksheet.Cell(currentRow, 3).SetValue<string>(item.numDocumento);
                    worksheet.Cell(currentRow, 4).SetValue<string>(item.apePaterno);
                    worksheet.Cell(currentRow, 5).SetValue<string>(item.apeMaterno);
                    worksheet.Cell(currentRow, 6).SetValue<string>(item.nombres);
                    worksheet.Cell(currentRow, 7).SetValue<string>(item.sexoCod);
                    worksheet.Cell(currentRow, 8).SetValue<string>(item.codigoTrabajador);
                    worksheet.Cell(currentRow, 9).SetValue<string>(item.vinculoCod);
                    worksheet.Cell(currentRow, 10).SetValue<string>(item.grupoOcupacionalCod);
                    worksheet.Cell(currentRow, 11).SetValue<string>(item.nivelRemunerativoCod);
                    worksheet.Cell(currentRow, 12).SetValue<string>(item.categoriaDocenteCod);
                    worksheet.Cell(currentRow, 13).SetValue<string>(item.dedicacionDocenteCod);
                    worksheet.Cell(currentRow, 14).SetValue<int?>(item.horasDocente);
                    worksheet.Cell(currentRow, 15).SetValue<string>(item.fechaIngreso);
                    worksheet.Cell(currentRow, 16).SetValue<string>(item.dependenciaCod);
                    worksheet.Cell(currentRow, 17).SetValue<string>(item.bancoCod);
                    worksheet.Cell(currentRow, 18).SetValue<string>(item.numeroCuentaBancaria);
                    worksheet.Cell(currentRow, 19).SetValue<string>(item.tipoCuentaBancariaCod);
                    worksheet.Cell(currentRow, 10).SetValue<string>(item.regimenPensionarioCod);
                    worksheet.Cell(currentRow, 21).SetValue<string>(item.afpCod);
                    worksheet.Cell(currentRow, 22).SetValue<string>(item.cuspp);
                    worksheet.Cell(currentRow, 23).SetValue<string>(item.codigoPlaza);

                    if (!item.esRegistroCorrecto)
                    {
                        var observacionesText = string.Join("\n", item.observaciones);

                        worksheet.Cell(currentRow, 24).SetValue<string>(observacionesText);
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
                        fileName = "Resultado de la lectura de archivo de trabajadores.xlsx"
                    };

                    return fileContent;
                }
            }
        }
    }
}
