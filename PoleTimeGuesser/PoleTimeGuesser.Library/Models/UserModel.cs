namespace PoleTimeGuesser.Library.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] StoredSalt { get; set; }
        public string Email { get; set; }
        public string AvatarSourceName { get; set; }
    }
}
