﻿namespace F1GuessAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; } = 1;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte[] StoredSalt { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AvatarSourceName { get; set; } = null!;
    }
}
