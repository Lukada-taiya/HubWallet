using HubWally.Application.DTOs.Accounts;
using MediatR; 

namespace HubWally.Application.Commands.Requests.Accounts
{
    public class LoginCommand : IRequest<ApiResponse>
    {
        public LoginDto loginDto { get; set; }
    }
}
