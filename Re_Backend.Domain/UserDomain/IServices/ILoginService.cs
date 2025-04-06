using Re_Backend.Domain.UserDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.UserDomain.IServices
{
     public interface ILoginService
    {
        public Task<string> Login(User user);
    }
}
