using Re_Backend.Domain.CommonDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.CommonDomain.IRespository
{
    public interface IPagesRespository
    {
        public Task<int> AddPage(Page page);
        public Task<List<Page>> GetAllPages();
        public Task<Page> GetPagesByIndex(int index);
    }
}
