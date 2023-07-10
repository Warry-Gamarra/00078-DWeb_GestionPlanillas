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
    public class ValorExternoConceptoService
    {
        public Response GrabarValoresExternos(List<ValorConceptoEntity> valores, int userID)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("I_TrabajadorID");
            dataTable.Columns.Add("I_PeriodoID");
            dataTable.Columns.Add("I_ConceptoID");
            dataTable.Columns.Add("M_ValorConcepto");
            dataTable.Columns.Add("I_ProveedorID");

            valores.ForEach(x => {
                dataTable.Rows.Add(x.trabajadorID);
                dataTable.Rows.Add(x.periodoID);
                dataTable.Rows.Add(x.conceptoID);
                dataTable.Rows.Add(x.valorConcepto);
                dataTable.Rows.Add(x.proveedorID);
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
