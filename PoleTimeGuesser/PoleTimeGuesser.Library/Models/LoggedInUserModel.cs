namespace PoleTimeGuesser.Library.Models
{
    public class LoggedInUserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string AvatarSourceName { get; set; }
    }
}
