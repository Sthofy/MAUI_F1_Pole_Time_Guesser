namespace PoleTimeGuesser.Api.Helpers
{
    public static class DataConverter
    {
        public static string ConvertTeamName(string team)
        {
            switch (team)
            {
                case "alfa":
                    return "Alfa-Romeo-Racing";
                case "alphatauri":
                    return "AlphaTauri";
                case "alpine":
                    return "Alpine";
                case "aston_martin":
                    return "Aston-Martin";
                case "ferrari":
                    return "Ferrari";
                case "haas":
                    return "Haas-F1-Team";
                case "mclaren":
                    return "McLaren";
                case "mercedes":
                    return "Mercedes";
                case "red_bull":
                    return "Red-Bull-Racing";
                case "williams":
                    return "Williams";
                default:
                    return null;
            }
        }

        public static string ConvertDrivername(string driver)
        {
            switch (driver)
            {
                case "albon":
                    return "/drivers/alexander-albon.html";
                case "max_verstappen":
                    return "/drivers/max-verstappen.html";
                case "perez":
                    return "/drivers/sergio-perez.html";
                case "leclerc":
                    return "/drivers/charles-leclerc.html";
                case "russell":
                    return "/drivers/george-russell.html";
                case "sainz":
                    return "/drivers/carlos-sainz.html";
                case "hamilton":
                    return "/drivers/lewis-hamilton.html";
                case "norris":
                    return "/drivers/lando-norris.html";
                case "ocon":
                    return "/drivers/esteban-ocon.html";
                case "alonso":
                    return "/drivers/fernando-alonso.html";
                case "bottas":
                    return "/drivers/valtteri-bottas.html";
                case "vettel":
                    return "/drivers/sebastian-vettel.html";
                case "ricciardo":
                    return "/drivers/daniel-ricciardo.html";
                case "gasly":
                    return "/drivers/pierre-gasly.html";
                case "kevin_magnussen":
                    return "/drivers/kevin-magnussen.html";
                case "stroll":
                    return "/drivers/lance-stroll.html";
                case "mick_schumacher":
                    return "/drivers/mick-schumacher.html";
                case "tsunoda":
                    return "/drivers/yuki-tsunoda.html";
                case "zhou":
                    return "/drivers/guanyu-zhou.html";
                case "latifi":
                    return "/drivers/nicholas-latifi.html";
                default:
                    return null;
            }
        }

        public static string ConvertCircuitName(string circuit)
        {
            switch (circuit)
            {
                case "albert_park":
                    return "/racing/2022/Australia/Circuit.html";
                case "americas":
                    return "/racing/2022/United_States/Circuit.html";
                case "bahrain":
                    return "/racing/2022/Bahrain/Circuit.html";
                case "baku":
                    return "/racing/2022/Azerbaijan/Circuit.html";
                case "catalunya":
                    return "/racing/2022/Spain/Circuit.html";
                case "hungaroring":
                    return "/racing/2022/Hungary/Circuit.html";
                case "imola":
                    return "/racing/2022/EmiliaRomagna/Circuit.html";
                case "interlagos":
                    return "/racing/2022/Brazil/Circuit.html";
                case "jeddah":
                    return "/racing/2022/Saudi_Arabia/Circuit.html";
                case "marina_bay":
                    return "/racing/2022/Singapore/Circuit.html";
                case "miami":
                    return "/racing/2022/Miami/Circuit.html";
                case "monaco":
                    return "/racing/2022/Monaco/Circuit.html";
                case "monza":
                    return "/racing/2022/Italy/Circuit.html";
                case "red_bull_ring":
                    return "/racing/2022/Austria/Circuit.html";
                case "ricard":
                    return "/racing/2022/France/Circuit.html";
                case "rodriguez":
                    return "/racing/2022/Mexico/Circuit.html";
                case "silverstone":
                    return "/racing/2022/Great_Britain/Circuit.html";
                case "spa":
                    return "/racing/2022/Belgium/Circuit.html";
                case "suzuka":
                    return "/racing/2022/Japan/Circuit.html";
                case "villeneuve":
                    return "/racing/2022/Canada/Circuit.html";
                case "yas_marina":
                    return "/racing/2022/United_Arab_Emirates.html";
                case "zandvoort":
                    return "/racing/2022/Netherlands.html";
                default:
                    return null;
            }
        }
    }
}
