using Re_Backend.Common;
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

        public async Task DeleteByid(int id)
        {
            var userResult = await _userRespository.QueryUserById(id);

            if (!userResult.IsDeleted)
            {
                await _userRespository.DeleteUser(id);
            }
            
        }

        public async Task<int> GetUserCount()
        {
            List<User> users = await _userRespository.QueryAllUser();
            return users.Count;
        }

        public async Task<UserRoleVo> GetUserInfo(int id)
        {
            var userResult = await _userRespository.QueryUserById(id);
            if (userResult != null)
            {
                userResult.Password = AESAlgorithm.DecryptString(userResult.Password);
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

        public async Task<List<User>> GetUserPages(int page, int size)
        {
            return await _userRespository.QueryUserPages(page, size);
        }

        public async Task<bool> UpdateUserInfo(User user)
        {

            if (!string.IsNullOrEmpty(user.Password) && user.Password != "null")
            {
                user.Password = AESAlgorithm.EncryptString(user.Password);
            }

            User userResult = await _userRespository.QueryUserById(user.Id);
            user.CreateTime = userResult.CreateTime;
            user.LastLoginTime = userResult.LastLoginTime;
            return await _userRespository.UpdateUser(user);
        }
    }
}
