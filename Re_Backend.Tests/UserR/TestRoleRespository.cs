using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.IRespository;
using Xunit.Abstractions;

namespace Re_Backend.Tests.UserR
{
    public class TestRoleRespository : IClassFixture<TestFixture>
    {
        private readonly ITestOutputHelper _testOutput;
        private readonly IRolesRespository _roleRespository;

        public TestRoleRespository(TestFixture fixture, ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
            _roleRespository = fixture.RoleRespository;
        }

        [Fact]
        public async Task TestAdd()
        {
            Role testRole = new Role
            {
                Name = "User",
                Weight = 3
            };

            int id = await _roleRespository.AddRole(testRole);

            _testOutput.WriteLine($"User added with ID: {id}");
        }

        [Fact]
        public async Task TestGetUserAll()
        {
            var users = await _roleRespository.QueryAllRole();
            foreach (var item in users)
            {
                _testOutput.WriteLine($"User: {item.Name}, ID: {item.Id}");
            }


        }

        [Fact]
        public async Task TestGetUserById()
        {
            var role = await _roleRespository.QueryRoleById(1);
            _testOutput.WriteLine($"User: {role.Name}, ID: {role.Id}");
        }

        [Fact]
        public async Task TestUpdateUser()
        {
            Role testRole = new Role
            {
                Id = 1,
                Name = "Test RolAse",
                Weight = 10086
            };

            _testOutput.WriteLine($"User updated with bool: {await _roleRespository.UpdateRole(testRole)}");
        }
    }
}
