using SqlSugar;

namespace Re_Backend.Domain.CommonDomain.Entity
{
    [SugarTable("Pages")]
    public class Page
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Index { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Component { get; set; }
        public string Title { get; set; }
        public bool ShowInSidebar { get; set; }
        public string Tag { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
