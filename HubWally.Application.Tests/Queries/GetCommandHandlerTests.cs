using AutoMapper;
using HubWally.Api.Configurations;
using HubWally.Application.DTOs.Wallets;
using HubWally.Application.Queries.RequestHandlers;
using HubWally.Application.Queries.Requests;
using HubWally.Application.Services.IServices;
using HubWally.Application.Tests.Mocks;
using Moq;
using Shouldly; 

namespace HubWally.Application.Tests.Queries
{
    public class GetCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IWalletService> _mockService;

        public GetCommandHandlerTests()
        {
            _mockService = MockWalletService.GetWalletService();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Mappers>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetWallet_IsValid_ReturnsCorrectWallet()
        {
            var handler = new GetWalletRequestHandler(_mockService.Object, _mapper);
            var result = await handler.Handle(new GetWalletRequest {  Id = 2}, CancellationToken.None);
            result.ShouldBeOfType<ApiResponse>();
            result.Body.ShouldBeOfType<GetWalletDto>();
            result.Success.ShouldBeTrue();
            var wallet = result.Body as GetWalletDto;
            wallet.Id.ShouldBe(2);
        }
        [Fact]
        public async Task GetWallet_IsInValid_ReturnsWalletNotFound()
        {
            var handler = new GetWalletRequestHandler(_mockService.Object, _mapper);
            var result = await handler.Handle(new GetWalletRequest {  Id = 0}, CancellationToken.None);
            result.ShouldBeOfType<ApiResponse>();
            result.Body.ShouldBeNull();
            result.Success.ShouldBeFalse();
        }
        
    }
}
