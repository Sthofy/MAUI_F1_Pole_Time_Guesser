﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PoleTimeGuesser.Model;

namespace PoleTimeGuesser.Services
{
    public class F1DataGetterService : IF1DataGetterService
    {
        private readonly string _year = DateTime.Now.Year.ToString();
        readonly HttpClient _httpClient;
        List<DriverStandingsModel> driverStadingModel = new();

        public F1DataGetterService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<DriverStandingsModel>> GetDriverStandings()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://ergast.com/api/f1/current/driverStandings.json");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var res = json["MRData"]["StandingsTable"]["StandingsLists"].First["DriverStandings"].ToString();
                    driverStadingModel = JsonConvert.DeserializeObject<List<DriverStandingsModel>>(res);

                    foreach (var item in driverStadingModel)
                    {
                        item.Driver.Image = new DriverImageModel
                        {
                            Front = $"{item.Driver.driverId}_front.png",
                            Side = $"{item.Driver.driverId}.png"
                        };
                    }

                    return driverStadingModel;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}