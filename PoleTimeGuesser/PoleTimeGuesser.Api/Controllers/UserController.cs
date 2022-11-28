using Microsoft.AspNetCore.Mvc;
using PoleTimeGuesser.Api.Repositories.Contracts;
using PoleTimeGuesser.Library.Models;
using PoleTimeGuesser.Library.Requests;

namespace PoleTimeGuesser.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<ActionResult<RegistrationModel>> Registration(RegistrationRequest request)
        {
            try
            {
                var response = await _userRepository.Registration(request.Username, request.Email, request.Password);

                if (response is null)
                {
                    return BadRequest();
                }
                else
                    return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("Authenticate/{username}/{password}")]
        public async Task<ActionResult<LoggedInUserModel>> Authenticate(string username, string password)
        {
            try
            {
                var loggedInUser = await _userRepository.Authenticate(username, password);

                if (loggedInUser is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(loggedInUser);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(UpdateUserRequest request)
        {
            try
            {
                var response = await _userRepository.Update(request.Id, request.Username, request.Email, request.Password);

                if (response)
                    return NoContent();
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("GetScoreboard")]
        public async Task<ActionResult<IEnumerable<ScoreboardModel>>> GetScoreboard()
        {
            try
            {
                var response = await _userRepository.GetScoreboard();

                if (response is null)
                    return NotFound();
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }
    }
}
