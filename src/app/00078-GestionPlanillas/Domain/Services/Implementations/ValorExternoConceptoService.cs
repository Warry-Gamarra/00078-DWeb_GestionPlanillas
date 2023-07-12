using Data.Procedures;
using Domain.Entities;
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
    }
}
