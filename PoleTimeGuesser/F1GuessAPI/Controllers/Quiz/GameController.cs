using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1GuessAPI.Controllers.Quiz
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        IQuestionFunctions _questionFunctions;

        public GameController(IQuestionFunctions questionFunctions)
        {
            _questionFunctions = questionFunctions;
        }

        [HttpGet("Questions")]
        public async Task<List<QuestionModel>> GetQuestions()
        {
            var data = await _questionFunctions.GetQuestion();

            return data;
        }

    }
}
