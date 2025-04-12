using Re_Backend.Common.Attributes;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.Entity.Vo;
using Re_Backend.Domain.UserDomain.IRespository;
using Re_Backend.Domain.UserDomain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Application.LoginApplication.Services
{
    [Injectable]
    public class UserService: IUserService
    {
        private readonly IUserRespository _userRespository;
        private readonly IRolesRespository _rolesRespository;

        public UserService(IUserRespository userRespository, IRolesRespository rolesRespository)
        {
            _userRespository = userRespository;
            _rolesRespository = rolesRespository;
        }

        public async Task<UserRoleVo> GetUserInfo(int id)
        {
            var userResult = await _userRespository.QueryUserById(id);
            if (userResult != null)
            {
                Role roleResult = await _rolesRespository.QueryRoleById(userResult.RoleId);
                if (roleResult != null)
                {
                    UserRoleVo userRoleResult = new UserRoleVo()
                    {
                        UserId = userResult.Id,
                        UserV = userResult,
                        RoleId = roleResult.Id,
                        RoleV = roleResult
                    };
                    return userRoleResult;
                }
                else
                {
                    return new UserRoleVo();
                }
            }
            else
            {
                return new UserRoleVo();
            }
        }

        public async Task<bool> UpdateUserInfo(User user)
        {
            return await _userRespository.UpdateUser(user);
        }
    }
}
