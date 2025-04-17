using Newtonsoft.Json;
using Re_Backend.Common;
using Re_Backend.Common.Attributes;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.Entity.Vo;
using Re_Backend.Domain.UserDomain.IRespository;
using Re_Backend.Domain.UserDomain.IServices;


namespace Re_Backend.Application.LoginApplication.Services
{
    [Injectable]
    public class LoginService : ILoginService
    {
        private readonly IUserRespository _userRespository;
        private readonly IRolesRespository _rolesRespository;
        private readonly IJwtService _jwtService;

        public LoginService(IUserRespository userRespository,IRolesRespository rolesRespository,IJwtService jwtService)
        {
            _userRespository = userRespository;
            _rolesRespository = rolesRespository;
            _jwtService = jwtService;
        }


        public async Task<string> Login(User user)
        {
            try
            {
                User loginuser = new User();
                user.Password = AESAlgorithm.EncryptString(user.Password);
                var loginusers = await _userRespository.QueryAllUser();
                if (!string.IsNullOrEmpty(user.Email) && user.Email != "null")
                {
                    loginuser = loginusers.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
                }
                else if (!string.IsNullOrEmpty(user.UserName) && user.UserName != "null")
                {
                    loginuser = loginusers.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();

                }

                if (loginuser == null)
                {
                    return "UserNotInDatabase";
                }
                return _jwtService.GenerateToken(loginuser.Id.ToString());

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
            if (user.NickName == null||string.IsNullOrEmpty(user.NickName))
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
