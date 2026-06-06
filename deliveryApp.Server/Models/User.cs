namespace deliveryApp.Server.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual Role Role { get; set; } = null!;
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        //Navigation property for the related Users
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
