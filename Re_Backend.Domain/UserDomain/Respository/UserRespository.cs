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
            _user.UserName = "DELETE_" + _user.UserName;
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

        public async Task<User> QueryUserByUser(User user)
        {
            var query = _db.Db.Queryable<User>();

            // 根据 User 对象的非空属性进行过滤
            if (!string.IsNullOrEmpty(user.UserName))
            {
                query = query.Where(u => u.UserName == user.UserName);
            }
            if (!string.IsNullOrEmpty(user.Password))
            {
                query = query.Where(u => u.Password == user.Password);
            }
            if (!string.IsNullOrEmpty(user.Email))
            {
                query = query.Where(u => u.Email == user.Email);
            }
            if (user.RoleId > 0)
            {
                query = query.Where(u => u.RoleId == user.RoleId);
            }
            // 可以根据需要添加更多属性的过滤条件

            return await query.FirstAsync();
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
