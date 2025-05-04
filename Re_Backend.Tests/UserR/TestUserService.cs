using Re_Backend.Domain.UserDomain.Entity.Dto;
using Re_Backend.Domain.UserDomain.IServices;
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
            var result = await _userService.QueryUserInfoPages(new UserDto
            {
                Email = "@qq.com",
                RoleId = 1
            }, 1, 10);
            foreach (var item in result.Data)
            {
                _testOutput.WriteLine(item.UserName);
            }
        }
    }
}
