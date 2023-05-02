using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// role enum
    /// user 1 - n reviewer 
    /// user 1 - 1 cart 
    /// user 1 - n wish list
    /// user 1 - n order
    /// </summary>
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public decimal CreditBalance { get; set; } = 0;
        public Role Role { get; set; } = Role.User;

    }
}
