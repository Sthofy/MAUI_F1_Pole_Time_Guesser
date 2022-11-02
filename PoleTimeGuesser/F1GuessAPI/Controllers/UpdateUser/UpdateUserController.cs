namespace F1GuessAPI.Controllers.UpdateUser
{
    [Route("[controller]")]
    [ApiController]
    public class UpdateUserController : ControllerBase
    {
        IUserFunctions _userFunctions;
        public UpdateUserController(IUserFunctions userFunctions)
        {
            _userFunctions = userFunctions;
        }

        [HttpPut]
        public IActionResult UserUpdate(UpdateUserRequest request)
        {
            var response = _userFunctions.Update(request.Id, request.Username, request.Email, request.Password).Result;
            if (response is null)
                return BadRequest(new { StatusMessage = "Something went wrong!" });

            return Ok(response);
        }
    }
}
