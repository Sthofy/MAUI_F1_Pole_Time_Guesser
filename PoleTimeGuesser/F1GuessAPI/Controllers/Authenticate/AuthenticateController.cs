namespace F1GuessAPI.Controllers.Authenticate
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        IUserData _userData;

        public AuthenticateController(IUserData userData)
        {
            _userData = userData;
        }

        [HttpPost]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _userData.Authenticate(request.Username, request.Password);
            if (response == null)
                return BadRequest(new { StatusMessage = "Invalid username or password!" });

            return Ok(response);
        }
    }
}
