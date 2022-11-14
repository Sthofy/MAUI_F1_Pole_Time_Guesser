namespace PoleTimeGuesser.Model
{
    public class SharedData : ISharedData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AvatarSourceName { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
