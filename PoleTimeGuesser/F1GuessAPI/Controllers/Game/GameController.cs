using Azure.Core;

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

        [HttpPut("UpdateScore")]
        public IActionResult UpdateScore(ScoreRequest request) 
        {
            var response = _scoreData.Update(request);
            if (!response) return BadRequest(new { StatusMessage = "Something went wrong!" });

            return Ok();
        }

        [HttpPost("InsertGuess")]
        public IActionResult InsertGuess(GuessRequest request)
        {
            var response = _guessData.Insert(request);
            if (!response) return BadRequest(new { StatusMessage = "Something went wrong!" });

            return Ok();
        }

        [HttpPut("UpdateGuess")]
        public IActionResult UpdateGuess(GuessRequest request)
        {
            var response = _guessData.Update(request);
            if (!response)
                return BadRequest(new { StatusMessage = "Something went wrong!" });
            return Ok();
        }

        [HttpPut("UpdateGuessDiff")]
        public IActionResult UpdateGuessDiff(GuessRequest request)
        {
            var response = _guessData.UpdateDiff(request);
            if (!response)
                return BadRequest(new { StatusMessage = "Something went wrong!" });
            return Ok();
        }

        [HttpPost("GuessByUserId")]
        public ListOfGuessModel GetByUserId([FromBody] int data)
        {
            var response = _guessData.GetByUserId(data);

            return response;
        }
    }
}
