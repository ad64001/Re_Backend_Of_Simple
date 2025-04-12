using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.Entity.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.UserDomain.IServices
{
    public interface IUserService
    {
        public Task<UserRoleVo> GetUserInfo(int id);
        public Task<bool> UpdateUserInfo(User user);
    }
}
