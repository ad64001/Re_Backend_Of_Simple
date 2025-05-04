using Re_Backend.Domain.CommonDomain.Entity;

namespace Re_Backend.Domain.CommonDomain.IServices
{
    public interface IPageService
    {
        public Task<List<Page>> GetPages(string roleName);
        public Task<int> CreatePage(Page page);

    }
}
