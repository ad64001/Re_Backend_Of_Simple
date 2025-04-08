using Re_Backend.Common.Attributes;
using Re_Backend.Common.SqlConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.Other
{
    public interface ITestDbService
    {
        string DoSomething();
    }

    [Injectable(IsSingleton = true)]
    public class TestDbService : ITestDbService
    {

        public string DoSomething()
        {
            try
            {
                return "测试成功！";
            }
            catch (Exception ex)
            {
                return $"测试失败: {ex.Message}";
            }
        }

    }
}
