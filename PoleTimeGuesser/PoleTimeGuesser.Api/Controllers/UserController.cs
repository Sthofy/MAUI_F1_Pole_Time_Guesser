﻿using Microsoft.AspNetCore.Http;
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
        public Task<ActionResult<RegistrationModel>> Registration(RegistrationRequest request)
        {

        }

        [HttpGet]
        [Route("Authenticate/{username}/{password}")]
        public async Task<ActionResult<LoggedInUserModel>> Authenticate(string username, string password)
        {
            try
            {
                var loggedInUser = await _userRepository.Authenticate(username, password);

                if (loggedInUser == null)
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
    }
}