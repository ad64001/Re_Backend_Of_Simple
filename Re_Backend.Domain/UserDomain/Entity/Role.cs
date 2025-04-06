using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.UserDomain.Entity
{
    [SugarTable("Roles")] // 指定表名（可选）
    public class Role
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 50)]
        public string Name { get; set; }

        // 其他字段...
        public int Weight { get; set; }
    }
}
