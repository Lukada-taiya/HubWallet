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
    public class GetWalletRequestHandler(IWalletService service, IMapper mapper) : IRequestHandler<GetWalletRequest, ApiResponse>
    {
        private readonly IWalletService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(GetWalletRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetWallet(request.Id) ?? throw new Exception("No wallet found");
                var WalletDto = _mapper.Map<GetWalletDto>(response);
                return new()
                {
                    Success = true,
                    Message = "Retrieved wallet successfully",
                    Body = WalletDto
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
