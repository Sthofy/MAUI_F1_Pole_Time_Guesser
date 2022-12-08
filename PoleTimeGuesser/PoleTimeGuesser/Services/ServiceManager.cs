namespace PoleTimeGuesser.Services
{
    public class ServiceManager : IServiceManager
    {
        DevHttpsConnectionHelper _devSslHelper;
        string Url = "https://f1guessapi.azurewebsites.net";
        private readonly ISharedData _sharedData;

        public ServiceManager(ISharedData sharedData)
        {
            //_devSslHelper = new DevHttpsConnectionHelper(sslPort: 7297);
            _devSslHelper = new DevHttpsConnectionHelper(sslPort: 7200);

            // Need to uncomment if you are use emulator
            //Url = _devSslHelper.DevServerRootUrl;
            _sharedData = sharedData;
        }

        public async Task<HttpResponseMessage> Registration(RegistrationRequest request)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri(Url + "/User/Registration");

            if (request is not null)
            {
                string jsonContent = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json");
                httpRequestMessage.Content = httpContent;
            }

            var response = await _devSslHelper.HttpClient.SendAsync(httpRequestMessage);

            return response;
        }

        public async Task<HttpResponseMessage> Authenticate(AuthenticateRequest request)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri(Url + $"/User/Authenticate/{request.Username}/{request.Password}");

            if (request is not null)
            {
                string jsonContent = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json");
                httpRequestMessage.Content = httpContent;
            }

            var response = await _devSslHelper.HttpClient.SendAsync(httpRequestMessage);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateUser(UpdateUserRequest request)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Put;
            httpRequestMessage.RequestUri = new Uri(Url + "/User/UpdateUser");
            httpRequestMessage.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _sharedData.Token);

            if (request is not null)
            {
                string jsonContent = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json");
                httpRequestMessage.Content = httpContent;
            }

            var response = await _devSslHelper.HttpClient.SendAsync(httpRequestMessage);

            return response;
        }

        public async Task<HttpResponseMessage> CallWebAPI<TRequest>(string apiUrl, HttpMethod httpMethod, TRequest request)
        {

            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = httpMethod;
            httpRequestMessage.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _sharedData.Token);

            if (httpMethod == HttpMethod.Get)
            {
                httpRequestMessage.RequestUri = new Uri(Url + apiUrl + $"/{request}");
            }
            else
            {
                httpRequestMessage.RequestUri = new Uri(Url + apiUrl);
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
            }

            var response = await _devSslHelper.HttpClient.SendAsync(httpRequestMessage);

            return response;
        }
    }
}
