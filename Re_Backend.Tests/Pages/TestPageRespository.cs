using Re_Backend.Domain.CommonDomain.Entity;
using Re_Backend.Domain.CommonDomain.IRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Re_Backend.Tests.Pages
{
    public class TestPageRespository : IClassFixture<TestFixture>
    {
        private readonly IPagesRespository _pagesRespository;
        private readonly ITestOutputHelper _output;

        public TestPageRespository(TestFixture fixture, ITestOutputHelper testOutput)
        {
            _pagesRespository = fixture.PagesRespository;
            _output = testOutput;
        }

        [Fact]
        public async Task TestAdd()
        {
            //Page page = new Page() 
            //{
            //    Path = "/home",
            //    Name = "home",
            //    Component = "@/views/HomeView.vue",
            //    Title = "首页",
            //    ShowInSidebar = true,
            //    Tag = "默认"
            //};
            Page page = new Page()
            {
                Path = "/defaultUser",
                Name = "user",
                Component = "@/views/UserView.vue",
                Title = "用户",
                ShowInSidebar = true,
                Tag = "默认"
            };
            await _pagesRespository.AddPage(page);
        }
    }
}
