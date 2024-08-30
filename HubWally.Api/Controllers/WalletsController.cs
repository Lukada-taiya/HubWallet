using HubWally.Application.Commands.Requests.Wallets;
using HubWally.Application.DTOs.Wallets;
using HubWally.Application.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HubWally.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WalletsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("AddWallet")]
        public async Task<IActionResult> Add(WalletDto wallet)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateWalletCommand { walletDto = wallet });

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }
        [HttpPut("UpdateWallet/{id:int}")]
        public async Task<IActionResult> Update(int id, WalletDto wallet)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new UpdateWalletCommand { walletDto = wallet, Id = id });

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }
        [HttpGet("GetWalletById")]
        public async Task<IActionResult> Get(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new GetWalletRequest { Id = id });

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }
        [HttpGet("GetAllWallets")]
        public async Task<IActionResult> GetAll()
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new GetAllWalletsRequest());

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }
        [HttpDelete("DeleteWallet/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new DeleteWalletCommand { Id = id});

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
