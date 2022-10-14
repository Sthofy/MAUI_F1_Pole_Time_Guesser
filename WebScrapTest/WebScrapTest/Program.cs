using HtmlAgilityPack;
using System.Diagnostics;
using WebScrapTest;

//const string URL = "https://m4sport.hu/f1-pontverseny/";
string driversHtml = $"https://www.formula1.com/en/results.html/{DateTime.Now.Year.ToString()}/drivers.html";
string teamsHtml = $"https://www.formula1.com/en/results.html/{DateTime.Now.Year.ToString()}/team.html";
string racesHtml = $"https://www.formula1.com/en/results.html/{DateTime.Now.Year.ToString()}/races.html";
const string FILENAME = "F1_hivatalos.txt";
const string DriverStanding = "DriverStanding.txt";
HtmlDocument htmlFile = ParseHTML(loadFromFile(FILENAME));

//var datalist = doc.DocumentNode.SelectNodes("//span[@class='pilotNameLong']");


Stopwatch stopwatch = Stopwatch.StartNew();
stopwatch.Start();

async Task<string> CallUrl(string fullUrl)
{
    HttpClient client = new HttpClient();
    var response = await client.GetStringAsync(fullUrl);
    return response;
}
void savetoFile(string filename, string html)
{
    using (FileStream fs = new FileStream(filename, FileMode.Create))
    {
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(html);
    }
}
string loadFromFile(string filename)
{
    string output = "";
    using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
    {
        StreamReader sr = new StreamReader(fs);
        string line = sr.ReadLine();
        while (line != null)
        {
            output += line;
            line = sr.ReadLine();
        }

    }
    return output;
}
HtmlDocument ParseHTML(string html)
{
    HtmlDocument doc = new HtmlDocument();
    doc.LoadHtml(html);


    return doc;
}

List<string> SelectData(HtmlDocument html, string selector)
{
    var driversList = html.DocumentNode.SelectNodes(selector);
    List<string> drivers = new List<string>();
    foreach (var item in driversList)
    {
        drivers.Add(item.InnerHtml);
    }
    return drivers;
}

List<string> GetDriversFullName()
{
    List<string> fullNames = new List<string>();
    List<string> firstNames = SelectData(htmlFile, "//span[@class='hide-for-tablet']");
    List<string> lastnames = SelectData(htmlFile, "//span[@class='hide-for-mobile']");

    for (int i = 0; i < firstNames.Count; ++i)
    {
        fullNames.Add(firstNames[i] + " " + lastnames[i]);
    }

    return fullNames;
}
List<string> GetDriversPoint()
{
    List<string> output = SelectData(htmlFile, "//td[@class='dark bold']");
    return output;
}
List<string> GetDriversTeam()
{
    List<string> output = SelectData(htmlFile, "//a[@class='grey semi-bold uppercase ArchiveLink']");
    return output;
}

void SaveStandingsToFile(string filename)
{
    List<string> fullNames = GetDriversFullName();
    List<string> points = GetDriversPoint();
    List<string> teams = GetDriversTeam();

    List<DriverStandingModel> model = new List<DriverStandingModel>();

    using (FileStream fs = new FileStream(filename, FileMode.Create))
    {
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i < fullNames.Count; ++i)
        {
            model.Add(new DriverStandingModel
            {
                Position = i + 1,
                DriverName = fullNames[i],
                Team = teams[i],
                Points = int.Parse(points[i])
            });
        }

        foreach (var item in model)
        {
            Console.WriteLine(item.ToString());
            sw.WriteLine(item.ToString());
            sw.Flush();
        }
    }
}

//savetoFile(FILENAME, CallUrl(driversHtml).Result);
SaveStandingsToFile(DriverStanding);

stopwatch.Stop();
TimeSpan ts = stopwatch.Elapsed;
Console.WriteLine("\n" + ts);


Console.ReadKey();