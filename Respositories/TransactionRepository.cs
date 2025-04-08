using Microsoft.EntityFrameworkCore;
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

        public async Task<List<TblTransaction>> GetAllTransaction()
        {
            return await _context.TblTransactions
                    .Include(u => u.ISenderUserIdFkNavigation)
                    .Include(u => u.IRecipientUserIdFkNavigation)
                    .Include(u => u.IBankAccountIdFkNavigation).ToListAsync();
        }

        public async Task<TblTransaction?> GetTransactionByID(int id)
        {
            return await _context.TblTransactions
                        .Include(u => u.ISenderUserIdFkNavigation)
                        .Include(u=>u.IRecipientUserIdFkNavigation)
                        .Include(u=>u.IBankAccountIdFkNavigation)
                        .FirstOrDefaultAsync(u => u.ITransactionIdPk == id);
        }
    }
}
