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
    public class TC_HorasDocente
    {
        public int I_HorasDocenteID { get; set; }

        public int I_DedicacionDocenteID { get; set; }

        public int I_Horas { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_HorasDocente> FindAll()
        {
            IEnumerable<TC_HorasDocente> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_HorasDocente WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_HorasDocente>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
