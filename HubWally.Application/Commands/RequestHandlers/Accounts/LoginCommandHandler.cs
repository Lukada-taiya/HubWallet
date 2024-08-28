using HubWally.Application.Commands.Requests.Accounts;
using HubWally.Application.Services.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HubWally.Application.Commands.RequestHandlers.Accounts
{
    public class LoginCommandHandler(IAuthService service) : IRequestHandler<LoginCommand, ApiResponse>
    {
        private readonly IAuthService _service = service;
        public async Task<ApiResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //TODO: Add validation, data annotations?
                var response = await _service.Login(request.loginDto);
                if(response != null)
                return new()
                {
                    Success = true,
                    Message = "Login successful",
                    Body = new { Token = response },
                };

                throw new Exception("Login failed");
            }
            catch (Exception ex)
            { 
                return new ()
                {
                    Success = false,
                    Message = ex.Message ?? "Something went wrong. Try again later."
                };
            }
        }
    }
    
}
