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
