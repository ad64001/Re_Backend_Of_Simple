using Re_Backend.Common;
using Re_Backend.Common.Attributes;
using Re_Backend.Common.SqlConfig;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.IRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.UserDomain.Respository
{
    [Injectable]
    public class UserRespository : IUserRespository
    {
        private readonly DbContext _db;

        public UserRespository(DbContext db)
        {
            _db = db;
        }

        [UseTran]
        public async Task<int> AddUser(User user)
        {
            int byid = await _db.Db.Insertable<User>(user).ExecuteReturnIdentityAsync();
            return byid;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var _user = await _db.Db.Queryable<User>().InSingleAsync(id);
            _user.IsDeleted = true;
            return await _db.Db.Updateable<User>(_user).ExecuteCommandAsync() > 0;
        }

        public async Task<List<User>> QueryAllUser()
        {
            List<User> list = await _db.Db.Queryable<User>().ToListAsync();
            return list;
        }

        public async Task<User> QueryUserById(int id)
        {
            var user = await _db.Db.Queryable<User>().InSingleAsync(id);
            return user;
        }

        [UseTran]
        public async Task<bool> UpdateUser(User user)
        {
            var _user = await _db.Db.Queryable<User>().InSingleAsync(user.Id);
            GlobalEntityUpdater.UpdateEntity(_user,user);
            return await _db.Db.Updateable<User>(_user).ExecuteCommandAsync() > 0;
        }

        
    }
}
