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

        public GameController(IQuestionData questionData, IScoreData scoreData)
        {
            _questionData = questionData;
            _scoreData = scoreData;
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
            var response = _scoreData.Insert(request.Id, request.Score);
            if (!response) return BadRequest(new { StatusMessage = "Something went wrong!" });

            return Ok();
        }
    }
}
