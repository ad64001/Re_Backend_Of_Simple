using Re_Backend.Domain.UserDomain.Entity;

namespace Re_Backend.Domain.UserDomain.IRespository
{
    public interface IRolesRespository
    {
        public Task<int> AddRole(Role role);
        public Task<List<Role>> QueryAllRole();
        public Task<Role> QueryRoleById(int id);
        public Task<bool> UpdateRole(Role role);
    }
}
