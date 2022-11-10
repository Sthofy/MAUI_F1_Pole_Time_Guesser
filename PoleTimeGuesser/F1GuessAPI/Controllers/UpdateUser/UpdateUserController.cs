namespace F1GuessAPI.Controllers.UpdateUser
{
    [Route("[controller]")]
    [ApiController]
    public class UpdateUserController : ControllerBase
    {
        IUserData _userFunctions;
        public UpdateUserController(IUserData userFunctions)
        {
            _userFunctions = userFunctions;
        }

        [HttpPut]
        public IActionResult UserUpdate(UpdateUserRequest request)
        {
            var response = _userFunctions.Update(request.Id, request.Username, request.Email, request.Password);
            if (!response)
                return BadRequest(new { StatusMessage = "Something went wrong!" });

            return Ok();
        }
    }
}
