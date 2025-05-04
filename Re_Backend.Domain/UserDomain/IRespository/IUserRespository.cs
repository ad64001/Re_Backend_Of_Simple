using Re_Backend.Common.OtherEntity;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.Entity.Dto;

namespace Re_Backend.Domain.UserDomain.IRespository
{
    public interface IUserRespository
    {
        public Task<int> AddUser(User user);
        public Task<List<User>> QueryAllUser();
        public Task<User> QueryUserById(int id);
        public Task<bool> UpdateUser(User user);
        public Task<bool> DeleteUser(int id);
        public Task<List<User>> QueryUserPages(int size, int page);
        public Task<PageResult<User>> QueryUsersByDto(UserDto dto, int pageNumber = 1, int pageSize = 10);
    }
}
