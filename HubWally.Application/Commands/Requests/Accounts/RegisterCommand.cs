using HubWally.Application.DTOs.Accounts;
using MediatR; 

namespace HubWally.Application.Commands.Requests.Accounts
{
    public class RegisterCommand : IRequest<ApiResponse>
    {
        public RegisterDto registerDto { get; set; }
    }

}
