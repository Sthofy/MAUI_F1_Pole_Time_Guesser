﻿namespace F1GuessAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AvatarSourceName { get; set; } = null!;
    }
}
