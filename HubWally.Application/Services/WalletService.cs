using HubWally.Application.Services.IServices;
using HubWally.Domain.Models;
using HubWally.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubWally.Application.Services
{
    public class WalletService(IBaseRepository<Wallet> repository) : IWalletService
    {
        private readonly IBaseRepository<Wallet> _repository = repository;
        public Task<int> AddWallet(Wallet entity)
        {
            return _repository.Add(entity);
        }

        public Task<int> DeleteWallet(int id)
        {
            return _repository.Delete(id);
        }

        public Task<IEnumerable<Wallet>> GetAllWallets()
        {
            return _repository.GetAllAsync();
        }

        public Task<Wallet> GetWallet(int id)
        {
            return _repository.Get(id);
        }

        public async Task<int> GetWalletsCountByOwner(string phoneNumber)
        {
            return await _repository.GetRecordCount("Owner", phoneNumber);
        }

        public Task<int> UpdateWallet(Wallet entity)
        {
            return _repository.Update(entity);
        }
    }
}
