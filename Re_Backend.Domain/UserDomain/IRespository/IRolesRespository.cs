using Re_Backend.Domain.UserDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.UserDomain.IRespository
{
    public interface IRolesRespository
    {
        public Task<int> AddRole(Role role);
        public Task<List<Role>> QueryAllRole();
        public Task<Role> QueryRoleById(int id);
        public Task<bool> UpdateRole(Role role);
    }
}
