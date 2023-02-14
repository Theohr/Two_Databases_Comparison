using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbOraComparison
{
    class dbTablesRowsComp
    {
        static string connectionString1 = "";
        static string connectionString2 = "";

        public static void retreiveData()
        {
            using (OracleConnection connection1 = new OracleConnection(connectionString1))
            {
                connection1.Open();

                using (OracleConnection connection2 = new OracleConnection(connectionString2))
                {
                    connection2.Open();

                    string query = @"SELECT column1, column2, count(*)
                                FROM (
                                  SELECT column1, column2
                                  FROM table1
                                  UNION
                                  SELECT column1, column2
                                  FROM table2
                                )
                                GROUP BY column1, column2
                                HAVING count(*) = 1";

                    using (OracleCommand command = new OracleCommand(query, connection1))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("Column1: {0}, Column2: {1}", reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }
                }
            }
        }
    }
}
