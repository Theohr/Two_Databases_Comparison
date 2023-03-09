using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbOraComparison
{
    class dbConn
    {
        static string oradb25 = "Data Source=db_host;Persist Security Info=False;User ID=dbuser;Password=dbpass;Connection Timeout=120;";
        public static OracleConnection orclConn25 = new OracleConnection(oradb25);

        static string oradb230 = "Data Source=db2_host;Persist Security Info=False;User ID=dbuser;Password=dbpass;Connection Timeout=120;";
        public static OracleConnection orclConn230 = new OracleConnection(oradb230);
    }
}
