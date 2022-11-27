namespace PoleTimeGuesser.Api.Helpers
{
    public class UrlHelper
    {
        HttpClient _httpClient = new HttpClient();
        const string F1_SITE_URL = "https://www.formula1.com/en";

        public async Task<string> CallUrl(string urlEnd)
        {
            try
            {
                string url = F1_SITE_URL + $"{urlEnd}";
                var resopse = await _httpClient.GetStringAsync(url);
                return resopse;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
