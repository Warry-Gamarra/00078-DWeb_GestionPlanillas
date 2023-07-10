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
        public List<ValorExternoConceptoDTO> ObtenerListaValoresDeConceptos(string filePath)
        {
            string[] expectedColNames = { "año", "mes", "tip_documento", "num_documento", "cod_concepto", "valor_concepto", "proveedor" };

            var lista = new List<ValorExternoConceptoDTO>();

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
                                };

                                if (!expectedColNames.SequenceEqual(colNames))
                                {
                                    throw new Exception("Las columnas no son las esperadas");
                                }

                                i++;
                            }
                            else
                            {
                                lista.Add(Mapper.ExcelDataReader_To_ConceptoExternoValorDTO(reader));
                            }
                        }

                    } while (reader.NextResult());
                }
            }

            return lista;
        }
    }
}
