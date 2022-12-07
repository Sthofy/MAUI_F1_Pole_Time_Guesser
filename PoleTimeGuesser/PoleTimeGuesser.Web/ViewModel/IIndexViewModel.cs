namespace PoleTimeGuesser.Web.ViewModel
{
    public interface IIndexViewModel
    {
        string Password { get; set; }
        string Username { get; set; }

        Task<bool> Authenticte();
    }
}