namespace PoleTimeGuesser.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
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

        [HttpPost]
        [AllowAnonymous]
        [Route("Authenticate")]
        public async Task<ActionResult<LoggedInUserModel>> Authenticate(AuthenticateRequest request)
        {
            try
            {
                var loggedInUser = await _userRepository.Authenticate(request.Username, request.Password);

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

        [HttpGet]
        [Route("GetByUsername/{username}")]
        public async Task<ActionResult<bool>> GetByUsername(string username)
        {
            var response = await _userRepository.GetByUsername(username);

            return Ok(response);
        }
    }
}
