using HubWally.Application.DTOs.Wallets;
using MediatR; 

namespace HubWally.Application.Commands.Requests.Wallets
{
    public class CreateWalletCommand : IRequest<ApiResponse>
    {
        public WalletDto walletDto { get; set; }
    }
}
