﻿using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Procedures
{
    public class USP_S_ListarDetallePlanillaTrabajador
    {
        public USP_S_ListarDetallePlanillaTrabajador(IEnumerable<string> columnasConcepto, IEnumerable<IDictionary<string, object>> detalle)
        {
            this.cabecera = new List<string>() { "AÑO", "MES", "COD.TRABAJADOR", "APELLIDOS Y NOMBRES", "TIP.DOC.", "NUM.DOC.", "ESTADO" };

            this.cabecera = this.cabecera.Concat(columnasConcepto);

            this.detalle = detalle;
        }

        public IEnumerable<string> cabecera { get; }

        public IEnumerable<IDictionary<string, object>> detalle { get; }

        public static USP_S_ListarDetallePlanillaTrabajador Execute(int I_Anio, int I_Mes, int I_CategoriaPlanillaID)
        {
            string command;
            DynamicParameters parameters;
            IEnumerable<string> columnasConcepto;
            IEnumerable<IDictionary<string, object>> detalle;
            USP_S_ListarDetallePlanillaTrabajador results;

            try
            {
                command = "USP_S_ListarDetallePlanillaTrabajador";

                parameters = new DynamicParameters();

                parameters.Add(name: "I_Anio", dbType: DbType.Int32, value: I_Anio);

                parameters.Add(name: "I_Mes", dbType: DbType.Int32, value: I_Mes);

                parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: I_CategoriaPlanillaID);

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    var query = _dbConnection.QueryMultiple(command, parameters, commandType: CommandType.StoredProcedure);

                    columnasConcepto = query.Read<string>();

                    if (columnasConcepto != null && columnasConcepto.Count() > 0)
                    {
                        detalle = query.Read().Select(row => (IDictionary<string, object>)row);
                    }
                    else
                    {
                        detalle = new List<Dictionary<string, object>>();
                    }

                    results = new USP_S_ListarDetallePlanillaTrabajador(columnasConcepto, detalle);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return results;
        }
    }
}
