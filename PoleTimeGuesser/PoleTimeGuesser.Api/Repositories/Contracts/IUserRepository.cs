﻿using PoleTimeGuesser.Library.Models;

namespace PoleTimeGuesser.Api.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<LoggedInUserModel> Authenticate(string username, string password);
        Task<RegistrationModel> Registration(string username, string email, string password);
    }
}
