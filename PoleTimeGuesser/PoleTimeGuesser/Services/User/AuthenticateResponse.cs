namespace PoleTimeGuesser.Services.User
{
    public class AuthenticateResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AvatarSourceName { get; set; } = null!;
    }
}
