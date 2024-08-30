using AutoMapper;
using HubWally.Application.Commands.Requests.Wallets;
using HubWally.Application.DTOs.Wallets;
using HubWally.Application.Services.IServices;
using HubWally.Domain.Models; 
using MediatR; 
using System.Transactions;

namespace HubWally.Application.Commands.RequestHandlers.Wallets
{
    public class CreateWalletCommandHandler(IWalletService service, IMapper mapper) : IRequestHandler<CreateWalletCommand, ApiResponse>
    {
        private readonly IWalletService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var validator = new CreateWalletDtoValidator(_service);
                var validationResult = await validator.ValidateAsync(request.walletDto);
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
                WalletEntity.Created_At = DateTime.UtcNow;
                if(WalletEntity.Type.ToLower() == "card")
                {
                    WalletEntity.Type = "card";
                    WalletEntity.AccountNumber = WalletEntity.AccountNumber.Substring(0, 6);
                }else
                {
                    WalletEntity.Type = "momo"; 
                }
                var response = await _service.AddWallet(WalletEntity);
                if (response > 0)
                {  
                    transactionScope.Complete();
                    return new()
                    {
                        Id = response,
                        Success = true,
                        Message = "Wallet has been created successfully"
                    }; 
                }
                throw new Exception("Failed to create Wallet");
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
