using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoleTimeGuesser.Api.Repositories;
using PoleTimeGuesser.Api.Repositories.Contracts;
using PoleTimeGuesser.Library.Models;

namespace PoleTimeGuesser.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public GameController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
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
    }
}
