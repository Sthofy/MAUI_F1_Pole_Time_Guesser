﻿namespace F1GuessAPI.Controllers.User
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}