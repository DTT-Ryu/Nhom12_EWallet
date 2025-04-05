using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories.Interfaces;

namespace Nhom12_EWallet.Respositories
{
    public class TransactionRepository : Repository<TblTransaction>, ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddTransactionAsync(TblTransaction transaction)
        {
            await _context.TblTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
