using PoleTimeGuesser.Services.UpdateUser;

namespace PoleTimeGuesser.Services
{
    public interface IServiceManager
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
        Task<TResponse> CallWebAPI<TRequest, TResponse>(string apiUrl, HttpMethod httpMethod, TRequest request) where TResponse : BaseResponse;
        Task<List<QuestionModel>> GetQuestions();
        Task<RegistrationResponse> Registration(RegistrationRequest request);
        Task<UpdateResponse> UpdateUser(UpdateRequest request);
    }
}