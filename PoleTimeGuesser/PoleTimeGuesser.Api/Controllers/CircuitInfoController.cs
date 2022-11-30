namespace PoleTimeGuesser.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CircuitInfoController : ControllerBase
    {
        private HtmlDocument doc = new HtmlDocument();
        private UrlHelper _urlHelper = new UrlHelper();

        [HttpGet("{id}")]
        public async Task<ActionResult<CircuitInfoModel>> GetCircuitInfo(string id)
        {
            try
            {
                var response = await _urlHelper.CallUrl(DataConverter.ConvertCircuitName(id));
                doc.LoadHtml(response);
                var table = doc.DocumentNode.SelectNodes("//p[@class='f1-bold--stat']");
                var description = doc.DocumentNode.SelectNodes("//fieldset[@class='f1-border--three-right f1-border--single f1-border-color--gray3']/p");

                if (table is null)
                    return NotFound();
                else
                {
                    List<string> data = new List<string>();

                    foreach (var item in table)
                    {
                        data.Add(item.InnerText);
                    }

                    CircuitInfoModel circuitInfo = new CircuitInfoModel
                    {
                        FristGrandPrix = data[0],
                        Laps = data[1],
                        Length = data[2],
                        RaceDistance = data[3],
                        Lapredcord = data[4],
                        Info = "",
                    };

                    foreach (var item in description)
                    {
                        circuitInfo.Info += item.InnerText;
                    }

                    return Ok(circuitInfo);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
