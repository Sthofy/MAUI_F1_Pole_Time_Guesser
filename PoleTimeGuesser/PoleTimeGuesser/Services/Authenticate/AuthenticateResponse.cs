namespace PoleTimeGuesser.Services.Authenticate
{
    public class AuthenticateResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AvatarSourceName { get; set; }
    }
}
