using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbOraComparison
{
    class dbData
    {
        OracleCommand orclCmd = new OracleCommand();

        public static DataTable retreiveDataDanaos(string orclCmd1)
        {

            DataTable dt = new DataTable("DanaosDataTable");

            try
            {
                dbConn.orclConn25.Close();
            }
            catch (InvalidOperationException ex)
            {

            }

            try
            {
                dbConn.orclConn25.Open();
            }
            catch (InvalidOperationException ex)
            {

            }

            OracleDataAdapter dr = new OracleDataAdapter(orclCmd1, dbConn.orclConn25);

            dr.Fill(dt);

            try
            {
                dbConn.orclConn25.Close();
            }
            catch (InvalidOperationException ex)
            {

            }

            return dt;
        }

    }
}
