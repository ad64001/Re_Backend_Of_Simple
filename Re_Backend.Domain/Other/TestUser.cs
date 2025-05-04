using SqlSugar;

namespace Re_Backend.Domain.Other
{
    //[SugarTable("TestUser")]
    public class TestUser
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
