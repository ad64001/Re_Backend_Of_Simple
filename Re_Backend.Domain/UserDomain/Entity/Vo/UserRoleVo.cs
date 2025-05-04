namespace Re_Backend.Domain.UserDomain.Entity.Vo
{
    public class UserRoleVo
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User UserV { get; set; }
        public Role RoleV { get; set; }

    }
}
