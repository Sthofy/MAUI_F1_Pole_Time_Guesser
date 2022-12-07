using Newtonsoft.Json;
using PoleTimeGuesser.Library.Models;
using PoleTimeGuesser.Library.Requests;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace PoleTimeGuesser.Web.ViewModel
{
    public class IndexViewModel : IIndexViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        private readonly ISharedData _sharedData;
        private readonly HttpClient _httpClient;

        public IndexViewModel(ISharedData sharedData,HttpClient httpClient)
        {
            _sharedData = sharedData;
            _httpClient = httpClient;
        }

        public async Task<bool> Authenticte()
        {
            try
            {
                var request = new AuthenticateRequest
                {
                    Username = Username,
                    Password = Password,
                };

                var response = await _httpClient.PostAsJsonAsync("https://localhost:7200/User/Authenticate", request);

                if(response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<LoggedInUserModel>(responseContent);

                    _sharedData.Id = result.Id;
                    _sharedData.Username = result.Username;
                    _sharedData.AvatarSourceName = result.AvatarSourceName;
                    _sharedData.Email = result.Email;
                    _sharedData.Token = result.Token;

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
