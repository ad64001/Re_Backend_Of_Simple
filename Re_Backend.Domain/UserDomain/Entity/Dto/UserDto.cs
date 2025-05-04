namespace Re_Backend.Domain.UserDomain.Entity.Dto
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? NickName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
