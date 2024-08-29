using HubWally.Application.DTOs.Wallets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubWally.Application.Commands.Requests.Wallets
{
    public class CreateWalletCommand : IRequest<ApiResponse>
    {
        public WalletDto walletDto { get; set; }
    }
}
