namespace PoleTimeGuesser.Services
{
    public class F1DataGetterService : IF1DataGetterService
    {
        private DateTime _date = DateTime.Now;
        readonly HttpClient _httpClient;
        private readonly ISharedData _sharedData;
        List<DriverStandingsModel> driverStadingModel = new();
        List<ScheduleModel> scheduleModels = new();
        string Url = "https://f1guessapi.azurewebsites.net";

        public F1DataGetterService(ISharedData sharedData)
        {
            _httpClient = new HttpClient();
            _sharedData = sharedData;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _sharedData.Token);
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
                            Side = $"{item.Driver.driverId}.png",
                            Full = $"{item.Driver.driverId}_full.png",
                        };
                    }

                    return driverStadingModel;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
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
                return null;
            }
        }

        public async Task<DriverInfoModel> GetDriverInfo(string id)
        {
            var response = await _httpClient.GetAsync($"{Url}/DriverInfo/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<DriverInfoModel>(responseContent);
                return result;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            else
                return null;
        }

        public async Task<CircuitInfoModel> GetCicuitInfoAsync(string id)
        {

            var response = await _httpClient.GetAsync($"{Url}/CircuitInfo/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var circuitInfoModel = JsonConvert.DeserializeObject<CircuitInfoModel>(result);

                return circuitInfoModel;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            else
                return null;
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
                return null;
            }
        }

        public async Task<QualifyingResultModel> GetQualifyingResult(string circuitId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://ergast.com/api/f1/current/{circuitId}/qualifying/1.json");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);

                    if (json["MRData"]["RaceTable"]["Races"].FirstOrDefault() is null)
                        return null;

                    var res = json["MRData"]["RaceTable"]["Races"].First["QualifyingResults"].ToString();
                    var qualiResult = JsonConvert.DeserializeObject<List<QualifyingResultModel>>(res).FirstOrDefault();

                    return qualiResult;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
