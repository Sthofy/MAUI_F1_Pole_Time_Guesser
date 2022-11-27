using PoleTimeGuesser.Library.Requests;

namespace PoleTimeGuesser.Services
{
    public class ServiceManager : IServiceManager
    {
        DevHttpsConnectionHelper _devSslHelper;
        string Url = "https://f1guessapi.azurewebsites.net";

        public ServiceManager()
        {
            //_devSslHelper = new DevHttpsConnectionHelper(sslPort: 7297);
            _devSslHelper = new DevHttpsConnectionHelper(sslPort: 7200);
        }

        public async Task<HttpResponseMessage> Registration(RegistrationRequest request)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri(Url + "/User/Registration");
            //httpRequestMessage.RequestUri = new Uri(_devSslHelper.DevServerRootUrl + "/User/Registration");

            if (request is not null)
            {
                string jsonContent = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json");
                httpRequestMessage.Content = httpContent;
            }

            try
            {
                var response = await _devSslHelper.HttpClient.SendAsync(httpRequestMessage);
                var responseConcent = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<RegistrationResponse>(responseConcent);
                result.StatusCode = (int)response.StatusCode;

                return result;
            }
            catch (Exception ex)
            {
                var result = new RegistrationResponse
                {
                    StatusCode = 500,
                    StatusMessage = ex.Message
                };

                return result;
            }
        }

        public async Task<HttpResponseMessage> Authenticate(AuthenticateRequest request)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Get;
            //httpRequestMessage.RequestUri = new Uri(Url + $"/User/Authenticate/{request.Username}/{request.Password}");
            httpRequestMessage.RequestUri = new Uri(_devSslHelper.DevServerRootUrl + $"/User/Authenticate/{request.Username}/{request.Password}");

            var response = await _devSslHelper.HttpClient.SendAsync(httpRequestMessage);

            return response;
        }

        public async Task<UpdateResponse> UpdateUser(UpdateRequest request)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Put;
            httpRequestMessage.RequestUri = new Uri(Url + "/User/UpdateUser");
            //httpRequestMessage.RequestUri = new Uri(_devSslHelper.DevServerRootUrl + "/User/UpdateUser");

            if (request is not null)
            {
                string jsonContent = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json");
                httpRequestMessage.Content = httpContent;
            }

            try
            {
                var response = await _devSslHelper.HttpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<UpdateResponse>(responseContent);
                result.StatusCode = (int)response.StatusCode;

                return result;
            }
            catch (Exception ex)
            {
                var result = new UpdateResponse
                {
                    StatusCode = 500,
                    StatusMessage = ex.Message
                };
                return result;
            }
        }

        public async Task<TResponse> CallWebAPI<TRequest, TResponse>(string apiUrl, HttpMethod httpMethod, TRequest request) where TResponse : BaseResponse
        {

            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = httpMethod;
            httpRequestMessage.RequestUri = new Uri(Url + apiUrl);
            //httpRequestMessage.RequestUri = new Uri(_devSslHelper.DevServerRootUrl + apiUrl);

            if (request != null)
            {
                string jsonContent = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json"); ;
                httpRequestMessage.Content = httpContent;
            }
            else
            {
                httpRequestMessage.Content = null;
            }

            try
            {
                var response = await _devSslHelper.HttpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TResponse>(responseContent);
                result.StatusCode = (int)response.StatusCode;

                return result;
            }
            catch (Exception ex)
            {
                var result = Activator.CreateInstance<TResponse>();
                result.StatusCode = 500;
                result.StatusMessage = ex.Message;
                return result;
            }
        }
    }
}
