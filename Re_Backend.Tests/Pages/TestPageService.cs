using Re_Backend.Domain.CommonDomain.IServices;
using Xunit.Abstractions;

namespace Re_Backend.Tests.Pages
{
    public class TestPageService : IClassFixture<TestFixture>
    {
        private readonly IPageService _pageService;
        private readonly ITestOutputHelper _output;

        public TestPageService(ITestOutputHelper output, TestFixture fixture)
        {
            _pageService = fixture.PageService;
            _output = output;
        }

        [Fact]
        public async Task GetPages()
        {
            var pages = await _pageService.GetPages("User");
            foreach (var item in pages)
            {
                _output.WriteLine(item.Name);
            }
        }
    }
}
