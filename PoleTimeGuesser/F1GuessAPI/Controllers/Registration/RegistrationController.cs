namespace F1GuessAPI.Controllers.Registration
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        IUserFunctions _userFunctions;

        public RegistrationController(IUserFunctions userFunctions)
        {
            _userFunctions = userFunctions;
        }

        [HttpPost]
        public IActionResult Registration(RegistrationRequest request)
        {
            var response = _userFunctions.Registration(request.Username, request.Email, request.Password).Result;
            if (response is null)
                return BadRequest(new { StatusMessage = "Registration error!" });

            return Ok(response);
        }
    }
}
