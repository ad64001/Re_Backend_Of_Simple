using Re_Backend.Common;
using Re_Backend.Common.Attributes;
using Re_Backend.Common.SqlConfig;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.IRespository;

namespace Re_Backend.Domain.UserDomain.Respository
{
    [Injectable]
    public class RolesRespository : IRolesRespository
    {
        private readonly DbContext _db;

        public RolesRespository(DbContext db)
        {
            _db = db;
        }

        [UseTran]
        public async Task<int> AddRole(Role role)
        {
            int byid = await _db.Db.Insertable<Role>(role).ExecuteReturnIdentityAsync();
            return byid;
        }

        public async Task<List<Role>> QueryAllRole()
        {
            List<Role> list = await _db.Db.Queryable<Role>().ToListAsync();
            return list;
        }

        public async Task<Role> QueryRoleById(int id)
        {
            var role = await _db.Db.Queryable<Role>().InSingleAsync(id);
            return role;
        }

        [UseTran]
        public async Task<bool> UpdateRole(Role role)
        {
            var _role = await _db.Db.Queryable<Role>().InSingleAsync(role.Id);
            GlobalEntityUpdater.UpdateEntity(_role, role);
            return await _db.Db.Updateable<Role>(_role).ExecuteCommandAsync() > 0;
        }
    }
}
