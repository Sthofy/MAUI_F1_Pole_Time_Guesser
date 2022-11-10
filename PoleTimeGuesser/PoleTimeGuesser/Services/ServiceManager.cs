﻿using PoleTimeGuesser.Services.UpdateUser;

namespace PoleTimeGuesser.Services
{
    public class ServiceManager : IServiceManager
    {
        DevHttpsConnectionHelper _devSslHelper;
        string Url = "https://f1infoapi.azurewebsites.net";

        public ServiceManager()
        {
            _devSslHelper = new DevHttpsConnectionHelper(sslPort: 7297);
        }

        public async Task<RegistrationResponse> Registration(RegistrationRequest request)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            //httpRequestMessage.RequestUri = new Uri(Url + "/Registration");
            httpRequestMessage.RequestUri = new Uri(_devSslHelper.DevServerRootUrl + "/Registration");

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

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            //httpRequestMessage.RequestUri = new Uri(Url + "/Authenticate");
            httpRequestMessage.RequestUri = new Uri(_devSslHelper.DevServerRootUrl + "/Authenticate");

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

                var result = JsonConvert.DeserializeObject<AuthenticateResponse>(responseContent);
                result.StatusCode = (int)response.StatusCode;

                if (result.StatusCode == 200)
                {

                }

                return result;
            }
            catch (Exception ex)
            {
                var result = new AuthenticateResponse
                {
                    StatusCode = 500,
                    StatusMessage = ex.Message
                };
                return result;
            }
        }

        public async Task<UpdateResponse> UpdateUser(UpdateRequest request)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Put;
            //httpRequestMessage.RequestUri = new Uri(Url + "/UpdateUser");
            httpRequestMessage.RequestUri = new Uri(_devSslHelper.DevServerRootUrl + "/UpdateUser");

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

        public async Task<List<QuestionModel>> GetQuestions()
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Get;
            //httpRequestMessage.RequestUri = new Uri(Url + "/Game/Questions");
            httpRequestMessage.RequestUri = new Uri(_devSslHelper.DevServerRootUrl + "/Game/Questions");

            try
            {
                var response = await _devSslHelper.HttpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<QuestionModel>>(responseContent);

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<TResponse> CallWebAPI<TRequest, TResponse>(string apiUrl, HttpMethod httpMethod, TRequest request) where TResponse : BaseResponse
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = httpMethod;
            httpRequestMessage.RequestUri = new Uri(_devSslHelper.DevServerRootUrl + apiUrl);

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
