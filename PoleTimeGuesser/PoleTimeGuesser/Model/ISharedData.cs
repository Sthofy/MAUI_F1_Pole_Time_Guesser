namespace PoleTimeGuesser.Model
{
    public interface ISharedData
    {
        string Username { get; set; }
        string AvatarSourceName { get; set; }
        string Email { get; set; }
        int Id { get; set; }
        DateTime Date { get; set; }
    }
}