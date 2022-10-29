namespace F1GuessAPI.Controllers.WebScrapp
{
    [Route("[controller]")]
    [ApiController]
    public class CircuitInfoController : ControllerBase
    {
        CircuitInfoModel _circuitInfo = new CircuitInfoModel();
        HtmlDocument doc = new HtmlDocument();
        UrlHelper urlHelper = new UrlHelper();

        [HttpGet("{id}")]
        public async Task<string> GetCircuitInfo(string id)
        {
            try
            {
                var response = await urlHelper.CallUrl(DataConverter.ConvertCircuitName(id));
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


                string output = JsonConvert.SerializeObject(_circuitInfo, formatting: Formatting.Indented);
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
