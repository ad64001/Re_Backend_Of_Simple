using Re_Backend.Domain.CommonDomain.Entity;

namespace Re_Backend.Domain.CommonDomain.IRespository
{
    public interface IPagesRespository
    {
        public Task<int> AddPage(Page page);
        public Task<List<Page>> GetAllPages();
        public Task<Page> GetPagesByIndex(int index);
    }
}
