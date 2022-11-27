using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PoleTimeGuesser.Api.Helpers;
using PoleTimeGuesser.Library.Models;

namespace PoleTimeGuesser.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriverInfoController : ControllerBase
    {
        HtmlDocument doc = new HtmlDocument();
        UrlHelper urlHelper = new UrlHelper();


        [HttpGet("{id}")]
        public async Task<ActionResult<DriverInfoModel>> GetDriverInfo(string id)
        {
            try
            {
                var response = await urlHelper.CallUrl(DataConverter.ConvertDrivername(id));
                doc.LoadHtml(response);
                var table = doc.DocumentNode.SelectNodes("//td[@class='stat-value']");
                var bio = doc.DocumentNode.SelectNodes("//div[@class='text parbase']/p");

                if (table is null)
                    return NotFound();
                else
                {
                    List<string> data = new List<string>();

                    foreach (var item in table)
                    {
                        data.Add(item.InnerText);
                    }

                    DriverInfoModel driverInfo = new DriverInfoModel
                    {
                        Bio = "",
                        Podiums = data[2],
                        Points = data[3],
                        RaceEnterded = data[4],
                        WorldChamps = data[5],
                        HighestFinish = data[6],
                        HighestPositoion = data[7],
                        BirthPlace = data[9],
                    };

                    foreach (var item in bio)
                    {
                        driverInfo.Bio += item.InnerText;
                    }

                    return Ok(driverInfo);
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
