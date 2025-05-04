using Re_Backend.Common.Attributes;

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
