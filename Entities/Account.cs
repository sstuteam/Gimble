namespace Entities
{
    public class Account
    {
        public byte [] Avatar { get; set; }

        public System.Guid Id { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}