using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.UserDomain.Entity
{
    [SugarTable("Users")] // 指定表名（可选）
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 50)]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastLoginTime { get; set; }

        public bool IsDeleted { get; set; }
        // 关联 Role 的 Id
        public int RoleId { get; set; }
    }
}
