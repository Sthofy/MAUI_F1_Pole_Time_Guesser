using PoleTimeGuesser.Library.Requests;

namespace PoleTimeGuesser.Services
{
    public interface IServiceManager
    {
        Task<HttpResponseMessage> Authenticate(AuthenticateRequest request);
        Task<TResponse> CallWebAPI<TRequest, TResponse>(string apiUrl, HttpMethod httpMethod, TRequest request) where TResponse : BaseResponse;
        Task<HttpResponseMessage> Registration(RegistrationRequest request);
        Task<UpdateResponse> UpdateUser(UpdateRequest request);
    }
}