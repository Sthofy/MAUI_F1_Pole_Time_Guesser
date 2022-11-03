﻿using PoleTimeGuesser.Services.UpdateUser;

namespace PoleTimeGuesser.Services
{
    public interface IServiceManager
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
        Task<RegistrationResponse> Registration(RegistrationRequest request);
        Task<UpdateResponse> UpdateUser(UpdateRequest request);
    }
}