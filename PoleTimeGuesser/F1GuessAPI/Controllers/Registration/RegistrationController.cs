using F1GuessAPI.DataAccess.User;

namespace F1GuessAPI.Controllers.Registration
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        readonly IUserData _userFunctions;

        public RegistrationController(IUserData userFunctions)
        {
            _userFunctions = userFunctions;
        }

        [HttpPost]
        public IActionResult Registration(RegistrationRequest request)
        {
            var response = _userFunctions.Registration(request.Username, request.Email, request.Password);
            if (!response)
                return BadRequest(new { StatusMessage = "Registration error!" });

            return Ok();
        }
    }
}
