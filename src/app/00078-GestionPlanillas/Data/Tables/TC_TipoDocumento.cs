﻿using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Tables
{
    public class TC_TipoDocumento
    {
        public int I_TipoDocumentoID { get; set; } 

        public string T_TipoDocumentoCod {  get; set; }

        public string T_TipoDocumentoDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_TipoDocumento> FindAll()
        {
            IEnumerable<TC_TipoDocumento> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_TipoDocumento WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_TipoDocumento>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_TipoDocumento>();
            }

            return result;
        }
    }
}
