﻿namespace F1GuessAPI.Functions.User
{
    public interface IUserFunctions
    {
        UserModel? Authenticate(string username, string password);
        Task<UserModel> Registration(string username, string email, string password);
        Task<UserModel> Update(int id, string username, string email, string password);
    }
}