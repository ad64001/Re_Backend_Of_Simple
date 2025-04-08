using Newtonsoft.Json;
using Re_Backend.Common;
using Re_Backend.Common.Attributes;
using Re_Backend.Domain.UserDomain.Entity;
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
    public class LoginService : ILoginService
    {
        private readonly IUserRespository _userRespository;
        private readonly IRolesRespository _rolesRespository;

        public LoginService(IUserRespository userRespository,IRolesRespository rolesRespository)
        {
            _userRespository = userRespository;
            _rolesRespository = rolesRespository;
        }
        public async Task<string> Login(User user)
        {
            try
            {
                var loginuser = await _userRespository.QueryUserByUser(user);
                if (loginuser == null)
                {
                    return "UserNotInDatabase";
                }
                return loginuser.UserName.ToString();

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [UseTran]
        public async Task<string> Register(User user)
        {
            User newUser = new User();
            if (user.Email == null)
            {
                return "Email can't null";
            }
            if (user.UserName == null)
            {
                return "username can't null";
            }
            if (user.Password.Length < 7)
            {
                return "passwd length error";
            }
            if (user.NickName == null)
            {
                user.NickName = "Te_"+ AESAlgorithm.EncryptString(user.UserName);
            }

            GlobalEntityUpdater.UpdateEntity(newUser, user);
            newUser.CreateTime = DateTime.Now;
            newUser.LastLoginTime = DateTime.Now.AddDays(-1);
            newUser.Password = AESAlgorithm.EncryptString(user.Password);
            newUser.IsDeleted = false;
            newUser.RoleId = 3;
            int result = await _userRespository.AddUser(newUser);
            return result.ToString();
        }
    }
}
