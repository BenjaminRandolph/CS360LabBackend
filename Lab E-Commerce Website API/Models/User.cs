namespace Lab_E_Commerce_Website_API.Models
{
    // The model for user accounts
    // This is how we access user accounts in the database for account permissions and data
    public class User
    {
        public int ID { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public double Funds { get; set; }

        public bool AdminPermission { get; set; }
    }
}
