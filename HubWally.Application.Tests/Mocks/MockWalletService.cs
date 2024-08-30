using HubWally.Application.Services.IServices;
using HubWally.Domain.Models;
using Moq; 

namespace HubWally.Application.Tests.Mocks
{
    public static class MockWalletService
    {

        public static Mock<IWalletService> GetWalletService()
        {
            var wallets = new List<Wallet>
            {
                new()
                {
                    Id = 1,
                    Name ="Mtn Wallet",
                    Type = "momo",
                    AccountNumber = "0558808984",
                    AccountScheme = "mtn",
                    Owner = "0558808984",
                    Created_At = DateTime.Now
                },
                new()
                {
                    Id = 2,
                    Name ="Voda Wallet",
                    Type = "momo",
                    AccountNumber = "0558808984",
                    AccountScheme = "vodafone",
                    Owner = "0558808984",
                    Created_At = DateTime.Now
                },
                new()
                {
                    Id = 3,
                    Name ="My card",
                    Type = "card",
                    AccountNumber = "938450",
                    AccountScheme = "mastercard",
                    Owner = "0558808984",
                    Created_At = DateTime.Now
                },

            };

            var mockService = new Mock<IWalletService>();

            mockService.Setup(r => r.GetAllWallets()).ReturnsAsync(wallets);

            mockService.Setup(r => r.GetWallet(It.IsAny<int>())).ReturnsAsync((int id) => wallets.FirstOrDefault(r => r.Id == id));
            mockService.Setup(r => r.AddWallet(It.IsAny<Wallet>())).ReturnsAsync((Wallet wallet) => {
                wallet.Id = wallets.Count;
                wallets.Add(wallet);
                return wallet.Id;
            });

            mockService.Setup(r => r.UpdateWallet(It.IsAny<Wallet>())).ReturnsAsync((Wallet wallet) => { 
                var oldWallet = wallets.FirstOrDefault(i => i.Id == wallet.Id);
                if(oldWallet != null)
                {
                    oldWallet.Owner = wallet.Owner;
                    oldWallet.Name = wallet.Name;
                    oldWallet.AccountNumber = wallet.AccountNumber;
                    oldWallet.AccountScheme = wallet.AccountScheme;
                    oldWallet.Type = wallet.Type;
                }else
                {
                    throw new InvalidOperationException("Record does not exist.");
                }
                return wallet.Id;
            });

            mockService.Setup(r => r.DeleteWallet(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var oldWallet = wallets.FirstOrDefault(i => i.Id == id);
                if (oldWallet != null)
                {
                    var result = wallets.Remove(oldWallet);                     
                    return result ? 1:0;
                }
                else
                {
                    throw new InvalidOperationException("Record does not exist.");
                }
            });

            return mockService; 
        }
    }
}
