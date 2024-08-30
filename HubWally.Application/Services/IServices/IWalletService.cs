using HubWally.Domain.Models; 

namespace HubWally.Application.Services.IServices
{
    public interface IWalletService
    {
        Task<int> AddWallet(Wallet entity);
        Task<IEnumerable<Wallet>> GetAllWallets();
        Task<Wallet> GetWallet(int id);
        Task<int> GetWalletsCountByOwner(string phoneNumber);
        Task<int> UpdateWallet(Wallet entity);
        Task<int> DeleteWallet(int id);
    }
}
