using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbOraComparison
{
    class dbData230
    {
        OracleCommand orclCmd = new OracleCommand();

        public static DataTable retreiveDataDanaos(string orclCmd1)
        {

            DataTable dt = new DataTable("DanaosDataTable");

            try
            {
                dbConn.orclConn230.Close();
            }
            catch (InvalidOperationException ex)
            {

            }

            try
            {
                dbConn.orclConn230.Open();
            }
            catch (InvalidOperationException ex)
            {

            }

            OracleDataAdapter dr = new OracleDataAdapter(orclCmd1, dbConn.orclConn230);

            dr.Fill(dt);

            try
            {
                dbConn.orclConn230.Close();
            }
            catch (InvalidOperationException ex)
            {

            }

            return dt;
        }

    }
}
