using System.Net.Mime;
using HubWally.Application;
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
        /// <param name="wallet">The object containing the details to register user</param>
        /// <returns>Success response</returns>
        /// <remarks>
        /// The <paramref name="wallet"/> contains the following properties:
        /// <ul type="bullet">
        /// <li><description><c>Name</c> - Name to identify wallet</description></li>
        /// <li><description><c>Type</c> - Type of wallet (momo or card)</description></li> 
        /// <li><description><c>AccountNumber</c> - Phone number or account number of wallet</description></li> 
        /// <li><description><c>AccountScheme</c> - Service provider or card type of account number (vodafone,mtn,airteltigo,mastercard,visa)</description></li> 
        /// <li><description><c>Owner</c> - Phone number of user the wallet belongs to. Should be a valid user</description></li> 
        /// </ul>
        /// </remarks>
        /// <response code="200">Returns a success api response with newly created wallet id</response>
        /// <response code="400">Invalid payload values passed</response>
        /// <response code="500">An error occurred creating wallet</response> 
        [HttpPost("AddWallet")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type =
            typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// </summary>
        /// <param name="wallet">The object containing the details to modify wallet</param>
        /// <param name="id">The id of the wallet to be modified</param>
        /// <returns>Returns a success api response.</returns>
        /// <remarks>
        /// The <paramref name="wallet"/> contains the following properties:
        /// <ul type="bullet">
        /// <li><description><c>Name</c> - Name to identify wallet</description></li>
        /// <li><description><c>Type</c> - Type of wallet (momo or card)</description></li> 
        /// <li><description><c>AccountNumber</c> - Phone number or account number of wallet</description></li> 
        /// <li><description><c>AccountScheme</c> - Service provider or card type of account number (vodafone,mtn,airteltigo,mastercard,visa)</description></li> 
        /// <li><description><c>Owner</c> - Phone number of user the wallet belongs to. Should be a valid user</description></li> 
        /// </ul>
        /// </remarks>
        /// <response code="202">Returns a success api response with newly updated wallet id</response>
        /// <response code="400">Invalid payload values passed</response>
        /// <response code="500">An error occurred updating wallet</response>
        [HttpPut("UpdateWallet/{id:int}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type =
            typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, WalletDto wallet)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new UpdateWalletCommand { walletDto = wallet, Id = id });

                if (result.Success)
                {
                    return Accepted(result);
                }

                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Retrieve a wallet based on its id.
        /// </summary>
        /// <param name="id">The id of the wallet to retrieve</param>
        /// <returns>Returns an object with the requested wallet.</returns>
        /// <response code="200">Returns api response with wallet object</response>
        /// <response code="400">Invalid id value passed</response>
        /// <response code="500">An error occurred getting wallet by id</response>
        [HttpGet("GetWalletById")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type =
            typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// </summary>
        /// <returns>Returns an object response with all wallets.</returns>
        /// <response code="200">Returns a success api response with all wallets</response> 
        /// <response code="500">An error occurred getting all wallets</response>
        [HttpGet("GetAllWallets")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type =
            typeof(ApiResponse))] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// </summary>
        /// <param name="id">The id of the wallet to delete</param> 
        /// <returns>Returns an object response with success key of true.</returns>
        /// <response code="200">Returns a success api response</response> 
        /// <response code="500">An error occurred deleting wallet</response>
        [HttpDelete("DeleteWallet/{id:int}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type =
            typeof(ApiResponse))] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
