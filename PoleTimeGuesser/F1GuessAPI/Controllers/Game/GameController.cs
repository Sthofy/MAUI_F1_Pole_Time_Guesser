using F1GuessAPI.Controllers.Game;
using F1GuessAPI.DataAccess.Game;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1GuessAPI.Controllers.Quiz
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IQuestionData _questionData;
        private readonly IScoreData _scoreData;
        private readonly IGuessData _guessData;

        public GameController(IQuestionData questionData, IScoreData scoreData, IGuessData guessData)
        {
            _questionData = questionData;
            _scoreData = scoreData;
            _guessData = guessData;
        }

        [HttpGet("Questions")]
        public ListOfQuestions GetQuestions()
        {
            var data = _questionData.GetQuestion();

            return data;
        }

        [HttpPost("InsertScore")]
        public IActionResult InsertScore(ScoreRequest request)
        {
            var response = _scoreData.Insert(request.UserId, request.Score);
            if (!response) return BadRequest(new { StatusMessage = "Something went wrong!" });

            return Ok();
        }

        //[HttpPost]
        //public IActionResult UpdateScore(ScoreRequest request) { }

        [HttpPost("InsertGuess")]
        public IActionResult InsertGuess(GuessRequest request)
        {
            var response = _guessData.Insert(request);
            if (!response) return BadRequest(new { StatusMessage = "Something went wrong!" });

            return Ok();
        }

        [HttpGet("GuessByUserId")]
        public ListOfGuessModel GetByUserId(int userId)
        {
            var response = _guessData.GetByUserId(userId);

            return response;
        }
    }
}
