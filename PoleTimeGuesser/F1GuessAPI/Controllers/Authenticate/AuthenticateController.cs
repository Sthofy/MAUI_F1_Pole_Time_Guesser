namespace F1GuessAPI.Controllers.Authenticate
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        IUserFunctions _userFunctions;

        public AuthenticateController(IUserFunctions userFunctions)
        {
            _userFunctions = userFunctions;
        }

        [HttpPost]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _userFunctions.Authenticate(request.Username, request.Password);
            if (response == null)
                return BadRequest(new { StatusMessage = "Invalid username or password!" });

            return Ok(response);
        }
    }
}
