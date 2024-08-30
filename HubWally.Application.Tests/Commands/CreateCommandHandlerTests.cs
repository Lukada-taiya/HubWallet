using AutoMapper;
using FluentValidation.Results;
using HubWally.Api.Configurations;
using HubWally.Application.Commands.RequestHandlers.Wallets;
using HubWally.Application.Commands.Requests.Wallets;
using HubWally.Application.DTOs.Wallets; 
using HubWally.Application.Services.IServices;
using HubWally.Application.Tests.Mocks;
using Moq;
using Shouldly; 

namespace HubWally.Application.Tests.Commands
{
    public class CreateCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IWalletService> _mockService;

        public CreateCommandHandlerTests()
        {
            _mockService = MockWalletService.GetWalletService();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Mappers>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task CreateWallet_IsValid_ReturnsSuccess()
        {
            var handler = new CreateWalletCommandHandler(_mockService.Object, _mapper);
            var walletDto = new WalletDto
            {
                Name = "New Wallet",
                AccountNumber = "0234543214",
                AccountScheme = "mtn",
                Type = "momo",
                Owner = "0558808984"
            };
            var result = await handler.Handle(new CreateWalletCommand { walletDto = walletDto}, CancellationToken.None);
             
            result.ShouldBeOfType<ApiResponse>();
            result.Id.ShouldNotBeNull(); 
            result.Success.ShouldBeTrue();
            var wallets = await _mockService.Object.GetAllWallets();
            wallets.Count().ShouldBe(4);
        }
        [Fact]
        public async Task CreateWallet_IsInvalid_ValidationErrorIsThrown()
        {
            var handler = new CreateWalletCommandHandler(_mockService.Object, _mapper);
            var walletDto = new WalletDto
            {
                Name = "",
                AccountNumber = "",
                AccountScheme = "",
                Type = "",
                Owner = ""
            };
            var result = await handler.Handle(new CreateWalletCommand { walletDto = walletDto}, CancellationToken.None); 

            result.Id.ShouldBeNull();
            result.Body.ShouldBeOfType<List<ValidationFailure>>(); 
            result.Success.ShouldBeFalse();
            var validationErrors = result.Body as List<ValidationFailure>;
            validationErrors?.ShouldContain(e =>
            e.PropertyName == "Name" && e.ErrorMessage == "'Name' must not be empty." ||
            e.PropertyName == "Type" && e.ErrorMessage == "'Type' must not be empty." ||
            e.PropertyName == "AccountNumber" && e.ErrorMessage == "'Account Number' must not be empty." ||
            e.PropertyName == "AccountScheme" && e.ErrorMessage == "'Account Scheme' must not be empty." ||
            e.PropertyName == "Owner" && e.ErrorMessage == "'Owner' must not be empty."
            );
        }

    }
}
