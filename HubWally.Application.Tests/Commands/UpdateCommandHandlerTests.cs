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
    public class UpdateCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IWalletService> _mockService;

        public UpdateCommandHandlerTests()
        {
            _mockService = MockWalletService.GetWalletService();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Mappers>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task UpdateWallet_IsValid_ReturnsCorrectWallet()
        {
            var handler = new UpdateWalletCommandHandler(_mockService.Object, _mapper);
            var walletDto = new WalletDto
            {
                Name = "My Item",
                AccountNumber = "495203459482093485",
                AccountScheme = "visa",
                Type = "card",
                Owner = "0558808984"
            };
            var result = await handler.Handle(new UpdateWalletCommand { Id = 1, walletDto = walletDto}, CancellationToken.None);
            
            result.ShouldBeOfType<ApiResponse>();
            result.Id.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            var wallet = await _mockService.Object.GetWallet(1);
            wallet.Name.ShouldBe(walletDto.Name);
            wallet.AccountScheme.ShouldBe(walletDto.AccountScheme);
            wallet.AccountNumber.ShouldBe(walletDto.AccountNumber);
            wallet.Type.ShouldBe(walletDto.Type);
            wallet.Owner.ShouldBe(walletDto.Owner);
        }
        [Fact]
        public async Task UpdateWallet_IsInValid_ReturnsValidationError()
        {
            var handler = new UpdateWalletCommandHandler(_mockService.Object, _mapper);
            var walletDto = new WalletDto
            {
                Name = "",
                AccountNumber = "",
                AccountScheme = "",
                Type = "",
                Owner = ""
            };
            var result = await handler.Handle(new UpdateWalletCommand { Id = 2, walletDto = walletDto}, CancellationToken.None);

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
