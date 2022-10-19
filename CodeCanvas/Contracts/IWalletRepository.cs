using CodeCanvas.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCanvas.Contracts
{
    public interface IWalletRepository
    {
        public Task<IEnumerable<WalletEntity>> GetWalletAsync();
        public Task<IEnumerable<WalletEntity>> GetWalletByConditionAsync(int id);
        public void UpdateWallet(WalletEntity wallet);
        public void SaveChangesAsync();

    }
}
