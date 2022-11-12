namespace F1GuessAPI.Controllers.User
{
    public class AuthenticateRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
