using CodeCanvas.Contracts;
using CodeCanvas.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCanvas.Database
{
    public class WalletRepository : RepositoryBase<WalletEntity> , IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WalletEntity>> GetWalletAsync() => await FindAll().ToListAsync();
        public async Task<IEnumerable<WalletEntity>> GetWalletByConditionAsync(int id) => await FindByCondition(c => c.Id == id).ToListAsync();
        public void UpdateWallet(WalletEntity wallet) => Update(wallet);
        public void SaveChangesAsync()
        {
            _context.SaveChanges();
        }
    }
}
