using Re_Backend.Common;
using Re_Backend.Common.Attributes;
using Re_Backend.Common.SqlConfig;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.Entity.Dto;
using Re_Backend.Domain.UserDomain.IRespository;

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

        public async Task<List<User>> QueryUserPages(int size, int page)
        {
            List<User> list = await _db.Db.Queryable<User>().ToPageListAsync(page, size);
            return list;
        }

        public async Task<PageResult<User>> QueryUsersByDto(UserDto dto, int pageNumber = 1, int pageSize = 10)
        {
            // 参数校验和分页设置
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var query = _db.Db.Queryable<User>();

            // 动态条件处理
            if (!string.IsNullOrEmpty(dto.UserName))
                query = query.Where(u => u.UserName.Contains(dto.UserName));

            if (!string.IsNullOrEmpty(dto.NickName))
                query = query.Where(u => u.NickName.Contains(dto.NickName));

            if (!string.IsNullOrEmpty(dto.Email))
                query = query.Where(u => u.Email.Contains(dto.Email));

            if (dto.RoleId.HasValue)
                query = query.Where(u => u.RoleId == dto.RoleId.Value);

            // 执行分页查询
            var totalCount = query.Count();
            var data = await query.ToPageListAsync(pageNumber, pageSize);

            return new PageResult<User>
            {
                Data = data,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        [UseTran]
        public async Task<bool> UpdateUser(User user)
        {
            var _user = await _db.Db.Queryable<User>().InSingleAsync(user.Id);
            GlobalEntityUpdater.UpdateEntity(_user, user);
            return await _db.Db.Updateable<User>(_user).ExecuteCommandAsync() > 0;
        }


    }
}
