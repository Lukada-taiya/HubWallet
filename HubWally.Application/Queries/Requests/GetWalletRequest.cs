using MediatR; 

namespace HubWally.Application.Queries.Requests
{
    public class GetWalletRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
