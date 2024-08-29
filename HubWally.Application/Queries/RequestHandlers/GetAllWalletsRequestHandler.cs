using AutoMapper;
using HubWally.Application.DTOs.Wallets;
using HubWally.Application.Queries.Requests;
using HubWally.Application.Services.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubWally.Application.Queries.RequestHandlers
{
    public class GetAllWalletsRequestHandler(IMapper mapper, IWalletService service) : IRequestHandler<GetAllWalletsRequest, ApiResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IWalletService _service = service;
        public async Task<ApiResponse> Handle(GetAllWalletsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetAllWallets() ?? throw new Exception("Failed to retrieve all wallets");
                var WalletDtos = _mapper.Map<IEnumerable<GetWalletDto>>(response);
                return new()
                {
                    Success = true,
                    Message = "Retrieved all wallets successfully",
                    Body = WalletDtos
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Success = false,
                    Message = ex.Message ?? "Something went wrong. Try again later.",
                };
            }
        }
    }
}
