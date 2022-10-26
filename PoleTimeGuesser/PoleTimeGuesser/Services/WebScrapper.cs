using HtmlAgilityPack;

namespace PoleTimeGuesser.Services
{
    public class WebScrapper
    {
        string F1SiteUrl = "https://www.formula1.com/en";

        HttpClient _httpClient = new HttpClient();
        HtmlDocument doc = new HtmlDocument();
        DriverInfoModel driverInfo = new DriverInfoModel();
        CircuitInfoModel _circuitInfo = new CircuitInfoModel();
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
                string url = $"{F1SiteUrl}{converter.ConvertDrivername(driver)}";
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

        public async Task<CircuitInfoModel> GetCircuitInfoAsync(string circuit)
        {
            try
            {
                var response = await CallUrl($"{F1SiteUrl}{converter.ConvertCircuitName(circuit)}");
                doc.LoadHtml(response);
                var table = doc.DocumentNode.SelectNodes("//p[@class='f1-bold--stat']");
                var description = doc.DocumentNode.SelectNodes("//fieldset[@class='f1-border--three-right f1-border--single f1-border-color--gray3']/p");

                List<string> data = new List<string>();
                foreach (var item in table)
                {
                    data.Add(item.InnerText);
                }

                _circuitInfo.FristGrandPrix = data[0];
                _circuitInfo.Laps = data[1];
                _circuitInfo.Length = data[2];
                _circuitInfo.RaceDistance = data[3];
                _circuitInfo.Lapredcord = data[4];
                _circuitInfo.Info = "";

                foreach (var item in description)
                {
                    _circuitInfo.Info += item.InnerText;
                }

                return _circuitInfo;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
