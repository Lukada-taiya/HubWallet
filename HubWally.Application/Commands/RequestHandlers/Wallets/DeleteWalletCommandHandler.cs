using HubWally.Application.Commands.Requests.Wallets;
using HubWally.Application.Services.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HubWally.Application.Commands.RequestHandlers.Wallets
{
    public class DeleteWalletCommandHandler(IWalletService service) : IRequestHandler<DeleteWalletCommand, ApiResponse>
    {
        private readonly IWalletService _service = service;
        public async Task<ApiResponse> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try { 
            var response = await _service.DeleteWallet(request.Id);
                if (response > 0)
                {
                
                    transactionScope.Complete();
                    return new()
                    {
                        Success = true,
                        Message = "Wallet has been deleted successfully"
                    };
                }
            throw new Exception("Failed to delete wallet");
            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                return new ()
                {
                    Success = false,
                    Message = ex.Message ?? "Something went wrong. Try again later."
                };
            }
        }
    }
}
