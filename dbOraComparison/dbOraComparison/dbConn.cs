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
        static string oradb25 = "Data Source=ip_host;Persist Security Info=False;User ID=db_username;Password=db_password;Connection Timeout=120;";
        public static OracleConnection orclConn25 = new OracleConnection(oradb25);

        static string oradb230 = "Data Source=ip_host_test;Persist Security Info=False;User ID=db_username;Password=db_password;Connection Timeout=120;";
        public static OracleConnection orclConn230 = new OracleConnection(oradb230);
    }
}
