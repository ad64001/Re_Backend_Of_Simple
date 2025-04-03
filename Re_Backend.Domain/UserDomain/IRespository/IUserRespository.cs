using Re_Backend.Domain.UserDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.UserDomain.IRespository
{
    public interface IUserRespository
    {
        public Task<int> AddUser(User user);
        public Task<List<User>> QueryAllUser();
        public Task<User> QueryUserById(int id);
    }
}
