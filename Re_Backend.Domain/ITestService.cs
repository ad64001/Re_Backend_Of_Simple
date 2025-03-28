using Re_Backend.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain
{
    public interface ITestService
    {
        string DoSomething();
    }

    [Injectable(IsSingleton = true)]
    public class TestService : ITestService
    {
        public string DoSomething()
        {
            // 这里可以添加具体的实现逻辑
            Console.WriteLine("CSH: TestService.DoSomething()");
            return "Ciallo";
        }
    }
}
