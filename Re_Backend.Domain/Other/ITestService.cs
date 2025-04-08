using Re_Backend.Common.Attributes;
using Re_Backend.Common.SqlConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.Other
{
    public interface ITestService
    {
        string DoSomething();    }

    [Injectable(IsSingleton = true)]
    public class TestService : ITestService
    {
        private readonly DbContext _dbContext;


        public TestService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string DoSomething()
        {
            try
            {
                // 尝试连接数据库
                _dbContext.Db.Ado.Connection.Open();
                _dbContext.Db.Ado.Connection.Close();
                return "数据库连接测试成功！";
            }
            catch (Exception ex)
            {
                return $"数据库连接测试失败: {ex.Message}";
            }
        }

        
    }
}
