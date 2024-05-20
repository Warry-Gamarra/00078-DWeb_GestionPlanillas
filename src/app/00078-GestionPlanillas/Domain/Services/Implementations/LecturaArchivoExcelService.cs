using Data.Procedures;
using Domain.Entities;
using Domain.Helpers;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class LecturaArchivoExcelService : ILecturaArchivoService
    {
        public List<ValorExternoLecturaDTO> ObtenerListaValoresDeConceptos(string filePath)
        {
            string[] expectedColNames = { "año", "mes", "tip_documento", "num_documento", "categoria", "cod_concepto", "valor_concepto", "proveedor" };

            var lista = new List<ValorExternoLecturaDTO>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        int i = 0;

                        while (reader.Read())
                        {
                            if (i == 0)
                            {
                                string[] colNames =
                                {
                                    reader.GetValue(0)?.ToString(),
                                    reader.GetValue(1)?.ToString(),
                                    reader.GetValue(2)?.ToString(),
                                    reader.GetValue(3)?.ToString(),
                                    reader.GetValue(4)?.ToString(),
                                    reader.GetValue(5)?.ToString(),
                                    reader.GetValue(6)?.ToString(),
                                    reader.GetValue(7)?.ToString()
                                };

                                if (!expectedColNames.SequenceEqual(colNames))
                                {
                                    throw new Exception("Las columnas no tienen el nombre correcto.");
                                }

                                i++;
                            }
                            else
                            {
                                lista.Add(Mapper.ExcelDataReader_To_ValorExternoLecturaDTO(reader));
                            }
                        }

                    } while (reader.NextResult());
                }
            }

            return lista;
        }

        public List<TrabajadorLecturaDTO> ObtenerListaTrabajadores(string filePath)
        {
            string[] expectedColNames = { "tip_documento", "num_documento", "apellido_paterno", "apellido_materno", "nombres",
                "sexo", "codigo_trabajador", "vinculo", "grupo_ocupacional", "nivel_remunerativo", "categoria_docente", "dedicacion_docente", "horas",
                "fecha_ingreso", "dependencia", "cuenta_banco", "numero_cuenta", "tipo_cuenta", "regimen", "afp", "codigo_plaza" };

            var lista = new List<TrabajadorLecturaDTO>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        int i = 0;

                        while (reader.Read())
                        {
                            if (i == 0)
                            {
                                string[] colNames =
                                {
                                    reader.GetValue(0)?.ToString(),
                                    reader.GetValue(1)?.ToString(),
                                    reader.GetValue(2)?.ToString(),
                                    reader.GetValue(3)?.ToString(),
                                    reader.GetValue(4)?.ToString(),
                                    reader.GetValue(5)?.ToString(),
                                    reader.GetValue(6)?.ToString(),
                                    reader.GetValue(7)?.ToString(),
                                    reader.GetValue(8)?.ToString(),
                                    reader.GetValue(9)?.ToString(),
                                    reader.GetValue(10)?.ToString(),
                                    reader.GetValue(11)?.ToString(),
                                    reader.GetValue(12)?.ToString(),
                                    reader.GetValue(13)?.ToString(),
                                    reader.GetValue(14)?.ToString(),
                                    reader.GetValue(15)?.ToString(),
                                    reader.GetValue(16)?.ToString(),
                                    reader.GetValue(17)?.ToString(),
                                    reader.GetValue(18)?.ToString(),
                                    reader.GetValue(19)?.ToString(),
                                    reader.GetValue(20)?.ToString()
                                };

                                if (!expectedColNames.SequenceEqual(colNames))
                                {
                                    throw new Exception("Las columnas no tienen el nombre correcto.");
                                }

                                i++;
                            }
                            else
                            {
                                lista.Add(Mapper.ExcelDataReader_To_TrabajadorLecturaDTO(reader));
                            }
                        }

                    } while (reader.NextResult());
                }
            }

            return lista;
        }
    }
}
