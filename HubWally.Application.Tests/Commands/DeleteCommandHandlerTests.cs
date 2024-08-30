using AutoMapper; 
using HubWally.Application.Commands.RequestHandlers.Wallets;
using HubWally.Application.Commands.Requests.Wallets; 
using HubWally.Application.Services.IServices;
using HubWally.Application.Tests.Mocks;
using Moq;
using Shouldly; 

namespace HubWally.Application.Tests.Commands
{
    public class DeleteCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IWalletService> _mockService;

        public DeleteCommandHandlerTests()
        {
            _mockService = MockWalletService.GetWalletService(); 
        }

        [Fact]
        public async Task DeleteWallet_IsValid_RemovesWallet()
        {
            var handler = new DeleteWalletCommandHandler(_mockService.Object); 
            var result = await handler.Handle(new DeleteWalletCommand { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<ApiResponse>(); 
            result.Success.ShouldBeTrue();
            var wallets = await _mockService.Object.GetAllWallets();
            wallets.Count().ShouldBe(2);
            var deletedWallet = wallets.FirstOrDefault(r => r.Id == 1);
            deletedWallet.ShouldBeNull();
        }
        [Fact]
        public async Task DeleteWallet_IsInValid_ReturnsNotExistsError()
        {
            var handler = new DeleteWalletCommandHandler(_mockService.Object); 
            var result = await handler.Handle(new DeleteWalletCommand { Id = 0 }, CancellationToken.None);

            result.ShouldBeOfType<ApiResponse>();
            result.Success.ShouldBeFalse();
            result.Message.ShouldBe("Record does not exist.");
        }
    }
}
