
namespace SafeVault.Models
{
    public class User
    {
        // User Information.
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Stored Roles.
        public List<string> Roles { get; set; } = new List<string>();
    }
}