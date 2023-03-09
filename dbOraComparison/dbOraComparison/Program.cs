using System;
using System.Data;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace dbOraComparison
{
    class Program
    {
        static string tableNamesQ = "SELECT TABLE_NAME FROM ALL_TABLES WHERE OWNER = 'DANAOS_NEW' order by TABLE_NAME ASC";
        static void Main(string[] args)
        {
            Console.WriteLine("Enstablishing connection to both databases... Please wait...");

            DataTable dtTableNames25 = dbData.retreiveDataDanaos(tableNamesQ);
            DataTable dtTableNames230 = dbData230.retreiveDataDanaos(tableNamesQ);

            Console.WriteLine("Connection Successful, Tables Received!");

            DataColumn dc25 = new DataColumn("TABLE_ROWS_COUNT", typeof(String));
            DataColumn dc230 = new DataColumn("TABLE_ROWS_COUNT", typeof(String));
            dtTableNames25.Columns.Add(dc25);
            dtTableNames230.Columns.Add(dc230);

            Console.WriteLine("Calculating Live Database Table Rows... Please wait...");

            foreach (DataRow row in dtTableNames25.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();

                string tableRowsCountQ = "SELECT COUNT(*) AS rowsCount, '" + tableName + "' FROM " + tableName + "";

                try
                {
                    DataTable dtTableRows25 = dbData.retreiveDataDanaos(tableRowsCountQ);
                    row["TABLE_ROWS_COUNT"] = dtTableRows25.Rows[0]["rowsCount"].ToString();
                }
                catch
                {
                    row["TABLE_ROWS_COUNT"] = 0;
                }
            }

            Console.WriteLine("Calculating Test Database Table Rows... Please wait...");

            foreach (DataRow row in dtTableNames230.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();

                string tableRowsCountQ = "SELECT COUNT(*) AS rowsCount, '" + tableName + "' FROM " + tableName + "";

                try
                {
                    DataTable dtTableRows230 = dbData230.retreiveDataDanaos(tableRowsCountQ);
                    row["TABLE_ROWS_COUNT"] = dtTableRows230.Rows[0]["rowsCount"].ToString();
                }
                catch
                {
                    row["TABLE_ROWS_COUNT"] = 0;
                }
            }

            int tableDifference = 0;
            int rowDifference = 0;

            Console.WriteLine("Printing all Tables and Rows received from both Databases...");
            Console.WriteLine();

            List<Results> _data = new List<Results>();

            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;

            string fileName = @"C:\Temp\dbCompLog.txt";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            ostrm = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            writer = new StreamWriter(ostrm);
            Console.SetOut(writer);

            Console.WriteLine("Starting Table and Rows Comparison...");
            Console.WriteLine();

            foreach (DataRow row in dtTableNames25.Rows)
            {
                List<tableData> tablesAll = new List<tableData>();
                string tableName25 = row["TABLE_NAME"].ToString();
                string numRows25 = row["TABLE_ROWS_COUNT"].ToString();
                string charDiffTable = "N";
                string charDiffRows = "N";
                string tableName230 = "";
                string numRows230 = "";

                foreach (DataRow rowTest in dtTableNames230.Rows)
                {
                    if(tableName25 == rowTest["TABLE_NAME"].ToString())
                    {
                        tableName230 = rowTest["TABLE_NAME"].ToString();
                        numRows230 = rowTest["TABLE_ROWS_COUNT"].ToString();

                        goto endOfLoop;
                    }
                }

                endOfLoop:

                if (tableName25 == tableName230)
                {
                    if (numRows25 != numRows230)
                    {
                        rowDifference += 1;
                        charDiffRows = "Y";
                    }

                    Console.WriteLine("Live Table Name: " + tableName25 + ", Table Rows Count: " + numRows25 + " || Test Table Name: " + tableName230 + ", Table Rows Count: " + numRows230 + "");
                    Console.WriteLine("Table " + tableName25 + " Difference = " + charDiffTable + " | Rows Difference: " + charDiffRows + "");
                    Console.WriteLine();

                    tablesAll.Add(new tableData()
                    {
                        TABLE_NAME = tableName25,
                        ROWS_COUNT = numRows25
                    });
                    tablesAll.Add(new tableData()
                    {
                        TABLE_NAME = tableName230,
                        ROWS_COUNT = numRows230
                    });
                    _data.Add(new Results()
                    {
                        data = tablesAll,
                        TABLE_DIFFERENCE = charDiffTable,
                        ROWS_DIFFERENCE = charDiffRows
                    });
                }
                else
                {
                    tableDifference += 1;
                    charDiffTable = "Y";
                    rowDifference += 1;
                    charDiffRows = "Y";

                    Console.WriteLine("Live Table Name: " + tableName25 + ", Table Rows Count: " + numRows25 + " || Table Name: " + tableName25 + " does not exit in Test Database.");
                    Console.WriteLine("Table " + tableName25 + " Difference = " + charDiffTable + " | Rows Difference: " + charDiffRows + "");
                    Console.WriteLine();

                    tablesAll.Add(new tableData()
                    {
                        TABLE_NAME = tableName25,
                        ROWS_COUNT = numRows25
                    });
                    tablesAll.Add(new tableData()
                    {
                        TABLE_NAME = "N/A",
                        ROWS_COUNT = "N/A"
                    });
                    _data.Add(new Results()
                    {
                        data = tablesAll,
                        TABLE_DIFFERENCE = charDiffTable,
                        ROWS_DIFFERENCE = charDiffRows
                    });
                }
            }

            Console.WriteLine("Total Table Differences = " + tableDifference + " | Total Rows Differences: " + rowDifference + "");

            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();

            Console.WriteLine("Log File Exported Successfully!");

            string fileName1 = @"C:\Temp\dbCompLog.json";

            if (File.Exists(fileName1))
            {
                File.Delete(fileName1);
            }

            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(fileName1, json);

            Console.WriteLine("Json Exported Succesfully!");
        }
    }
}
