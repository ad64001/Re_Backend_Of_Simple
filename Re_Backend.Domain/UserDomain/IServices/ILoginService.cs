using Re_Backend.Domain.UserDomain.Entity;

namespace Re_Backend.Domain.UserDomain.IServices
{
    public interface ILoginService
    {
        public Task<string> Login(User user);
        public Task<string> Register(User user);
    }
}
