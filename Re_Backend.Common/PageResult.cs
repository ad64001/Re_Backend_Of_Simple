namespace Re_Backend.Common
{
    public class PageResult<T>
    {
        public List<T>? Data { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
