using Re_Backend.Domain.CommonDomain.Entity;
using Re_Backend.Domain.UserDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.CommonDomain.IServices
{
    public interface IPageService
    {
        public Task<List<Page>> GetPages(string roleName);
        public Task<int> CreatePage(Page page);

    }
}
