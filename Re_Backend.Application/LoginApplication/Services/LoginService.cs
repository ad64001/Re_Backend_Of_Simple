using Newtonsoft.Json;
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
    }
}
