using MediatR; 

namespace HubWally.Application.Commands.Requests.Wallets
{
    public class DeleteWalletCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
