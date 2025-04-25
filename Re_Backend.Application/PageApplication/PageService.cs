using Re_Backend.Common.Attributes;
using Re_Backend.Domain.CommonDomain.Entity;
using Re_Backend.Domain.CommonDomain.IRespository;
using Re_Backend.Domain.CommonDomain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Application.PageApplication
{
    [Injectable]
    public class PageService : IPageService
    {
        private readonly IPagesRespository _pageRepository;

        public PageService(IPagesRespository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        public async Task<int> CreatePage(Page page)
        {
            return await _pageRepository.AddPage(page);
        }

        public async Task<List<Page>> GetPages(string roleName)
        {
            List<Page> pages = await _pageRepository.GetAllPages();
            return pages.Where(p => roleName == "Admin"
                           || roleName == "SuperAdmin"
                           || p.Index != 2).ToList();
        }
    }
}
