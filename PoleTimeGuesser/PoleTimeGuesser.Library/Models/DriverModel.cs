namespace PoleTimeGuesser.Library.Models
{
    public class DriverModel
    {
        public string driverId { get; set; }
        public string permanentNumber { get; set; }
        public string code { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public string dateOfBirth { get; set; }
        public string nationality { get; set; }
        public DriverImageModel Image { get; set; }
    }
}
