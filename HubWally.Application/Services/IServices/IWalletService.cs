using HubWally.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
