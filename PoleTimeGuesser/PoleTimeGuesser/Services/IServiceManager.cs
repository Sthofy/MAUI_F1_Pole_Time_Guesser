namespace PoleTimeGuesser.Services
{
    public interface IServiceManager
    {
        Task<HttpResponseMessage> Authenticate(AuthenticateRequest request);
        Task<HttpResponseMessage> CallWebAPI<T>(string apiUrl, HttpMethod httpMethod, T request);
        Task<HttpResponseMessage> Registration(RegistrationRequest request);
        Task<HttpResponseMessage> UpdateUser(UpdateUserRequest request);
    }
}