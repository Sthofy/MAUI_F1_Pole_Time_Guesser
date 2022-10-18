using HtmlAgilityPack;

namespace PoleTimeGuesser.Services
{
    public class WebScrapper
    {
        string DriverUrl = "https://www.formula1.com/en/drivers/";
        HttpClient _httpClient = new HttpClient();
        HtmlDocument doc = new HtmlDocument();
        DriverInfoModel driverInfo = new DriverInfoModel();
        DataConverter converter = new DataConverter();

        async Task<string> CallUrl(string url)
        {
            try
            {
                var resopse = await _httpClient.GetStringAsync(url);
                return resopse;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<DriverInfoModel> GetDriverInfo(string driver)
        {
            try
            {
                string url = $"{DriverUrl}{converter.ConvertDrivername(driver)}";
                var response = await CallUrl(url);
                doc.LoadHtml(response);
                var table = doc.DocumentNode.SelectNodes("//td[@class='stat-value']");
                var bio = doc.DocumentNode.SelectNodes("//div[@class='text parbase']/p");

                List<string> data = new List<string>();

                foreach (var item in table)
                {
                    data.Add(item.InnerText);
                }

                driverInfo.Bio = "";

                foreach (var item in bio)
                {
                    driverInfo.Bio += item.InnerText;
                }

                driverInfo.Podiums = data[2];
                driverInfo.Points = data[3];
                driverInfo.RaceEnterded = data[4];
                driverInfo.WorldChamps = data[5];
                driverInfo.HighestFinish = data[6];
                driverInfo.HighestPositoion = data[7];
                driverInfo.BirthPlace = data[9];

                return driverInfo;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
