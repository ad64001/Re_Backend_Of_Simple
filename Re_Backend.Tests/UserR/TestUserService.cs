using Re_Backend.Domain.UserDomain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Re_Backend.Tests.UserR
{
    public class TestUserService : IClassFixture<TestFixture>
    {
        private readonly IUserService _userService;
        private readonly ITestOutputHelper _testOutput;

        public TestUserService(TestFixture fixture, ITestOutputHelper testOutput)
        {
            _userService = fixture.UserService;
            _testOutput = testOutput;
        }

        [Fact]
        public async Task TestPage()
        {
            List<Domain.UserDomain.Entity.User> users = await _userService.GetUserPages(1,10);
            foreach (var item in users)
            {
                _testOutput.WriteLine(item.UserName);
            }
        }
    }
}
