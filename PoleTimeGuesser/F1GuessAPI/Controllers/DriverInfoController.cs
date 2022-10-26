namespace F1GuessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriverInfoController : ControllerBase
    {
        DriverInfoModel _driverInfo = new DriverInfoModel();
        HtmlDocument doc = new HtmlDocument();
        UrlHelper urlHelper = new UrlHelper();


        [HttpGet("{id}")]
        public async Task<string> GetDriverInfo(string id)
        {
            try
            {
                var response = await urlHelper.CallUrl(DataConverter.ConvertDrivername(id));
                doc.LoadHtml(response);
                var table = doc.DocumentNode.SelectNodes("//td[@class='stat-value']");
                var bio = doc.DocumentNode.SelectNodes("//div[@class='text parbase']/p");

                List<string> data = new List<string>();

                foreach (var item in table)
                {
                    data.Add(item.InnerText);
                }

                _driverInfo.Bio = "";

                foreach (var item in bio)
                {
                    _driverInfo.Bio += item.InnerText;
                }

                _driverInfo.Podiums = data[2];
                _driverInfo.Points = data[3];
                _driverInfo.RaceEnterded = data[4];
                _driverInfo.WorldChamps = data[5];
                _driverInfo.HighestFinish = data[6];
                _driverInfo.HighestPositoion = data[7];
                _driverInfo.BirthPlace = data[9];

                string output = JsonConvert.SerializeObject(_driverInfo,formatting: Formatting.Indented);
                return output;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
