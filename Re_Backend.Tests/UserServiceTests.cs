using Re_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Re_Backend.Tests
{
    public class UserServiceTests : IClassFixture<TestFixture>
    {
        private readonly ITestUserService userService;
        private readonly ITestOutputHelper testOutput;

        public UserServiceTests(TestFixture fixture, ITestOutputHelper testOutput)
        {
            this.userService = fixture.UserService;
            this.testOutput = testOutput;
        }

        [Fact]
        public void TestAdd()
        {
            var user = new TestUser
            {
                Name = "Test User",
                Age = 25
            };
            userService.AddUser(user);
            var allUsers = userService.GetAllUsers();
            Assert.Contains(allUsers, u => u.Name == user.Name && u.Age == user.Age);
            foreach (var item in allUsers)
            {
                testOutput.WriteLine(item.Name);
            }
        }

        [Fact]
        public void TestAll()
        {
            var allUsers = userService.GetAllUsers();
            foreach (var item in allUsers)
            {
                testOutput.WriteLine(item.Name);
            }
        }

        [Fact]
        public void TestTran()
        {
            userService.DoSomethingWithTransaction();
            var result = userService.GetDbContext().Db.Queryable<dynamic>();
            Assert.NotNull(result);
        }
    }
}
