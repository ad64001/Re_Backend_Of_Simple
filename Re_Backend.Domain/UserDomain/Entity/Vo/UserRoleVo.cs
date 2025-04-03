using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.UserDomain.Entity.Vo
{
    public class UserRoleVo
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User UserV { get; set; }
        public Role RoleV { get; set; }

    }
}
