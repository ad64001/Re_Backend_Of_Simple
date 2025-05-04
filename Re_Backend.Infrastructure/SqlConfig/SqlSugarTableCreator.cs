using SqlSugar;
using System.Reflection;

namespace Re_Backend.Infrastructure.SqlConfig
{
    public class SqlSugarTableCreator
    {
        private readonly SqlSugarClient _db;

        public SqlSugarTableCreator(SqlSugarClient db)
        {
            _db = db;
        }

        public void CreateTablesFromModels()
        {
            try
            {
                // 加载 Re_Backend.Domain 程序集
                var domainAssembly = Assembly.Load("Re_Backend.Domain");

                // 获取带有 SqlSugar 注解的类型
                var modelTypes = domainAssembly.GetTypes()
                                               .Where(t => t.GetCustomAttributes(typeof(SqlSugar.SugarTable), true).Length > 0)
                                               .ToList();
                if (modelTypes.Count > 0)
                {
                    _db.CodeFirst.InitTables(modelTypes.ToArray());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"自动建表时出错: {ex.Message}");
            }
        }
    }
}
