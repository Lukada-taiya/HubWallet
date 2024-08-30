using AutoMapper;
using HubWally.Application.Commands.Requests.Wallets;
using HubWally.Application.DTOs.Wallets;
using HubWally.Application.Services.IServices;
using HubWally.Domain.Models;
using MediatR; 
using System.Transactions;

namespace HubWally.Application.Commands.RequestHandlers.Wallets
{
    public class UpdateWalletCommandHandler(IWalletService service, IMapper mapper) : IRequestHandler<UpdateWalletCommand, ApiResponse>
    {
        private readonly IWalletService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var validator = new UpdateWalletDtoValidator();
                var validationResult = validator.Validate(request.walletDto);
                if (!validationResult.IsValid)
                {
                    return new()
                    {
                        Success = false,
                        Message = "Validation Failed",
                        Body = validationResult.Errors
                    };
                }
                var WalletEntity = _mapper.Map<Wallet>(request.walletDto);
                WalletEntity.Id = request.Id;
                WalletEntity.Updated_At = DateTime.UtcNow;
                int response = await _service.UpdateWallet(WalletEntity);
                if (response > 0)
                {
                    transactionScope.Complete();
                    return new()
                    {
                        Id = WalletEntity.Id,
                        Success = true,
                        Message = "Wallet has been updated successfully"
                    };
                }
                throw new Exception("Failed to update Wallet");
            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                return new()
                {
                    Success = false,
                    Message = ex.Message ?? "Something went wrong. Try again later."
                };
            }
        }
    }
}
