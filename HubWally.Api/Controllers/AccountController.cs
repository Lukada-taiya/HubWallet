using System.Net.Mime;
using HubWally.Application;
using HubWally.Application.Commands.Requests.Accounts;
using HubWally.Application.DTOs.Accounts;
using MediatR;  
using Microsoft.AspNetCore.Mvc; 

namespace HubWally.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Signs user up into the system.
        /// </summary> 
        /// <param name="registerDto">The object containing the details to register user</param>
        /// <returns>Success response</returns>
        /// <remarks>
        /// The <paramref name="registerDto"/> contains the following properties:
        /// <ul>
        /// <li><description><c>PhoneNumber</c> - Mobile number of user to be registered</description></li>
        /// <li><description><c>Password</c> - Password for the user</description></li> 
        /// </ul>
        /// </remarks>
        /// <response code="200">Returns a success api response</response>
        /// <response code="400">Invalid payload values passed</response>
        /// <response code="500">An error occurred registering user</response> 
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type =
            typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            { 
                var result = await _mediator.Send(new RegisterCommand { registerDto = registerDto});

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Signs user into the system
        /// </summary>
        /// <param name="loginDto">The object containing the details to login user</param>
        /// <returns>Success response</returns>
        /// <remarks>
        /// The <paramref name="loginDto"/> contains the following properties:
        /// <ul>
        /// <li><description><c>PhoneNumber</c> - Mobile number of user to be logged in</description></li>
        /// <li><description><c>Password</c> - Login Password of the user</description></li> 
        /// </ul>
        /// </remarks>
        /// <response code="200">Returns a success api response with login token</response>
        /// <response code="400">Invalid payload values passed</response>
        /// <response code="500">An error occurred login user in</response> 
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type =
            typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new LoginCommand { loginDto = loginDto });

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest(ModelState);
        } 
    }

}
