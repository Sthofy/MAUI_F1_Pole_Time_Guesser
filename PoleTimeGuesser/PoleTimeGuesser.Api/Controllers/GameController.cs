namespace PoleTimeGuesser.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IScoreRepository _scoreRepository;
        private readonly IGuessRepository _guessRepository;

        public GameController(IQuestionRepository questionRepository, IScoreRepository scoreRepository, IGuessRepository guessRepository)
        {
            _questionRepository = questionRepository;
            _scoreRepository = scoreRepository;
            _guessRepository = guessRepository;
        }

        [HttpGet]
        [Route("Questions")]
        public async Task<ActionResult<IEnumerable<QuestionModel>>> GetQuestions()
        {
            try
            {
                var response = await _questionRepository.GetQuestions();

                if (response is null)
                    return NotFound();
                else
                    return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPost]
        [Route("InsertScore")]
        public async Task<ActionResult> InsertScore(ScoreRequest request)
        {
            try
            {
                await _scoreRepository.Insert(request);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPut]
        [Route("UpdateScore")]
        public async Task<ActionResult> UpdateScore(ScoreRequest request)
        {
            try
            {
                await _scoreRepository.Update(request);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPost]
        [Route("InsertGuess")]
        public async Task<ActionResult> InsertGuess(GuessRequest request)
        {
            try
            {
                await _guessRepository.Insert(request);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPut]
        [Route("UpdateGuess")]
        public async Task<ActionResult> UpdateGuess(GuessRequest request)
        {
            try
            {
                await _guessRepository.Update(request);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("GuessGetByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<GuessModel>>> GuessGetByUserId(int userId)
        {
            try
            {
                var response = await _guessRepository.GuessGetByUserId(userId);
                if (response is null)
                    return BadRequest();
                else
                    return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

    }
}
