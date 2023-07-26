using Data.Connection;
using Data.Procedures;
using Data.Views;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class ValorExternoConceptoService : IValorExternoConceptoService
    {
        public Response GrabarValoresExternos(List<ValorConceptoEntity> valores, int userID)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("I_ID");
            dataTable.Columns.Add("I_TrabajadorID");
            dataTable.Columns.Add("I_PeriodoID");
            dataTable.Columns.Add("I_ConceptoID");
            dataTable.Columns.Add("M_ValorConcepto");
            dataTable.Columns.Add("I_ProveedorID");

            int id = 1;

            valores.ForEach(x => {
                dataTable.Rows.Add(
                    id,
                    x.trabajadorID,
                    x.periodoID,
                    x.conceptoID,
                    x.valorConcepto,
                    x.proveedorID);
                id++;
            });

            var grabarValorExterno = new USP_IU_GrabarValorExterno()
            {
                Tbl_ValorExterno = dataTable,
                I_UserID = userID
            };

            var result = grabarValorExterno.Execute();

            return Mapper.Result_To_Response(result);
        }

        public List<ValorExternoConceptoDTO> ListarValoresExternosConceptos(int anio, int mes, int categoriaPlanillaID)
        {
            var lista = VW_ValoresExternos.FindAll(anio, mes, categoriaPlanillaID)
                .Select(x => Mapper.VW_ValoresExternos_To_ValorExternoConceptoDTO(x))
                .ToList();

            return lista;
        }

        public ValorExternoConceptoDTO ObtenerValorExternoConcepto(int conceptoExternoValorID)
        {
            ValorExternoConceptoDTO valorExternoConceptoDTO;

            var view = VW_ValoresExternos.FindByID(conceptoExternoValorID);

            if (view == null)
            {
                valorExternoConceptoDTO = null;
            }
            else
            {
                valorExternoConceptoDTO = Mapper.VW_ValoresExternos_To_ValorExternoConceptoDTO(view);
            }

            return valorExternoConceptoDTO;
        }

        public Response ActualizarValorExternoConcepto(int conceptoExternoValorID, decimal valorConcepto, int userID)
        {
            Result result;

            try
            {
                var actualizarConcepto = new USP_U_ActualizarValorExterno()
                {
                    I_ConceptoExternoValorID = conceptoExternoValorID,
                    M_ValorConcepto = valorConcepto,
                    I_UserID = userID
                };

                result = actualizarConcepto.Execute();
            }
            catch (Exception ex)
            {
                result = new Result()
                {
                    Message = ex.Message
                };
            }

            return Mapper.Result_To_Response(result);
        }

        public Response Eliminar(int conceptoExternoValorID, int userID)
        {
            Result result;

            try
            {
                var plantillaPlanillaConceptoDTO = ObtenerValorExternoConcepto(conceptoExternoValorID);

                if (plantillaPlanillaConceptoDTO != null)
                {
                    var uspEliminar = new USP_U_EliminarValorExternoConcepto()
                    {
                        I_ConceptoExternoValorID = conceptoExternoValorID,
                        I_UserID = userID
                    };

                    result = uspEliminar.Execute();
                }
                else
                {
                    result = new Result()
                    {
                        Success = true,
                        Message = "El registro seleccionado ha sido eliminado con anterioridad."
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Result()
                {
                    Message = ex.Message
                };
            }

            return Mapper.Result_To_Response(result);
        }
    }
}
