using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubWally.Application.Queries.Requests
{
    public class GetWalletRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
