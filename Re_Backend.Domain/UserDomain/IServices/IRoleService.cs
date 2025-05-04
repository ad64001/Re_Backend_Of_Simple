using Re_Backend.Domain.UserDomain.Entity;

namespace Re_Backend.Domain.UserDomain.IServices
{
    public interface IRoleService
    {
        public Task<List<Role>> GetRoles();
    }
}
