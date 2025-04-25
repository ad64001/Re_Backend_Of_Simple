using Re_Backend.Common.Attributes;
using Re_Backend.Common.SqlConfig;
using Re_Backend.Domain.CommonDomain.Entity;
using Re_Backend.Domain.CommonDomain.IRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.CommonDomain.Respository
{
    [Injectable]
    public class PagesRespository : IPagesRespository
    {
        private readonly DbContext _db;

        public PagesRespository(DbContext db)
        {
            _db = db;
        }

        [UseTran]
        public async Task<int> AddPage(Page page)
        {
            int byindex = await _db.Db.Insertable(page).ExecuteCommandAsync();
            return byindex;
        }

        public async Task<List<Page>> GetAllPages()
        {
            return await _db.Db.Queryable<Page>().ToListAsync();
        }

        public async Task<Page> GetPagesByIndex(int index)
        {
            return await _db.Db.Queryable<Page>().InSingleAsync(index);
        }
    }
}
