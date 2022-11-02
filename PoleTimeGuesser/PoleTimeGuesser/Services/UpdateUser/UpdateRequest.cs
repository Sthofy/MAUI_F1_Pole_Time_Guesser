namespace PoleTimeGuesser.Services.UpdateUser
{
    public class UpdateRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
