using AutoMapper;
using HubWally.Api.Configurations;
using HubWally.Application.DTOs.Wallets;
using HubWally.Application.Queries.RequestHandlers;
using HubWally.Application.Queries.Requests;
using HubWally.Application.Services.IServices;
using HubWally.Application.Tests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubWally.Application.Tests.Queries
{
    public class GetAllCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IWalletService> _mockService; 

        public GetAllCommandHandlerTests()
        {
            _mockService = MockWalletService.GetWalletService();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Mappers>();
            });

            _mapper = mapperConfig.CreateMapper();            
        }

        [Fact]
        public async Task GetWallets_IsValid_ReturnsListOfWallets()
        {
            var handler = new GetAllWalletsRequestHandler(_mapper, _mockService.Object);
            var result = await handler.Handle(new GetAllWalletsRequest(), CancellationToken.None); 
            result.ShouldBeOfType<ApiResponse>();
            result.Body.ShouldBeOfType<List<GetWalletDto>>();
            result.Success.ShouldBeTrue();
            var wallets = result.Body as List<GetWalletDto>;
            wallets?.Count.ShouldBe(3);
        }
    }
}
