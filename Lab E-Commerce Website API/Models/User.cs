namespace Lab_E_Commerce_Website_API.Models
{
    // The model for user accounts
    // This is how we access user accounts in the database for account permissions and data
    public class User
    {
        public int id { get; set; }

        public string? username { get; set; }

        public string? password { get; set; }

        public string? phonenumber { get; set; }

        public string? address { get; set; }

        public string? email { get; set; }

        public decimal funds { get; set; }

        public bool adminpermission { get; set; }
    }
}
