using SqlSugar;

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
