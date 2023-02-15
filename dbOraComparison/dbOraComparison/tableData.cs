using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbOraComparison
{
    public class tableData
    {
        public string TABLE_NAME { get; set; }
        public string ROWS_COUNT { get; set; }
    }

    public class Results
    {
        public List<tableData> data { get; set; }
        public string TABLE_DIFFERENCE { get; set; }
        public string ROWS_DIFFERENCE { get; set; }
    }


}
