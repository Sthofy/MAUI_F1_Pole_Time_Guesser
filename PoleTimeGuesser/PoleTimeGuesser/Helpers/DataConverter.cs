namespace PoleTimeGuesser.Helpers
{
    public class DataConverter
    {
        public string ConvertTeamName(string team)
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

        public string ConvertDrivername(string driver)
        {
            switch (driver)
            {
                case "albon":
                    return "alexander-albon.html";
                case "max_verstappen":
                    return "max-verstappen.html";
                case "perez":
                    return "sergio-perez.html";
                case "leclerc":
                    return "charles-leclerc.html";
                case "russell":
                    return "george-russell.html";
                case "sainz":
                    return "carlos-sainz.html";
                case "hamilton":
                    return "lewis-hamilton.html";
                case "norris":
                    return "lando-norris.html";
                case "ocon":
                    return "esteban-ocon.html";
                case "alonso":
                    return "fernando-alonso.html";
                case "bottas":
                    return "valtteri-bottas.html";
                case "vettel":
                    return "sebastian-vettel.html";
                case "ricciardo":
                    return "daniel-ricciardo.html";
                case "gasly":
                    return "pierre-gasly.html";
                case "kevin_magnussen":
                    return "kevin-magnussen.html";
                case "stroll":
                    return "lance-stroll.html";
                case "mick_schumacher":
                    return "mick-schumacher.html";
                case "tsunoda":
                    return "yuki-tsunoda.html";
                case "zhou":
                    return "guanyu-zhou.html";
                case "latifi":
                    return "nicholas-latifi.html";
                default:
                    return null;
            }
        }
    }
}
