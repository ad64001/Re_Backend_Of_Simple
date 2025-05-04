using Re_Backend.Common;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.Entity.Dto;
using Re_Backend.Domain.UserDomain.Entity.Vo;

namespace Re_Backend.Domain.UserDomain.IServices
{
    public interface IUserService
    {
        public Task<UserRoleVo> GetUserInfo(int id);
        public Task<bool> UpdateUserInfo(User user);
        public Task<PageResult<User>> GetUserPages(int page, int size);
        public Task DeleteByid(int id);
        public Task<PageResult<User>> QueryUserInfoPages(UserDto userDto, int page, int size);
    }
}
