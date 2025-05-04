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
