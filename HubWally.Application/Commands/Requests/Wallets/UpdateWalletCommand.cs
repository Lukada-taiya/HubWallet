using HubWally.Application.DTOs.Wallets;
using MediatR; 

namespace HubWally.Application.Commands.Requests.Wallets
{
    public class UpdateWalletCommand : IRequest<ApiResponse>
    {
        public int  Id { get; set; }
        public WalletDto walletDto { get; set; }
    }
}
