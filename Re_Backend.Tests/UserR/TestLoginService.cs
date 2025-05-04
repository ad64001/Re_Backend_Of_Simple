using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.IServices;
using Xunit.Abstractions;

namespace Re_Backend.Tests.UserR
{
    public class TestLoginService : IClassFixture<TestFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly ILoginService _loginService;
        public TestLoginService(TestFixture fixture, ITestOutputHelper testOutput)
        {
            _output = testOutput;
            _loginService = fixture.LoginService;
        }

        [Fact]
        public async Task RegisterTest()
        {
            User user = new User()
            {
                UserName = "TestUserrrrrr",
                NickName = "ssssqqs",
                Password = "114514222w2",
                Email = "Ewxd@Ex.com"
            };
            string s = await _loginService.Register(user);
            _output.WriteLine(s);
        }
    }
}
