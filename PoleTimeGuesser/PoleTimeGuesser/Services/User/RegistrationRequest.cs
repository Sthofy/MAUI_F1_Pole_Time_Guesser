﻿namespace PoleTimeGuesser.Services.User
{
    public class RegistrationRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}