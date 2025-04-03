using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.IRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Re_Backend.Tests
{
    public class TestUserRespository : IClassFixture<TestFixture>
    {
        private readonly ITestOutputHelper _testOutput;
        private readonly IUserRespository _userRespository;

        public TestUserRespository(TestFixture fixture, ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
            _userRespository = fixture.UserRespository;
        }

        [Fact]
        public async Task TestAdd()
        {
            User testUser = new User
            {
                UserName = "TestUser",
                Password = "TestPassword123",
                Email = "testuser@example.com",
                CreateTime = DateTime.Now,
                LastLoginTime = DateTime.Now.AddDays(-1),
                IsDeleted = false,
                RoleId = 1
            };

            int id = await _userRespository.AddUser(testUser);

            _testOutput.WriteLine($"User added with ID: {id}");
        }

        [Fact]
        public async Task TestGetUserAll()
        {
            var users = await _userRespository.QueryAllUser();
            foreach (var item in users)
            {
                _testOutput.WriteLine($"User: {item.UserName}, ID: {item.Id}");
            }
        }

        [Fact]
        public async Task TestGetUserById()
        {
            var user = await _userRespository.QueryUserById(1);
            _testOutput.WriteLine($"User: {user.UserName}, ID: {user.Id}");
        }
    }
}
