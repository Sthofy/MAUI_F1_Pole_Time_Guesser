namespace PoleTimeGuesser.Services
{
    public class F1DataGetterService
    {
        private DateTime _date = DateTime.Now;
        readonly HttpClient _httpClient;
        List<DriverStandingsModel> driverStadingModel = new();
        List<ScheduleModel> scheduleModels = new();

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

        public async Task<List<ScheduleModel>> GetSchedule()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://ergast.com/api/f1/current.json");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var res = json["MRData"]["RaceTable"]["Races"].ToString();
                    scheduleModels = JsonConvert.DeserializeObject<List<ScheduleModel>>(res);

                    foreach (var item in scheduleModels)
                    {
                        string country = item.Circuit.Location.Country.Equals("Saudi Arabia") ? "saudi_arabia" : item.Circuit.Location.Country.ToLower();
                        item.Circuit.Location.Image = $"{country}.png";
                        item.Circuit.Image = $"{item.Circuit.CircuitId}_circuit.png";
                    }

                    return scheduleModels;
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

        public async Task<DriverInfoModel> GetDriverInfo(string id)
        {

            try
            {
                var response = await _httpClient.GetAsync($"https://f1infoapi.azurewebsites.net/DriverInfo/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var driverInfoModel = JsonConvert.DeserializeObject<DriverInfoModel>(result);

                    return driverInfoModel;
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

        public async Task<CircuitInfoModel> GetCicuitInfoAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://f1infoapi.azurewebsites.net/CircuitInfo/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var circuitInfoModel = JsonConvert.DeserializeObject<CircuitInfoModel>(result);

                    return circuitInfoModel;
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

        public async Task<ScheduleModel> GetUpComingEvent()
        {
            try
            {
                var shedules = await GetSchedule();
                ScheduleModel output = new ScheduleModel();

                foreach (var item in shedules)
                {
                    if (DateTime.Parse(item.Date).CompareTo(_date) == 1)
                    {
                        output = item;
                        break;
                    }
                    else output = null;
                }

                return output;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<DriverStandingsModel>> GetTopDrivers()
        {
            try
            {
                var drivers = await GetDriverStandings();
                List<DriverStandingsModel> output = new List<DriverStandingsModel>();

                for (int i = 0; i < 3; ++i)
                {
                    output.Add(drivers[i]);
                }

                return output;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
