namespace F1GuessAPI.Controllers.User
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserData _userData;

        public UserController(IUserData userData)
        {
            _userData = userData;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _userData.Authenticate(request.Username, request.Password);
            if (response == null)
                return BadRequest(new { StatusMessage = "Invalid username or password!" });

            return Ok(response);
        }

        [HttpPost("Registration")]
        public IActionResult Registration(RegistrationRequest request)
        {
            var response = _userData.Registration(request.Username, request.Email, request.Password);
            if (response is null)
                return BadRequest(new { StatusMessage = "Registration error!" });

            return Ok(response);
        }

        [HttpPut("Update")]
        public IActionResult UserUpdate(UpdateUserRequest request)
        {
            var response = _userData.Update(request.Id, request.Username, request.Email, request.Password);
            if (!response)
                return BadRequest(new { StatusMessage = "Something went wrong!" });

            return Ok();
        }

        //TODO: MIÉRT NEM MŰKÖDIK??? [HttpGet("GetByUsername")] 405 Method Not Allowed
        //public GetByUsernameResponse GetByUsername([FromQuery] string username)

        [HttpPost("GetByUsername")]
        public GetByUsernameResponse GetByUsername([FromBody] string username)
        {
            var response = _userData.GetUserByUsername(username);

            return new GetByUsernameResponse { IsExist = response };
        }

        [HttpGet("GetScoreboard")]
        public ScoreboardResponse GetScoreboard()
        {
            var response = _userData.GetScoreboard();

            return response;
        }
    }
}
