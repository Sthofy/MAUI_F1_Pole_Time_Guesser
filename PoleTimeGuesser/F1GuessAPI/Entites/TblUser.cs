namespace F1GuessAPI.Entites
{
    public class TblUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte[] StoredSalt { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AvatarSourceName { get; set; } = null!;
    }
}
