using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Common.SqlConfig
{
    public class DbContext
    {
        private readonly SqlSugarClient _db;

        public DbContext(SqlSugarClient db)
        {
            _db = db;
        }

        public SqlSugarClient Db => _db;
    }
}
