﻿namespace PoleTimeGuesser.Model
{
    public interface ISharedData
    {
        string Username { get; set; }
        string AvatarSourceName { get; set; }
        string Email { get; set; }
    }
}