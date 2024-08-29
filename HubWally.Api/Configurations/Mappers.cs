using AutoMapper;
using HubWally.Application.DTOs.Wallets;
using HubWally.Domain.Models;

namespace HubWally.Api.Configurations
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, GetWalletDto>().ReverseMap();
        }
    }
}
