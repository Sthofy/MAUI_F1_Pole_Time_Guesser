namespace PoleTimeGuesser.Services.Game
{
    public class GuessGetByUserIdResponse : BaseResponse
    {
        public List<GuessModel> Guesses { get; set; }
    }
}
