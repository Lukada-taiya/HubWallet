using HubWally.Application.DTOs.Accounts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubWally.Application.Commands.Requests.Accounts
{
    public class RegisterCommand : IRequest<ApiResponse>
    {
        public RegisterDto registerDto { get; set; }
    }

}
