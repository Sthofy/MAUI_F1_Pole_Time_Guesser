namespace PoleTimeGuesser.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConstructorInfoController : ControllerBase
    {
        private HtmlDocument doc = new HtmlDocument();
        private UrlHelper _urlHelper = new UrlHelper();

        [HttpGet("{id}")]
        public async Task<ActionResult<ConstructorInfoModel>> GetConstructorInfo(string id)
        {
            try
            {
                var response = await _urlHelper.CallUrl(DataConverter.ConvertTeamName(id));
                doc.LoadHtml(response);
                var table = doc.DocumentNode.SelectNodes("//td[@class='stat-value']");
                var description = doc.DocumentNode.SelectNodes("//div[@class='text parbase']/p").First();

                if (table is null)
                    return NotFound();
                else
                {
                    List<string> data = new List<string>();

                    foreach (var item in table)
                    {
                        data.Add(item.InnerText);
                    }

                    ConstructorInfoModel constructorInfo = new ConstructorInfoModel
                    {
                        FullName = data[0],
                        Base = data[1],
                        TeamChief = data[2],
                        TechnicalChief = data[3],
                        PowerUnit = data[5],
                        FirstTeamEntry = data[6],
                        WorldChamps = data[7],
                        HighestRaceFinish = data[8],
                        Poles = data[9],
                        FastestLaps = data[10],
                        Info = description.InnerHtml,
                    };

                    return Ok(constructorInfo);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
