using SqlSugar;


namespace Re_Backend.Common.SqlConfig
{
    public class SqlSugarSetup
    {
        public static List<ConnectionConfig> GetConnectionConfigs()
        {
            SqlDefault sqlDefault = new SqlDefault();
            return new List<ConnectionConfig>
            {
                new ConnectionConfig
                {
                    ConnectionString = sqlDefault.ConnectionString,
                    DbType = (DbType)sqlDefault.DbType,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute
                }
            };
        }
    }

}
