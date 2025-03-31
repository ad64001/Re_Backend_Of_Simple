using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Common.SqlConfig
{
    public class SqlDefault
    {
        public SqlDefault()
        {
            ConnectionString = JsonSettings.GetValue("SqlSugar:Connection");
            DbType = int.Parse(JsonSettings.GetValue("SqlSugar:DbType"));
        }
        public string ConnectionString { get; set; }
        public int DbType { get; set; }
    }
}
