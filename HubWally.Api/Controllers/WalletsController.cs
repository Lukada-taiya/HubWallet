using HubWally.Application.Commands.Requests.Wallets;
using HubWally.Application.DTOs.Wallets;
using HubWally.Application.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;

namespace HubWally.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WalletsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Adds a new wallet.
        /// </summary>
        /// <returns>Returns an object response with newly created id.</returns>
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

        /// <summary>
        /// Modifies an already existing wallet.
        ///</summary>
        ///<returns>Returns an object response with success key of true.</returns>
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

        /// <summary>
        /// Retrieve a wallet based on its id.
        ///</summary>
        ///<returns>Returns an object response with returns an object with the requested wallet.</returns>
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

        /// <summary>
        /// Retrieves all wallets.
        ///</summary>
        ///<returns>Returns an object response with all wallets.</returns>
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

        /// <summary>
        /// Deletes an already existing wallet.
        ///</summary>
        ///<returns>Returns an object response with success key of true.</returns>
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
