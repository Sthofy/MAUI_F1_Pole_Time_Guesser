using HtmlAgilityPack;
using InformationAPI.Models;
using Newtonsoft.Json;
using System.Diagnostics;


var builder = WebApplication.CreateBuilder(args);
DriverInfoModel driveInfo = new DriverInfoModel();

string F1SiteUrl = "https://www.formula1.com/en";

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
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("api/driverinfo/{id}", async (string id) =>
{
    try
    {
        string url = $"{F1SiteUrl}{converter.ConvertDrivername(id)}";
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

        string output = JsonConvert.SerializeObject(driverInfo);
        return output;
    }
    catch (Exception ex)
    {
        Debug.WriteLine(ex.Message);
        return null;
    }
});


app.Run();
