using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoleTimeGuesser.Api.Repositories;
using PoleTimeGuesser.Api.Repositories.Contracts;
using PoleTimeGuesser.Library.Models;
using PoleTimeGuesser.Library.Requests;

namespace PoleTimeGuesser.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IScoreRepository _scoreRepository;

        public GameController(IQuestionRepository questionRepository, IScoreRepository scoreRepository)
        {
            _questionRepository = questionRepository;
            _scoreRepository = scoreRepository;
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
                await _scoreRepository.Insert(request.UserId, request.Score);

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
                await _scoreRepository.Update(request.UserId, request.Score);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

    }
}
